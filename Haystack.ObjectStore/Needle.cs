using System;

namespace Haystack.ObjectStore
{
    public class Needle
    {
        public int HeaderMagicNumber { get; set; }

        public int Cookie { get; set; }

        public long Key { get; set; }

        public int AlternateKey { get; set; }

        public int Flags { get; set; }

        public int DataSize { get; set; }

        public byte[] Data { get; set; }

        public int FooterMagicNumber { get; set; }

        public int DataCheckSum { get; set; }

        public int Padding { get; set; }

        public void VerifyValidRequest(long key, int alternateKey, int cookie)
        {
            if (key == Key && AlternateKey == alternateKey && cookie == Cookie)
                return;

            // check sum ??

            throw new InvalidOperationException("Invalid key and cookie combination");
        }
    }
}
