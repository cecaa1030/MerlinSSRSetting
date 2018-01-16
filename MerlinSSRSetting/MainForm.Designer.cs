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
            this.connectButton = new System.Windows.Forms.Button();
            this.consoleOutput = new System.Windows.Forms.TextBox();
            this.serverText = new System.Windows.Forms.TextBox();
            this.changeServerButton = new System.Windows.Forms.Button();
            this.configText = new System.Windows.Forms.TextBox();
            this.changeConfigButton = new System.Windows.Forms.Button();
            this.serverDataGridView = new System.Windows.Forms.DataGridView();
            this.cmdTextBox = new System.Windows.Forms.TextBox();
            this.cmdExecButton = new System.Windows.Forms.Button();
            this.updateSubscribeButton = new System.Windows.Forms.Button();
            this.loadLocalConfigsButton = new System.Windows.Forms.Button();
            this.reloadConfigButton = new System.Windows.Forms.Button();
            this.pingServersButton = new System.Windows.Forms.Button();
            this.subscribeAddressText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.serverDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 22);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(160, 23);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // consoleOutput
            // 
            this.consoleOutput.Location = new System.Drawing.Point(12, 477);
            this.consoleOutput.Multiline = true;
            this.consoleOutput.Name = "consoleOutput";
            this.consoleOutput.ReadOnly = true;
            this.consoleOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consoleOutput.Size = new System.Drawing.Size(1056, 185);
            this.consoleOutput.TabIndex = 1;
            // 
            // serverText
            // 
            this.serverText.Location = new System.Drawing.Point(12, 61);
            this.serverText.Name = "serverText";
            this.serverText.Size = new System.Drawing.Size(352, 21);
            this.serverText.TabIndex = 2;
            // 
            // changeServerButton
            // 
            this.changeServerButton.Location = new System.Drawing.Point(12, 97);
            this.changeServerButton.Name = "changeServerButton";
            this.changeServerButton.Size = new System.Drawing.Size(160, 23);
            this.changeServerButton.TabIndex = 3;
            this.changeServerButton.Text = "ChangeServer";
            this.changeServerButton.UseVisualStyleBackColor = true;
            this.changeServerButton.Click += new System.EventHandler(this.changeServerButton_Click);
            // 
            // configText
            // 
            this.configText.Location = new System.Drawing.Point(12, 141);
            this.configText.Multiline = true;
            this.configText.Name = "configText";
            this.configText.Size = new System.Drawing.Size(352, 165);
            this.configText.TabIndex = 4;
            // 
            // changeConfigButton
            // 
            this.changeConfigButton.Location = new System.Drawing.Point(12, 321);
            this.changeConfigButton.Name = "changeConfigButton";
            this.changeConfigButton.Size = new System.Drawing.Size(160, 23);
            this.changeConfigButton.TabIndex = 5;
            this.changeConfigButton.Text = "ChangeConfig";
            this.changeConfigButton.UseVisualStyleBackColor = true;
            this.changeConfigButton.Click += new System.EventHandler(this.changeConfigButton_Click);
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
            // cmdExecButton
            // 
            this.cmdExecButton.Location = new System.Drawing.Point(940, 667);
            this.cmdExecButton.Name = "cmdExecButton";
            this.cmdExecButton.Size = new System.Drawing.Size(128, 23);
            this.cmdExecButton.TabIndex = 10;
            this.cmdExecButton.Text = "Exec";
            this.cmdExecButton.UseVisualStyleBackColor = true;
            this.cmdExecButton.Click += new System.EventHandler(this.cmdExecButton_Click);
            // 
            // updateSubscribeButton
            // 
            this.updateSubscribeButton.Location = new System.Drawing.Point(384, 22);
            this.updateSubscribeButton.Name = "updateSubscribeButton";
            this.updateSubscribeButton.Size = new System.Drawing.Size(160, 23);
            this.updateSubscribeButton.TabIndex = 11;
            this.updateSubscribeButton.Text = "Update Subscribe Servers";
            this.updateSubscribeButton.UseVisualStyleBackColor = true;
            this.updateSubscribeButton.Click += new System.EventHandler(this.updateSubscribeButton_Click);
            // 
            // loadLocalConfigsButton
            // 
            this.loadLocalConfigsButton.Location = new System.Drawing.Point(908, 22);
            this.loadLocalConfigsButton.Name = "loadLocalConfigsButton";
            this.loadLocalConfigsButton.Size = new System.Drawing.Size(160, 23);
            this.loadLocalConfigsButton.TabIndex = 12;
            this.loadLocalConfigsButton.Text = "Load Local Configs";
            this.loadLocalConfigsButton.UseVisualStyleBackColor = true;
            this.loadLocalConfigsButton.Click += new System.EventHandler(this.loadLocalConfigsButton_Click);
            // 
            // reloadConfigButton
            // 
            this.reloadConfigButton.Location = new System.Drawing.Point(205, 22);
            this.reloadConfigButton.Name = "reloadConfigButton";
            this.reloadConfigButton.Size = new System.Drawing.Size(160, 23);
            this.reloadConfigButton.TabIndex = 13;
            this.reloadConfigButton.Text = "Reload Config";
            this.reloadConfigButton.UseVisualStyleBackColor = true;
            this.reloadConfigButton.Click += new System.EventHandler(this.reloadConfigButton_Click);
            // 
            // pingServersButton
            // 
            this.pingServersButton.Location = new System.Drawing.Point(569, 22);
            this.pingServersButton.Name = "pingServersButton";
            this.pingServersButton.Size = new System.Drawing.Size(160, 23);
            this.pingServersButton.TabIndex = 14;
            this.pingServersButton.Text = "Ping Subscribe Servers";
            this.pingServersButton.UseVisualStyleBackColor = true;
            this.pingServersButton.Click += new System.EventHandler(this.pingServersButton_Click);
            // 
            // subscribeAddressText
            // 
            this.subscribeAddressText.Location = new System.Drawing.Point(12, 450);
            this.subscribeAddressText.Name = "subscribeAddressText";
            this.subscribeAddressText.Size = new System.Drawing.Size(351, 21);
            this.subscribeAddressText.TabIndex = 15;
            this.subscribeAddressText.Text = "https://npsboost.com/link/0v097PgufaO1yXzJ?mu=1";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 702);
            this.Controls.Add(this.subscribeAddressText);
            this.Controls.Add(this.pingServersButton);
            this.Controls.Add(this.reloadConfigButton);
            this.Controls.Add(this.loadLocalConfigsButton);
            this.Controls.Add(this.updateSubscribeButton);
            this.Controls.Add(this.cmdExecButton);
            this.Controls.Add(this.cmdTextBox);
            this.Controls.Add(this.serverDataGridView);
            this.Controls.Add(this.changeConfigButton);
            this.Controls.Add(this.configText);
            this.Controls.Add(this.changeServerButton);
            this.Controls.Add(this.serverText);
            this.Controls.Add(this.consoleOutput);
            this.Controls.Add(this.connectButton);
            this.Name = "mainForm";
            this.Text = "MerlinSSRChanger";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.mainForm_Closing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.serverDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox consoleOutput;
        private System.Windows.Forms.TextBox serverText;
        private System.Windows.Forms.Button changeServerButton;
        private System.Windows.Forms.TextBox configText;
        private System.Windows.Forms.Button changeConfigButton;
        private System.Windows.Forms.DataGridView serverDataGridView;
        private System.Windows.Forms.TextBox cmdTextBox;
        private System.Windows.Forms.Button cmdExecButton;
        private Button updateSubscribeButton;
        private Button loadLocalConfigsButton;
        private Button reloadConfigButton;
        private Button pingServersButton;
        private TextBox subscribeAddressText;
    }
}

