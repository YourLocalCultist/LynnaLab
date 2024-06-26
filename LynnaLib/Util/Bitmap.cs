using System;

namespace LynnaLib
{
    /// Thin wrapper over Cairo.ImageSurface. Called "Bitmap" due to baggage
    /// from using the deprecated System.Drawing.Common.Bitmap.
    public class Bitmap : System.IDisposable
    {
        Cairo.ImageSurface surface;

        /// Constructor for blank surface
        public Bitmap(int width, int height)
        {
            surface = new Cairo.ImageSurface(Cairo.Format.RGB24, width, height);
        }

        /// Constructor from ImageSurface
        public Bitmap(Cairo.ImageSurface surface)
        {
            this.surface = surface;
        }

        /// Constructor from file
        public Bitmap(string filename)
        {
            this.surface = new Cairo.ImageSurface(filename);
        }


        public int Width
        {
            get { return surface.Width; }
        }

        public int Height
        {
            get { return surface.Height; }
        }


        public Cairo.Context CreateContext()
        {
            return new Cairo.Context(surface);
        }

        public Color GetPixel(int x, int y)
        {
            // Ensure the coordinates are within the surface bounds
            if (x < 0 || x >= surface.Width || y < 0 || y >= surface.Height)
            {
                throw new ArgumentOutOfRangeException("Coordinates are out of bounds.");
            }

            // Get the pixel data
            surface.Flush();
            IntPtr dataPtr = surface.DataPtr;
            int stride = surface.Stride;

            // Calculate the offset for the pixel
            int offset = y * stride + x * 4;

            // Read the pixel data (ARGB format)
            byte b = System.Runtime.InteropServices.Marshal.ReadByte(dataPtr, offset);
            byte g = System.Runtime.InteropServices.Marshal.ReadByte(dataPtr, offset + 1);
            byte r = System.Runtime.InteropServices.Marshal.ReadByte(dataPtr, offset + 2);
            byte a = System.Runtime.InteropServices.Marshal.ReadByte(dataPtr, offset + 3);

            // Convert to Color
            return Color.FromRgba(r, g, b, a);
        }

        public void Dispose()
        {
            if (surface != null)
                surface.Dispose();
            surface = null;
            System.GC.SuppressFinalize(this);
        }

        /// Implicit conversion to Cairo.ImageSurface, should be seamless
        public static implicit operator Cairo.ImageSurface(Bitmap b)
        {
            return b.surface;
        }
    }
}
