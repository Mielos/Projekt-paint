using System;
using System.Drawing;

namespace image
{
    class processing
    {
        public static bool ZamienNaSzare(Bitmap b)
        {
            for (int i = 0; i < b.Width; i++)

                for (int j = 0; j < b.Height; j++)
                {
                    Color c1 = b.GetPixel(i, j);
                    int r1 = c1.R;
                    int g1 = c1.G;
                    int b1 = c1.B;
                    int gray = (byte)(.299 * r1 + .587 * g1 + .114 * b1);
                    r1 = gray;
                    g1 = gray;
                    b1 = gray;
                    b.SetPixel(i, j, Color.FromArgb(r1, g1, b1));
                }
            return true;
        }
        public static bool ZamienNaSepie(Bitmap b)
        {
            for (int i = 0; i < b.Width; i++)

                for (int j = 0; j < b.Height; j++)
                {
                    Color c1 = b.GetPixel(i, j);
                    int a1 = c1.A;
                    int r1 = c1.R;
                    int g1 = c1.G;
                    int b1 = c1.B;

                    int tr = (int)(0.393 * r1 + 0.769 * g1 + 0.189 * b1);
                    int tg = (int)(0.349 * r1 + 0.686 * g1 + 0.168 * b1);
                    int tb = (int)(0.272 * r1 + 0.534 * g1 + 0.131 * b1);

                    if (tr > 255) r1 = 255;
                    else r1 = tr;

                    if (tg > 255) g1 = 255;
                    else g1 = tg;

                    if (tb > 255) b1 = 255;
                    else b1 = tb;

                    b.SetPixel(i, j, Color.FromArgb(a1, r1, g1, b1));
                }
            return true;
        }

        //Rozmycie
        public static bool Rozmycie(Bitmap b)
        {
            // x = y = 2 - stopień rozmycia
            for (int x = 2; x < b.Width; x++)
            {
                for (int y = 2; y < b.Height; y++)
                {
                    try
                    {
                        Color prevX = b.GetPixel(x - 2, y);
                        Color nextX = b.GetPixel(x + 2, y);
                        Color prevY = b.GetPixel(x, y - 2);
                        Color nextY = b.GetPixel(x, y + 2);

                        int avgR = (int)((prevX.R + nextX.R + prevY.R + nextY.R) / 4);
                        int avgG = (int)((prevX.G + nextX.G + prevY.G + nextY.G) / 4);
                        int avgB = (int)((prevX.B + nextX.B + prevY.B + nextY.B) / 4);

                        b.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                    }
                    catch (Exception) { }
                }
            }
            return true;
        }
    }
}
