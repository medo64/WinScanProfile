using System;

namespace WinScanProfile.IO {
    internal sealed class ContrastProperty : Property {

        public ContrastProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "Contrast";
        }

    }
}
