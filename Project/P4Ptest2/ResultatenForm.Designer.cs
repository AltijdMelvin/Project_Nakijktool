namespace P4Ptest2
{
    partial class ResultatenForm
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
            this.ResultatenGrid = new System.Windows.Forms.DataGridView();
            this.Toets = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Resultaten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bekijk = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Gemiddelde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TerugButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ResultatenGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ResultatenGrid
            // 
            this.ResultatenGrid.AllowUserToAddRows = false;
            this.ResultatenGrid.AllowUserToDeleteRows = false;
            this.ResultatenGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultatenGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Toets,
            this.Resultaten,
            this.Bekijk,
            this.Gemiddelde});
            this.ResultatenGrid.Location = new System.Drawing.Point(12, 45);
            this.ResultatenGrid.Name = "ResultatenGrid";
            this.ResultatenGrid.ReadOnly = true;
            this.ResultatenGrid.Size = new System.Drawing.Size(683, 386);
            this.ResultatenGrid.TabIndex = 0;
            // 
            // Toets
            // 
            this.Toets.HeaderText = "Toets";
            this.Toets.Name = "Toets";
            this.Toets.ReadOnly = true;
            // 
            // Resultaten
            // 
            this.Resultaten.HeaderText = "Resultaten";
            this.Resultaten.Name = "Resultaten";
            this.Resultaten.ReadOnly = true;
            // 
            // Bekijk
            // 
            this.Bekijk.HeaderText = "Bekijk";
            this.Bekijk.Name = "Bekijk";
            this.Bekijk.ReadOnly = true;
            // 
            // Gemiddelde
            // 
            this.Gemiddelde.HeaderText = "Gemiddelde";
            this.Gemiddelde.Name = "Gemiddelde";
            this.Gemiddelde.ReadOnly = true;
            // 
            // TerugButton
            // 
            this.TerugButton.Location = new System.Drawing.Point(13, 13);
            this.TerugButton.Name = "TerugButton";
            this.TerugButton.Size = new System.Drawing.Size(75, 23);
            this.TerugButton.TabIndex = 1;
            this.TerugButton.Text = "Terug";
            this.TerugButton.UseVisualStyleBackColor = true;
            // 
            // ResultatenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 443);
            this.Controls.Add(this.TerugButton);
            this.Controls.Add(this.ResultatenGrid);
            this.Name = "ResultatenForm";
            this.Text = "Resultaten";
            ((System.ComponentModel.ISupportInitialize)(this.ResultatenGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ResultatenGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Toets;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resultaten;
        private System.Windows.Forms.DataGridViewButtonColumn Bekijk;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gemiddelde;
        private System.Windows.Forms.Button TerugButton;
    }
}