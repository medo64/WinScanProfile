using System;

namespace WinScanProfile.Document {
    internal class Property {
        public Property(int id, int type, string value) {
            Id = id;
            Type = type;
            Value = value;
        }

        public int Id { get; init; }
        public int Type { get; init; }
        public string? Value { get; set; }

    }
}
