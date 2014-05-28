﻿using System;
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
    /// popSiza - wielkosc populacji tworzonej na start 
    /// epochNumber --- okienko z ilosca epok do symulowania po nacisnieciu klawiasza 
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
        populacja people;

        //Tworzenie populacji button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int popSize;
                double chance;
                chance = Convert.ToDouble(infectChance.Text);
                popSize = Convert.ToInt32(PopSize.Text);
                people = new populacja(popSize, chance);
                StatBarItem.Content = "Stworzono populacje";
                people.rysujPopulacje(canvas);
                updatePopulationNumers();
            }
            catch(FormatException)
            {
                StatBarItem.Content = "Błedny format wielkości populacji lub szansa na zarazenie ";
            };
        }

        //przesuwa dzieci na kanvasie ---- jedna epoka 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.people.moveCanvasChilds(canvas);
            StatBarItem.Content = "Przesunieto";           
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
                    this.people.moveCanvasChilds(canvas);
                }
                StatBarItem.Content = "Zasymulowano "+ n.ToString() + " epok. ";
            }
            catch (FormatException)
            {
                StatBarItem.Content = "Błedny rozmiar symulacji epok";
            };
        }

        public void updatePopulationNumers()
        {
            currentState a = this.people.getPopulationState();
            aliveNumber.Content = a.alive.ToString();
            healthyNumber.Content = a.heathy.ToString();
            sickNumber.Content = a.sick.ToString();
            deathNumber.Content = a.sick.ToString();

        }
    }
}
