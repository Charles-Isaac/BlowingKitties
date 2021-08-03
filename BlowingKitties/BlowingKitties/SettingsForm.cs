using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlowingKitties
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void SaveSettings()
        {
            // Create or get existing Registry subkey
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\BlowingKitties_ScreenSaver");
            key.SetValue("MaxCatPartsCount", nudCatPartsCount.Value);
            key.SetValue("ExplosionDelay", nudExplosionDelay.Value);
        }

        private void LoadSettings()
        {
            // Get the value stored in the Registry
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\BlowingKitties_ScreenSaver");

            if (int.TryParse((string)key?.GetValue("MaxCatPartsCount"), out int MaxCatPartsCount))
            {
                nudCatPartsCount.Value = MaxCatPartsCount;
            }


            if (int.TryParse((string)key?.GetValue("ExplosionDelay"), out int ExplosionDelay))
            {
                nudExplosionDelay.Value = ExplosionDelay;
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
