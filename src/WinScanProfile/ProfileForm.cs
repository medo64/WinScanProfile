using System;
using System.Windows.Forms;
using WinScanProfile.IO;

namespace WinScanProfile {
    internal partial class ProfileForm : Form {

        public ProfileForm(Document document, Profile profile) {
            InitializeComponent();

            Document = document;
            Profile = profile;

            Text = profile.ProfileName;

            var marginVertical = btnSetDefault.Left;
            var marginHorizontal = ClientRectangle.Height - btnSetDefault.Bottom;

            var top = ClientRectangle.Top + marginVertical;
            foreach (var property in profile.Properties) {
                var txt = new TextBox() {
                    Tag = property,
                    Text = property.Value ?? "",
                    Width = ClientRectangle.Width * 2 / 3,
                };
                txt.Top = top;
                txt.Left = ClientRectangle.Right - txt.Width - marginHorizontal;

                var lbl = new Label() {
                    Text = property.Id.ToString() + ":",
                };
                lbl.Top = top + (txt.Height - lbl.Height) / 2;
                lbl.Left = ClientRectangle.Left + txt.Margin.Left;

                Controls.Add(lbl);
                Controls.Add(txt);
                top = txt.Bottom + txt.Margin.Top + txt.Margin.Bottom;
            }

            var heightDiff = Height - ClientRectangle.Height;
            Height = top + heightDiff + btnOk.Height + 2 * marginVertical + btnOk.Margin.Top + btnOk.Margin.Bottom;

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
