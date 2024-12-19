using System;
using System.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using LastTexture.FileFormat;
using LastTexture.Exceptions;

namespace LastTexture.FileFormat
{
    class SHTXHeader
    {
        public const string ExpectedMagicNumber = "SHTXPS";

        public string MagicNumber { get; private set; }
        public ushort NumColours { get; private set; }
        public ushort unk1 { get; private set; }
        public ushort Width { get; private set; }
        public byte LogW { get; private set; }
        public ushort Height { get; private set; }
        public byte LogH { get; private set; }
        public int PixelSize { get; private set; }
        public SHTXHeader(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);

            MagicNumber = Encoding.ASCII.GetString(reader.ReadBytes(6));
            if (MagicNumber != ExpectedMagicNumber)
                throw new InvalidMagicException("Invalid SHTX header.");

            NumColours = reader.ReadUInt16();
            unk1 = reader.ReadUInt16();
            Width = reader.ReadUInt16();
            Height = reader.ReadUInt16();
            LogW = reader.ReadByte();
            LogH = reader.ReadByte();
            PixelSize = reader.ReadInt32();

            if (LogW != (int)Math.Ceiling(Math.Log(Width, 2)))
                throw new LogaritmoException("Invalid Width logaritmo.");
            if (LogH != (int)Math.Ceiling(Math.Log(Height, 2)))
                throw new LogaritmoException("Invalid Height logaritmo.");

        }
        public SHTXHeader(Bitmap SHTXBitmap, ushort NColours)
        {
            MagicNumber = "SHTXPS";
            NumColours = NColours;
            unk1 = 1;
            Width = (ushort)SHTXBitmap.Width;
            Height = (ushort)SHTXBitmap.Height;
            LogW = (byte)Math.Ceiling(Math.Log(Width, 2));
            LogH = (byte)Math.Ceiling(Math.Log(Height, 2));
            PixelSize = Width * Height;
        }
        public byte[] GetHeaderBytes()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    // Write MagicString
                    bw.Write(Encoding.ASCII.GetBytes(MagicNumber));
                    // Write other uint properties
                    bw.Write(NumColours);
                    bw.Write(unk1);
                    bw.Write(Width);
                    bw.Write(Height);
                    bw.Write(LogW);
                    bw.Write(LogH);
                    bw.Write(PixelSize);
                }
                return ms.ToArray();
            }
        }
    }
}
