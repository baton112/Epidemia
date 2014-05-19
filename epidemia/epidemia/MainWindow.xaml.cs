using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace epidemia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
  

        public MainWindow()
        {
            InitializeComponent();
           
        }
        int popSize;
        populacja people;

        //start button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popSize = Convert.ToInt32(PopSize.Text);
            people = new populacja(popSize);
            StatBarItem.Content = "Stworzono populacje";
        }
        

    }
    enum state { zdrowy, chory, wyzdrowial, martwy };
    public class osobnik
    {
        state condition;
        Vector position;

        public osobnik(int x, int y)
        {
            this.condition = state.zdrowy;
            this.position.X = x;
            this.position.Y = y;
        }
        public osobnik() { }
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
            if (condition == state.zdrowy )
            {
                return true;
            }
            else return false;
        }
    }

    public class populacja
    {
        private List<osobnik> curretPopulation;
        public int alive;

        public populacja(int size)
        {
            curretPopulation = new List<osobnik>();
            this.alive = size;
            for (int i = 0; i < alive; i++)
            {
                curretPopulation.Add(new osobnik(0, 0));
            }
        }

    }

}
