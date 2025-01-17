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

            var imgSrc = new BitmapImage(new Uri("src/hajo.jpg", UriKind.Relative));
            IMG.Source = imgSrc;


            foreach (var h in hajo)
            {
                hajoNev.Items.Add(h.Nev);
            }

            var kisHajok = hajo.Where(h => h.MaxUtas < 6).Select(h => $"{h.Nev}, {h.NapiBerletiDij}").ToList();

            File.WriteAllLines(@"..\..\..\src\kisHajok.txt", kisHajok);
            
        }

        public double Ar(string napokSzama)
        {
            var napok = int.Parse(napokSzama);
            var valasztottHajo = hajoNev.SelectedItem.ToString();

            var hajoNapiBerlete = hajo.Where(h => h.Nev.Contains(valasztottHajo))
                                      .Select(h => h.NapiBerletiDij).FirstOrDefault();

            double osszesitetAr = hajoNapiBerlete * napok;

            return osszesitetAr;
        }

        public double OsszAr(string napokSzama)
        {
            var napok = int.Parse(napokSzama);

            var osszHajoBerlet = 0;
            foreach(var h in hajo)
            {
                osszHajoBerlet += h.NapiBerletiDij * napok;
            }

            return osszHajoBerlet;
        }

        private void napiKoltseg_Click(object sender, RoutedEventArgs e)
        {
            napiKoltsegLB.Content = string.Empty;
            napiKoltsegLB.Content = $"{napok.Text} napi költség: {Ar(napok.Text)} FT";
        }
        private void osszesKoltseg_Click(object sender, RoutedEventArgs e)
        {
            osszeKoltsegLB.Content = string.Empty;
            osszeKoltsegLB.Content += $"{napok.Text} napi költség: {OsszAr(napok.Text)} FT az összes hajóra.";
        }
        private void kereses_Click(object sender, RoutedEventArgs e)
        {
            ajanlottHajo.Content = string.Empty;
            var ajanlott = hajo.Where(a => a.MaxUtas >= int.Parse(utasok.Text))
                               .Select(a => $"Ajánlott hajó: {a.Nev} \n {a.MaxUtas} Fő befogadására képes \n Napidíja: {a.NapiBerletiDij} FT").FirstOrDefault();    

            if (ajanlott != null)
            {
                ajanlottHajo.Content = ajanlott;
            }
            else
            {
                MessageBox.Show("Sajnos nem tudunk hajót ajánlani!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                utasok.Focus();
            }
        }
    }
}