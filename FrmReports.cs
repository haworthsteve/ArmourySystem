using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmourySystem
{
    public partial class FrmReports : Form
    {
        public FrmReports()
        {
            InitializeComponent();
            LoadFormSettings();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadFormSettings()
        {
            if (Properties.Settings.Default.ResultsLastSize.Width > 0)
            {
                this.Size = Properties.Settings.Default.ResultsLastSize;
            }

            if (Properties.Settings.Default.ResultsLastLocation.X > 0 ||
                Properties.Settings.Default.ResultsLastLocation.Y > 0)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = Properties.Settings.Default.ResultsLastLocation;
            }
        }

        private void FrmReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ResultsLastSize = this.Size;
            Properties.Settings.Default.ResultsLastLocation = this.Location;
            Properties.Settings.Default.Save();
        }
    }
}