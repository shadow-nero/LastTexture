using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using LastTexture.FileFormat;

namespace LastTexture
{
    class SHTXWritter
    {
        public SHTXHeader header { get; private set; }
        public SHTXTextureInfo textureInfo { get; private set; }
        public Bitmap SHTXIndexedBpp { get; private set; }
        public CreateSHTX CreateSHTX { get; private set; }
        public SHTXWritter(string pngSHTX, string Dest)
        {
            Bitmap originalimage = new Bitmap(pngSHTX);
            SHTXIndexedBpp = Create8bppIndexedBitmap(originalimage);

            header = new SHTXHeader(SHTXIndexedBpp, 256);
            textureInfo = new SHTXTextureInfo(SHTXIndexedBpp, header);
            CreateSHTX = new CreateSHTX(Dest, header, textureInfo);

        }
        private static Bitmap Create8bppIndexedBitmap(Bitmap original)
        {
            // Gather image data
            List<Color> Palette = new List<Color>();
            byte[] indexes = new byte[original.Width * original.Height];
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color cur = original.GetPixel(x, y);

                    int index = Palette.IndexOf(cur);
                    if (index == -1)
                    {
                        Palette.Add(cur);
                        index = Palette.Count - 1;
                    }
                    indexes[y * original.Width + x] = (byte)index; // Correct index calculation
                }
            }

            if (Palette.Count > 256)
                throw new Exception("The image contains more colors than allowed for an 8bpp Indexed format.");

            while (Palette.Count < 256)
            {
                Palette.Add(Color.Black);
            }
            // Create a new 8bpp image
            Bitmap image = new Bitmap(original.Width, original.Height, PixelFormat.Format8bppIndexed);
            BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Calculate stride
            int stride = data.Stride; // Stride calculation includes padding
            byte[] imageBytes = new byte[stride * original.Height];

            // Copy indexes to imageBytes considering stride
            for (int y = 0; y < original.Height; y++)
            {
                Array.Copy(indexes, y * original.Width, imageBytes, y * stride, original.Width);
            }

            // Copy pixel indexes to the bitmap
            Marshal.Copy(imageBytes, 0, data.Scan0, imageBytes.Length);

            image.UnlockBits(data);

            // Update palette
            ColorPalette palette = image.Palette;
            for (int i = 0; i < Palette.Count; i++)
                palette.Entries[i] = Palette[i];
            image.Palette = palette;

            return image;
        }
    }
}
