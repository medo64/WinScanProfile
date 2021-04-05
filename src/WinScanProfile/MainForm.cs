using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WinScanProfile.IO;

namespace WinScanProfile {
    internal partial class MainForm : Form {

        public MainForm() {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e) {
            Form_Resize(sender, e);
        }

        private void Form_Shown(object sender, EventArgs e) {
            RefreshItems();
        }

        private void Form_Resize(object sender, EventArgs e) {
            lsvProfiles.Columns[0].Width = lsvProfiles.ClientRectangle.Width - SystemInformation.BorderSize.Width * 2;
        }


        private void lsvProfiles_AfterLabelEdit(object sender, LabelEditEventArgs e) {
            if (!string.IsNullOrWhiteSpace(e.Label)) {
                var item = lsvProfiles.Items[e.Item];
                if (item.Tag is Profile profile) {
                    profile.ProfileName = e.Label;
                    profile.Save();
                }
            } else {
                e.CancelEdit = true;
            }
        }

        private void lsvProfiles_ItemActivate(object sender, EventArgs e) {
            mnuContextEdit_Click(sender, e);
        }

        private void lsvProfiles_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.F2) {
                var selectedItem = lsvProfiles.FocusedItem;
                if (selectedItem != null) {
                    selectedItem.BeginEdit();
                }
            }
        }


        private void mnuContext_Opening(object sender, CancelEventArgs e) {
            var selectedItem = lsvProfiles.FocusedItem;
            var hasSelection = (selectedItem != null);
            mnuContextEdit.Enabled = hasSelection;
            mnuContextSetDefault.Enabled = hasSelection;
            if (!mnuContextEdit.Font.Bold) {
                mnuContextEdit.Font = new Font(mnuContextEdit.Font, FontStyle.Bold);
            }
        }

        private void mnuContextEdit_Click(object sender, EventArgs e) {
            var selectedItem = lsvProfiles.FocusedItem;
            if (selectedItem.Tag is Profile profile) {
                using var frm = new ProfileForm(Document, profile);
                frm.ShowDialog(this);
            }
        }

        private void mnuContextSetDefault_Click(object sender, EventArgs e) {
            var selectedItem = lsvProfiles.FocusedItem;
            if (selectedItem.Tag is Profile profile) {
                SetProfileAsDouble(profile);
                RefreshItems();
            }
        }


        private readonly Document Document = new();

        public void RefreshItems() {
            using var _ = new Medo.Windows.Forms.WaitCursor(this);

            var selectedProfile = lsvProfiles.FocusedItem?.Tag;

            lsvProfiles.BeginUpdate();
            lsvProfiles.Items.Clear();
            var dict = new Dictionary<string, ListViewGroup>();
            foreach (var profile in Document.Profiles) {
                var groupKey = profile.DeviceId ?? string.Empty;
                if (!dict.TryGetValue(groupKey, out var group)) {
                    group = new ListViewGroup(profile.DeviceFriendlyName);
                    lsvProfiles.Groups.Add(group);
                    dict.Add(groupKey, group);
                }

                var item = new ListViewItem(profile.ProfileName, group) {
                    Tag = profile
                };
                if (profile.IsDefault) { item.Font = new Font(item.Font, FontStyle.Bold); }
                lsvProfiles.Items.Add(item);
                if (item.Tag.Equals(selectedProfile)) { item.Selected = true; }
            }

            if (lsvProfiles.Groups.Count == 1) {
                lsvProfiles.Groups.Clear();
            }
            lsvProfiles.EndUpdate();
        }

        private void SetProfileAsDouble(Profile profile) {
            Document.SetDefaultProfile(profile);
        }

    }
}
