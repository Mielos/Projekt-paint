// Projekt - paint

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
    public partial class MyPaint : Form
    {
        // Dostepne narzędzia
        public enum Narzedzie
        {
            Tekst, Linia, Prostokat, Elipsa, Olowek, Pedzel, Gumka
        }

        // Zmienne:
        private int mouseStartX = 0;
        private int mouseStartY = 0;
        private int mouseCurrentX = 0;
        private int mouseCurrentY = 0;
        private int recSartPointX = 0;
        private int recSartPointY = 0;
        private int recSizeY = 0;
        private int recSizeX = 0;
        private bool mouseDown = false;
        public Bitmap bm;
        private Color setPaintColor;
        private Color setPaintColorFill;
        public bool czyotwarte = false;
        Narzedzie wybor;
        List<Point> currentLine = new List<Point>(); // Do zmazywania gumką

        public MyPaint()
        {
            InitializeComponent();
            bm = new Bitmap(pictureBox.Width, pictureBox.Height);

            // Domyślne kolory dla linii i wypełnienia
            setPaintColor = Color.Black;
            setPaintColorFill = Color.White;

            // Wyświetlenie domyślnych kolorów linii i wypełnienia
            pictureBox1.BackColor = setPaintColor;
            pictureBox2.BackColor = setPaintColorFill;
        }
        
        #region Wczytanie fontów
        private void MyPaint_Load(object sender, EventArgs e)
        {
            FontFamily[] family = FontFamily.Families;
            foreach (FontFamily font in family)
            {
                toolStripComboBox1.Items.Add(font.GetName(1).ToString());
            }
        }
        #endregion

        #region Otwieranie obrazu
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofile = new OpenFileDialog();
            ofile.Filter = "PNG files (*.png)| *.png|JPG files (*.jpg)| *.jpg|Bitmaps (*.bmp)| *.bmp";
            if (DialogResult.OK == ofile.ShowDialog())
            {
                Bitmap oldbm = bm;
                bm = new Bitmap(ofile.FileName);
                oldbm.Dispose();
                this.pictureBox.Image = bm;
                czyotwarte = true;
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }
        #endregion

        #region Zapisywanie grafiki
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Image File |*.bmp;,*.jpg;,*.png";
            ImageFormat format = ImageFormat.Png; //zapisuje normlanie

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;

                }
                pictureBox.Image.Save(sfd.FileName, format);
            }
        }
        #endregion

        #region Czyszczenie pictureBox'a
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy chcesz wyszyścić obszar roboczy i zacząć od nowa?", "Czyszczenie obszaru roboczego", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                bm = new Bitmap(pictureBox.Width, pictureBox.Height);
                this.pictureBox.Image = bm;
            }
        }

        #endregion

        #region Rysowanie kształtów
        // Rysowanie kształtów w obszarze pictureBox'a
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics setPaint = e.Graphics;
            if (mouseDown == true)
            {
                // Ustawienie grubości linii lub grubości konturu
                Pen size = new Pen(setPaintColor, float.Parse(toolStripComboBox4.Text));
                if (wybor == Narzedzie.Linia)
                {
                    // Rysowanie linii
                    setPaint.DrawLine(size, new Point(mouseStartX, mouseStartY), new Point(mouseCurrentX + mouseStartX, mouseCurrentY + mouseStartY));
                }
                else if (wybor == Narzedzie.Prostokat)
                {
                    // Rysowanie konturu prostokąta
                    setPaint.DrawRectangle(size, recSartPointX, recSartPointY, recSizeX, recSizeY);
                }
                else if (wybor == Narzedzie.Elipsa)
                {
                    // Rysowanie konturu elipsy
                    setPaint.DrawEllipse(size, mouseStartX, mouseStartY, mouseCurrentX, mouseCurrentY);
                }
                // Rysowanie na obszarze bitmapy
                setPaint.DrawImage(bm, new Point(0, 0));
            }
        }
        #endregion

        #region Mysz - obsługa, malowanie, rysowanie, dodawanie tekstu

        // Działania gdy LPM nie jest wciśnięty na obszarze pictureBox'a
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Rozmiar linii lub rozmiar konturu kształtu
                Pen size = new Pen(setPaintColor, float.Parse(toolStripComboBox4.Text));

                mouseDown = false;
                Graphics setPaint = Graphics.FromImage(bm);

                switch (wybor)
                {
                    // Rysowanie linii prostej
                    case Narzedzie.Linia:
                        {
                            setPaint.DrawLine(size, new Point(mouseStartX, mouseStartY), new Point(mouseCurrentX + mouseStartX, mouseCurrentY + mouseStartY));
                            break;
                        }
                    // Rysowanie prostokąta z konturem
                    case Narzedzie.Prostokat:
                        {
                            setPaint.DrawRectangle(size, recSartPointX, recSartPointY, recSizeX, recSizeY);
                            setPaint.FillRectangle(new SolidBrush(setPaintColorFill), recSartPointX, recSartPointY, recSizeX, recSizeY);
                            break;
                        }
                    // Rysowanie elipsy z konturem
                    case Narzedzie.Elipsa:
                        {
                            setPaint.DrawEllipse(size, mouseStartX, mouseStartY, mouseCurrentX, mouseCurrentY);
                            setPaint.FillEllipse(new SolidBrush(setPaintColorFill), mouseStartX, mouseStartY, mouseCurrentX, mouseCurrentY);
                            break;
                        }
                    // Dodawanie tekstu
                    case Narzedzie.Tekst:
                        {
                            FontStyle style = FontStyle.Regular;
                            switch (toolStripComboBox3.Text)
                            {
                                case "Domyślny":
                                    style = FontStyle.Regular;
                                    break;
                                case "Pogrubienie":
                                    style = FontStyle.Bold;
                                    break;
                                case "Pokreślenie":
                                    style = FontStyle.Underline;
                                    break;
                                case "Przekreślenie":
                                    style = FontStyle.Strikeout;
                                    break;
                                case "Kursywa":
                                    style = FontStyle.Italic;
                                    break;
                            }
                            setPaint.DrawString(toolStripTextBox1.Text, new Font(toolStripComboBox1.Text, float.Parse(toolStripComboBox2.Text), style),  new SolidBrush(setPaintColor), new Point(mouseStartX, mouseStartY));
                            toolStripTextBox1.Text = String.Empty;
                            break;
                        }
                    // Zmazywanie gumką
                    case Narzedzie.Gumka:
                        {
                            currentLine.Clear();
                            break;
                        }
                }
                pictureBox.Image = bm;
                pictureBox.Cursor = Cursors.Default;
                Cursor = Cursors.Default;
            }
        }

        // Działania gdy LPM  jest wciśnięty na obszarze pictureBox'a
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pictureBox.Cursor = Cursors.Cross;
                mouseDown = true;
                mouseStartX = e.X;
                mouseStartY = e.Y;

                if (e.Button == MouseButtons.Left && wybor == Narzedzie.Gumka)
                {
                    currentLine.Add(e.Location);
                }
            }
        }

        // Poruszanie myszą po obszarze pictureBox'a, malowanie, rysowanie
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // Wyświetlanie aktualnej pozycji myszy
            toolStripTextBox2.Text = Convert.ToString(e.X);
            toolStripTextBox3.Text = Convert.ToString(e.Y);

            if (mouseDown == true)
            {
                mouseCurrentX = e.X - mouseStartX;
                mouseCurrentY = e.Y - mouseStartY;

                pictureBox.Invalidate();

                switch (wybor)
                {
                    // Malowanie pędzlem
                    case Narzedzie.Pedzel:
                        {
                            using (Graphics gr = Graphics.FromImage(bm))
                            {
                                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                gr.FillEllipse(new SolidBrush(setPaintColor), e.X, e.Y, float.Parse(toolStripComboBox4.Text), float.Parse(toolStripComboBox4.Text));
                                pictureBox.Refresh();
                            }
                            break;
                        }
                    // Malowanie ołówkiem
                    case Narzedzie.Olowek:
                        {
                            using (Graphics gr = Graphics.FromImage(bm))
                            {
                                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                Pen size = new Pen(Color.Black, 1);
                                size.StartCap = size.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                                gr.DrawLine(size, mouseStartX, mouseStartY, e.X, e.Y);
                                mouseStartX = e.X;
                                mouseStartY = e.Y;
                                pictureBox.Refresh();
                            }
                            break;
                        }
                    // Zmazywanie gumką
                    case Narzedzie.Gumka:
                        {
                            currentLine.Add(e.Location);
                            transparentRysowanie();
                            break;
                        }
                }
            }
            else
            {
                pictureBox.Invalidate();
            }
            // Określenie miejsca narysowania prostokąta
            // Współrzędną x prostokąta jest minimum pomiędzy startową współrzędną x, a aktualną współrzędną x
            recSartPointX = Math.Min(mouseStartX, e.X);
            // Współrzędną y prostokąta jest minimum pomiędzy startową współrzędną y, a aktualną współrzędną y
            recSartPointY = Math.Min(mouseStartY, e.Y);
            // Szerokością (recSizeX) prostokąta jest maksimum pomiędzy startową współrzędną x, a aktualną współrzędną x
            // minus minimum pomiędzy startową współrzędną x, a aktualną współrzędną x
            recSizeX = Math.Max(mouseStartX, e.X) - Math.Min(mouseStartX, e.X);
            // Wysokość (recSizeY) prostokąta value, podobnie jak powyżej, ale z współrzędnymi y
            recSizeY = Math.Max(mouseStartY, e.Y) - Math.Min(mouseStartY, e.Y);
        }
        
        #endregion

        #region Dodawanie tekstu
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "Wprowadź tekst...";
            Cursor = Cursors.Cross;
            pictureBox.Cursor = Cursors.Cross;
            wybor = Narzedzie.Tekst;
        }
        #endregion

        #region Kształty, gumka - przyciski
        // Rysowanie linii
        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            wybor = Narzedzie.Linia;
        }
        // Rysowanie prostokąta
        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            wybor = Narzedzie.Prostokat;
        }
        // Rysowanie elipsy
        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            wybor = Narzedzie.Elipsa;
        }
        // Rysowanie pędzlem
        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            wybor = Narzedzie.Pedzel;
        }
        // Rysowanie ołówkiem
        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            wybor = Narzedzie.Olowek;
        }
        // Zmazywanie gumką
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            wybor = Narzedzie.Gumka;
        }
        #endregion

        #region Kolory linii lub konturu - przyciski
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            setPaintColor = Color.Black;
            pictureBox1.BackColor = setPaintColor;
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            setPaintColor = Color.DarkBlue;
            pictureBox1.BackColor = setPaintColor;
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            setPaintColor = Color.Green;
            pictureBox1.BackColor = setPaintColor;
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            setPaintColor = Color.Red;
            pictureBox1.BackColor = setPaintColor;
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            setPaintColor = Color.Yellow;
            pictureBox1.BackColor = setPaintColor;
        }
        // Przycisk otwierający okno palety kolorów
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ColorDialog colorWheel = new ColorDialog();
            if (colorWheel.ShowDialog() == DialogResult.OK)
            {
                setPaintColor = colorWheel.Color;
                pictureBox1.BackColor = setPaintColor;
            }
        }
        #endregion

        #region Kolory wypełnienia - przyciski
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            setPaintColorFill = Color.Black;
            pictureBox2.BackColor = setPaintColorFill;
        }
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            setPaintColorFill = Color.DarkBlue;
            pictureBox2.BackColor = setPaintColorFill;
        }
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            setPaintColorFill = Color.Green;
            pictureBox2.BackColor = setPaintColorFill;
        }
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            setPaintColorFill = Color.Red;
            pictureBox2.BackColor = setPaintColorFill;
        }
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            setPaintColorFill = Color.Yellow;
            pictureBox2.BackColor = setPaintColorFill;
        }
        // Przycisk otwierający okno palety kolorów
        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            ColorDialog colorWheel = new ColorDialog();
            if (colorWheel.ShowDialog() == DialogResult.OK)
            {
                setPaintColorFill = colorWheel.Color;
                pictureBox2.BackColor = setPaintColorFill;
            }
        }
        #endregion

        #region Filtry
        // Szarość
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bm = new Bitmap(this.pictureBox.Image);
            processing.ZamienNaSzare(bm);
            this.pictureBox.Image = bm;
        }
        // Sepia
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bm = new Bitmap(this.pictureBox.Image);
            processing.ZamienNaSepie(bm);
            this.pictureBox.Image = bm;
        }
        #endregion

        #region Galeria
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Galeria g1 = new Galeria();
            g1.ShowDialog();
        }
        #endregion

        #region Funkcja wymazująca - do gumki
        void transparentRysowanie()
        {
            using (Graphics G = Graphics.FromImage(pictureBox.Image))
            {
                G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                G.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                using (Pen somePen = new Pen(Color.Transparent, float.Parse(toolStripComboBox4.Text)))
                {
                    somePen.MiterLimit = float.Parse(toolStripComboBox4.Text) / 2;
                    somePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    somePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    somePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    if (currentLine.Count > 1)
                        G.DrawCurve(somePen, currentLine.ToArray());
                }

            }
            pictureBox.Image = pictureBox.Image;
        }
    }
    #endregion
}
