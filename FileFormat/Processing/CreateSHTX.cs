using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTexture.FileFormat
{
    class CreateSHTX
    {
        public byte[] byteHeader { get; private set; }
        public byte[] byteTextureInfo { get; private set; }
        public CreateSHTX(string Dest, SHTXHeader header, SHTXTextureInfo textureInfo)
        {
            byteHeader = header.GetHeaderBytes();
            byteTextureInfo = textureInfo.GetInfoBytes();
            /*
            if (!Directory.Exists(Path.GetDirectoryName(Dest)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Dest));
            }*/

            using (FileStream fs = new FileStream(Dest, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Cabeçalho SHTX
                writer.Write(byteHeader);
                writer.Write(byteTextureInfo);
            }

        }
    }
}
