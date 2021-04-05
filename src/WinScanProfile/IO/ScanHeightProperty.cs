using System;

namespace WinScanProfile.IO {
    internal sealed class ScanHeightProperty : Property {

        public ScanHeightProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "Scan height";
        }

    }
}
