using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace BusinessLogic
{
    public sealed class BLL_ScreenCapture
    {
        /*
         * Singleton
         */
        private static readonly Lazy<BLL_ScreenCapture> Lazy = new Lazy<BLL_ScreenCapture>(() => new BLL_ScreenCapture());

        public static BLL_ScreenCapture Instance => Lazy.Value;

        private BLL_ScreenCapture()
        {
        }

        /// Creates an Image object containing a screen shot of the entire desktop
        public Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }
        
        /// Creates an Image object containing a screen shot of a specific window
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);

            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);

            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;

            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);

            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);

            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);

            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.Srccopy);

            // restore selection
            GDI32.SelectObject(hdcDest, hOld);

            // clean up
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);

            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);

            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);

            return img;
        }
        
        /// Captures a screen shot of a specific window, and saves it to a file
        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }
        
        /// Captures a screen shot of the entire desktop, and saves it to a file
        public void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }

        /// Helper class containing Gdi32 API functions
        private class GDI32
        {

            public const int Srccopy = 0x00CC0020; // BitBlt dwRop parameter

            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDc, int nWidth,
                int nHeight);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDc);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDc);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);

            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDc, IntPtr hObject);
        }

        /// Helper class containing User32 API functions
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public readonly int left;
                public readonly int top;
                public readonly int right;
                public readonly int bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
    }
}