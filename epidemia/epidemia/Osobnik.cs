using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace epidemia
{
    enum state { zdrowy, chory, wyzdrowial, martwy };
    enum direction { up, down, left, right};

    public class Osobnik
    {
        state condition;
        Point position;

        public Osobnik() { }
        public Osobnik(int x, int y)
        {
            this.condition = state.zdrowy;
            this.position.X = x;
            this.position.Y = y;
        }
        
        public Boolean isSick()
        {
            if (condition == state.chory)
            {
                return true;
            }
            else return false;
        }
        public Boolean canGetSic()
        {
            if (condition == state.zdrowy)
            {
                return true;
            }
            else return false;
        }
        public void wyswietl(Canvas c)
        {
            Point startPoint ;
            Rectangle rect;
            startPoint = new Point(this.position.X, this.position.Y);
            if(this.condition == state.zdrowy)
            {
                rect = new Rectangle
                {
                    Stroke = Brushes.Green,
                    StrokeThickness = MainWindow.osobnikSize
                };
            }
            else
            {
                rect = new Rectangle
                {
                    Stroke = Brushes.Red,
                    StrokeThickness = MainWindow.osobnikSize
                };
            }
            Canvas.SetLeft(rect, startPoint.X);
            Canvas.SetTop(rect, startPoint.Y);
            c.Children.Add(rect);
        }

        public void move(int rand)
        {
            switch(rand)
            {
                case 0:
                    this.position.X++;
                    break;
                case 1:
                    this.position.X--;
                    break;
                case 2:
                    this.position.Y++;
                    break;
                case 3:
                    this.position.Y--;
                    break;
            }
        }
        public void moveCanvasChilds(Canvas c, int index, int rand)
        {
            this.move(rand);         
            Rectangle rectangle; 
            rectangle = (Rectangle)c.Children[index];
            if (index == -1) return;
            Canvas.SetTop(rectangle, this.position.Y);
            Canvas.SetLeft(rectangle, this.position.X);
        }


    }
}
