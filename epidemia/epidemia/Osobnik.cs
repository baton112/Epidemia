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
        private Point position;
        public Direction direction;
        int age;

        public Osobnik() { }
        public Osobnik(int x, int y, int rand)
        {
            this.condition = State.zdrowy;
            this.position.X = x;
            this.position.Y = y;
            this.direction = (Direction) rand;
            this.age = 0; 
        }
        
        public Boolean isSick() // true if sick, flase if healthy 
        {
            if (condition == State.chory)
            {
                return true;
            }
            else return false;
        }
        public Boolean canGetSick()
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

        public void move(int maxMove)
        {
            Random r = new Random();
            int move = r.Next(maxMove);

            switch (this.direction)
            {
                case Direction.right:
                    if (this.position.X + move < MainWindow.canvasSizeX)
                        this.position.X+=move;
                    break;
                case Direction.left:
                    if (this.position.X - move >= 0)
                        this.position.X-=move;
                    break;
                case Direction.down:
                    if (this.position.Y + move < MainWindow.canvasSizeY)
                        this.position.Y +=move;
                    break;
                case Direction.up:
                    if (this.position.Y - move >= 0)
                        this.position.Y -= move;
                    break;
            }
        }

        public void changeDirection(Direction d)
        {
            this.direction = d;
        }
        public void moveCanvasChilds(Canvas c, int index,int maxMove)
        {
            this.move(maxMove);         
            Rectangle rectangle;
            rectangle = (Rectangle)c.Children[index];
            if (this.condition == State.zdrowy) rectangle.Stroke = Brushes.Green;
            else rectangle.Stroke = Brushes.Red;
            Canvas.SetTop(rectangle, this.position.Y);
            Canvas.SetLeft(rectangle, this.position.X);
        }

        public Point getPosition()
        {
            Point tmp = new Point(this.position.X / MainWindow.osobnikSize, this.position.Y /MainWindow.osobnikSize);

            return tmp;
        }

        public void getOlder()
        {
            this.age++;
        }

        public int getAge()
        {
            return this.age;
        }

        public void getSick()
        {
            this.condition = State.chory;
        }


    }
}
