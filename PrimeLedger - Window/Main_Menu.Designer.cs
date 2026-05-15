namespace PrimeLedger___Window
{
    partial class Main_Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            panel1 = new Panel();
            BtnProductMetadata = new Button();
            BtnProduct = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 20F);
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(216, 46);
            label1.TabIndex = 0;
            label1.Tag = "";
            label1.Text = "Main Menu";
            // 
            // panel1
            // 
            panel1.Controls.Add(BtnProductMetadata);
            panel1.Controls.Add(BtnProduct);
            panel1.Location = new Point(12, 97);
            panel1.Name = "panel1";
            panel1.Size = new Size(1307, 582);
            panel1.TabIndex = 1;
            // 
            // BtnProductMetadata
            // 
            BtnProductMetadata.BackColor = Color.Teal;
            BtnProductMetadata.ForeColor = Color.White;
            BtnProductMetadata.Location = new Point(26, 130);
            BtnProductMetadata.Name = "BtnProductMetadata";
            BtnProductMetadata.Size = new Size(130, 99);
            BtnProductMetadata.TabIndex = 1;
            BtnProductMetadata.Text = "Product Metadata";
            BtnProductMetadata.UseVisualStyleBackColor = false;
            BtnProductMetadata.Click += BtnProductMetadata_Click;
            // 
            // BtnProduct
            // 
            BtnProduct.BackColor = Color.Teal;
            BtnProduct.ForeColor = Color.White;
            BtnProduct.Location = new Point(26, 25);
            BtnProduct.Name = "BtnProduct";
            BtnProduct.Size = new Size(130, 99);
            BtnProduct.TabIndex = 0;
            BtnProduct.Text = "Product Setup";
            BtnProduct.UseVisualStyleBackColor = false;
            BtnProduct.Click += BtnProduct_Click;
            // 
            // Main_Menu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1344, 712);
            Controls.Add(panel1);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Main_Menu";
            Text = "Main_Menu";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Button BtnProduct;
        private Button BtnProductMetadata;
    }
}