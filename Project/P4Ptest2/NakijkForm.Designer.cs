namespace P4Ptest2
{
    partial class NakijkForm
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
            this.StopNakijkenButton = new System.Windows.Forms.Button();
            this.VolgendeButton = new System.Windows.Forms.Button();
            this.Vorige = new System.Windows.Forms.Button();
            this.OpdrachtBox = new System.Windows.Forms.GroupBox();
            this.SoortFoutDropdown = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.CommentaarBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.FoutBox = new System.Windows.Forms.GroupBox();
            this.OpdrachtLabel = new System.Windows.Forms.Label();
            this.FoutLabel = new System.Windows.Forms.Label();
            this.VorigeVraagButton = new System.Windows.Forms.Button();
            this.VolgendeVraagButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.OpdrachtBox.SuspendLayout();
            this.FoutBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // StopNakijkenButton
            // 
            this.StopNakijkenButton.Location = new System.Drawing.Point(13, 13);
            this.StopNakijkenButton.Name = "StopNakijkenButton";
            this.StopNakijkenButton.Size = new System.Drawing.Size(205, 23);
            this.StopNakijkenButton.TabIndex = 0;
            this.StopNakijkenButton.Text = "Nakijken stoppen en opslaan";
            this.StopNakijkenButton.UseVisualStyleBackColor = true;
            // 
            // VolgendeButton
            // 
            this.VolgendeButton.Location = new System.Drawing.Point(598, 12);
            this.VolgendeButton.Name = "VolgendeButton";
            this.VolgendeButton.Size = new System.Drawing.Size(116, 23);
            this.VolgendeButton.TabIndex = 1;
            this.VolgendeButton.Text = "Volgende opdracht";
            this.VolgendeButton.UseVisualStyleBackColor = true;
            // 
            // Vorige
            // 
            this.Vorige.Location = new System.Drawing.Point(480, 12);
            this.Vorige.Name = "Vorige";
            this.Vorige.Size = new System.Drawing.Size(112, 23);
            this.Vorige.TabIndex = 2;
            this.Vorige.Text = "Vorige opdracht";
            this.Vorige.UseVisualStyleBackColor = true;
            this.Vorige.Click += new System.EventHandler(this.Vorige_Click);
            // 
            // OpdrachtBox
            // 
            this.OpdrachtBox.Controls.Add(this.textBox2);
            this.OpdrachtBox.Controls.Add(this.VolgendeVraagButton);
            this.OpdrachtBox.Controls.Add(this.VorigeVraagButton);
            this.OpdrachtBox.Controls.Add(this.OpdrachtLabel);
            this.OpdrachtBox.Controls.Add(this.FoutBox);
            this.OpdrachtBox.Controls.Add(this.textBox1);
            this.OpdrachtBox.Controls.Add(this.CommentaarBox);
            this.OpdrachtBox.Controls.Add(this.progressBar1);
            this.OpdrachtBox.Controls.Add(this.SoortFoutDropdown);
            this.OpdrachtBox.Location = new System.Drawing.Point(13, 52);
            this.OpdrachtBox.Name = "OpdrachtBox";
            this.OpdrachtBox.Size = new System.Drawing.Size(701, 352);
            this.OpdrachtBox.TabIndex = 3;
            this.OpdrachtBox.TabStop = false;
            this.OpdrachtBox.Text = "Nakijken";
            // 
            // SoortFoutDropdown
            // 
            this.SoortFoutDropdown.FormattingEnabled = true;
            this.SoortFoutDropdown.Items.AddRange(new object[] {
            "Goed",
            "Fout",
            "Compilatiefout"});
            this.SoortFoutDropdown.Location = new System.Drawing.Point(7, 68);
            this.SoortFoutDropdown.Name = "SoortFoutDropdown";
            this.SoortFoutDropdown.Size = new System.Drawing.Size(142, 21);
            this.SoortFoutDropdown.TabIndex = 0;
            this.SoortFoutDropdown.Text = "Beoordeling opdracht...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 39);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(176, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // CommentaarBox
            // 
            this.CommentaarBox.Location = new System.Drawing.Point(7, 95);
            this.CommentaarBox.Multiline = true;
            this.CommentaarBox.Name = "CommentaarBox";
            this.CommentaarBox.Size = new System.Drawing.Size(175, 217);
            this.CommentaarBox.TabIndex = 2;
            this.CommentaarBox.Text = "Commentaar hier...";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(189, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(496, 230);
            this.textBox1.TabIndex = 3;
            // 
            // FoutBox
            // 
            this.FoutBox.Controls.Add(this.FoutLabel);
            this.FoutBox.Location = new System.Drawing.Point(189, 256);
            this.FoutBox.Name = "FoutBox";
            this.FoutBox.Size = new System.Drawing.Size(496, 90);
            this.FoutBox.TabIndex = 4;
            this.FoutBox.TabStop = false;
            this.FoutBox.Text = "Foutmeldingen";
            // 
            // OpdrachtLabel
            // 
            this.OpdrachtLabel.AutoSize = true;
            this.OpdrachtLabel.Location = new System.Drawing.Point(6, 19);
            this.OpdrachtLabel.Name = "OpdrachtLabel";
            this.OpdrachtLabel.Size = new System.Drawing.Size(122, 13);
            this.OpdrachtLabel.TabIndex = 5;
            this.OpdrachtLabel.Text = "Opdracht 1/3 (1 van 25)";
            // 
            // FoutLabel
            // 
            this.FoutLabel.AutoSize = true;
            this.FoutLabel.Location = new System.Drawing.Point(7, 20);
            this.FoutLabel.Name = "FoutLabel";
            this.FoutLabel.Size = new System.Drawing.Size(69, 13);
            this.FoutLabel.TabIndex = 0;
            this.FoutLabel.Text = "Geen fouten.";
            // 
            // VorigeVraagButton
            // 
            this.VorigeVraagButton.Location = new System.Drawing.Point(9, 319);
            this.VorigeVraagButton.Name = "VorigeVraagButton";
            this.VorigeVraagButton.Size = new System.Drawing.Size(75, 23);
            this.VorigeVraagButton.TabIndex = 6;
            this.VorigeVraagButton.Text = "Vorige";
            this.VorigeVraagButton.UseVisualStyleBackColor = true;
            this.VorigeVraagButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // VolgendeVraagButton
            // 
            this.VolgendeVraagButton.Location = new System.Drawing.Point(90, 318);
            this.VolgendeVraagButton.Name = "VolgendeVraagButton";
            this.VolgendeVraagButton.Size = new System.Drawing.Size(89, 23);
            this.VolgendeVraagButton.TabIndex = 7;
            this.VolgendeVraagButton.Text = "Volgende";
            this.VolgendeVraagButton.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(159, 69);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(24, 20);
            this.textBox2.TabIndex = 8;
            // 
            // NakijkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 416);
            this.Controls.Add(this.OpdrachtBox);
            this.Controls.Add(this.Vorige);
            this.Controls.Add(this.VolgendeButton);
            this.Controls.Add(this.StopNakijkenButton);
            this.Name = "NakijkForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nakijken";
            this.Load += new System.EventHandler(this.NakijkForm_Load);
            this.OpdrachtBox.ResumeLayout(false);
            this.OpdrachtBox.PerformLayout();
            this.FoutBox.ResumeLayout(false);
            this.FoutBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StopNakijkenButton;
        private System.Windows.Forms.Button VolgendeButton;
        private System.Windows.Forms.Button Vorige;
        private System.Windows.Forms.GroupBox OpdrachtBox;
        private System.Windows.Forms.Label OpdrachtLabel;
        private System.Windows.Forms.GroupBox FoutBox;
        private System.Windows.Forms.Label FoutLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox CommentaarBox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox SoortFoutDropdown;
        private System.Windows.Forms.Button VorigeVraagButton;
        private System.Windows.Forms.Button VolgendeVraagButton;
        private System.Windows.Forms.TextBox textBox2;
    }
}