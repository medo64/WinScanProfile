using System;
using System.Windows.Forms;
using WinScanProfile.IO;

namespace WinScanProfile {
    internal partial class ProfileForm : Form {

        public ProfileForm(Document document, Profile profile) {
            InitializeComponent();

            Document= document;
            Profile = profile;

            btnSetDefault.Enabled = !profile.IsDefault;
        }

        private readonly Profile Profile;
        private readonly Document Document;

        private void btnSetDefault_Click(object sender, EventArgs e) {
            Document.SetDefaultProfile(Profile);
            btnSetDefault.Enabled = false;
        }

        private void btnOk_Click(object sender, EventArgs e) {
            Profile.Save();
        }
    }
}
