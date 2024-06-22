using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Classes
{
    internal class DarkMode
    {
        private readonly Color darkBackgroundColor = Color.FromArgb(30, 30, 30);
        private readonly Color darkTextColor = Color.White;
        private readonly Color darkButtonColor = Color.FromArgb(45, 45, 48);
        private readonly Color darkButtonTextColor = Color.White;

        public void ApplyDarkMode(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                ApplyControlStyles(childControl);

                if (childControl.HasChildren)
                {
                    ApplyDarkMode(childControl);
                }
            }
        }

        private void ApplyControlStyles(Control control)
        {
            if (control is Button)
            {
                ApplyButtonStyle(control as Button);
            }
            else if (control is Label)
            {
                ApplyLabelStyle(control as Label);
            }
            else if (control is TextBox)
            {
                ApplyTextBoxStyle(control as TextBox);
            }
            else
            {
                control.BackColor = darkBackgroundColor;
                control.ForeColor = darkTextColor;
            }
        }

        private void ApplyButtonStyle(Button button)
        {
            button.BackColor = darkButtonColor;
            button.ForeColor = darkButtonTextColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = darkButtonColor;
        }

        private void ApplyLabelStyle(Label label)
        {
            label.ForeColor = darkTextColor;
        }

        private void ApplyTextBoxStyle(TextBox textBox)
        {
            textBox.BackColor = darkBackgroundColor;
            textBox.ForeColor = darkTextColor;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
