using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLogic
{
    public class BLL_RichTextBoxEx : RichTextBox
    {
        private readonly object mustHideCaretLocker = new object();

        private bool mustHideCaret;

        [DefaultValue(false)]
        public bool MustHideCaret
        {
            get
            {
                lock (this.mustHideCaretLocker)
                    return this.mustHideCaret;
            }
            set
            {
                TabStop = false;
                if (value)
                    SetHideCaret();
                else
                    SetShowCaret();
            }
        }

        [DllImport("user32.dll")]
        private static extern int HideCaret(IntPtr hwnd);
        [DllImport("user32.dll", EntryPoint = "ShowCaret")]
        public static extern long ShowCaret(IntPtr hwnd);

        private void SetHideCaret()
        {
            MouseDown += new MouseEventHandler(ReadOnlyRichTextBox_Mouse);
            MouseUp += new MouseEventHandler(ReadOnlyRichTextBox_Mouse);
            Resize += new EventHandler(ReadOnlyRichTextBox_Resize);
            HideCaret(Handle);
            lock (this.mustHideCaretLocker)
                this.mustHideCaret = true;
        }

        private void SetShowCaret()
        {
            try
            {
                MouseDown -= new MouseEventHandler(ReadOnlyRichTextBox_Mouse);
                MouseUp -= new MouseEventHandler(ReadOnlyRichTextBox_Mouse);
                Resize -= new EventHandler(ReadOnlyRichTextBox_Resize);
            }
            catch
            {
            }
            ShowCaret(Handle);
            lock (this.mustHideCaretLocker)
                this.mustHideCaret = false;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (MustHideCaret)
            {
                HideCaret(Handle);
                this.Parent.Focus();//here we select parent control in my case it is panel
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            if (MustHideCaret)
                HideCaret(Handle);
        }

        private void ReadOnlyRichTextBox_Mouse(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HideCaret(Handle);
        }

        private void ReadOnlyRichTextBox_Resize(object sender, System.EventArgs e)
        {
            HideCaret(Handle);
        }
    }
}
