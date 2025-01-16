using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace SZGYA13C_Setahajok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Hajo> hajo = new List<Hajo>();
        public MainWindow()
        {
            InitializeComponent();

            hajo = Hajo.Fromfile(@"..\..\..\src\hajo.txt");

            foreach(var h in hajo)
            {
                hajoNev.Items.Add(h.Nev);
            }

            var kisHajok = hajo.Where(h => h.MaxUtas < 6).ToList();

            File.WriteAllLines(@"..\..\..\src\kisHajok.txt", $"{kisHajok}");
            
        }

        public double Ar(string napokSzama)
        {
            var napok = int.Parse(napokSzama);
            var valasztottHajo = hajoNev.SelectedItem.ToString();

            var hajoNapiBerlete = hajo.Where(h => h.Nev.Contains(valasztottHajo)).Select(h => h.NapiBerletiDij).FirstOrDefault();

            double osszesitetAr = hajoNapiBerlete * napok;

            return osszesitetAr;
        }

        private void napiKoltseg_Click(object sender, RoutedEventArgs e)
        {
            napiKoltsegLB.Content = string.Empty;
            napiKoltsegLB.Content = $"{napok.Text} napi költség: {Ar(napok.Text)} FT";
        }
    }
}