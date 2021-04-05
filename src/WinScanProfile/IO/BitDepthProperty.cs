using System;

namespace WinScanProfile.IO {
    internal sealed class BitDepthProperty : Property {

        public BitDepthProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "Bit depth";
        }

    }
}
