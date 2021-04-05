using System;
using System.Globalization;
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
                Control ctl;
                if (property.Values.Count > 0) {
                    var text = property.Value ?? string.Empty;
                    foreach (var value in property.Values) {
                        if (string.Equals(value.Key, text, StringComparison.OrdinalIgnoreCase)) { text = value.Value; }
                    }
                    var cmb = new ComboBox() {
                        Tag = property,
                        Text = text,
                        Width = ClientRectangle.Width * 2 / 3,
                    };
                    foreach (var value in property.Values) {
                        cmb.Items.Add(value.Value);
                    }
                    ctl = cmb;
                } else {
                    ctl = new TextBox() {
                        Tag = property,
                        Text = property.Value ?? "",
                        Width = ClientRectangle.Width * 2 / 3,
                    };
                }
                ctl.Top = top;
                ctl.Left = ClientRectangle.Right - ctl.Width - marginHorizontal;

                var lbl = new Label() {
                    AutoSize = true,
                    Text = property.Name + ":",
                };
                lbl.Top = top + (ctl.Height - lbl.Height) / 2;
                lbl.Left = ClientRectangle.Left + ctl.Margin.Left;

                Controls.Add(lbl);
                Controls.Add(ctl);
                top = ctl.Bottom + ctl.Margin.Top + ctl.Margin.Bottom;
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
            bool anyChanges = false;
            foreach (Control control in Controls) {
                if (control.Tag is Property property) {
                    var enteredValue = control.Text;
                    if (!(property.Value ?? "").Equals(enteredValue)) {
                        foreach (var value in property.Values) {
                            if (string.Equals(value.Value, enteredValue, StringComparison.OrdinalIgnoreCase)) { enteredValue = value.Key; }
                        }
                        property.Value = enteredValue;
                        anyChanges |= true;
                    }
                }
            }
            if (anyChanges) { Profile.Save(); }
        }

    }
}
