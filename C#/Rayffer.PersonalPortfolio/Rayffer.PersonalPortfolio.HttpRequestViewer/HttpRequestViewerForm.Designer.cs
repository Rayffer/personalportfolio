namespace Rayffer.PersonalPortfolio.HttpRequestViewer
{
    partial class HttpRequestViewerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxHostURL = new System.Windows.Forms.TextBox();
            this.StartLogButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.portNumberControl = new System.Windows.Forms.NumericUpDown();
            this.textBoxPublishedMethod = new System.Windows.Forms.TextBox();
            this.textBoxReceptionDate = new System.Windows.Forms.TextBox();
            this.textBoxRequestBody = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBoxRequestImage = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxRequestHeader = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxBaseAddress = new System.Windows.Forms.TextBox();
            this.textBoxEndpointName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.responseBodyTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.portNumberControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRequestImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host URL:";
            // 
            // textBoxHostURL
            // 
            this.textBoxHostURL.Location = new System.Drawing.Point(115, 91);
            this.textBoxHostURL.Name = "textBoxHostURL";
            this.textBoxHostURL.ReadOnly = true;
            this.textBoxHostURL.Size = new System.Drawing.Size(347, 20);
            this.textBoxHostURL.TabIndex = 1;
            // 
            // StartLogButton
            // 
            this.StartLogButton.Location = new System.Drawing.Point(359, 64);
            this.StartLogButton.Name = "StartLogButton";
            this.StartLogButton.Size = new System.Drawing.Size(103, 23);
            this.StartLogButton.TabIndex = 4;
            this.StartLogButton.Text = "Start logging";
            this.StartLogButton.UseVisualStyleBackColor = true;
            this.StartLogButton.Click += new System.EventHandler(this.StartLogButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Host port:";
            // 
            // portNumberControl
            // 
            this.portNumberControl.Location = new System.Drawing.Point(115, 65);
            this.portNumberControl.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.portNumberControl.Name = "portNumberControl";
            this.portNumberControl.Size = new System.Drawing.Size(238, 20);
            this.portNumberControl.TabIndex = 7;
            this.portNumberControl.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.portNumberControl.ValueChanged += new System.EventHandler(this.portNumberControl_ValueChanged);
            // 
            // textBoxPublishedMethod
            // 
            this.textBoxPublishedMethod.Location = new System.Drawing.Point(115, 143);
            this.textBoxPublishedMethod.Name = "textBoxPublishedMethod";
            this.textBoxPublishedMethod.ReadOnly = true;
            this.textBoxPublishedMethod.Size = new System.Drawing.Size(347, 20);
            this.textBoxPublishedMethod.TabIndex = 9;
            // 
            // textBoxReceptionDate
            // 
            this.textBoxReceptionDate.Location = new System.Drawing.Point(115, 117);
            this.textBoxReceptionDate.Name = "textBoxReceptionDate";
            this.textBoxReceptionDate.ReadOnly = true;
            this.textBoxReceptionDate.Size = new System.Drawing.Size(347, 20);
            this.textBoxReceptionDate.TabIndex = 10;
            // 
            // textBoxRequestBody
            // 
            this.textBoxRequestBody.Location = new System.Drawing.Point(115, 283);
            this.textBoxRequestBody.MaxLength = 255500000;
            this.textBoxRequestBody.Multiline = true;
            this.textBoxRequestBody.Name = "textBoxRequestBody";
            this.textBoxRequestBody.ReadOnly = true;
            this.textBoxRequestBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxRequestBody.Size = new System.Drawing.Size(347, 193);
            this.textBoxRequestBody.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Reception date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Published Method:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Request Body:";
            // 
            // pictureBoxRequestImage
            // 
            this.pictureBoxRequestImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxRequestImage.Location = new System.Drawing.Point(115, 482);
            this.pictureBoxRequestImage.Name = "pictureBoxRequestImage";
            this.pictureBoxRequestImage.Size = new System.Drawing.Size(347, 335);
            this.pictureBoxRequestImage.TabIndex = 15;
            this.pictureBoxRequestImage.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 482);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Request Image:";
            // 
            // textBoxRequestHeader
            // 
            this.textBoxRequestHeader.Location = new System.Drawing.Point(115, 169);
            this.textBoxRequestHeader.MaxLength = 255500000;
            this.textBoxRequestHeader.Multiline = true;
            this.textBoxRequestHeader.Name = "textBoxRequestHeader";
            this.textBoxRequestHeader.ReadOnly = true;
            this.textBoxRequestHeader.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxRequestHeader.Size = new System.Drawing.Size(347, 108);
            this.textBoxRequestHeader.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Request Header:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Endpoint name:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Base address:";
            // 
            // textBoxBaseAddress
            // 
            this.textBoxBaseAddress.Location = new System.Drawing.Point(115, 13);
            this.textBoxBaseAddress.Name = "textBoxBaseAddress";
            this.textBoxBaseAddress.Size = new System.Drawing.Size(347, 20);
            this.textBoxBaseAddress.TabIndex = 20;
            this.textBoxBaseAddress.TextChanged += new System.EventHandler(this.textBoxBaseAddress_TextChanged);
            // 
            // textBoxEndpointName
            // 
            this.textBoxEndpointName.Location = new System.Drawing.Point(115, 39);
            this.textBoxEndpointName.Name = "textBoxEndpointName";
            this.textBoxEndpointName.Size = new System.Drawing.Size(347, 20);
            this.textBoxEndpointName.TabIndex = 21;
            this.textBoxEndpointName.TextChanged += new System.EventHandler(this.textBoxEndpointName_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(469, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Response body:";
            // 
            // responseBodyTextBox
            // 
            this.responseBodyTextBox.Location = new System.Drawing.Point(559, 12);
            this.responseBodyTextBox.Multiline = true;
            this.responseBodyTextBox.Name = "responseBodyTextBox";
            this.responseBodyTextBox.Size = new System.Drawing.Size(491, 805);
            this.responseBodyTextBox.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(14, 504);
            this.label11.MaximumSize = new System.Drawing.Size(95, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 117);
            this.label11.TabIndex = 16;
            this.label11.Text = "(an image will be displayed if a json field is a serialized image (Base64String w" +
    "ith the image bytes) and the json body has a field named \"Image\")";
            // 
            // WebApiExternalSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 829);
            this.Controls.Add(this.responseBodyTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxEndpointName);
            this.Controls.Add(this.textBoxBaseAddress);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxRequestHeader);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBoxRequestImage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxRequestBody);
            this.Controls.Add(this.textBoxReceptionDate);
            this.Controls.Add(this.textBoxPublishedMethod);
            this.Controls.Add(this.portNumberControl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StartLogButton);
            this.Controls.Add(this.textBoxHostURL);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(1078, 868);
            this.MinimumSize = new System.Drawing.Size(1078, 868);
            this.Name = "WebApiExternalSimulator";
            this.Text = "Http Request Viewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WebApiExternalSimulator_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.portNumberControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRequestImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxHostURL;
        private System.Windows.Forms.Button StartLogButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown portNumberControl;
        private System.Windows.Forms.TextBox textBoxPublishedMethod;
        private System.Windows.Forms.TextBox textBoxReceptionDate;
        private System.Windows.Forms.TextBox textBoxRequestBody;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBoxRequestImage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxRequestHeader;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxBaseAddress;
        private System.Windows.Forms.TextBox textBoxEndpointName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox responseBodyTextBox;
        private System.Windows.Forms.Label label11;
    }
}

