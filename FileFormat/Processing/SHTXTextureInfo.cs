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
    class SHTXTextureInfo
    {
        public byte[][] Palette { get; private set; }
        public byte[] BytePalette { get; private set; }
        public byte[] PixelData { get; private set; }
        public PixelFormat TexturePixelFormatBitmap { get; private set; }
        public SHTXTextureInfo(Stream stream, SHTXHeader header)
        {
            BinaryReader reader = new BinaryReader(stream);

            Palette = new byte[header.NumColours][];
            PixelData = new byte[header.PixelSize];

            for(int i = 0; i < header.NumColours; i++)
            {
                Palette[i] = reader.ReadBytes(4);
            }

            PixelData = reader.ReadBytes(header.PixelSize);
        }
        public SHTXTextureInfo(Bitmap bitmap, SHTXHeader header)
        {
            BytePalette = CreatePalette(bitmap, header.NumColours);
            PixelData = GetPixelData(bitmap);
        }
        private byte[] CreatePaletteColors(byte R, byte G, byte B, byte A)
        {
            byte[] outputPalette = new byte[4];
            outputPalette[3] = R;
            outputPalette[2] = G;
            outputPalette[1] = B;
            outputPalette[0] = A;

            return outputPalette;
        }
        public byte[] CreatePalette(Bitmap bitmap, int Ncolor)
        {
            byte[] palette = new byte[Ncolor * 4];
            ColorPalette paletteOri = bitmap.Palette;

            for (int i = 0; i < Ncolor; i++)
            {
                Color color = paletteOri.Entries[i];
                byte R = color.R;
                byte G = color.G;
                byte B = color.B;
                byte A = color.A;

                byte[] paletteNew;
                paletteNew = CreatePaletteColors(A, B, G, R);
                    
                Array.Copy(paletteNew, 0, palette, i * 4, 4);
            }
            return palette;
        }
        public byte[] GetPixelData(Bitmap bitmap)
        {
            TexturePixelFormatBitmap = PixelFormat.Format8bppIndexed;
            //Bitmap bitmap = PostProcessing.Swizzle(bitmap1);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, TexturePixelFormatBitmap);

            // Calcula o stride alinhado a 8 bytes
            int calculatedStride = (bitmap.Width + 7) & ~7;  // Isso assegura que o stride é múltiplo de 8

            int stride = Math.Max(calculatedStride, bmpData.Stride); // Usa o maior valor entre o calculado e o do bitmap

            PixelData = new byte[stride * bitmap.Height];

            // Se o stride calculado e o stride do bmpData forem diferentes, pode ser necessário ajustar a cópia de dados
            if (calculatedStride != bmpData.Stride)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    IntPtr row = IntPtr.Add(bmpData.Scan0, y * bmpData.Stride);
                    Marshal.Copy(row, PixelData, y * stride, calculatedStride);
                }
            }
            else
            {
                Marshal.Copy(bmpData.Scan0, PixelData, 0, PixelData.Length);
            }

            bitmap.UnlockBits(bmpData);
            return PixelData;
        }
        public byte[] GetInfoBytes()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(BytePalette);
                    bw.Write(PixelData);
                }
                return ms.ToArray();
            }
        }
    }
}
