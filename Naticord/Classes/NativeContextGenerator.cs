using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Naticord.Classes
{
    [ToolboxItem(true)]
    [DefaultEvent("ItemClicked")]
    internal class NativeContextGenerator : ContextMenuStrip
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CreatePopupMenu();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyMenu(IntPtr hMenu);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool InsertMenuItem(IntPtr hMenu, uint uItem, bool fByPosition, ref MENUITEMINFO lpmii);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int TrackPopupMenuEx(IntPtr hMenu, uint uFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [StructLayout(LayoutKind.Sequential)]
        private struct BITMAPINFOHEADER
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BITMAPINFO
        {
            public BITMAPINFOHEADER bmiHeader;
            public uint bmiColors;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct MENUITEMINFO
        {
            public uint cbSize;
            public uint fMask;
            public uint fType;
            public uint fState;
            public uint wID;
            public IntPtr hSubMenu;
            public IntPtr hbmpChecked;
            public IntPtr hbmpUnchecked;
            public IntPtr dwItemData;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string dwTypeData;
            public uint cch;
            public IntPtr hbmpItem;

            public static MENUITEMINFO Create() => new MENUITEMINFO
            {
                cbSize = (uint)Marshal.SizeOf(typeof(MENUITEMINFO))
            };
        }

        private const uint MIIM_STATE = 0x0001;
        private const uint MIIM_ID = 0x0002;
        private const uint MIIM_SUBMENU = 0x0004;
        private const uint MIIM_BITMAP = 0x0080;
        private const uint MIIM_FTYPE = 0x0100;
        private const uint MIIM_STRING = 0x0040;
        private const uint MFT_STRING = 0x0000;
        private const uint MFT_SEPARATOR = 0x0800;
        private const uint MFS_ENABLED = 0x0000;
        private const uint MFS_GRAYED = 0x0003;
        private const uint MFS_CHECKED = 0x0008;
        private const uint TPM_LEFTALIGN = 0x0000;
        private const uint TPM_RIGHTBUTTON = 0x0002;
        private const uint TPM_RETURNCMD = 0x0100;
        private const uint TPM_NONOTIFY = 0x0080;

        private readonly Dictionary<uint, ToolStripMenuItem> _idToItem = new Dictionary<uint, ToolStripMenuItem>();
        private readonly List<IntPtr> _hBitmaps = new List<IntPtr>();
        private uint _idCounter;

        public NativeContextGenerator() { }
        public NativeContextGenerator(IContainer container) : base(container) { }

        protected override void OnOpening(CancelEventArgs e)
        {
            e.Cancel = true;
            var pos = Cursor.Position;
            ShowNative(null, pos.X, pos.Y);
        }

        public new void Show()
        {
            var pos = Cursor.Position;
            ShowNative(null, pos.X, pos.Y);
        }

        public new void Show(int screenX, int screenY) => ShowNative(null, screenX, screenY);
        public new void Show(System.Drawing.Point screenPoint) => ShowNative(null, screenPoint.X, screenPoint.Y);
        public new void Show(Control owner, int clientX, int clientY) => Show(owner, new System.Drawing.Point(clientX, clientY));

        public new void Show(Control owner, System.Drawing.Point clientPoint)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            var screen = owner.PointToScreen(clientPoint);
            ShowNative(owner, screen.X, screen.Y);
        }

        private void ShowNative(Control owner, int screenX, int screenY)
        {
            _idToItem.Clear();
            _hBitmaps.Clear();
            _idCounter = 1;

            var hMenu = CreatePopupMenu();
            if (hMenu == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            try
            {
                PopulateMenu(hMenu, Items);

                var hwnd = owner?.Handle ?? IntPtr.Zero;
                if (hwnd != IntPtr.Zero)
                    SetForegroundWindow(hwnd);

                var flags = TPM_LEFTALIGN | TPM_RIGHTBUTTON | TPM_RETURNCMD | TPM_NONOTIFY;
                var selectedId = TrackPopupMenuEx(hMenu, flags, screenX, screenY, hwnd, IntPtr.Zero);

                if (selectedId > 0 && _idToItem.TryGetValue((uint)selectedId, out var item))
                    item.PerformClick();
            }
            finally
            {
                DestroyMenu(hMenu);

                foreach (var hBmp in _hBitmaps)
                    DeleteObject(hBmp);
                _hBitmaps.Clear();
            }
        }

        private void PopulateMenu(IntPtr hMenu, ToolStripItemCollection items)
        {
            uint position = 0;
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripSeparator)
                    InsertSeparator(hMenu, position);
                else if (item is ToolStripMenuItem menuItem)
                    InsertItem(hMenu, position, menuItem);

                position++;
            }
        }

        private void InsertSeparator(IntPtr hMenu, uint position)
        {
            var info = MENUITEMINFO.Create();
            info.fMask = MIIM_FTYPE;
            info.fType = MFT_SEPARATOR;
            InsertMenuItem(hMenu, position, true, ref info);
        }

        private void InsertItem(IntPtr hMenu, uint position, ToolStripMenuItem menuItem)
        {
            var info = MENUITEMINFO.Create();
            info.fType = MFT_STRING;
            info.fState = menuItem.Enabled ? MFS_ENABLED : MFS_GRAYED;

            if (menuItem.Checked)
                info.fState |= MFS_CHECKED;

            if (menuItem.Image != null)
            {
                var hBmp = CreatePremultipliedBitmap(menuItem.Image);
                if (hBmp != IntPtr.Zero)
                {
                    _hBitmaps.Add(hBmp);
                    info.fMask |= MIIM_BITMAP;
                    info.hbmpItem = hBmp;
                }
            }

            if (menuItem.HasDropDownItems)
            {
                var hSubMenu = CreatePopupMenu();
                if (hSubMenu == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                PopulateMenu(hSubMenu, menuItem.DropDownItems);

                info.fMask |= MIIM_STRING | MIIM_FTYPE | MIIM_SUBMENU | MIIM_STATE;
                info.hSubMenu = hSubMenu;
                info.dwTypeData = menuItem.Text;
            }
            else
            {
                var id = _idCounter++;
                _idToItem[id] = menuItem;

                info.fMask |= MIIM_STRING | MIIM_FTYPE | MIIM_ID | MIIM_STATE;
                info.wID = id;
                info.dwTypeData = menuItem.Text;
            }

            InsertMenuItem(hMenu, position, true, ref info);
        }

        private IntPtr CreatePremultipliedBitmap(Image source)
        {
            int w = source.Width, h = source.Height;

            var bmpInfo = new BITMAPINFO();
            bmpInfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            bmpInfo.bmiHeader.biWidth = w;
            bmpInfo.bmiHeader.biHeight = -h;
            bmpInfo.bmiHeader.biPlanes = 1;
            bmpInfo.bmiHeader.biBitCount = 32;
            bmpInfo.bmiHeader.biCompression = 0;

            var hBitmap = CreateDIBSection(IntPtr.Zero, ref bmpInfo, 0, out var ppvBits, IntPtr.Zero, 0);
            if (hBitmap == IntPtr.Zero) return IntPtr.Zero;

            using (var bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Transparent);
                    g.DrawImage(source, 0, 0, w, h);
                }

                var data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                var pixels = new byte[w * h * 4];
                Marshal.Copy(data.Scan0, pixels, 0, pixels.Length);
                bmp.UnlockBits(data);

                for (int i = 0; i < pixels.Length; i += 4)
                {
                    var a = pixels[i + 3];
                    pixels[i + 0] = (byte)(pixels[i + 0] * a / 255);
                    pixels[i + 1] = (byte)(pixels[i + 1] * a / 255);
                    pixels[i + 2] = (byte)(pixels[i + 2] * a / 255);
                }

                Marshal.Copy(pixels, 0, ppvBits, pixels.Length);
            }

            return hBitmap;
        }
    }
}