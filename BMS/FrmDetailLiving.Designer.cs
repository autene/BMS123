namespace BMS
{
    partial class FrmDetailLiving
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetailLiving));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btAdd = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btEdit = new DevComponents.DotNetBar.ButtonX();
            this.btDelete = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btChangeHost = new DevComponents.DotNetBar.ButtonX();
            this.btView = new DevComponents.DotNetBar.ButtonX();
            this.dtgridLiving = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridLiving)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            // 
            // 
            // 
            this.txtSearch.Border.Class = "TextBoxBorder";
            this.txtSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSearch.Location = new System.Drawing.Point(234, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(297, 32);
            this.txtSearch.TabIndex = 51;
            this.txtSearch.Text = "Nhập tên nhân khẩu ....";
            this.txtSearch.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtSearch_MouseClick);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // btAdd
            // 
            this.btAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btAdd.Image = ((System.Drawing.Image)(resources.GetObject("btAdd.Image")));
            this.btAdd.ImeMode = System.Windows.Forms.ImeMode.AlphaFull;
            this.btAdd.Location = new System.Drawing.Point(956, 5);
            this.btAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(91, 43);
            this.btAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btAdd.TabIndex = 46;
            this.btAdd.Text = "Thêm";
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Image = ((System.Drawing.Image)(resources.GetObject("labelX7.Image")));
            this.labelX7.Location = new System.Drawing.Point(106, -5);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(130, 62);
            this.labelX7.TabIndex = 45;
            this.labelX7.Text = "Tìm kiếm";
            // 
            // btEdit
            // 
            this.btEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btEdit.Image = ((System.Drawing.Image)(resources.GetObject("btEdit.Image")));
            this.btEdit.Location = new System.Drawing.Point(1053, 5);
            this.btEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(91, 43);
            this.btEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btEdit.TabIndex = 47;
            this.btEdit.Text = "Sửa";
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btDelete
            // 
            this.btDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btDelete.Image = ((System.Drawing.Image)(resources.GetObject("btDelete.Image")));
            this.btDelete.Location = new System.Drawing.Point(1150, 5);
            this.btDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(91, 43);
            this.btDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btDelete.TabIndex = 48;
            this.btDelete.Text = "Xóa";
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.txtSearch);
            this.groupPanel2.Controls.Add(this.btChangeHost);
            this.groupPanel2.Controls.Add(this.btView);
            this.groupPanel2.Controls.Add(this.btAdd);
            this.groupPanel2.Controls.Add(this.labelX7);
            this.groupPanel2.Controls.Add(this.btEdit);
            this.groupPanel2.Controls.Add(this.btDelete);
            this.groupPanel2.Location = new System.Drawing.Point(4, 2);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(1259, 59);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 31;
            // 
            // btChangeHost
            // 
            this.btChangeHost.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btChangeHost.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btChangeHost.Image = ((System.Drawing.Image)(resources.GetObject("btChangeHost.Image")));
            this.btChangeHost.ImeMode = System.Windows.Forms.ImeMode.AlphaFull;
            this.btChangeHost.Location = new System.Drawing.Point(719, 5);
            this.btChangeHost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btChangeHost.Name = "btChangeHost";
            this.btChangeHost.Size = new System.Drawing.Size(134, 43);
            this.btChangeHost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btChangeHost.TabIndex = 46;
            this.btChangeHost.Text = "Đổi chủ hộ";
            this.btChangeHost.Click += new System.EventHandler(this.btChangeHost_Click);
            // 
            // btView
            // 
            this.btView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btView.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.btView.Image = ((System.Drawing.Image)(resources.GetObject("btView.Image")));
            this.btView.ImeMode = System.Windows.Forms.ImeMode.AlphaFull;
            this.btView.Location = new System.Drawing.Point(859, 5);
            this.btView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btView.Name = "btView";
            this.btView.Size = new System.Drawing.Size(91, 43);
            this.btView.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btView.TabIndex = 46;
            this.btView.Text = "Xem";
            this.btView.Click += new System.EventHandler(this.btView_Click);
            // 
            // dtgridLiving
            // 
            this.dtgridLiving.AllowUserToAddRows = false;
            this.dtgridLiving.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgridLiving.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgridLiving.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgridLiving.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgridLiving.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgridLiving.EnableHeadersVisualStyles = false;
            this.dtgridLiving.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.dtgridLiving.Location = new System.Drawing.Point(4, 67);
            this.dtgridLiving.Name = "dtgridLiving";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgridLiving.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgridLiving.RowTemplate.Height = 24;
            this.dtgridLiving.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgridLiving.Size = new System.Drawing.Size(1259, 508);
            this.dtgridLiving.TabIndex = 33;
            // 
            // check
            // 
            this.check.FillWeight = 15F;
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.Width = 20;
            // 
            // FrmDetailLiving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 596);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.dtgridLiving);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDetailLiving";
            this.Text = "FrmDetaiLiving";
            this.Load += new System.EventHandler(this.FrmDetailLiving_Load);
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgridLiving)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private DevComponents.DotNetBar.ButtonX btAdd;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btEdit;
        private DevComponents.DotNetBar.ButtonX btDelete;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dtgridLiving;
        private DevComponents.DotNetBar.ButtonX btView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private DevComponents.DotNetBar.ButtonX btChangeHost;
    }
}