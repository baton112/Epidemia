using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace epidemia
{
    public class populacja
    {
        private List<Osobnik> curretPopulation;
        public int alive;

        public populacja(int size)
        {
            curretPopulation = new List<Osobnik>();
            this.alive = 0;
            //while (alive < size)
            {
                for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) 
                {
                    for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++)
                    {
                        curretPopulation.Add(new Osobnik(j * MainWindow.osobnikSize, i * MainWindow.osobnikSize));
                        this.alive += 1;
                        if (alive == size) break; 
                    }
                    if (alive == size) break; 
                }
            }
           
        }
        public void rysujPopulacje(Canvas c)
        {
            c.Children.Clear();
            for(int i = 0; i < alive; i++)
            {
                curretPopulation.ElementAt(i).wyswietl(c);
            }
        }
        public void move()
        {
            Random r = new Random();
            for(int i = 0; i < alive ; i++)
            {
                int direc = r.Next(4);
                curretPopulation.ElementAt(i).move(direc);
            }
        }

    }
}
