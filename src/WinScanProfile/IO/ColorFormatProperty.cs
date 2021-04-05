using System;
using System.Collections.Generic;

namespace WinScanProfile.IO {
    internal sealed class ColorFormatProperty : Property {

        public ColorFormatProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "Color format";
            Values = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string,string>("0", "Black and white"),
                new KeyValuePair<string,string>("2", "Grayscale"),
                new KeyValuePair<string,string>("3", "Color"),
            }.AsReadOnly();
        }

    }
}
