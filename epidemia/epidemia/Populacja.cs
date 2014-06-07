using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Threading;
using System.Windows;
using System.Windows.Media;


namespace epidemia
{
    public struct currentState {
        public int alive;
        public int sick;
        public int heathy;
        public int dead;
        public bool randomMovment;
    }

    public struct xyk
    {
        public int x;
        public int y;
        public int k;

    }

    public class populacja
    {
        private List<Osobnik>[,] currentPop;
        public int alive;
        public int heatly;
        public int sick;
        public int dead;
        public double infectChance;
        public double babyChance;
        bool radomMovment; // true poruszamy dalej w tym samym kierunku
        public double changeDirectionChance;
        public int currentyear;
        public int sicknessTime; // 0 - nieskonczona choroba .. nikt nie umeira

        public populacja(int size, double chance, bool randomMove)
        {
            //curretPopulation = new List<Osobnik>();
            //tworzy tablice list populacji -- jedna lista do jednej pozycji [x,y] 
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
            //tworzy osobnikowpopulacji 
            while (alive < size)
            {
                for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
                {
                    for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                    {
                        currentPop[j, i].Add(new Osobnik(j, i, r.Next(4)));
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
            this.currentPop[(int)o.getPosition().X, (int)o.getPosition().Y].Add(new Osobnik(o));
        }

        public void infect(int n)
        {
            /*Random r = new Random();
            for(int i = 0; i < n; i++)
            {
                this.currentPop[MainWindow.canvasSizeX / 2 / MainWindow.osobnikSize, MainWindow.canvasSizeY / 2 / MainWindow.osobnikSize]
                    .Add(new Osobnik(MainWindow.canvasSizeX / 2 / MainWindow.osobnikSize,
                        MainWindow.canvasSizeY / 2 / MainWindow.osobnikSize, r.Next(4), true));
                this.sick++;
                this.alive++;
            }*/
            for (int i = 0; i < n; i++)
            {
                this.currentPop[i % MainWindow.populationGrigSize, i / MainWindow.populationGrigSize % MainWindow.populationGrigSize][0].getSick();
                this.sick++;
                this.heatly--;
            }

        }
        public void getSick()
        {
            List<xyk> allList = new List<xyk>();
            Random r = new Random();
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    //zmienic sposob wybierania osob do spodkan (zeby nie bylo zawsze od pierwszej osoby 
                    foreach(Osobnik o in currentPop[j,i]) // po zarazonych osobnikach 
                    {
                        if(o.isSick())
                        {
                            for (int k = 0; (k < MainWindow.maxMeet || MainWindow.maxMeet ==0)  && k < currentPop[j, i].Count ;k++ ) /// po kandydatach na zarazenie 
                            {
                                if (currentPop[j, i][k]!= o && !currentPop[j, i][k].isSick() && currentPop[j, i][k].canGetSick())
                                {
                                    double chance = r.NextDouble();
                                    if (chance <= this.infectChance)
                                    {
                                        //currentPop[j, i][k].getSick();
                                        xyk tmp;
                                        tmp.x = j;
                                        tmp.y = i;
                                        tmp.k = k;
                                        this.sick++;
                                        this.heatly--;
                                        allList.Add(tmp);
                                        break; // tylko jeden moze zarazic tylko jednego 
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (xyk a in allList)
            {
                this.currentPop[a.x, a.y][a.k].canGetSick();
            }
        }

        public void makeBabies(Canvas c)
        {
            List<Osobnik> allList = new List<Osobnik>();
            Random r = new Random();
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    foreach(Osobnik o in currentPop[j,i]) // po zdrowych osobnikach 
                    {
                        if (!o.isSick())
                        {
                            //Parallel.ForEach(currentPop[j, i], target =>
                            //foreach(Osobnik target in currentPop[j, i]) // po celach 
                            for (int k = 0; (k < MainWindow.maxMeet || MainWindow.maxMeet == 0) && k < currentPop[j, i].Count / 2; k++) /// po kandydatach na zarazenie 
                            {
                                if (!currentPop[j, i][k].isSick())
                                {
                                    double chance = r.NextDouble();
                                    if (chance <= babyChance)
                                    {
                                        Osobnik a = new Osobnik((int)o.getPosition().X, (int)o.getPosition().Y, (int)r.Next(4), o.getAge());
                                        this.alive++;
                                        this.heatly++;
                                        allList.Add(a);
                                        break;
                                        
                                    }
                                }
                            }
                            //);
                        }
                    }//);
                }
            }
            foreach(Osobnik o in allList)
            {
                addToList(o);
            }
        }

        public void newDisplay(Canvas c)
        {
            c.Children.Clear();
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    ///dodanie kwadratu ---- kolor to 100 % zdrowych to zielony ... przechodzi w czerwony
                    int sickCount = 0;
                    int oCount = 0;
                    foreach(Osobnik o in currentPop[j, i])
                    {
                        if (o.isSick()) sickCount++;
                        oCount++;
                    }
                    double percent; // procent chorych 
                    if(oCount !=0)
                        percent = ((double)oCount - (double)sickCount) / (double)oCount * 100.0;
                    else percent = 0;
                    //stworzenie kwaratu do wyswielenia 
                    Point startPoint;
                    Rectangle rect;
                    startPoint = new Point(j * MainWindow.osobnikSize, i * MainWindow.osobnikSize);
                    if(oCount != 0)
                    {                       
                        double r, g;
                        r = percent < 50 ? 255 : (255 - (percent * 2 - 100) * 255 );
                        g = percent > 50 ? 255 : ((percent * 2) * 255 );
                        //System.Console.WriteLine(percent.ToString());
                        rect = new Rectangle
                        {
                            Stroke = new SolidColorBrush(Color.FromRgb((byte)r ,(byte)g,0)),
                            StrokeThickness = MainWindow.osobnikSize
                        };
                    }
                    else
                    {
                        rect = new Rectangle
                        {
                            Stroke = Brushes.White,
                            StrokeThickness = MainWindow.osobnikSize
                        };
                    }

                    Canvas.SetLeft(rect, startPoint.X);
                    Canvas.SetTop(rect, startPoint.Y);
                    c.Children.Add(rect);
                }
            }
        }

        public void newMove()
        {
            Random r = new Random();
            int selectedPreson = 0;
            // dla kazdej pozycji przesuwa ja w wybranym kierunku 
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    //dla kazdego osobnika w liscie
                    foreach (Osobnik o in currentPop[j, i]) // nie mozna zrownolegli ze wzgledu na canvas--- nie moze zniego korzystac wiecej niz jeden na raz 
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
                        o.move();
                        //przesunieto na canvasie 
                        o.getOlder();
                        selectedPreson += 1;
                    }
                }
            }
            currentyear += 1;
            // czyszczenie starych list i wpisanie w nowe miejsca w tablicy list 
            List<Osobnik> allList = new List<Osobnik>();
            //przerzucenie osobnikow do tymczasowej listy ogolnej 
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    for (int k = 0; k < currentPop[j, i].Count; k++)
                    {
                        allList.Add(new Osobnik(currentPop[j, i][k]));
                    }
                    //czyszczenie tej listy 
                    currentPop[j, i].Clear();
                }
            }
            for (int i = 0; i < allList.Count; i++)
            {
                if (allList[i] != null)
                    currentPop[(int)allList[i].getPosition().X, (int)allList[i].getPosition().Y].Add(allList[i]);
            }
        }

        public void executeSick()
        {
            List<xyk> allList = new List<xyk>();
            List<Osobnik> allList2 = new List<Osobnik>();
            int sssss = 0;
            for (int i = 0; i * MainWindow.osobnikSize < MainWindow.canvasSizeY; i++) ///Y 
            {
                for (int j = 0; j * MainWindow.osobnikSize < MainWindow.canvasSizeX; j++) ///X 
                {
                    //foreach(Osobnik o in currentPop[j,i]) // po zarazonych osobnikach 
                    for (int k = 0; k < currentPop[j, i].Count; k++ )
                    {
                        if (currentPop[j, i][k].isSick() && currentPop[j, i][k].survived() == this.sicknessTime && sicknessTime != 0)
                        {
                            //System.Console.WriteLine(currentPop[j, i][k].getAge());
                             //jesli za dlugo chory 
                           // {
                                xyk tmp;
                                tmp.x = j;
                                tmp.y = i;
                                tmp.k = k;
                                this.sick--;
                                this.alive--;
                                this.dead++;
                                allList2.Add(currentPop[j, i][k]);
                                sssss++;
                            //}
                        }
                    }
                }
            }
            foreach (Osobnik o in allList2)
            {
                this.currentPop[(int)o.getPosition().X, (int)o.getPosition().Y].Remove(o);
                //sssss++;
            }
            System.Console.WriteLine("usunieto"+sssss);
        }
    }
}
