using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Button
{
    public partial class SaveMap : Form
    {
        #region Data
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        private string mFileName = "default";
        public string FileName
        {
            get { return mFileName + ".xml"; }
            set { mFileName = value; }
        }

        private bool mDone = false;
        public bool Done
        {
            get { return mDone; }
            set { mDone = value; }
        }
        #endregion

        #region Construction
        public SaveMap()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;

            IsAccessible = false;
            InitializeComponent();
            this._Sample.Text = "";
            _FileNameInput.Text = "";
        }
        #endregion

        #region Methods
        private void _Save_Click(object sender, EventArgs e)
        {
            mDone = true;
            Off();
        }

        private void _FileNameInput_TextChanged(object sender, EventArgs e)
        {
            FileName = _FileNameInput.Text;
            this._Sample.Text = FileName;
        }

        public void On()
        {
            const short SWP_NOZORDER = 0X4;
            const int SWP_SHOWWINDOW = 0x0040;

            SetWindowPos(Handle, 0, 500, 100, 250, 95, SWP_NOZORDER | SWP_SHOWWINDOW);
            this.TopMost = true;

            IsAccessible = true;
            Show();
        }

        public void Off()
        {
            IsAccessible = false;
            Hide();
        }
        #endregion
    }
}
