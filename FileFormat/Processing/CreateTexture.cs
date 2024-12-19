using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace LastTexture.FileFormat
{
    class TextureCreate
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public PixelFormat pixelFormat { get; private set; }
        public byte[] PixelData { get; private set; }
        public TextureCreate(string Dest, SHTXHeader header, SHTXTextureInfo textureInfo)
        {
            Height = (int)header.Height;
            Width = (int)header.Width;

            pixelFormat = PixelFormat.Format8bppIndexed;
            PixelData = textureInfo.PixelData;

            Bitmap bitmap = new Bitmap(Width, Height, pixelFormat);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, pixelFormat);

            byte[] pixelsForBmp = new byte[bmpData.Height * bmpData.Stride];
            int bitsPerPixel = Bitmap.GetPixelFormatSize(pixelFormat);

            int lineSize, copySize;

            lineSize = (textureInfo.PixelData.Length / bmpData.Height);

            copySize = (bmpData.Width / (bitsPerPixel < 8 ? 2 : 1)) * (bitsPerPixel < 8 ? 1 : bitsPerPixel / 8);

            for (int y = 0; y < bmpData.Height; y++)
            {
                int srcOffset = y * lineSize;
                int dstOffset = y * bmpData.Stride;
                if (srcOffset >= PixelData.Length || dstOffset >= pixelsForBmp.Length) continue;
                Buffer.BlockCopy(PixelData, srcOffset, pixelsForBmp, dstOffset, copySize);
            }

            Marshal.Copy(pixelsForBmp, 0, bmpData.Scan0, pixelsForBmp.Length);

            bitmap.UnlockBits(bmpData);

            ColorPalette palette = bitmap.Palette;

            for (int i = 0; i < header.NumColours; i++)
            {
                palette.Entries[i] = Color.FromArgb(textureInfo.Palette[i][3], textureInfo.Palette[i][0], textureInfo.Palette[i][1], textureInfo.Palette[i][2]);
            }
            bitmap.Palette = palette;

            bitmap.Save(Dest, ImageFormat.Png);
        }
    }
}
