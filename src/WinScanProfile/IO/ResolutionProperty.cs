using System;

namespace WinScanProfile.IO {
    internal sealed class ResolutionProperty : Property {

        public ResolutionProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "Resolution (DPI)";
        }

    }
}
