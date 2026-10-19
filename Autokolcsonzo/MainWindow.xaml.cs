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

namespace Autokolcsonzo
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



        private bool Nev_Validate()
        {
            if (nev.Text.Length == 0)
            {
                MessageBox.Show("A név mező nemlehet üres!");
                return false;
            }
            else if (nev.Text.Length < 3)
            {
                MessageBox.Show("A név mezőnek legalább 3 karakter hosszúnak kell lennie!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool Eletkor_Validate()
        {
            try
            {
                int age = Convert.ToInt32(eletkor.Text);
                if (age < 18)
                {
                    MessageBox.Show("A kölcsönzéshez legalább 18 évesnek kell lenned!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (age > 100)
                {
                    MessageBox.Show("A kölcsönzéshez maximum 100 éves lehetsz!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
            catch (FormatException)
            {
                MessageBox.Show("Az életkorhoz számot adj meg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        private bool Jogositvany_Validate() 
        {
            if (jogositvanySzam.Text.Length == 0)
            {
                MessageBox.Show("A jogosítvány mező nem lehet üres!");
                return false;
            }
            else if (jogositvanySzam.Text.Length < 6)
            {
                MessageBox.Show("A jogosítvány mezőnek legalább 6 karakter hosszúnak kell lennie!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool Auto_Validate()
        {
            if (auto_valaszt.SelectedItem == null)
            {
                MessageBox.Show("Válassz ki egy autót!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool Berles_Validate() {
            try { int berles = Convert.ToInt32(kolcsonzesIdotartama.Text); 
                if (berles < 1)
                {
                    MessageBox.Show("A kölcsönzés időtartama minimum 1 nap kell hogy legyen!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (berles > 30)
                {
                    MessageBox.Show("A kölcsönzés időtartama maximum 30 nap lehet!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    return true;
                }

            }
            
            catch (FormatException)
            {
                MessageBox.Show("A kölcsönzés időtartamához számot adj meg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool biztositas_Validate()
        {
            if (radioButton1.IsChecked == false && radioButton2.IsChecked == false && radioButton3.IsChecked == false)
            {
                MessageBox.Show("A biztosítás kiválasztása kötelező!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool fizetes_Validate()
        {
            if (radioButton4.IsChecked == false && radioButton5.IsChecked == false)
            {
                MessageBox.Show("Válassz ki egy fizetési módot!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool elfogadas_Validate()
        {
            if (checkBox5.IsChecked == false)
            {
                MessageBox.Show("El kell fogadnod a feltételeket!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private int Osszes_Ar()
        {
            int ar = 0;
            switch (((ComboBoxItem)auto_valaszt.SelectedItem).Content.ToString())
            {
                case "Suzuki Swift – 12 000 Ft / nap":
                    ar += 12000 * (Convert.ToInt32(kolcsonzesIdotartama.Text));
                    break;
                case "Toyota Corolla – 18 000 Ft / nap":
                    ar += 18000 * (Convert.ToInt32(kolcsonzesIdotartama.Text));
                    break;
                case "BMW 3 – 30 000 Ft / nap":
                    ar += 30000 * (Convert.ToInt32(kolcsonzesIdotartama.Text));
                    break;
                case "Tesla Model 3 – 35 000 Ft / nap":
                    ar += 35000 * (Convert.ToInt32(kolcsonzesIdotartama.Text));
                    break;
            }

            if (radioButton2.IsChecked == true)
            {
                ar += 3000 * (Convert.ToInt32(kolcsonzesIdotartama.Text)); 
            }
            else if (radioButton3.IsChecked == true)
            {
                ar += 6000 * (Convert.ToInt32(kolcsonzesIdotartama.Text)); 
            }

            if (checkBox1.IsChecked == true)
            {
                ar += 1000 * (Convert.ToInt32(kolcsonzesIdotartama.Text));
            }
            if (checkBox2.IsChecked == true)
            {
                ar += 1500 * (Convert.ToInt32(kolcsonzesIdotartama.Text));
            }
            if (checkBox3.IsChecked == true)
            {
                ar += 2500 * (Convert.ToInt32(kolcsonzesIdotartama.Text));
            }
            if (checkBox4.IsChecked == true)
            {
                ar += 5000;
            }
            return ar;
        }

        private void Kolcsonzes_Click(object sender, RoutedEventArgs e)
        {
            if (Nev_Validate() && Eletkor_Validate() && Jogositvany_Validate() && Auto_Validate() && Berles_Validate() && biztositas_Validate() && fizetes_Validate() && elfogadas_Validate())
            {
                string kivalasztottAutoTeljes = ((ComboBoxItem)auto_valaszt.SelectedItem).Content.ToString();
                string autoNeve = kivalasztottAutoTeljes.Split('–')[0].Trim();

                string biztositasTípus = "";
                if (radioButton1.IsChecked == true) biztositasTípus = "Alap";
                else if (radioButton2.IsChecked == true) biztositasTípus = "Extra";
                else if (radioButton3.IsChecked == true) biztositasTípus = "Teljes";

                string extrakSzoveg = "";
                if (checkBox1.IsChecked == true) extrakSzoveg += "GPS, ";
                if (checkBox2.IsChecked == true) extrakSzoveg += "Gyerekülés, ";
                if (checkBox3.IsChecked == true) extrakSzoveg += "Extra sofőr, ";
                if (checkBox4.IsChecked == true) extrakSzoveg += "Autópálya matrica, ";

                if (extrakSzoveg.Length > 0)
                {
                    extrakSzoveg = extrakSzoveg.Substring(0, extrakSzoveg.Length - 2);
                }
                else
                {
                    extrakSzoveg = "Nincs";
                }

                string fizetesiMod = radioButton4.IsChecked == true ? "Készpénz" : "Bankkártya";

                string uzenet = "Sikeres autóbérlés!\n\n" +
                                $"Ügyfél: {nev.Text}\n" +
                                $"Autó: {autoNeve}\n" +
                                $"Időtartam: {kolcsonzesIdotartama.Text} nap\n" +
                                $"Biztosítás: {biztositasTípus}\n" +
                                $"Extrák: {extrakSzoveg}\n" +
                                $"Fizetés: {fizetesiMod}\n" +
                                $"Fizetendő: {Osszes_Ar()} Ft";

                MessageBox.Show(uzenet, "Sikeres foglalás", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}