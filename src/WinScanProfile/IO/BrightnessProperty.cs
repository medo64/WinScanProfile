using System;

namespace WinScanProfile.IO {
    internal sealed class BrightnessProperty : Property {

        public BrightnessProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "Brightness";
        }

    }
}
