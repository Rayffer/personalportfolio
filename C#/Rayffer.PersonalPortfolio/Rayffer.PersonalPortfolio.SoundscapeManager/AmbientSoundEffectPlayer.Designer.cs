namespace Rayffer.PersonalPortfolio.SoundscapeManager.Controls
{
    partial class AmbientSoundEffectPlayer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.loopAudioCheckBox = new System.Windows.Forms.CheckBox();
            this.loopIntervalComboBox = new System.Windows.Forms.ComboBox();
            this.ambientSoundPlayerButton = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ambientSoundPlayerButton)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ambientSoundPlayerButton, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(200, 80);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(200, 80);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 80);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.volumeTrackBar, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(55, 25);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(145, 55);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.volumeTrackBar.Location = new System.Drawing.Point(3, 3);
            this.volumeTrackBar.Maximum = 30;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Size = new System.Drawing.Size(139, 21);
            this.volumeTrackBar.TabIndex = 0;
            this.volumeTrackBar.Value = 10;
            this.volumeTrackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.loopAudioCheckBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.loopIntervalComboBox, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(145, 28);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // loopAudioCheckBox
            // 
            this.loopAudioCheckBox.AutoSize = true;
            this.loopAudioCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loopAudioCheckBox.Location = new System.Drawing.Point(3, 3);
            this.loopAudioCheckBox.Name = "loopAudioCheckBox";
            this.loopAudioCheckBox.Size = new System.Drawing.Size(49, 22);
            this.loopAudioCheckBox.TabIndex = 0;
            this.loopAudioCheckBox.Text = "loop";
            this.loopAudioCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loopAudioCheckBox.UseVisualStyleBackColor = true;
            this.loopAudioCheckBox.CheckedChanged += new System.EventHandler(this.loopAudioCheckBox_CheckedChanged);
            // 
            // loopIntervalComboBox
            // 
            this.loopIntervalComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loopIntervalComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loopIntervalComboBox.FormattingEnabled = true;
            this.loopIntervalComboBox.Location = new System.Drawing.Point(58, 3);
            this.loopIntervalComboBox.Name = "loopIntervalComboBox";
            this.loopIntervalComboBox.Size = new System.Drawing.Size(84, 21);
            this.loopIntervalComboBox.TabIndex = 1;
            // 
            // ambientSoundPlayerButton
            // 
            this.ambientSoundPlayerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ambientSoundPlayerButton.Location = new System.Drawing.Point(3, 28);
            this.ambientSoundPlayerButton.Name = "ambientSoundPlayerButton";
            this.ambientSoundPlayerButton.Size = new System.Drawing.Size(49, 49);
            this.ambientSoundPlayerButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ambientSoundPlayerButton.TabIndex = 2;
            this.ambientSoundPlayerButton.TabStop = false;
            this.ambientSoundPlayerButton.Click += new System.EventHandler(this.ambientSoundPlayerButton_Click);
            // 
            // AmbientSoundEffectPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(204, 84);
            this.MinimumSize = new System.Drawing.Size(204, 84);
            this.Name = "AmbientSoundEffectPlayer";
            this.Size = new System.Drawing.Size(204, 84);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ambientSoundPlayerButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox ambientSoundPlayerButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public System.Windows.Forms.TrackBar volumeTrackBar;
        public System.Windows.Forms.CheckBox loopAudioCheckBox;
        public System.Windows.Forms.ComboBox loopIntervalComboBox;
    }
}
