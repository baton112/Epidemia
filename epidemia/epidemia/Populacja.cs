using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace epidemia
{
    public struct currentState {
        public int alive;
        public int sick;
        public int heathy;
        public int dead;
    }

    public class populacja
    {
        private List<Osobnik> curretPopulation;
        public int alive;
        public int heatly;
        public int sick;
        public int dead;
        public double infectChance;

        public populacja(int size, double chance)
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
            this.sick = 0;
            this.heatly = alive;
            this.dead = 0;
        }
        public void rysujPopulacje(Canvas c)
        {
            c.Children.Clear();
            for(int i = 0; i < alive; i++)
            {
                curretPopulation[i].wyswietl(c);
            }
        }
        public void moveCanvasChilds(Canvas c)
        {
            //c.Children.Clear();
            Random r = new Random();
            for (int i = 0; i < alive; i++)
            {
                int direc = r.Next(4);
                 curretPopulation[i].moveCanvasChilds(c, i, direc);
            }

        }

        public currentState getPopulationState()
        {
            currentState ret;
            ret.alive = this.alive;
            ret.heathy = this.heatly;
            ret.sick = this.sick;
            ret.dead = this.dead;
            return ret;
        }

    }
}
