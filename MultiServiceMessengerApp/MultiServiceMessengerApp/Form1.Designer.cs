namespace MultiServiceMessengerApp
{
    partial class Form1
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
            this.pushMessageTextBox = new System.Windows.Forms.TextBox();
            this.MessagesListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IndexTextBox = new System.Windows.Forms.TextBox();
            this.webServiceGroupBox = new System.Windows.Forms.GroupBox();
            this.wcfRadioButton = new System.Windows.Forms.RadioButton();
            this.webApi2RadioButton = new System.Windows.Forms.RadioButton();
            this.createMessageButton = new System.Windows.Forms.Button();
            this.getMessageButton = new System.Windows.Forms.Button();
            this.getAllMessagesButton = new System.Windows.Forms.Button();
            this.deleteSelectedMessageButton = new System.Windows.Forms.Button();
            this.deleteAllMessagesButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.updateMessageTextBox = new System.Windows.Forms.TextBox();
            this.updateMessageButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.millisecondsLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.uploadNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.uploadButton = new System.Windows.Forms.Button();
            this.webServiceGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uploadNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Message to create:";
            // 
            // pushMessageTextBox
            // 
            this.pushMessageTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pushMessageTextBox.Location = new System.Drawing.Point(226, 184);
            this.pushMessageTextBox.Name = "pushMessageTextBox";
            this.pushMessageTextBox.Size = new System.Drawing.Size(157, 22);
            this.pushMessageTextBox.TabIndex = 1;
            // 
            // MessagesListBox
            // 
            this.MessagesListBox.FormattingEnabled = true;
            this.MessagesListBox.Location = new System.Drawing.Point(12, 12);
            this.MessagesListBox.Name = "MessagesListBox";
            this.MessagesListBox.Size = new System.Drawing.Size(531, 147);
            this.MessagesListBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Get message by index:";
            // 
            // IndexTextBox
            // 
            this.IndexTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IndexTextBox.Location = new System.Drawing.Point(226, 227);
            this.IndexTextBox.Name = "IndexTextBox";
            this.IndexTextBox.Size = new System.Drawing.Size(157, 22);
            this.IndexTextBox.TabIndex = 8;
            // 
            // webServiceGroupBox
            // 
            this.webServiceGroupBox.Controls.Add(this.wcfRadioButton);
            this.webServiceGroupBox.Controls.Add(this.webApi2RadioButton);
            this.webServiceGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.webServiceGroupBox.Location = new System.Drawing.Point(374, 517);
            this.webServiceGroupBox.Name = "webServiceGroupBox";
            this.webServiceGroupBox.Size = new System.Drawing.Size(169, 73);
            this.webServiceGroupBox.TabIndex = 12;
            this.webServiceGroupBox.TabStop = false;
            this.webServiceGroupBox.Text = "Web service";
            // 
            // wcfRadioButton
            // 
            this.wcfRadioButton.AutoSize = true;
            this.wcfRadioButton.Location = new System.Drawing.Point(7, 44);
            this.wcfRadioButton.Name = "wcfRadioButton";
            this.wcfRadioButton.Size = new System.Drawing.Size(99, 24);
            this.wcfRadioButton.TabIndex = 1;
            this.wcfRadioButton.TabStop = true;
            this.wcfRadioButton.Text = "WCF (tcp)";
            this.wcfRadioButton.UseVisualStyleBackColor = true;
            this.wcfRadioButton.CheckedChanged += new System.EventHandler(this.wcfRadioButton_CheckedChanged);
            // 
            // webApi2RadioButton
            // 
            this.webApi2RadioButton.AutoSize = true;
            this.webApi2RadioButton.Location = new System.Drawing.Point(7, 20);
            this.webApi2RadioButton.Name = "webApi2RadioButton";
            this.webApi2RadioButton.Size = new System.Drawing.Size(145, 24);
            this.webApi2RadioButton.TabIndex = 0;
            this.webApi2RadioButton.TabStop = true;
            this.webApi2RadioButton.Text = "Web API 2 (http)";
            this.webApi2RadioButton.UseVisualStyleBackColor = true;
            this.webApi2RadioButton.CheckedChanged += new System.EventHandler(this.webApi2RadioButton_CheckedChanged);
            // 
            // createMessageButton
            // 
            this.createMessageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createMessageButton.Location = new System.Drawing.Point(389, 179);
            this.createMessageButton.Name = "createMessageButton";
            this.createMessageButton.Size = new System.Drawing.Size(154, 29);
            this.createMessageButton.TabIndex = 13;
            this.createMessageButton.Text = "Create Message";
            this.createMessageButton.UseVisualStyleBackColor = true;
            this.createMessageButton.Click += new System.EventHandler(this.createMessageButton_Click);
            // 
            // getMessageButton
            // 
            this.getMessageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getMessageButton.Location = new System.Drawing.Point(389, 223);
            this.getMessageButton.Name = "getMessageButton";
            this.getMessageButton.Size = new System.Drawing.Size(154, 29);
            this.getMessageButton.TabIndex = 14;
            this.getMessageButton.Text = "Get Message";
            this.getMessageButton.UseVisualStyleBackColor = true;
            this.getMessageButton.Click += new System.EventHandler(this.getMessageButton_Click);
            // 
            // getAllMessagesButton
            // 
            this.getAllMessagesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getAllMessagesButton.Location = new System.Drawing.Point(13, 356);
            this.getAllMessagesButton.Name = "getAllMessagesButton";
            this.getAllMessagesButton.Size = new System.Drawing.Size(531, 39);
            this.getAllMessagesButton.TabIndex = 15;
            this.getAllMessagesButton.Text = "Get All Messages";
            this.getAllMessagesButton.UseVisualStyleBackColor = true;
            this.getAllMessagesButton.Click += new System.EventHandler(this.getAllMessagesButton_Click);
            // 
            // deleteSelectedMessageButton
            // 
            this.deleteSelectedMessageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteSelectedMessageButton.Location = new System.Drawing.Point(12, 400);
            this.deleteSelectedMessageButton.Name = "deleteSelectedMessageButton";
            this.deleteSelectedMessageButton.Size = new System.Drawing.Size(531, 39);
            this.deleteSelectedMessageButton.TabIndex = 16;
            this.deleteSelectedMessageButton.Text = "Delete Selected Message";
            this.deleteSelectedMessageButton.UseVisualStyleBackColor = true;
            this.deleteSelectedMessageButton.Click += new System.EventHandler(this.deleteSelectedMessageButton_Click);
            // 
            // deleteAllMessagesButton
            // 
            this.deleteAllMessagesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteAllMessagesButton.Location = new System.Drawing.Point(12, 445);
            this.deleteAllMessagesButton.Name = "deleteAllMessagesButton";
            this.deleteAllMessagesButton.Size = new System.Drawing.Size(531, 39);
            this.deleteAllMessagesButton.TabIndex = 17;
            this.deleteAllMessagesButton.Text = "Delete All Messages";
            this.deleteAllMessagesButton.UseVisualStyleBackColor = true;
            this.deleteAllMessagesButton.Click += new System.EventHandler(this.deleteAllMessagesButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Update selected message:";
            // 
            // updateMessageTextBox
            // 
            this.updateMessageTextBox.Location = new System.Drawing.Point(226, 271);
            this.updateMessageTextBox.Name = "updateMessageTextBox";
            this.updateMessageTextBox.Size = new System.Drawing.Size(157, 20);
            this.updateMessageTextBox.TabIndex = 2;
            // 
            // updateMessageButton
            // 
            this.updateMessageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateMessageButton.Location = new System.Drawing.Point(389, 265);
            this.updateMessageButton.Name = "updateMessageButton";
            this.updateMessageButton.Size = new System.Drawing.Size(154, 33);
            this.updateMessageButton.TabIndex = 4;
            this.updateMessageButton.Text = "Update Message";
            this.updateMessageButton.UseVisualStyleBackColor = true;
            this.updateMessageButton.Click += new System.EventHandler(this.updateMessageButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 563);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(265, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Milliseconds taken for last operation:";
            // 
            // millisecondsLabel
            // 
            this.millisecondsLabel.AutoSize = true;
            this.millisecondsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.millisecondsLabel.Location = new System.Drawing.Point(283, 563);
            this.millisecondsLabel.Name = "millisecondsLabel";
            this.millisecondsLabel.Size = new System.Drawing.Size(19, 20);
            this.millisecondsLabel.TabIndex = 19;
            this.millisecondsLabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 315);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(275, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Size in MB of garbage data to upload:";
            // 
            // uploadNumericUpDown
            // 
            this.uploadNumericUpDown.Location = new System.Drawing.Point(293, 314);
            this.uploadNumericUpDown.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.uploadNumericUpDown.Name = "uploadNumericUpDown";
            this.uploadNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.uploadNumericUpDown.TabIndex = 22;
            // 
            // uploadButton
            // 
            this.uploadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadButton.Location = new System.Drawing.Point(389, 309);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(154, 33);
            this.uploadButton.TabIndex = 24;
            this.uploadButton.Text = "Upload";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 603);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.uploadNumericUpDown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.millisecondsLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.updateMessageButton);
            this.Controls.Add(this.updateMessageTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deleteAllMessagesButton);
            this.Controls.Add(this.deleteSelectedMessageButton);
            this.Controls.Add(this.getAllMessagesButton);
            this.Controls.Add(this.getMessageButton);
            this.Controls.Add(this.createMessageButton);
            this.Controls.Add(this.webServiceGroupBox);
            this.Controls.Add(this.IndexTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MessagesListBox);
            this.Controls.Add(this.pushMessageTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Multi service messenger";
            this.webServiceGroupBox.ResumeLayout(false);
            this.webServiceGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uploadNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pushMessageTextBox;
        private System.Windows.Forms.ListBox MessagesListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IndexTextBox;
        private System.Windows.Forms.GroupBox webServiceGroupBox;
        private System.Windows.Forms.RadioButton wcfRadioButton;
        private System.Windows.Forms.RadioButton webApi2RadioButton;
        private System.Windows.Forms.Button createMessageButton;
        private System.Windows.Forms.Button getMessageButton;
        private System.Windows.Forms.Button getAllMessagesButton;
        private System.Windows.Forms.Button deleteSelectedMessageButton;
        private System.Windows.Forms.Button deleteAllMessagesButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox updateMessageTextBox;
        private System.Windows.Forms.Button updateMessageButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label millisecondsLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown uploadNumericUpDown;
        private System.Windows.Forms.Button uploadButton;
    }
}

