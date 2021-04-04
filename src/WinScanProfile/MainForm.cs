using System;
using System.Collections.Generic;
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


        public void RefreshItems() {
            using var _ = new Medo.Windows.Forms.WaitCursor(this);

            var document = new Document.Document();

            var dict = new Dictionary<string, ListViewGroup>();
            foreach (var profile in document.Profiles) {
                var groupKey = profile.DeviceId ?? string.Empty;
                if (!dict.TryGetValue(groupKey, out var group)) {
                    group = new ListViewGroup(profile.DeviceFriendlyName);
                    lsvProfiles.Groups.Add(group);
                    dict.Add(groupKey, group);
                }

                var item = new ListViewItem(profile.ProfileName, group);
                lsvProfiles.Items.Add(item);
            }

            if (lsvProfiles.Groups.Count == 1) {
                lsvProfiles.Groups.Clear();
            }
        }

    }
}
