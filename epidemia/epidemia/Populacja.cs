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
        public bool randomMovment;
    }

    public class populacja
    {
        private List<Osobnik> curretPopulation;
        public int alive;
        public int heatly;
        public int sick;
        public int dead;
        public double infectChance;
        bool radomMovment; // true poruszamy dalej w tym samym kierunku
        public double changeDirectionChance;

        public populacja(int size, double chance, bool randomMove)
        {
            curretPopulation = new List<Osobnik>();
            Random r = new Random();
            this.alive = 0;
            while (alive < size)
            {
                for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) 
                {
                    for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++)
                    {
                        curretPopulation.Add(new Osobnik(j * MainWindow.osobnikSize, i * MainWindow.osobnikSize, r.Next(4)));
                        this.alive += 1;
                        if (alive == size) break; 
                    }
                    if (alive == size) break; 
                }
            }
            this.sick = 0;
            this.heatly = alive;
            this.dead = 0;
            this.radomMovment = randomMove;
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
                if(!this.radomMovment) // losujemy kierunek poruszania
                {
                    int direc = r.Next(4);
                    curretPopulation[i].changeDirection((Direction)direc);
                    
                }
                else // poruszamy dalej w tym samym kierynku chyba ze wylosowana liczba jest mniejsza od szansy 
                {
                    double chance = r.NextDouble();
                    if (chance < this.changeDirectionChance) //losujemy nowy kierunek inny niz byl przedtem 
                    {
                        int newDirec = r.Next(4);
                        if (curretPopulation[i].direction == (Direction)newDirec)
                        {
                            while (curretPopulation[i].direction == (Direction)newDirec)
                            {
                                newDirec = r.Next(4);
                            }
                        }
                        curretPopulation[i].changeDirection((Direction)newDirec);
                    }
                }
                curretPopulation[i].moveCanvasChilds(c, i);
                
            }

        }

        public currentState getPopulationState()
        {
            currentState ret;
            ret.alive = this.alive;
            ret.heathy = this.heatly;
            ret.sick = this.sick;
            ret.dead = this.dead;
            ret.randomMovment = this.radomMovment;
            return ret;
        }

        public void changeMoveMethod(bool method)
        {
            this.radomMovment = method;
        }

    }
}
