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
    public partial class MainWindow : Window
    {
        public static int osobnikSize = 4; // wielkosc kwadratu ktory bedzie przedstawial osobnika wyswietlanego
        public static int populationGrigSize = 50;
        public static int canvasSizeX = osobnikSize * populationGrigSize; // rozmiar canvas X
        public static int canvasSizeY = osobnikSize * populationGrigSize; // rozmiar canvas Y
        public static int maxMeet = 2; 
  
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Symylacja epidemi - Grzegorz Sychowszki, Kacper Stamski";           
        }
        public populacja people;

        //Tworzenie populacji button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int popSize;
                double chance;
                chance = Convert.ToDouble(infectChance.Text);
                popSize = Convert.ToInt32(PopSize.Text);
                people = new populacja(popSize, chance, (bool)checkBox.IsChecked);
                StatBarItem.Content = "Stworzono populacje";
                //people.rysujPopulacje(canvas);
                people.newDisplay(canvas);
                updatePopulationNumers();
                people.changeMoveMethod((bool)checkBox.IsChecked);
                people.changeDirectionChance = Convert.ToDouble(changeDirectionChance.Text);
                people.infectChance = Convert.ToDouble(infectChance.Text);
                people.babyChance = Convert.ToDouble(babyChance.Text);  
            }
            catch(FormatException)
            {
                StatBarItem.Content = "Błedny format wielkości populacji lub szansa na zarazenie ";
            };
        }

        //przesuwa dzieci na kanvasie ---- jedna epoka 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.people.newMove();
            this.people.getSick();
            this.people.makeBabies(canvas);
            this.people.newDisplay(canvas);
            StatBarItem.Content = "Przesunieto";
            updatePopulationNumers();
        }

        // Symuluje kilka epok nadchodzacych po sobie 
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(epochNumber.Text);
                for (int i = 0; i < n; i++)
                {
                    this.people.newMove();
                    this.people.getSick();
                    this.people.makeBabies(canvas);
                    this.people.newDisplay(canvas);
                }
                StatBarItem.Content = "Zasymulowano "+ n.ToString() + " epok. ";
            }
            catch (FormatException)
            {
                StatBarItem.Content = "Błedny rozmiar symulacji epok";
            };
            updatePopulationNumers();
        }

        //Tworzy zarazoncych osobnikow i dodaje ich do populacji - klawisz
        private void Button_Click_AddSick(object sender, RoutedEventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(newSickTextBox.Text);
                this.people.infect(n);
                this.people.newDisplay(canvas);
                StatBarItem.Content = "Dodano " + n.ToString() + " chorych ";
            }
            catch (FormatException)
            {
                StatBarItem.Content = "Nie udalo sie dodac chorych (brak populacji, zly format liczby) ";
            };
            updatePopulationNumers();
        }

        public void updatePopulationNumers()
        {
            currentState a = this.people.getPopulationState();
            aliveNumber.Content = a.alive.ToString();
            healthyNumber.Content = a.heathy.ToString();
            sickNumber.Content = a.sick.ToString();
            deathNumber.Content = a.dead.ToString();
            this.currentEpochNumber.Content = this.people.currentyear.ToString();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            if (people != null) people.changeMoveMethod((bool)checkBox.IsChecked);
        }

        private void changeDirectionChance_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double chance;
                chance = Convert.ToDouble(changeDirectionChance.Text);
                StatBarItem.Content = "Zmienino szanse na zmiane kierunku";
                if(people != null) people.changeDirectionChance = chance;
            }
            catch (FormatException)
            {
                StatBarItem.Content = "Zły format szansy na zmiane kierunku ";
            };
        }

        private void infectChance_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double chance;
                chance = Convert.ToDouble(infectChance.Text);
                StatBarItem.Content = "Zmienino szanse na zarazenie,";
                if (people != null) people.infectChance = chance;
                System.Console.WriteLine("zarazenie" + chance);
            }
            catch (FormatException)
            {
                StatBarItem.Content = "Zły format szansy na zarazenie ";
            };
        }

        private void babyChance_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double chance;
                chance = Convert.ToDouble(babyChance.Text);
                StatBarItem.Content = "Zmienino szanse na rozmnozenie,";
                if (people != null) people.babyChance = chance;
                System.Console.WriteLine("rozmnozenie"+chance);
            }
            catch (FormatException)
            {
                StatBarItem.Content = "Zły format szansy na rozmnozenie ";
            };
        }

        private void maxMeet_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int max;
                max = Convert.ToInt32(maxMeetTextBox.Text);
                StatBarItem.Content = "Zmieniono maks spodkan";
                maxMeet = max;
            }
            catch (FormatException)
            {
                StatBarItem.Content = "Zły format max spodkan ";
            };

        }

    }
}
