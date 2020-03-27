using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool czyotwarte = false;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofile = new OpenFileDialog();
            ofile.Filter = "Image File (*.bmp,*.jpg)|*.bmp;,*.jpg;";
            if (DialogResult.OK == ofile.ShowDialog())
            {
                this.pictureBox1.Image = new Bitmap(ofile.FileName);
                czyotwarte = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap copy = new Bitmap(this.pictureBox1.Image) ;
            processing.ZamienNaSzare(copy);
            this.pictureBox1.Image = copy;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap copy = new Bitmap(this.pictureBox1.Image);
            processing.ZamienNaSepie(copy);
            this.pictureBox1.Image = copy;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(czyotwarte==true)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Image File |*.bmp;,*.jpg;,*.png";
                ImageFormat format = ImageFormat.Png;//zapisuje normlanie

                if (sfd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                {
            
                    string ext = Path.GetExtension(sfd.FileName);
                    
                    switch(ext)
                    {
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;

                    }
                    pictureBox1.Image.Save(sfd.FileName, format);
                }
                

            }
            else
                {
                MessageBox.Show("Nie otworzono zdjęcia");
            }
        }
    }
}
