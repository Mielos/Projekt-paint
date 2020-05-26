namespace image
{
    partial class Galeria
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
            this.lista = new System.Windows.Forms.ListView();
            this.dodaj = new System.Windows.Forms.Button();
            this.ladowanie = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.powrot = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lista
            // 
            this.lista.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lista.Dock = System.Windows.Forms.DockStyle.Right;
            this.lista.HideSelection = false;
            this.lista.Location = new System.Drawing.Point(557, 0);
            this.lista.Margin = new System.Windows.Forms.Padding(2);
            this.lista.Name = "lista";
            this.lista.Size = new System.Drawing.Size(194, 443);
            this.lista.TabIndex = 1;
            this.lista.UseCompatibleStateImageBehavior = false;
            this.lista.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lista_ItemSelectionChanged);
            // 
            // dodaj
            // 
            this.dodaj.BackColor = System.Drawing.Color.Transparent;
            this.dodaj.BackgroundImage = global::image.Properties.Resources.plus;
            this.dodaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dodaj.Location = new System.Drawing.Point(11, 73);
            this.dodaj.Margin = new System.Windows.Forms.Padding(2);
            this.dodaj.Name = "dodaj";
            this.dodaj.Size = new System.Drawing.Size(42, 44);
            this.dodaj.TabIndex = 3;
            this.dodaj.UseVisualStyleBackColor = false;
            this.dodaj.Click += new System.EventHandler(this.dodaj_Click);
            // 
            // ladowanie
            // 
            this.ladowanie.BackColor = System.Drawing.Color.Transparent;
            this.ladowanie.BackgroundImage = global::image.Properties.Resources.add__2_;
            this.ladowanie.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ladowanie.Location = new System.Drawing.Point(11, 121);
            this.ladowanie.Margin = new System.Windows.Forms.Padding(2);
            this.ladowanie.Name = "ladowanie";
            this.ladowanie.Size = new System.Drawing.Size(42, 44);
            this.ladowanie.TabIndex = 4;
            this.ladowanie.UseVisualStyleBackColor = false;
            this.ladowanie.Click += new System.EventHandler(this.ladowanie_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(273, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Podgląd:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(59, 25);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(485, 388);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // powrot
            // 
            this.powrot.BackColor = System.Drawing.Color.Transparent;
            this.powrot.BackgroundImage = global::image.Properties.Resources.back;
            this.powrot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.powrot.Location = new System.Drawing.Point(11, 25);
            this.powrot.Margin = new System.Windows.Forms.Padding(2);
            this.powrot.Name = "powrot";
            this.powrot.Size = new System.Drawing.Size(42, 44);
            this.powrot.TabIndex = 0;
            this.powrot.UseVisualStyleBackColor = false;
            this.powrot.Click += new System.EventHandler(this.powrot_Click);
            // 
            // Galeria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(751, 443);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ladowanie);
            this.Controls.Add(this.dodaj);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lista);
            this.Controls.Add(this.powrot);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Galeria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Galeria";
            this.Load += new System.EventHandler(this.Galeria_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button powrot;
        private System.Windows.Forms.ListView lista;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button dodaj;
        private System.Windows.Forms.Button ladowanie;
        private System.Windows.Forms.Label label1;
    }
}