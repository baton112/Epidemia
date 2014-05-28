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
    public enum State { zdrowy, chory, wyzdrowial, martwy };
    public enum Direction { up, down, left, right};

    public class Osobnik
    {
        private State condition;
        Point position;
        public Direction direction;

        public Osobnik() { }
        public Osobnik(int x, int y, int rand)
        {
            this.condition = State.zdrowy;
            this.position.X = x;
            this.position.Y = y;
            this.direction = (Direction) rand;
        }
        
        public Boolean isSick()
        {
            if (condition == State.chory)
            {
                return true;
            }
            else return false;
        }
        public Boolean canGetSic()
        {
            if (condition == State.zdrowy)
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
            if(this.condition == State.zdrowy)
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

        public void move()
        {
            switch (this.direction)
            {
                case Direction.right:
                    if(this.position.X+1 < MainWindow.canvasSizeX)
                        this.position.X++;
                    break;
                case Direction.left:
                    if (this.position.X -1 >= 0)
                        this.position.X--;
                    break;
                case Direction.down:
                    if (this.position.Y + 1 < MainWindow.canvasSizeY)
                        this.position.Y++;
                    break;
                case Direction.up:
                    if (this.position.Y- 1 >= 0)
                        this.position.Y--;
                    break;
            }
        }

        public void changeDirection(Direction d)
        {
            this.direction = d;
        }
        public void moveCanvasChilds(Canvas c, int index)
        {
            this.move();         
            Rectangle rectangle; 
            rectangle = (Rectangle)c.Children[index];
            Canvas.SetTop(rectangle, this.position.Y);
            Canvas.SetLeft(rectangle, this.position.X);
        }


    }
}
