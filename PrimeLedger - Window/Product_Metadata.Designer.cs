namespace PrimeLedger___Window
{
    partial class Product_Metadata
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            panel2 = new Panel();
            panel3 = new Panel();
            BtnClearSubGroup = new Button();
            BtnCreateSubGroup = new Button();
            rbInactiveSub = new RadioButton();
            rbActiveSub = new RadioButton();
            label5 = new Label();
            txtSubGroupCode = new TextBox();
            label6 = new Label();
            label7 = new Label();
            txtSubGroupDesc = new TextBox();
            label4 = new Label();
            dgvSubGroup = new DataGridView();
            pnlGroup = new Panel();
            BtnClose = new Button();
            label3 = new Label();
            dgvGroup = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colDeleteGroup = new DataGridViewImageColumn();
            Code = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colCreate = new DataGridViewTextBoxColumn();
            colCreateBy = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            rbInactive = new RadioButton();
            rbActive = new RadioButton();
            label2 = new Label();
            txtGroupCode = new TextBox();
            label1 = new Label();
            label12 = new Label();
            txtDescription = new TextBox();
            BtnClearGroup = new Button();
            BtnCreateGroup = new Button();
            tabPage2 = new TabPage();
            colSubGroupID = new DataGridViewTextBoxColumn();
            colDeleteSubGroup = new DataGridViewImageColumn();
            colSubGroupCode = new DataGridViewTextBoxColumn();
            colSubGroupDescription = new DataGridViewTextBoxColumn();
            colSubStatus = new DataGridViewTextBoxColumn();
            colSubCreateDate = new DataGridViewTextBoxColumn();
            colSubCreateBy = new DataGridViewTextBoxColumn();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSubGroup).BeginInit();
            pnlGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGroup).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(-4, -1);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1345, 699);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(panel2);
            tabPage1.Controls.Add(pnlGroup);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1337, 661);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Group";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ScrollBar;
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(dgvSubGroup);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(3, 339);
            panel2.Name = "panel2";
            panel2.Size = new Size(1331, 319);
            panel2.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Controls.Add(BtnClearSubGroup);
            panel3.Controls.Add(BtnCreateSubGroup);
            panel3.Controls.Add(rbInactiveSub);
            panel3.Controls.Add(rbActiveSub);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(txtSubGroupCode);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(txtSubGroupDesc);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 281);
            panel3.Name = "panel3";
            panel3.Size = new Size(1331, 38);
            panel3.TabIndex = 30;
            // 
            // BtnClearSubGroup
            // 
            BtnClearSubGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnClearSubGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnClearSubGroup.BackColor = SystemColors.Control;
            BtnClearSubGroup.FlatAppearance.BorderSize = 0;
            BtnClearSubGroup.FlatStyle = FlatStyle.Flat;
            BtnClearSubGroup.Font = new Font("Tahoma", 10.5F);
            BtnClearSubGroup.ForeColor = Color.Black;
            BtnClearSubGroup.Location = new Point(1166, 3);
            BtnClearSubGroup.Name = "BtnClearSubGroup";
            BtnClearSubGroup.Size = new Size(74, 34);
            BtnClearSubGroup.TabIndex = 41;
            BtnClearSubGroup.Text = "Clear";
            BtnClearSubGroup.UseVisualStyleBackColor = false;
            BtnClearSubGroup.Click += BtnClearSubGroup_Click;
            // 
            // BtnCreateSubGroup
            // 
            BtnCreateSubGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnCreateSubGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnCreateSubGroup.BackColor = Color.Navy;
            BtnCreateSubGroup.FlatAppearance.BorderSize = 0;
            BtnCreateSubGroup.FlatStyle = FlatStyle.Flat;
            BtnCreateSubGroup.Font = new Font("Tahoma", 10.5F);
            BtnCreateSubGroup.ForeColor = Color.White;
            BtnCreateSubGroup.Location = new Point(1242, 2);
            BtnCreateSubGroup.Name = "BtnCreateSubGroup";
            BtnCreateSubGroup.Size = new Size(88, 34);
            BtnCreateSubGroup.TabIndex = 40;
            BtnCreateSubGroup.Text = "Create";
            BtnCreateSubGroup.UseVisualStyleBackColor = false;
            BtnCreateSubGroup.Click += btnCreateSubGroup_Click;
            // 
            // rbInactiveSub
            // 
            rbInactiveSub.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rbInactiveSub.AutoSize = true;
            rbInactiveSub.Font = new Font("Tahoma", 10F);
            rbInactiveSub.Location = new Point(961, 4);
            rbInactiveSub.Name = "rbInactiveSub";
            rbInactiveSub.Size = new Size(106, 28);
            rbInactiveSub.TabIndex = 39;
            rbInactiveSub.TabStop = true;
            rbInactiveSub.Text = "Inactive";
            rbInactiveSub.UseVisualStyleBackColor = true;
            // 
            // rbActiveSub
            // 
            rbActiveSub.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rbActiveSub.AutoSize = true;
            rbActiveSub.Font = new Font("Tahoma", 10F);
            rbActiveSub.Location = new Point(870, 5);
            rbActiveSub.Name = "rbActiveSub";
            rbActiveSub.Size = new Size(89, 28);
            rbActiveSub.TabIndex = 38;
            rbActiveSub.TabStop = true;
            rbActiveSub.Text = "Active";
            rbActiveSub.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 10F);
            label5.Location = new Point(800, 7);
            label5.Name = "label5";
            label5.Size = new Size(66, 24);
            label5.TabIndex = 37;
            label5.Text = "Status";
            // 
            // txtSubGroupCode
            // 
            txtSubGroupCode.BorderStyle = BorderStyle.FixedSingle;
            txtSubGroupCode.Location = new Point(63, 4);
            txtSubGroupCode.Name = "txtSubGroupCode";
            txtSubGroupCode.Size = new Size(174, 31);
            txtSubGroupCode.TabIndex = 36;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Tahoma", 10F);
            label6.Location = new Point(2, 7);
            label6.Name = "label6";
            label6.Size = new Size(55, 24);
            label6.TabIndex = 35;
            label6.Text = "Code";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Tahoma", 10F);
            label7.Location = new Point(239, 7);
            label7.Name = "label7";
            label7.Size = new Size(110, 24);
            label7.TabIndex = 34;
            label7.Text = "Description";
            // 
            // txtSubGroupDesc
            // 
            txtSubGroupDesc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSubGroupDesc.BorderStyle = BorderStyle.FixedSingle;
            txtSubGroupDesc.Location = new Point(355, 4);
            txtSubGroupDesc.Name = "txtSubGroupDesc";
            txtSubGroupDesc.Size = new Size(442, 31);
            txtSubGroupDesc.TabIndex = 33;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            label4.Location = new Point(1, 8);
            label4.Name = "label4";
            label4.Size = new Size(182, 24);
            label4.TabIndex = 29;
            label4.Text = "Sub Group Setup";
            // 
            // dgvSubGroup
            // 
            dgvSubGroup.AllowUserToResizeRows = false;
            dgvSubGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSubGroup.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Tahoma", 10F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.White;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvSubGroup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvSubGroup.ColumnHeadersHeight = 34;
            dgvSubGroup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSubGroup.Columns.AddRange(new DataGridViewColumn[] { colSubGroupID, colDeleteSubGroup, colSubGroupCode, colSubGroupDescription, colSubStatus, colSubCreateDate, colSubCreateBy });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Tahoma", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvSubGroup.DefaultCellStyle = dataGridViewCellStyle3;
            dgvSubGroup.EnableHeadersVisualStyles = false;
            dgvSubGroup.Location = new Point(0, 37);
            dgvSubGroup.Name = "dgvSubGroup";
            dgvSubGroup.RowHeadersVisible = false;
            dgvSubGroup.RowHeadersWidth = 62;
            dgvSubGroup.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSubGroup.Size = new Size(1330, 244);
            dgvSubGroup.TabIndex = 2;
            dgvSubGroup.CellClick += dgvSubGroup_CellClick;
            dgvSubGroup.CellContentClick += dgvSubGroup_CellContentClick;
            // 
            // pnlGroup
            // 
            pnlGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlGroup.BackColor = SystemColors.ScrollBar;
            pnlGroup.Controls.Add(BtnClose);
            pnlGroup.Controls.Add(label3);
            pnlGroup.Controls.Add(dgvGroup);
            pnlGroup.Controls.Add(panel1);
            pnlGroup.Location = new Point(3, 3);
            pnlGroup.Name = "pnlGroup";
            pnlGroup.Size = new Size(1331, 335);
            pnlGroup.TabIndex = 3;
            // 
            // BtnClose
            // 
            BtnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnClose.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnClose.BackColor = SystemColors.Control;
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatStyle = FlatStyle.Flat;
            BtnClose.Font = new Font("Tahoma", 10.5F);
            BtnClose.ForeColor = Color.Black;
            BtnClose.Location = new Point(1255, 2);
            BtnClose.Name = "BtnClose";
            BtnClose.Size = new Size(74, 34);
            BtnClose.TabIndex = 29;
            BtnClose.Text = "Close";
            BtnClose.UseVisualStyleBackColor = false;
            BtnClose.Click += BtnClose_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            label3.Location = new Point(2, 7);
            label3.Name = "label3";
            label3.Size = new Size(222, 24);
            label3.TabIndex = 28;
            label3.Text = "Product Group Setup";
            // 
            // dgvGroup
            // 
            dgvGroup.AllowUserToResizeRows = false;
            dgvGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGroup.BackgroundColor = Color.White;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Tahoma", 10F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvGroup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvGroup.ColumnHeadersHeight = 30;
            dgvGroup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvGroup.Columns.AddRange(new DataGridViewColumn[] { colID, colDeleteGroup, Code, Description, colStatus, colCreate, colCreateBy });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Tahoma", 10F);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvGroup.DefaultCellStyle = dataGridViewCellStyle6;
            dgvGroup.EnableHeadersVisualStyles = false;
            dgvGroup.Location = new Point(2, 39);
            dgvGroup.Name = "dgvGroup";
            dgvGroup.RowHeadersVisible = false;
            dgvGroup.RowHeadersWidth = 62;
            dgvGroup.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGroup.Size = new Size(1329, 258);
            dgvGroup.TabIndex = 0;
            dgvGroup.CellClick += dgvGroup_CellClick;
            dgvGroup.CellContentClick += dgvGroup_CellContentClick;
            dgvGroup.CellDoubleClick += dgvGroup_CellDoubleClick;
            // 
            // colID
            // 
            colID.DataPropertyName = "id";
            colID.HeaderText = "colID";
            colID.MinimumWidth = 8;
            colID.Name = "colID";
            colID.Visible = false;
            colID.Width = 150;
            // 
            // colDeleteGroup
            // 
            colDeleteGroup.HeaderText = "";
            colDeleteGroup.Image = Properties.Resources.icons8_cross_symbol_24;
            colDeleteGroup.MinimumWidth = 8;
            colDeleteGroup.Name = "colDeleteGroup";
            colDeleteGroup.Width = 35;
            // 
            // Code
            // 
            Code.DataPropertyName = "code";
            Code.HeaderText = "Code";
            Code.MinimumWidth = 8;
            Code.Name = "Code";
            Code.ReadOnly = true;
            Code.Width = 150;
            // 
            // Description
            // 
            Description.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Description.DataPropertyName = "description";
            Description.HeaderText = "Description";
            Description.MinimumWidth = 8;
            Description.Name = "Description";
            Description.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.DataPropertyName = "status";
            colStatus.HeaderText = "Status";
            colStatus.MinimumWidth = 8;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Width = 110;
            // 
            // colCreate
            // 
            colCreate.DataPropertyName = "createdAt";
            dataGridViewCellStyle5.Format = "dd/MM/yyyy";
            colCreate.DefaultCellStyle = dataGridViewCellStyle5;
            colCreate.HeaderText = "Create Date";
            colCreate.MinimumWidth = 8;
            colCreate.Name = "colCreate";
            colCreate.ReadOnly = true;
            colCreate.Width = 150;
            // 
            // colCreateBy
            // 
            colCreateBy.HeaderText = "Create By";
            colCreateBy.MinimumWidth = 8;
            colCreateBy.Name = "colCreateBy";
            colCreateBy.Visible = false;
            colCreateBy.Width = 150;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ScrollBar;
            panel1.Controls.Add(rbInactive);
            panel1.Controls.Add(rbActive);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtGroupCode);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(txtDescription);
            panel1.Controls.Add(BtnClearGroup);
            panel1.Controls.Add(BtnCreateGroup);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 298);
            panel1.Name = "panel1";
            panel1.Size = new Size(1331, 37);
            panel1.TabIndex = 1;
            // 
            // rbInactive
            // 
            rbInactive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rbInactive.AutoSize = true;
            rbInactive.Font = new Font("Tahoma", 10F);
            rbInactive.Location = new Point(953, 3);
            rbInactive.Name = "rbInactive";
            rbInactive.Size = new Size(106, 28);
            rbInactive.TabIndex = 32;
            rbInactive.TabStop = true;
            rbInactive.Text = "Inactive";
            rbInactive.UseVisualStyleBackColor = true;
            // 
            // rbActive
            // 
            rbActive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rbActive.AutoSize = true;
            rbActive.Font = new Font("Tahoma", 10F);
            rbActive.Location = new Point(862, 4);
            rbActive.Name = "rbActive";
            rbActive.Size = new Size(89, 28);
            rbActive.TabIndex = 30;
            rbActive.TabStop = true;
            rbActive.Text = "Active";
            rbActive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 10F);
            label2.Location = new Point(792, 6);
            label2.Name = "label2";
            label2.Size = new Size(66, 24);
            label2.TabIndex = 29;
            label2.Text = "Status";
            // 
            // txtGroupCode
            // 
            txtGroupCode.BorderStyle = BorderStyle.FixedSingle;
            txtGroupCode.Location = new Point(69, 3);
            txtGroupCode.Name = "txtGroupCode";
            txtGroupCode.Size = new Size(178, 31);
            txtGroupCode.TabIndex = 28;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 10F);
            label1.Location = new Point(8, 6);
            label1.Name = "label1";
            label1.Size = new Size(55, 24);
            label1.TabIndex = 27;
            label1.Text = "Code";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Tahoma", 10F);
            label12.Location = new Point(246, 6);
            label12.Name = "label12";
            label12.Size = new Size(110, 24);
            label12.TabIndex = 26;
            label12.Text = "Description";
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Location = new Point(362, 3);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(426, 31);
            txtDescription.TabIndex = 25;
            // 
            // BtnClearGroup
            // 
            BtnClearGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnClearGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnClearGroup.BackColor = SystemColors.Control;
            BtnClearGroup.FlatAppearance.BorderSize = 0;
            BtnClearGroup.FlatStyle = FlatStyle.Flat;
            BtnClearGroup.Font = new Font("Tahoma", 10.5F);
            BtnClearGroup.ForeColor = Color.Black;
            BtnClearGroup.Location = new Point(1164, 1);
            BtnClearGroup.Name = "BtnClearGroup";
            BtnClearGroup.Size = new Size(74, 34);
            BtnClearGroup.TabIndex = 22;
            BtnClearGroup.Text = "Clear";
            BtnClearGroup.UseVisualStyleBackColor = false;
            BtnClearGroup.Click += BtnClearGroup_Click;
            // 
            // BtnCreateGroup
            // 
            BtnCreateGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnCreateGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnCreateGroup.BackColor = Color.Navy;
            BtnCreateGroup.FlatAppearance.BorderSize = 0;
            BtnCreateGroup.FlatStyle = FlatStyle.Flat;
            BtnCreateGroup.Font = new Font("Tahoma", 10.5F);
            BtnCreateGroup.ForeColor = Color.White;
            BtnCreateGroup.Location = new Point(1241, 1);
            BtnCreateGroup.Name = "BtnCreateGroup";
            BtnCreateGroup.Size = new Size(88, 34);
            BtnCreateGroup.TabIndex = 21;
            BtnCreateGroup.Text = "Create";
            BtnCreateGroup.UseVisualStyleBackColor = false;
            BtnCreateGroup.Click += BtnCreateGroup_Click;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1337, 661);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // colSubGroupID
            // 
            colSubGroupID.DataPropertyName = "id";
            colSubGroupID.HeaderText = "colID";
            colSubGroupID.MinimumWidth = 8;
            colSubGroupID.Name = "colSubGroupID";
            colSubGroupID.Visible = false;
            colSubGroupID.Width = 150;
            // 
            // colDeleteSubGroup
            // 
            colDeleteSubGroup.HeaderText = "";
            colDeleteSubGroup.Image = Properties.Resources.icons8_cross_symbol_24;
            colDeleteSubGroup.MinimumWidth = 8;
            colDeleteSubGroup.Name = "colDeleteSubGroup";
            colDeleteSubGroup.Width = 35;
            // 
            // colSubGroupCode
            // 
            colSubGroupCode.DataPropertyName = "code";
            colSubGroupCode.HeaderText = "Code";
            colSubGroupCode.MinimumWidth = 8;
            colSubGroupCode.Name = "colSubGroupCode";
            colSubGroupCode.ReadOnly = true;
            colSubGroupCode.Width = 150;
            // 
            // colSubGroupDescription
            // 
            colSubGroupDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colSubGroupDescription.DataPropertyName = "description";
            colSubGroupDescription.HeaderText = "Description";
            colSubGroupDescription.MinimumWidth = 8;
            colSubGroupDescription.Name = "colSubGroupDescription";
            colSubGroupDescription.ReadOnly = true;
            // 
            // colSubStatus
            // 
            colSubStatus.DataPropertyName = "status";
            colSubStatus.HeaderText = "Status";
            colSubStatus.MinimumWidth = 8;
            colSubStatus.Name = "colSubStatus";
            colSubStatus.ReadOnly = true;
            colSubStatus.Width = 110;
            // 
            // colSubCreateDate
            // 
            colSubCreateDate.DataPropertyName = "createdAt";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            colSubCreateDate.DefaultCellStyle = dataGridViewCellStyle2;
            colSubCreateDate.HeaderText = "Create Date";
            colSubCreateDate.MinimumWidth = 8;
            colSubCreateDate.Name = "colSubCreateDate";
            colSubCreateDate.ReadOnly = true;
            colSubCreateDate.Width = 150;
            // 
            // colSubCreateBy
            // 
            colSubCreateBy.HeaderText = "Create By";
            colSubCreateBy.MinimumWidth = 8;
            colSubCreateBy.Name = "colSubCreateBy";
            colSubCreateBy.Visible = false;
            colSubCreateBy.Width = 150;
            // 
            // Product_Metadata
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1344, 710);
            Controls.Add(tabControl1);
            MaximizeBox = false;
            Name = "Product_Metadata";
            Text = "Product_Metadata";
            WindowState = FormWindowState.Maximized;
            Load += Product_Metadata_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSubGroup).EndInit();
            pnlGroup.ResumeLayout(false);
            pnlGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGroup).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dgvGroup;
        private Panel panel1;
        private Button BtnClearGroup;
        private Button BtnCreateGroup;
        private RadioButton rbInactive;
        private RadioButton rbActive;
        private Label label2;
        private TextBox txtGroupCode;
        private Label label1;
        private Label label12;
        private TextBox txtDescription;
        private Panel pnlGroup;
        private DataGridView dgvSubGroup;
        private Panel panel2;
        private Label label4;
        private Label label3;
        private Panel panel3;
        private Button BtnClearSubGroup;
        private Button BtnCreateSubGroup;
        private RadioButton rbInactiveSub;
        private RadioButton rbActiveSub;
        private Label label5;
        private TextBox txtSubGroupCode;
        private Label label6;
        private Label label7;
        private TextBox txtSubGroupDesc;
        private Button BtnClose;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewImageColumn colDeleteGroup;
        private DataGridViewTextBoxColumn Code;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colCreate;
        private DataGridViewTextBoxColumn colCreateBy;
        private DataGridViewTextBoxColumn colSubGroupID;
        private DataGridViewImageColumn colDeleteSubGroup;
        private DataGridViewTextBoxColumn colSubGroupCode;
        private DataGridViewTextBoxColumn colSubGroupDescription;
        private DataGridViewTextBoxColumn colSubStatus;
        private DataGridViewTextBoxColumn colSubCreateDate;
        private DataGridViewTextBoxColumn colSubCreateBy;
    }
}