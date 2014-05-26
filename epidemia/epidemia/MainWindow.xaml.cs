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
        public static int osobnikSize = 2; // wielkosc kwadratu ktory bedzie przedstawial osobnika wyswietlanego
        public static int canvasSizeX = 400; // rozmiar canvas X
        public static int canvasSizeY = 400; // rozmiar canvas Y
  
        public MainWindow()
        {
            InitializeComponent();
           
        }
        int popSize;
        populacja people;

        //Tworzenie populacji button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                popSize = Convert.ToInt32(PopSize.Text);
                people = new populacja(popSize);
                StatBarItem.Content = "Stworzono populacje";
                people.rysujPopulacje(canvas);
            }
            catch(FormatException)
            {
                StatBarItem.Content = "Błedny format wielkości populacji";
            };
        }

        //epoka
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.people.move();
            StatBarItem.Content = "Przesunieto";
            this.people.rysujPopulacje(canvas);
        }
    }
}
