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
        private List<CheckBox> feladatLista = new List<CheckBox>();
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
                feladatLista.Add(hozzáAdandó);
            }
            FeladatLista.ItemsSource = feladatLista; 
            FeladatLista.Items.Refresh();
        }
    }
}
