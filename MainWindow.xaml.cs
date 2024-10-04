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


namespace WPF2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Orszag> adatok = new List<Orszag>();
        public MainWindow()
        {
            InitializeComponent();
            foreach (var i in File.ReadAllLines("orszag.txt").Skip(1))
            {
                
                Orszag atmenet = new Orszag(i);
                adatok.Add(atmenet);
            }
            dtg1.ItemsSource = adatok;
            SortedSet<string> orszagneve = new SortedSet<string>();
            foreach (var i in adatok)
            {
                orszagneve.Add(i.Orszagnev);
            }
            orszagneve.Add(" Minden látszik");
            cmb.ItemsSource = orszagneve;

        }

        private void btnFeladat1_Click(object sender, RoutedEventArgs e)
        {
            // Házi feladat lenne ezt megírni LINQ nélkül,
            // figyelembe véve a comboboxos megoldásnál alkalmazott módszert!!!
            // pont azt kell csinálni itt is!
            //kész,mostmár országnév részletre is lehet keresni kisbetűvel pl "magy"
            var a = adatok.Where(x=>x.Orszagnev.ToLower() == txtFeladat1.Text.ToLower()).ToList();
            List<Orszag> b = new List<Orszag>();
            foreach(var orszag in adatok)
            {
                if (orszag.Orszagnev.ToLower().Contains(txtFeladat1.Text.ToLower()))
                {
                    b.Add(orszag);
                }
            }
            // az a.Any() egy logikai értékkel tér vissza, igaz, ha van eleme az a-nak!
            if (b.Any())
            {
                dtg1.ItemsSource = b;
            }
            else
            {
                MessageBox.Show("Nem írtál be országot, vagy rossz országnevet írtál be!");
            }
        }

        private void btnFeladat2_Click(object sender, RoutedEventArgs e)
        {
            // a listák rendezése nagyon összetett dolog LINQ-t érdemes használni

            var ossz = adatok.OrderBy(x=>x.Orszagnev).ToList();
            dtg1.ItemsSource = ossz;
           
        }

        private void btnFeladat3_Click(object sender, RoutedEventArgs e)
        {
            // itt kimazsolázok mezőket egy új listához
            var of = adatok.Select(x => new { Ország = x.Orszagnev, Főváros = x.Fovaros.ToUpper() }).ToList();
            dtg1.ItemsSource = of;
        }

        private void btnFeladat4_Click(object sender, RoutedEventArgs e)
        {            
            // itt csökkenő sorrendbe rendezek, majd kihagyom az első találatot ( Skip(1) )
            // és utána 3 találatot jelenítek meg ( Take(3) )

            var z = adatok.OrderByDescending(x => x.Terulet)
               .Skip(1)
               .Take(3).ToList();
                
            dtg1.ItemsSource = z;
                     
            // Megnézzük, hogy a comboboxból mit választ ki a felhasználó
            string kivalasztott = cmb.SelectedItem.ToString();

            // Létrehozunk egy üres listát, ebbe tesszük majd bele
            // annak az egy kiválasztott országnak az objektumát
            // és majd a listát adjuk át a datagrid forrásának
            List<Orszag> ujlista = new List<Orszag>();
            // az index változóban tároljuk a kiválasztott ország listában elfoglalt helyét
            int index = 0;

            if (kivalasztott == " Minden látszik")
            {
                dtg1.ItemsSource = adatok;
            }
            else
            {
                for (int i = 0;i< adatok.Count;i++)
                {
                    if (kivalasztott == adatok[i].Orszagnev)
                    {
                        index = i;
                        break;
                    }
                }
                ujlista.Add(adatok[index]);
                dtg1.ItemsSource = ujlista;
                
                /*
                LINQ-s megoldás

                var a = adatok.Where(x => x.Orszagnev == kivalasztott).ToList();
                dtg1.ItemsSource = a;
                */
            }                                   

        }

        private void btnFeladat5_Click(object sender, RoutedEventArgs e)
        {
            // páratlan a főváros népessége és a főváros nevében van b betű
            var t = adatok.Where(x=>x.Fovnepessege % 2 == 1 && x.Fovaros.ToUpper().Contains('B')) ;
            dtg1.ItemsSource = t;

        }

        private void btnFeladat6_Click(object sender, RoutedEventArgs e)
        {
            // Hány ország területe kisebb a listában Magyarország területénél?
            var m = adatok.Where(x=>x.Orszagnev=="Magyarország").ToList();
            int terulet = m[0].Terulet;
            var kisebb = adatok.Count(x=>x.Terulet < terulet);
            MessageBox.Show(kisebb.ToString());
        }
    }
}