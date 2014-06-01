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
        //private List<Osobnik> curretPopulation;
        private List<Osobnik>[,] currentPop;
        public int alive;
        public int heatly;
        public int sick;
        public int dead;
        public double infectChance;
        bool radomMovment; // true poruszamy dalej w tym samym kierunku
        public double changeDirectionChance;
        public int currentyear;

        public populacja(int size, double chance, bool randomMove)
        {
            //curretPopulation = new List<Osobnik>();
            currentPop = new List<Osobnik>[MainWindow.canvasSizeX/MainWindow.osobnikSize, MainWindow.canvasSizeY/MainWindow.osobnikSize];
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    currentPop[j, i] = new List<Osobnik>();
                }
            }

            Random r = new Random();
            this.alive = 0;
            while (alive < size)
            {
                for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
                {
                    for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                    {
                        //curretPopulation.Add(new Osobnik(j * MainWindow.osobnikSize, i * MainWindow.osobnikSize, r.Next(4)));
                        currentPop[j, i].Add(new Osobnik(j * MainWindow.osobnikSize, i * MainWindow.osobnikSize, r.Next(4)));
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
            this.currentyear = 0;
        }
        public void rysujPopulacje(Canvas c)
        {
            c.Children.Clear();
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    for(int k = 0; k < currentPop[j,i].Count ; k++)
                    {
                        currentPop[j, i][k].wyswietl(c);
                    }
                }
            }
        }
        public void moveCanvasChilds(Canvas c)
        {
            //c.Children.Clear();
            Random r = new Random();
            int selectedPreson = 0;
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    /// (int k = 0, iteato; k < currentPop[j, i].Count; k++)
                    //List<Osobnik> tmp = new List<Osobnik>(currentPop[j, i]);
                    //tmp = currentPop[j, i].Coppy;
                    //currentPop[j, i].Clear();
                    foreach( Osobnik o in currentPop[j, i])
                    {
                        //if (this.currentyear != currentPop[j, i][k].getAge()) continue;
                        if (!this.radomMovment) // losujemy kierunek poruszania
                        {
                            int direc = r.Next(4);
                            //currentPop[j, i][k].changeDirection((Direction)direc);
                            o.changeDirection((Direction)direc);
                        }
                        else // poruszamy dalej w tym samym kierynku chyba ze wylosowana liczba jest mniejsza od szansy 
                        {
                            double chance = r.NextDouble();
                            if (chance < this.changeDirectionChance) //losujemy nowy kierunek inny niz byl przedtem 
                            {
                                int newDirec = r.Next(4);
                                if (o.direction == (Direction)newDirec)
                                {
                                    while (o.direction == (Direction)newDirec)
                                    {
                                        newDirec = r.Next(4);
                                    }
                                }
                                o.changeDirection((Direction)newDirec);
                            }
                        }
                        //wygrano nowy kierunek 
                        o.moveCanvasChilds(c, selectedPreson);
                        //przesunieto na canvasie 
                        // --------Przesuniecie aktualnego osobnika do innej listy 
                        //Osobnik tmpOsobnik = o;
                        o.getOlder();
                        //currentPop[j, i].Remove(o);
                        //addToList(o);
                        selectedPreson += 1;
                    }
                }
            }
            currentyear += 1;
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

        //dodaje osobni do listy na podstawie jego pozycji 
        private void addToList(Osobnik o)
        {
            currentPop[(int)o.getPosition().X, (int)o.getPosition().Y].Add(o);
        }

    }
}
