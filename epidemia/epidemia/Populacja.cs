using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Threading;

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
            Random r = new Random();
            int selectedPreson = 0;
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    foreach( Osobnik o in currentPop[j, i]) // nie mozna zrownolegli ze wzgledu na canvas--- nie moze zniego korzystac wiecej niz jeden na raz 
                    {
                        if (!this.radomMovment) // losujemy kierunek poruszania
                        {
                            int direc = r.Next(4);
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
                        o.getOlder();
                        selectedPreson += 1;
                    }
                }
            }
            currentyear += 1;
            // czyszczenie starych list i wpisanie w nowe miejsca w tablicy list 
            List<Osobnik> allList = new List<Osobnik>();
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    foreach (Osobnik o in currentPop[j, i]) // nie da sie zrownoleglic bo add(o) chce dodac nulla -- nastepne instrukcje robia sie zanim skoncza sie te 
                    {
                        allList.Add(o);
                    }
                    currentPop[j, i].Clear();
                }
            }
            foreach (Osobnik o in allList) // to samo co wyzej ... zrwnoleglenie gubi gdzies obiekty 
            {
                addToList(o);
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

        //dodaje osobni do listy na podstawie jego pozycji 
        private void addToList(Osobnik o)
        {
            currentPop[(int)o.getPosition().X, (int)o.getPosition().Y].Add(o);
        }

        public void infect(int n)
        {
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    if(n> 0)
                    {
                        currentPop[j, i][0].getSick();
                        n--;
                    }
                }
            }

        }

        public void getSick()
        {
            Random r = new Random();
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    Parallel.ForEach(currentPop[j,i], o =>
                    {
                        if(o.isSick())
                        {
                            Parallel.ForEach(currentPop[j, i], target =>
                            {
                                if(!target.isSick() && target.canGetSick())
                                {
                                    double chance = r.NextDouble();
                                    if (chance <= this.infectChance)
                                    {
                                        target.getSick();
                                        this.sick++;
                                    }
                                }
                            });
                        }
                    });
                }
            }
        }

    }
}
