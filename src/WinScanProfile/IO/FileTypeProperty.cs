using System;
using System.Collections.Generic;

namespace WinScanProfile.IO {
    internal sealed class FileTypeProperty : Property {

        public FileTypeProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "File type";
            Values = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string,string>("{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}", "BMP"),
                new KeyValuePair<string,string>("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}", "JPG"),
                new KeyValuePair<string,string>("{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}", "PNG"),
                new KeyValuePair<string,string>("{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}", "TIF"),
            }.AsReadOnly();
        }

    }
}
