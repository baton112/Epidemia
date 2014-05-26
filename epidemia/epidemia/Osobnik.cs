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


    }
}
