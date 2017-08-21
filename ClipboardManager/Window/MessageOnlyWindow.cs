using ClipboardManager.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ClipboardManager.VirtualWindow
{
    public sealed class MessageOnlyWindow : IDisposable
    {
        private HwndSource m_hwndSource;

        public event EventHandler OnClipboardContentChanged;

        public MessageOnlyWindow()
        {
            //Message only WPF Window
            m_hwndSource = new HwndSource(0, 0, 0, 0, 0, 0, 0, null, NativeMethods.HWND_MESSAGE);
            //Add hook
            m_hwndSource.AddHook(new HwndSourceHook(WndProc));
            //Now 
            NativeMethods.AddClipboardFormatListener(m_hwndSource.Handle);


        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == NativeMethods.WM_CLIPBOARDUPDATE)
            {
                Console.WriteLine("[WPF Window Message Only]: " + System.Windows.Forms.Clipboard.GetText());

                OnClipboardContentChanged?.Invoke(this, EventArgs.Empty);
            }

            return IntPtr.Zero;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    NativeMethods.RemoveClipboardFormatListener(m_hwndSource.Handle);
                    m_hwndSource.RemoveHook(WndProc);
                    m_hwndSource.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MessageOnlyWindow() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
