namespace PorkSpleenClient
{
    partial class ChatControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.locationTab = new System.Windows.Forms.TabPage();
            this.friendsTab = new System.Windows.Forms.TabPage();
            this.ignoreTab = new System.Windows.Forms.TabPage();
            this.locationListBox = new System.Windows.Forms.ListBox();
            this.friendsListBox = new System.Windows.Forms.ListBox();
            this.ignoreListBox = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.locationTab.SuspendLayout();
            this.friendsTab.SuspendLayout();
            this.ignoreTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 127);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(181, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 3);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(262, 115);
            this.textBox2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(190, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.locationTab);
            this.tabControl1.Controls.Add(this.friendsTab);
            this.tabControl1.Controls.Add(this.ignoreTab);
            this.tabControl1.Location = new System.Drawing.Point(271, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(182, 144);
            this.tabControl1.TabIndex = 3;
            // 
            // locationTab
            // 
            this.locationTab.Controls.Add(this.locationListBox);
            this.locationTab.Location = new System.Drawing.Point(4, 22);
            this.locationTab.Name = "locationTab";
            this.locationTab.Size = new System.Drawing.Size(174, 118);
            this.locationTab.TabIndex = 0;
            this.locationTab.Text = "Location";
            this.locationTab.UseVisualStyleBackColor = true;
            // 
            // friendsTab
            // 
            this.friendsTab.Controls.Add(this.friendsListBox);
            this.friendsTab.Location = new System.Drawing.Point(4, 22);
            this.friendsTab.Name = "friendsTab";
            this.friendsTab.Size = new System.Drawing.Size(174, 118);
            this.friendsTab.TabIndex = 1;
            this.friendsTab.Text = "Friends";
            this.friendsTab.UseVisualStyleBackColor = true;
            // 
            // ignoreTab
            // 
            this.ignoreTab.Controls.Add(this.ignoreListBox);
            this.ignoreTab.Location = new System.Drawing.Point(4, 22);
            this.ignoreTab.Name = "ignoreTab";
            this.ignoreTab.Size = new System.Drawing.Size(174, 118);
            this.ignoreTab.TabIndex = 2;
            this.ignoreTab.Text = "Ignore";
            this.ignoreTab.UseVisualStyleBackColor = true;
            // 
            // locationListBox
            // 
            this.locationListBox.FormattingEnabled = true;
            this.locationListBox.Location = new System.Drawing.Point(3, 7);
            this.locationListBox.Name = "locationListBox";
            this.locationListBox.Size = new System.Drawing.Size(168, 108);
            this.locationListBox.TabIndex = 0;
            // 
            // friendsListBox
            // 
            this.friendsListBox.FormattingEnabled = true;
            this.friendsListBox.Location = new System.Drawing.Point(3, 7);
            this.friendsListBox.Name = "friendsListBox";
            this.friendsListBox.Size = new System.Drawing.Size(168, 108);
            this.friendsListBox.TabIndex = 0;
            // 
            // ignoreListBox
            // 
            this.ignoreListBox.FormattingEnabled = true;
            this.ignoreListBox.Location = new System.Drawing.Point(3, 7);
            this.ignoreListBox.Name = "ignoreListBox";
            this.ignoreListBox.Size = new System.Drawing.Size(168, 108);
            this.ignoreListBox.TabIndex = 0;
            // 
            // ChatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "ChatControl";
            this.Size = new System.Drawing.Size(456, 150);
            this.tabControl1.ResumeLayout(false);
            this.locationTab.ResumeLayout(false);
            this.friendsTab.ResumeLayout(false);
            this.ignoreTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage locationTab;
        private System.Windows.Forms.ListBox locationListBox;
        private System.Windows.Forms.TabPage friendsTab;
        private System.Windows.Forms.ListBox friendsListBox;
        private System.Windows.Forms.TabPage ignoreTab;
        private System.Windows.Forms.ListBox ignoreListBox;
    }
}
