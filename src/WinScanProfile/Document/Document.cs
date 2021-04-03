using System;
using System.Collections.Generic;
using System.IO;

namespace WinScanProfile.Document {
    internal class Document {

        public Document() {
            var appDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var scanDir = Path.Combine(appDir, "Microsoft", "UserScanProfiles");
            if (!Directory.Exists(scanDir)) { Directory.CreateDirectory(scanDir); }
            foreach (var filename in Directory.GetFiles(scanDir, "*.xml")) {
                var profile = new Profile(filename);
                if (profile.DeviceId != null) {
                    InternalProfiles.Add(profile);
                }
            }
            ScanProfilesDirectory = scanDir;
        }

        public string ScanProfilesDirectory { get; init; }

        private readonly List<Profile> InternalProfiles = new();
        public IReadOnlyList<Profile> Profiles {
            get { return InternalProfiles.AsReadOnly(); }
        }

    }
}
