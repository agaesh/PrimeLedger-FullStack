using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrimeLedger___Window
{
    public partial class Main_Menu : Form
    {
        public Main_Menu()
        {
            InitializeComponent();
        }

        private void BtnProduct_Click(object sender, EventArgs e)
        {
            var f = new Product();
            f.ShowDialog();
        }

        private void BtnProductMetadata_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new Product_Metadata();
                f.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while opening Product Metadata: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
   
        }
    }
}
