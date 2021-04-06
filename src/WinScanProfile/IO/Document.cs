using System;
using System.Collections.Generic;
using System.IO;

namespace WinScanProfile.IO {
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

        public string ScanProfilesDirectory { get; }

        private readonly List<Profile> InternalProfiles = new();
        public IReadOnlyList<Profile> Profiles {
            get { return InternalProfiles.AsReadOnly(); }
        }


        public void SetDefaultProfile(Profile profile) {
            foreach (var otherProfile in Profiles) {
                if (profile.Equals(otherProfile)) {
                    profile.IsDefault = true;
                    profile.Save();
                } else if (otherProfile.IsDefault) {
                    otherProfile.IsDefault = false;
                    otherProfile.Save();
                }
            }
        }

    }
}
