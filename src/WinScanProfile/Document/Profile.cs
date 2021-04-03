using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;

namespace WinScanProfile.Document {
    internal class Profile {

        internal Profile(string fileName) {
            using var xml = new XmlTextReader(File.OpenRead(fileName));
            while (xml.Read()) {  // simple reading is good enough for this
                if (xml.NodeType == XmlNodeType.Element) {
                    switch (xml.Name) {
                        case "ScanProfile": break;  // ignore

                        case "ProfileGUID": {
                                var content = xml.ReadElementContentAsString();
                                if (Guid.TryParse(content, out var result)) {
                                    ProfileGuid = result;
                                }
                            }
                            break;

                        case "DeviceID": {
                                DeviceId = xml.ReadElementContentAsString();
                                using var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                                using var key = baseKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\" + DeviceId);
                                if (key != null) {
                                    DeviceFriendlyName = key.GetValue("FriendlyName") as string;
                                }
                            }
                            break;

                        case "ProfileName":
                            ProfileName = xml.ReadElementContentAsString();
                            break;

                        case "WiaItem": {
                                var content = xml.ReadElementContentAsString();
                                if (Guid.TryParse(content, out var result)) {
                                    WiaItem = result;
                                }
                            }
                            break;

                        case "Default":
                            IsDefault = true;
                            break;

                        case "Properties": break;  // ignore

                        case "Property":
                            var attrId = xml.GetAttribute("id");
                            var attrType = xml.GetAttribute("type");
                            if (int.TryParse(attrId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var propId)
                             && int.TryParse(attrType, NumberStyles.Integer, CultureInfo.InvariantCulture, out var propType)) {
                                Properties.Add(new Property(propId, propType, xml.ReadElementContentAsString()));
                            }
                            break;

                        default:
                            Debug.WriteLine(xml.Name);
                            break;

                    }
                }
            }

            if (DeviceFriendlyName == null) { DeviceFriendlyName = "Unknown"; }
        }


        public Guid? ProfileGuid { get; init; }
        public string? DeviceId { get; init; }
        public string DeviceFriendlyName { get; init; }
        public string? ProfileName { get; set; }
        public Guid? WiaItem { get; init; }
        public bool IsDefault { get; set; }

        public List<Property> Properties { get; } = new();

    }
}
