using System;
using System.Collections.Generic;
using System.IO;
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

namespace Feladatnyilvántartó_FarkasLevente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CheckBox> hozzáAdottElemek = new List<CheckBox>();
        private List<CheckBox> töröltElemek = new List<CheckBox>();
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.Exit += new ExitEventHandler(Bezaras);
            Betoltes();
        }

        CheckBox lastItem = null;

        private void Hozzáadás(object sender, RoutedEventArgs e)
        {
            
            if(FeladatSzöveg != null)
            {
                CheckBox hozzáAdandó = new CheckBox();
                hozzáAdandó.IsChecked = false;
                hozzáAdandó.Content = FeladatSzöveg.Text;
                hozzáAdottElemek.Add(hozzáAdandó);
                hozzáAdandó.Checked += new RoutedEventHandler(Vizsgál);
                hozzáAdandó.Unchecked += new RoutedEventHandler(Vizsgál);
            }
            RefreshListBox(FeladatLista, hozzáAdottElemek);

        }


        public void Vizsgál(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            if (box.IsChecked == true)
            {
                box.FontStyle = FontStyles.Italic;
                box.Foreground = Brushes.Gray;
            }
            else
            {
                box.FontStyle = FontStyles.Normal;
                box.Foreground = Brushes.Black;
            }
           
        }

        private void FeladatTörlésGomb_Click(object sender, RoutedEventArgs e)
        {
            if (FeladatLista.SelectedItem == null) return;


            CheckBox törlendő = (CheckBox)FeladatLista.SelectedItem;
            töröltElemek.Add(törlendő);
            hozzáAdottElemek.Remove(törlendő);
            
            

            RefreshListBox(FeladatLista, hozzáAdottElemek);
            RefreshListBox(TöröltElemLista, töröltElemek);
        }
        

        private void RefreshListBox(ListBox listBox, List<CheckBox> elemek)
        {
            listBox.ItemsSource = elemek;
            listBox.Items.Refresh();
        }

        private void VisszaÁllít(object sender, RoutedEventArgs e)
        {
            if (TöröltElemLista.SelectedItem == null) return;


            CheckBox visszaállítandó = (CheckBox)TöröltElemLista.SelectedItem;
            hozzáAdottElemek.Add(visszaállítandó);
            töröltElemek.Remove(visszaállítandó);
            



            RefreshListBox(FeladatLista, hozzáAdottElemek);
            RefreshListBox(TöröltElemLista, töröltElemek);
        }

        private void VeglegesTorles(object sender, RoutedEventArgs e)
        {
            if (TöröltElemLista.SelectedItem == null) return;


            CheckBox törlendő = (CheckBox)TöröltElemLista.SelectedItem;
            töröltElemek.Remove(törlendő);

            RefreshListBox(FeladatLista, hozzáAdottElemek);
            RefreshListBox(TöröltElemLista, töröltElemek);

        }

        private void FeladatLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            CheckBox selectedItem = (CheckBox)FeladatLista.SelectedItem;
            if (selectedItem == null) return;
            lastItem = selectedItem;
            FeladatSzöveg.Text = selectedItem.Content.ToString();
            
        }

        private void Módosít(object sender, RoutedEventArgs e)
        {
           
            if (FeladatLista.SelectedItem == null) return;
            
           
            lastItem.Content = FeladatSzöveg.Text;
           
        }

        private void Bezaras(object sender, ExitEventArgs e)
        {
            string[] feladatok = new string[FeladatLista.Items.Count];
            string[] toroltFeladatok = new string[TöröltElemLista.Items.Count];

            for (int i = 0; i < feladatok.Length; i++)
            {
                CheckBox box = (CheckBox)FeladatLista.Items[i];

                feladatok[i] = box.Content.ToString() + ";" + box.IsChecked ;
            }

            File.WriteAllLines(@".\feladatok.txt", feladatok);


            for (int i = 0; i < toroltFeladatok.Length; i++)
            {
                CheckBox box = (CheckBox)TöröltElemLista.Items[i];

                toroltFeladatok[i] = box.Content.ToString() + ";" + box.IsChecked ;
            }

            File.WriteAllLines(@".\toroltFeladatok.txt", toroltFeladatok);
        }

        private void Betoltes()
        {
            if (!File.Exists("feladatok.txt")) return;
            string[] feladatok = File.ReadAllLines("feladatok.txt");
            List<CheckBox> checkBoxes = new List<CheckBox>();
            
            for (int i = 0; i < feladatok.Length; i++)
            {
                string[] sor = feladatok[i].Split(';');
                CheckBox boxToAdd = new CheckBox();
                boxToAdd.Content = sor[0];
                if(sor[1] == "True")
                {
                    boxToAdd.IsChecked = true;
                }
                boxToAdd.Checked += new RoutedEventHandler(Vizsgál);
                boxToAdd.Unchecked += new RoutedEventHandler(Vizsgál);
                if (boxToAdd.IsChecked == true)
                {
                    boxToAdd.FontStyle = FontStyles.Italic;
                    boxToAdd.Foreground = Brushes.Gray;
                }
                else
                {
                    boxToAdd.FontStyle = FontStyles.Normal;
                    boxToAdd.Foreground = Brushes.Black;
                }
                checkBoxes.Add(boxToAdd);
                hozzáAdottElemek.Add(boxToAdd);
            }
            FeladatLista.ItemsSource = checkBoxes;


            if (!File.Exists("toroltFeladatok.txt")) return;
            string[]    torolFeladatok = File.ReadAllLines("toroltFeladatok.txt");
            List<CheckBox> toroltCheckBoxes = new List<CheckBox>();

            for (int i = 0; i < torolFeladatok.Length; i++)
            {
                string[] sor = torolFeladatok[i].Split(';');
                CheckBox boxToAdd = new CheckBox();
                boxToAdd.Content = sor[0];
                if (sor[1] == "True")
                {
                    boxToAdd.IsChecked = true;
                }
                boxToAdd.Checked += new RoutedEventHandler(Vizsgál);
                boxToAdd.Unchecked += new RoutedEventHandler(Vizsgál);
                if (boxToAdd.IsChecked == true)
                {
                    boxToAdd.FontStyle = FontStyles.Italic;
                    boxToAdd.Foreground = Brushes.Gray;
                }
                else
                {
                    boxToAdd.FontStyle = FontStyles.Normal;
                    boxToAdd.Foreground = Brushes.Black;
                }

                toroltCheckBoxes.Add(boxToAdd);
                töröltElemek.Add(boxToAdd);
               
            }
            TöröltElemLista.ItemsSource = toroltCheckBoxes;

        }

       
    }
}
