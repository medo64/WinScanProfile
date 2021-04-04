using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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


        private readonly Document.Document Document = new();

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

        private void lsvProfiles_DoubleClick(object sender, EventArgs e) {
            var selectedItem = lsvProfiles.FocusedItem;
            if (selectedItem.Tag is Document.Profile profile) {
                if (!profile.IsDefault) {
                    foreach (var otherProfile in Document.Profiles) {
                        if (profile == otherProfile) {
                            profile.IsDefault = true;
                        } else {
                            otherProfile.IsDefault = false;
                        }
                    }
                }
            }
            Document.Save();

            RefreshItems();
        }

        private void lsvProfiles_AfterLabelEdit(object sender, LabelEditEventArgs e) {
            if (!string.IsNullOrWhiteSpace(e.Label)) {
                var item = lsvProfiles.Items[e.Item];
                if (item.Tag is Document.Profile profile) {
                    profile.ProfileName = e.Label;
                    Document.Save();
                }
            } else {
                e.CancelEdit = true;
            }
        }

        private void lsvProfiles_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.F2) {
                var selectedItem = lsvProfiles.FocusedItem;
                if (selectedItem != null) {
                    selectedItem.BeginEdit();
                }
            }
        }

    }
}
