namespace Chess
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDebug = new System.Windows.Forms.Button();
            this.btnActualiser = new System.Windows.Forms.Button();
            this.btnCommencer = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnMenaceNoir = new System.Windows.Forms.Button();
            this.btnMenaceBlanc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(26, 545);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 0;
            this.btnDebug.Text = "Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnActualiser
            // 
            this.btnActualiser.Location = new System.Drawing.Point(713, 545);
            this.btnActualiser.Name = "btnActualiser";
            this.btnActualiser.Size = new System.Drawing.Size(75, 23);
            this.btnActualiser.TabIndex = 1;
            this.btnActualiser.Text = "Actualiser";
            this.btnActualiser.UseVisualStyleBackColor = true;
            this.btnActualiser.Click += new System.EventHandler(this.btnActualiser_Click);
            // 
            // btnCommencer
            // 
            this.btnCommencer.Location = new System.Drawing.Point(632, 545);
            this.btnCommencer.Name = "btnCommencer";
            this.btnCommencer.Size = new System.Drawing.Size(75, 23);
            this.btnCommencer.TabIndex = 2;
            this.btnCommencer.Text = "Commencer";
            this.btnCommencer.UseVisualStyleBackColor = true;
            this.btnCommencer.Click += new System.EventHandler(this.btnCommencer_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(77, 489);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(81, 13);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "Nom de la case";
            // 
            // btnMenaceNoir
            // 
            this.btnMenaceNoir.Location = new System.Drawing.Point(694, 130);
            this.btnMenaceNoir.Name = "btnMenaceNoir";
            this.btnMenaceNoir.Size = new System.Drawing.Size(90, 23);
            this.btnMenaceNoir.TabIndex = 5;
            this.btnMenaceNoir.Text = "Menace noir";
            this.btnMenaceNoir.UseVisualStyleBackColor = true;
            this.btnMenaceNoir.Click += new System.EventHandler(this.btnMenaceNoir_Click);
            // 
            // btnMenaceBlanc
            // 
            this.btnMenaceBlanc.Location = new System.Drawing.Point(694, 169);
            this.btnMenaceBlanc.Name = "btnMenaceBlanc";
            this.btnMenaceBlanc.Size = new System.Drawing.Size(90, 23);
            this.btnMenaceBlanc.TabIndex = 6;
            this.btnMenaceBlanc.Text = "menace blanc";
            this.btnMenaceBlanc.UseVisualStyleBackColor = true;
            this.btnMenaceBlanc.Click += new System.EventHandler(this.btnMenaceBlanc_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.btnMenaceBlanc);
            this.Controls.Add(this.btnMenaceNoir);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnCommencer);
            this.Controls.Add(this.btnActualiser);
            this.Controls.Add(this.btnDebug);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Button btnActualiser;
        private System.Windows.Forms.Button btnCommencer;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnMenaceNoir;
        private System.Windows.Forms.Button btnMenaceBlanc;
    }
}

