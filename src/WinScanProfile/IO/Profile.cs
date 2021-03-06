using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace WinScanProfile.IO {
    [DebuggerDisplay("{ProfileName}")]
    internal class Profile {

        internal Profile(string fileName) {
            FileName = fileName;
            using var xml = new XmlTextReader(File.OpenRead(fileName));
            while (xml.Read()) {  // simple reading is good enough for this
                if (xml.NodeType == XmlNodeType.Element) {
                    switch (xml.Name) {
                        case "ScanProfile": break;  // ignore

                        case "ProfileGUID":
                            ProfileGuid = xml.ReadString();
                            break;

                        case "DeviceID": {
                                DeviceId = xml.ReadString();
                                using var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                                using var key = baseKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\" + DeviceId);
                                if (key != null) {
                                    DeviceFriendlyName = key.GetValue("FriendlyName") as string;
                                }
                            }
                            break;

                        case "ProfileName":
                            ProfileName = xml.ReadString();
                            break;

                        case "WiaItem":
                            WiaItem = xml.ReadString();
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
                                Properties.Add(Property.FromData(propId, propType, xml.ReadString()));
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

        private readonly Encoding Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        public void Save() {
            using var xml = new XmlTextWriter(File.Create(FileName), Encoding);

            xml.WriteStartElement("ScanProfile");

            if (ProfileGuid != null) { xml.WriteElementString("ProfileGUID", ProfileGuid); }
            if (DeviceId != null) { xml.WriteElementString("DeviceID", DeviceId); }
            if (ProfileName != null) { xml.WriteElementString("ProfileName", ProfileName); }
            if (WiaItem != null) { xml.WriteElementString("WiaItem", WiaItem); }
            if (IsDefault) { xml.WriteElementString("Default", null); }

            xml.WriteStartElement("Properties");
            foreach (var property in Properties) {
                xml.WriteStartElement("Property");
                xml.WriteAttributeString("id", property.Id.ToString(CultureInfo.InvariantCulture));
                xml.WriteAttributeString("type", property.Type.ToString(CultureInfo.InvariantCulture));
                xml.WriteString(property.Value);
                xml.WriteEndElement();  // Property
            }
            xml.WriteEndElement();  // Properties

            xml.WriteEndElement();  // ScanProfile
        }


        public string FileName { get; }
        public string? ProfileGuid { get; }
        public string? DeviceId { get; }
        public string? DeviceFriendlyName { get; }
        public string? ProfileName { get; set; }
        public string? WiaItem { get; }
        public bool IsDefault { get; set; }

        public List<Property> Properties { get; } = new();

    }
}
