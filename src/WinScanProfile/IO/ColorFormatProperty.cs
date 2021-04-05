using System;

namespace WinScanProfile.IO {
    internal sealed class ColorFormatProperty : Property {

        public ColorFormatProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "Color format";
            // 0: Black and White
            // 2: Grayscale
            // 3: Color
        }

    }
}
