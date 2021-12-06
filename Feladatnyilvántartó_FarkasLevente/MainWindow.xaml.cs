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
        }

        

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
    }
}
