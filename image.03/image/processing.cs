using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image
{
    class processing
    {
        public processing()
        { }
        public static bool ZamienNaSzare(Bitmap b)
        {
            for (int i = 0; i < b.Width; i++)
            
                for (int j = 0; j < b.Height; j++)
                {
                    Color c1 = b.GetPixel(i,j);
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

                    if (tr > 255)
                    {
                        r1 = 255;
                    }
                    else
                    {
                        r1 = tr;
                    }

                    if (tg > 255)
                    {
                        g1 = 255;
                    }
                    else
                    {
                        g1 = tg;
                    }

                    if (tb > 255)
                    {
                        b1 = 255;
                    }
                    else
                    {
                        b1 = tb;
                    }
                    b.SetPixel(i, j, Color.FromArgb(a1, r1, g1, b1));
                

                }
            return true;
        }
    }
}
