using System;
using System.Collections.Generic;
using System.Globalization;

namespace WinScanProfile.IO {
    internal class Property {
        protected Property(int id, int type, string value) {
            Id = id;
            Type = type;
            Value = value;
            Name = id.ToString(CultureInfo.InvariantCulture);
            Values = new List<KeyValuePair<string, string>>().AsReadOnly();
        }

        public int Id { get; }
        public int Type { get; }
        public string? Value { get; set; }

        public string Name { get; protected set; }
        public IReadOnlyList<KeyValuePair<string, string>> Values { get; protected set; }

        public static Property FromData(int id, int type, string value) {
            return id switch {
                4103 => new ColorFormatProperty(id, type, value),
                4104 => new BitDepthProperty(id, type, value),
                4106 => new FileTypeProperty(id, type, value),
                6147 => new ResolutionProperty(id, type, value),
                6148 => new ResolutionProperty(id, type, value),
                6151 => new ScanWidthProperty(id, type, value),
                6152 => new ScanHeightProperty(id, type, value),
                6154 => new BrightnessProperty(id, type, value),
                6155 => new ContrastProperty(id, type, value),
                _ => new Property(id, type, value),
            };
        }

    }
}
