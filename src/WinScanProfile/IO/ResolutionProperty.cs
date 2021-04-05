using System;
using System.Collections.Generic;

namespace WinScanProfile.IO {
    internal sealed class ResolutionProperty : Property {

        public ResolutionProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "Resolution (DPI)";
            Values = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string,string>( "150",  "150"),
                new KeyValuePair<string,string>( "300",  "300"),
                new KeyValuePair<string,string>( "600",  "600"),
                new KeyValuePair<string,string>("1200", "1200"),
            }.AsReadOnly();
        }

    }
}
