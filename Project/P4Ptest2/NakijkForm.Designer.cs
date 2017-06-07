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
            this.NakijkenBox = new System.Windows.Forms.GroupBox();
            this.VolgendeVraagButton = new System.Windows.Forms.Button();
            this.VorigeVraagButton = new System.Windows.Forms.Button();
            this.OpdrachtLabel = new System.Windows.Forms.Label();
            this.FoutBox = new System.Windows.Forms.GroupBox();
            this.FoutLabel = new System.Windows.Forms.Label();
            this.CodeBox = new System.Windows.Forms.TextBox();
            this.CommentaarBox = new System.Windows.Forms.TextBox();
            this.OpdrachtProgress = new System.Windows.Forms.ProgressBar();
            this.SoortFoutDropdown = new System.Windows.Forms.ComboBox();
            this.NakijkenBox.SuspendLayout();
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
            // NakijkenBox
            // 
            this.NakijkenBox.Controls.Add(this.VolgendeVraagButton);
            this.NakijkenBox.Controls.Add(this.VorigeVraagButton);
            this.NakijkenBox.Controls.Add(this.OpdrachtLabel);
            this.NakijkenBox.Controls.Add(this.FoutBox);
            this.NakijkenBox.Controls.Add(this.CodeBox);
            this.NakijkenBox.Controls.Add(this.CommentaarBox);
            this.NakijkenBox.Controls.Add(this.OpdrachtProgress);
            this.NakijkenBox.Controls.Add(this.SoortFoutDropdown);
            this.NakijkenBox.Location = new System.Drawing.Point(13, 52);
            this.NakijkenBox.Name = "NakijkenBox";
            this.NakijkenBox.Size = new System.Drawing.Size(701, 352);
            this.NakijkenBox.TabIndex = 3;
            this.NakijkenBox.TabStop = false;
            this.NakijkenBox.Text = "Nakijken";
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
            // VorigeVraagButton
            // 
            this.VorigeVraagButton.Location = new System.Drawing.Point(9, 319);
            this.VorigeVraagButton.Name = "VorigeVraagButton";
            this.VorigeVraagButton.Size = new System.Drawing.Size(75, 23);
            this.VorigeVraagButton.TabIndex = 6;
            this.VorigeVraagButton.Text = "Vorige";
            this.VorigeVraagButton.UseVisualStyleBackColor = true;
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
            // FoutLabel
            // 
            this.FoutLabel.AutoSize = true;
            this.FoutLabel.Location = new System.Drawing.Point(7, 20);
            this.FoutLabel.Name = "FoutLabel";
            this.FoutLabel.Size = new System.Drawing.Size(69, 13);
            this.FoutLabel.TabIndex = 0;
            this.FoutLabel.Text = "Geen fouten.";
            // 
            // CodeBox
            // 
            this.CodeBox.Location = new System.Drawing.Point(189, 19);
            this.CodeBox.Multiline = true;
            this.CodeBox.Name = "CodeBox";
            this.CodeBox.Size = new System.Drawing.Size(496, 230);
            this.CodeBox.TabIndex = 3;
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
            // OpdrachtProgress
            // 
            this.OpdrachtProgress.Location = new System.Drawing.Point(7, 39);
            this.OpdrachtProgress.Name = "OpdrachtProgress";
            this.OpdrachtProgress.Size = new System.Drawing.Size(176, 23);
            this.OpdrachtProgress.TabIndex = 1;
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
            this.SoortFoutDropdown.Size = new System.Drawing.Size(172, 21);
            this.SoortFoutDropdown.TabIndex = 0;
            this.SoortFoutDropdown.Text = "Beoordeling opdracht...";
            // 
            // NakijkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 416);
            this.Controls.Add(this.NakijkenBox);
            this.Controls.Add(this.Vorige);
            this.Controls.Add(this.VolgendeButton);
            this.Controls.Add(this.StopNakijkenButton);
            this.Name = "NakijkForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nakijken";
            this.Load += new System.EventHandler(this.NakijkForm_Load);
            this.NakijkenBox.ResumeLayout(false);
            this.NakijkenBox.PerformLayout();
            this.FoutBox.ResumeLayout(false);
            this.FoutBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StopNakijkenButton;
        private System.Windows.Forms.Button VolgendeButton;
        private System.Windows.Forms.Button Vorige;
        private System.Windows.Forms.GroupBox NakijkenBox;
        private System.Windows.Forms.Label OpdrachtLabel;
        private System.Windows.Forms.GroupBox FoutBox;
        private System.Windows.Forms.Label FoutLabel;
        private System.Windows.Forms.TextBox CodeBox;
        private System.Windows.Forms.TextBox CommentaarBox;
        private System.Windows.Forms.ProgressBar OpdrachtProgress;
        private System.Windows.Forms.ComboBox SoortFoutDropdown;
        private System.Windows.Forms.Button VorigeVraagButton;
        private System.Windows.Forms.Button VolgendeVraagButton;
    }
}