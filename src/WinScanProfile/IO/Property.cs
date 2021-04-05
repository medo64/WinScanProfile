using System;
using System.Globalization;

namespace WinScanProfile.IO {
    internal class Property {
        protected Property(int id, int type, string value) {
            Id = id;
            Type = type;
            Value = value;
            Name = id.ToString(CultureInfo.InvariantCulture);
        }

        public int Id { get; init; }
        public int Type { get; init; }
        public string? Value { get; set; }

        public string Name { get; init; }


        public static Property FromData(int id, int type, string value) {
            return id switch {
                4103 => new ColorFormatProperty(id, type, value),
                4106 => new FileTypeProperty(id, type, value),
                6147 => new ResolutionProperty(id, type, value),
                6154 => new BrightnessProperty(id, type, value),
                6155 => new ContrastProperty(id, type, value),
                _ => new Property(id, type, value),
            };
        }

    }
}
