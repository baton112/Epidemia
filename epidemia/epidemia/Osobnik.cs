﻿using System;
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
        int wasInfected;
        int randLifeTime;
        Random r;

        public Osobnik( Osobnik o )
        {
            this.condition = o.condition;
            this.position = o.position;
            this.direction = o.direction;
            this.age = o.age;
            this.wasInfected = o.wasInfected;
         //   this.r = new Random();
            this.randLifeTime = o.randLifeTime;
        }
        public Osobnik(int x, int y, int rand, int rand2)
        {
            this.condition = State.zdrowy;
            this.position.X = x*MainWindow.osobnikSize;
            this.position.Y = y*MainWindow.osobnikSize;
            this.direction = (Direction) rand;
            this.age = 0;
            this.randLifeTime = rand2;
        }
        public Osobnik(int x, int y, int rand, int age, int rand2)
        {
            this.condition = State.zdrowy;
            this.position.X = x*MainWindow.osobnikSize;
            this.position.Y = y*MainWindow.osobnikSize;
            this.direction = (Direction)rand;
            this.age = age;
            this.randLifeTime = rand2;
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
        public void move()
        {
            switch (this.direction)
            {
                case Direction.right:
                    if (this.position.X + MainWindow.osobnikSize < MainWindow.canvasSizeX)
                        this.position.X+=MainWindow.osobnikSize;
                    break;
                case Direction.left:
                    if (this.position.X - MainWindow.osobnikSize >= 0)
                        this.position.X-=MainWindow.osobnikSize;
                    break;
                case Direction.down:
                    if (this.position.Y + MainWindow.osobnikSize < MainWindow.canvasSizeY)
                        this.position.Y += MainWindow.osobnikSize;
                    break;
                case Direction.up:
                    if (this.position.Y - MainWindow.osobnikSize >= 0)
                        this.position.Y -= MainWindow.osobnikSize;
                    break;
            }
        }
        public void changeDirection(Direction d)
        {
            this.direction = d;
        }
        
        //zwraca pozycje x, y w tablicy 
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
            this.wasInfected = getAge();

        }

        public int survived()
        {
            return ((getAge() + this.randLifeTime) - GetInfected());
        }

        public int GetInfected()
        {
            return this.wasInfected;
        }

        public void setCondition(State condition){
            this.condition = condition;
        }

    }
}
