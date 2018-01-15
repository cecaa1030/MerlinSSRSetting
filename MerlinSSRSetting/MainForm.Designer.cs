using System.Windows.Forms;

namespace MerlinSSRSetting
{
    partial class mainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.connectBtn = new System.Windows.Forms.Button();
            this.cmdOutput = new System.Windows.Forms.TextBox();
            this.serverText = new System.Windows.Forms.TextBox();
            this.changeBtn = new System.Windows.Forms.Button();
            this.configText = new System.Windows.Forms.TextBox();
            this.applyConfigBtn = new System.Windows.Forms.Button();
            this.serverDataGridView = new System.Windows.Forms.DataGridView();
            this.cmdTextBox = new System.Windows.Forms.TextBox();
            this.cmdExecBtn = new System.Windows.Forms.Button();
            this.updateBtn = new System.Windows.Forms.Button();
            this.serverLoadBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.serverDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(12, 22);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(175, 23);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // cmdOutput
            // 
            this.cmdOutput.Location = new System.Drawing.Point(12, 477);
            this.cmdOutput.Multiline = true;
            this.cmdOutput.Name = "cmdOutput";
            this.cmdOutput.ReadOnly = true;
            this.cmdOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cmdOutput.Size = new System.Drawing.Size(1056, 185);
            this.cmdOutput.TabIndex = 1;
            // 
            // serverText
            // 
            this.serverText.Location = new System.Drawing.Point(12, 61);
            this.serverText.Name = "serverText";
            this.serverText.Size = new System.Drawing.Size(352, 21);
            this.serverText.TabIndex = 2;
            // 
            // changeBtn
            // 
            this.changeBtn.Location = new System.Drawing.Point(12, 97);
            this.changeBtn.Name = "changeBtn";
            this.changeBtn.Size = new System.Drawing.Size(175, 23);
            this.changeBtn.TabIndex = 3;
            this.changeBtn.Text = "ChangeServer";
            this.changeBtn.UseVisualStyleBackColor = true;
            this.changeBtn.Click += new System.EventHandler(this.changeBtn_Click);
            // 
            // configText
            // 
            this.configText.Location = new System.Drawing.Point(12, 141);
            this.configText.Multiline = true;
            this.configText.Name = "configText";
            this.configText.Size = new System.Drawing.Size(352, 165);
            this.configText.TabIndex = 4;
            // 
            // applyConfigBtn
            // 
            this.applyConfigBtn.Location = new System.Drawing.Point(12, 321);
            this.applyConfigBtn.Name = "applyConfigBtn";
            this.applyConfigBtn.Size = new System.Drawing.Size(175, 23);
            this.applyConfigBtn.TabIndex = 5;
            this.applyConfigBtn.Text = "ChangeConfig";
            this.applyConfigBtn.UseVisualStyleBackColor = true;
            this.applyConfigBtn.Click += new System.EventHandler(this.applyConfigBtn_Click);
            // 
            // serverDataGridView
            // 
            this.serverDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.serverDataGridView.Location = new System.Drawing.Point(384, 61);
            this.serverDataGridView.Name = "serverDataGridView";
            this.serverDataGridView.ReadOnly = true;
            this.serverDataGridView.RowTemplate.Height = 23;
            this.serverDataGridView.Size = new System.Drawing.Size(684, 410);
            this.serverDataGridView.TabIndex = 8;
            this.serverDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.serverDataGridView_CellContentClick);
            // 
            // cmdTextBox
            // 
            this.cmdTextBox.Location = new System.Drawing.Point(13, 669);
            this.cmdTextBox.Name = "cmdTextBox";
            this.cmdTextBox.Size = new System.Drawing.Size(921, 21);
            this.cmdTextBox.TabIndex = 9;
            this.cmdTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmdTextBox_keyPress);
            // 
            // cmdExecBtn
            // 
            this.cmdExecBtn.Location = new System.Drawing.Point(940, 667);
            this.cmdExecBtn.Name = "cmdExecBtn";
            this.cmdExecBtn.Size = new System.Drawing.Size(128, 23);
            this.cmdExecBtn.TabIndex = 10;
            this.cmdExecBtn.Text = "Exec";
            this.cmdExecBtn.UseVisualStyleBackColor = true;
            this.cmdExecBtn.Click += new System.EventHandler(this.cmdExecBtn_Click);
            // 
            // updateBtn
            // 
            this.updateBtn.Location = new System.Drawing.Point(384, 22);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(174, 23);
            this.updateBtn.TabIndex = 11;
            this.updateBtn.Text = "Update Configs";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // serverLoadBtn
            // 
            this.serverLoadBtn.Location = new System.Drawing.Point(600, 22);
            this.serverLoadBtn.Name = "serverLoadBtn";
            this.serverLoadBtn.Size = new System.Drawing.Size(173, 23);
            this.serverLoadBtn.TabIndex = 12;
            this.serverLoadBtn.Text = "Load Configs";
            this.serverLoadBtn.UseVisualStyleBackColor = true;
            this.serverLoadBtn.Click += new System.EventHandler(this.serverLoadBtn_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 702);
            this.Controls.Add(this.serverLoadBtn);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.cmdExecBtn);
            this.Controls.Add(this.cmdTextBox);
            this.Controls.Add(this.serverDataGridView);
            this.Controls.Add(this.applyConfigBtn);
            this.Controls.Add(this.configText);
            this.Controls.Add(this.changeBtn);
            this.Controls.Add(this.serverText);
            this.Controls.Add(this.cmdOutput);
            this.Controls.Add(this.connectBtn);
            this.Name = "mainForm";
            this.Text = "MerlinSSRChanger";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.mainForm_Closing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.serverDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.TextBox cmdOutput;
        private System.Windows.Forms.TextBox serverText;
        private System.Windows.Forms.Button changeBtn;
        private System.Windows.Forms.TextBox configText;
        private System.Windows.Forms.Button applyConfigBtn;
        private System.Windows.Forms.DataGridView serverDataGridView;
        private System.Windows.Forms.TextBox cmdTextBox;
        private System.Windows.Forms.Button cmdExecBtn;
        private Button updateBtn;
        private Button serverLoadBtn;
    }
}

