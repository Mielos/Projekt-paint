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
        int p1 = 45;
        

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofile = new OpenFileDialog();
            ofile.Filter = "Image File (*.bmp,*.jpg)|*.bmp;,*.jpg;";
            if (DialogResult.OK == ofile.ShowDialog())
            {
                this.pictureBox1.Image = new Bitmap(ofile.FileName);
                czyotwarte = true;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
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

        private void btn_addText_Click(object sender, EventArgs e)
        {
            CustomLabel lbl = new CustomLabel();
            this.Controls.Add(lbl);

            lbl.Top = 300;
            lbl.Left = 300;
            lbl.ForeColor = Color.Red;
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Arial", 20.25F, FontStyle.Regular, GraphicsUnit.Point, ((Byte)(0)));
            lbl.AutoSize = true;
            lbl.Text = textBox1.Text;
            lbl.BringToFront();

            /*var image = new Bitmap(this.pictureBox1.Image);
            var graphics = Graphics.FromImage(image);
            graphics.DrawString(textBox1.Text, lbl.Font, Brushes.Red, new Point(lbl.Top, lbl.Left));
            this.pictureBox1.Image = image;
            lbl.Visible = false;*/
        }

        private void Ksztalty_MouseHover(object sender, EventArgs e)
        {

            this.panel1.Size = new Size(this.panel1.Size.Width, p1);
            timer1.Start();
        }

        private void Ksztalty_MouseLeave(object sender, EventArgs e)
        {
            timer1.Stop();
            p1 = 45;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (p1 > 225)
            { timer1.Stop(); }
            else
            {
                this.panel1.Size = new Size(this.panel1.Size.Width, p1);
                p1 += 35;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           // Graphics objekt = pictureBox1.CreateGraphics();
          //  Brush red = new SolidBrush(Color.Red);
           // Pen redPen = new Pen(red, 8);
           // objekt.DrawLine(redPen, 10, 10, 400, 376);
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Graphics objekt = pictureBox1.CreateGraphics();
            Brush red = new SolidBrush(Color.Red);
            Pen redPen = new Pen(red, 8);
            objekt.DrawEllipse(redPen, 10, 10, 200, 200);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Graphics objekt = pictureBox1.CreateGraphics();
            Brush red = new SolidBrush(Color.Red);
            Pen redPen = new Pen(red, 8);
            objekt.DrawLine(redPen, 10, 10, 200, 200);
        }

        private void h(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Graphics objekt = pictureBox1.CreateGraphics();
            Brush red = new SolidBrush(Color.Red);
            Pen redPen = new Pen(red, 8);
            objekt.DrawRectangle(redPen, 10, 10, 200, 200);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        bool czyRusza = false;
        int rysX = -1;
        int rysY = -1;
        Pen rys =new Pen(Color.Black,5);
        

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            
            PictureBox  p = (PictureBox)sender;
            rys.Color = p.BackColor;
        }
      
        

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Cursor = Cursors.Cross;
            czyRusza = true;
            rysX = e.X;
            rysY = e.Y;

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
           
                Graphics g = pictureBox1.CreateGraphics();
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                PictureBox p = (PictureBox)sender;
                rys.StartCap = rys.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                if (czyRusza && rysX != -1 && rysY != -1)
                {
                    g.DrawLine(rys, new Point(rysX, rysY), e.Location);
                    rysX = e.X;
                    rysY = e.Y;

                }
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            czyRusza = true;
            rysX = -1;
            rysY = -1;
            pictureBox1.Cursor = Cursors.Default;
        }
    }
}
