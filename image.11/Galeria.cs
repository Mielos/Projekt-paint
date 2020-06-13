using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace image
{
    public partial class Galeria : Form
    {
        private List<Image> LoadedImages { get; set; }
        private bool czyPuste = true;

        public Galeria()
        {
            InitializeComponent();
        }

        private void powrot_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyPaint g1 = new MyPaint();
            g1.ShowDialog();
        }

        private void Galeria_Load(object sender, EventArgs e)
        {
            ZaladujGalerie();

            ImageList images = new ImageList();
            images.ImageSize = new System.Drawing.Size(90, 90);

            foreach (var image in LoadedImages)
            {
                images.Images.Add(image);

            }

            lista.LargeImageList = images;

            for (int i = 0; i < LoadedImages.Count; i++)
            {
                lista.Items.Add(new ListViewItem($" ", i));
            }
        }

        //Zliczanie liczby obrazków
        static string path = Path.Combine(Directory.GetCurrentDirectory(), @"Galeria");
        int fileCount = Directory.EnumerateFiles(path, "*.jpg", SearchOption.AllDirectories).Count();

        void ZaladujGalerie()
        {  
            LoadedImages = new List<Image>();

            for (int i = 0; i < fileCount; i++)
            {
                string lokacja = Path.Combine(path, $@"{i}.jpg");
                var Tymczas = Image.FromFile(lokacja);
                LoadedImages.Add(Tymczas);
            }
        }

        private void lista_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lista.SelectedIndices.Count > 0)
            {
                var selectedIndex = lista.SelectedIndices[0];
                Image selectedImge = LoadedImages[selectedIndex];
                pictureBox1.Image = selectedImge;
                czyPuste = false;
            }
        }

        private void dodaj_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
           
             if (openFileDialog.ShowDialog() == DialogResult.OK)
              {
                string destinationFile = Path.Combine(path, $@"{fileCount}.jpg");
                string sourceFile = openFileDialog.FileName;

                File.Move(sourceFile, destinationFile);
            }
        }

       public void ladowanie_Click(object sender, EventArgs e)
       { 
            Image selectedImge = pictureBox1.Image;

            this.Hide();

            MyPaint g1 = new MyPaint();

            if (!czyPuste)
            {
                Bitmap oldbm = g1.bm;
                g1.bm = new Bitmap(selectedImge);
                oldbm.Dispose();
                g1.pictureBox.Image = g1.bm;
                g1.czyotwarte = true;
                g1.pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            }
            g1.ShowDialog();
        }
    }
}