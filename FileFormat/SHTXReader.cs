using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LastTexture.FileFormat;

namespace LastTexture
{
    class SHTXReader
    {
        public SHTXHeader header { get; private set; }
        public SHTXTextureInfo textureInfo { get; private set; }
        public TextureCreate TextureCreate { get; private set; }
        public SHTXReader(string FileSHTX, string Dest)
        {
            FileStream stream = new FileStream(FileSHTX, FileMode.Open, FileAccess.Read);

            header = new SHTXHeader(stream);
            textureInfo = new SHTXTextureInfo(stream, header);
            TextureCreate = new TextureCreate(Dest, header, textureInfo);

        }
    }
}
