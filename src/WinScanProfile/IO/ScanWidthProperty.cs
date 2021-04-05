using System;

namespace WinScanProfile.IO {
    internal sealed class ScanWidthProperty : Property {

        public ScanWidthProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "Scan height";
        }

    }
}
