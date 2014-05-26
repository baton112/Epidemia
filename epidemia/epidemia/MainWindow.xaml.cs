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

        //Tworzenie populacji button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try{
                popSize = Convert.ToInt32(PopSize.Text);
                people = new populacja(popSize);
                StatBarItem.Content = "Stworzono populacje";
            }
            catch(FormatException)
            {
                StatBarItem.Content = "Błedny format wielkości populacji";
            }
        }

    }
}
