using ClipboardManager.Interop;
using ClipboardManager.VirtualWindow;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace ClipboardManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Forms.NotifyIcon m_NotificationIcon = new System.Windows.Forms.NotifyIcon();
        MessageOnlyWindow testwindow = new MessageOnlyWindow();


        bool test;


        private static NotificationForm _form = new NotificationForm();


        private class NotificationForm : Form
        {
            public NotificationForm()
            {
                NativeMethods.SetParent(Handle, NativeMethods.HWND_MESSAGE);
                NativeMethods.AddClipboardFormatListener(Handle);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE)
                {
                    Console.WriteLine("yea how" + System.Windows.Forms.Clipboard.GetText());
                }
                base.WndProc(ref m);
            }
        }

        public MainWindow()
        {
            Console.WriteLine("blub test;");
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.WindowState = System.Windows.WindowState.Minimized;
            m_NotificationIcon.Icon = new Icon(@"../../icon.ico");
            m_NotificationIcon.Visible = true;

            test = System.Windows.Clipboard.ContainsText();
            m_NotificationIcon.ShowBalloonTip(5000, "Hi", "This is a BallonTip from Windows Notification" + test, System.Windows.Forms.ToolTipIcon.Info);
        }
    }
}
