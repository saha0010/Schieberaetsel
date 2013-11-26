using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schieberätsel
{
    public class Tile
    {
        public BitmapImage img;
        public int colPos;
        public int rowPos;
        public Tile(BitmapImage image, int x, int y)
        {
            img = image;
            colPos = x;
            rowPos = y;
        }
    }
    public class DaPuzzle
    {
        Image[,] myMap = new Image[4, 4];
        public int currcol=0;
        public int currrow=0;
        public Image Da;

        public DaPuzzle(Window w)
        {
            BitmapSource bs = (BitmapSource)w.FindResource("masterImage");
            for (int i = 0; i < 4; i++ )
            {
                for (int j = 0; j < 4; j++)
                {
                    CroppedBitmap cb = new CroppedBitmap(
                           bs, new Int32Rect(0,0, 25,25));

                    
                    
                    Image temp = new Image();
                    temp.Width = 100;
                    temp.Margin = new Thickness(5);
                    temp.Source = cb.Source;
                    myMap[i,j] =  temp;
                }
            }
       
        }

        public Image getTile(int x, int y)
        {
            return myMap[x, y];
        }
        public Image getCurrentTile()
        {
            return getTile(currcol, currrow);
        }
    }
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int nextrow = 0;
        int nextcol = 0;
        DaPuzzle p;
        public MainWindow()
        {
            InitializeComponent();
            p=new DaPuzzle(this);
            apply();
        }
        public void apply()
        {
            Object[,] oa = new Object[4, 4];
            Image[,] ia = new Image[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    oa[i, j] = panel.FindName("tile" + i.ToString() + j.ToString());
                    ia[i, j] = oa[i, j] as Image;
                    ia[i, j] = p.getTile(i, j) ;
                }
            }
        }
        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            Image currT = p.getCurrentTile();
            
            if (e.Key == Key.Left)
            {
                nextrow = nextrow - 1;
                if (nextrow < 0) nextrow = 0;
                Console.WriteLine("LEFT! col:"+nextcol+" row:"+nextrow);

            }
            if (e.Key == Key.Up)
            {
                nextcol = nextcol - 1;
                if (nextcol < 0) nextcol = 0;
                Console.WriteLine("UP! col:" + nextcol + " row:" + nextrow);
            }
            if (e.Key == Key.Down)
            {
                nextcol = nextcol + 1;
                if (nextcol >1) nextcol = 1;
                Console.WriteLine("DOWN! col:" + nextcol + " row:" + nextrow);
            }
            if (e.Key == Key.Right)
            {
                nextrow = nextrow + 1;
                if (nextrow >1) nextrow = 1;
                Console.WriteLine("RIGHT! col:" + nextcol + " row:" + nextrow);
            }
            Image destT = p.getTile(nextcol, nextrow) ;
            Image tmp = destT;
            destT = currT;
            currT = tmp;

            p.currcol = nextcol;
            p.currrow = nextrow;
            apply();
        }
    }


}
