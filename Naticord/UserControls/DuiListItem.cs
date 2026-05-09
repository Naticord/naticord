using DirectUI.Net;
using DirectUI.Net.Core;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace Naticord.UserControls
{
    internal class DuiListItem : DuiUserControl
    {
        // Status icons integers:
        // 1 = Online
        // 2 = Busy / Do not disturb
        // 3 = Idle
        // 4 = Offline

        // Extra status icon integers:
        // 1 = Playing music
        // 2 = Playing a game
        // 3 = Streaming on a platform
        // 4 = Watching something
        // 5 = No status / Custom status

        private const string TemplatePath = @"User Interface\ClientFlow\Sidebar\DuiListItem.xml";
        protected override string ResId => $"listItemMain_{_itemId}";
        protected override string XmlSource => _template.Value.Replace("{N}", _itemId.ToString());

        private static readonly Lazy<string> _template = new Lazy<string>(() =>
            File.ReadAllText(DuiInitializer.Instance.ResolveXmlPath(TemplatePath)));

        private readonly int _itemId;

        private IntPtr _hProfilePic;
        private IntPtr _hStatusIcon;
        private IntPtr _hExtraStatusIcon;

        public DuiListItem(int itemId)
        {
            _itemId = itemId;
        }

        public void SetUsername(string name) => Host?.SetText($"listItemName_{_itemId}", name ?? "");
        public void SetStatus(string status) => Host?.SetText($"listItemStatus_{_itemId}", status ?? "");

        public void SetProfilePicture(Bitmap bmp)
        {
            if (_hProfilePic != IntPtr.Zero) { DeleteObject(_hProfilePic); _hProfilePic = IntPtr.Zero; }
            if (bmp != null) _hProfilePic = bmp.GetHbitmap(Color.Black);

            Host?.SetContentBitmap($"listItemAvatar_{_itemId}", _hProfilePic);
        }

        public void SetStatusIcon(Bitmap bmp)
        {
            if (_hStatusIcon != IntPtr.Zero) { DeleteObject(_hStatusIcon); _hStatusIcon = IntPtr.Zero; }
            if (bmp != null) _hStatusIcon = bmp.GetHbitmap(Color.Black);

            Host?.SetContentBitmap($"listItemBadge_{_itemId}", _hStatusIcon);
        }

        public void SetStatusIcon(int statusCode, int extraStatusCode = 5)
        {
            LoadIconTo(StatusName(statusCode), $"listItemBadge_{_itemId}", ref _hStatusIcon);
            LoadIconTo(ExtraStatusName(extraStatusCode), $"listItemStatusIcon_{_itemId}", ref _hExtraStatusIcon);
        }

        private void LoadIconTo(string name, string elementId, ref IntPtr hCache)
        {
            if (hCache != IntPtr.Zero) { DeleteObject(hCache); hCache = IntPtr.Zero; }

            bool hasIcon = !string.IsNullOrEmpty(name) && name != "none";
            if (hasIcon)
            {
                string suffix = string.Equals(Properties.Settings.Default.iconStyle, "Skeuomorphic", StringComparison.OrdinalIgnoreCase) ? "-skeuo" : "";
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtResources", $"{name}{suffix}.bmp");

                if (File.Exists(path))
                {
                    using (var src = Image.FromFile(path))
                    using (var bmp = new Bitmap(src))
                        hCache = bmp.GetHbitmap(Color.Black);
                }
                else hasIcon = false;
            }

            Host?.SetContentBitmap(elementId, hCache);
            Host?.SetLayoutPos(elementId, hasIcon ? 0 : -3);
        }

        private static string StatusName(int code)
        {
            switch (code)
            {
                case 1: return "online";
                case 2: return "busy";
                case 3: return "idle";
                case 4: return "offline";
                case 5: return "none";
                default: return null;
            }
        }

        private static string ExtraStatusName(int code)
        {
            switch (code)
            {
                case 1: return "music";
                case 2: return "gaming";
                case 3: return "streaming";
                case 4: return "watching";
                case 5: return "none";
                default: return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_hProfilePic != IntPtr.Zero) { DeleteObject(_hProfilePic); _hProfilePic = IntPtr.Zero; }
                if (_hStatusIcon != IntPtr.Zero) { DeleteObject(_hStatusIcon); _hStatusIcon = IntPtr.Zero; }
                if (_hExtraStatusIcon != IntPtr.Zero) { DeleteObject(_hExtraStatusIcon); _hExtraStatusIcon = IntPtr.Zero; }
            }
            base.Dispose(disposing);
        }

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
    }
}