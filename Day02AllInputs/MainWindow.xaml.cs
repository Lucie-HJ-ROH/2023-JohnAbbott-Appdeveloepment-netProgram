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

namespace Day02AllInputs
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

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string name = TbxName.Text;
            if (name == "")
            {
                MessageBox.Show(this, "Name must not be empty", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // 
            // var checkedButton = GridMain.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true && r.GroupName == "RbnAge");
            // FIXME: If checkedButton == null show error
            // Console.WriteLine("Checked rb: " + checkedButton.Content);
            // string age = checkedButton.Content;

            string age = "";
            if (RbnAgeBelow18.IsChecked == true)
            {
                age = "Below 18";
            }
            else if (RbnAge18to35.IsChecked == true)
            {
                age = "18 to 35";
            }
            else if (RbnAge36Plus.IsChecked == true)
            {
                age = "36 or above";
            }
            else
            { // internal error
                MessageBox.Show(this, "Error reading radio buttons state", "Internal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //
            // List<CheckBox> CheckBoxes = new List<CheckBox> { CbxPetsCats, CbxPetsDogs, CbxPetsOther };
            //string pets = string.Join(",", CheckBoxes.Where(Ckb => Ckb.IsChecked == true).Select(cb => cb.Content));
            List<string> petsList = new List<string>();
            if (CbxPetsCats.IsChecked == true)
            {
                petsList.Add("Cats");
            }
            if (CbxPetsDogs.IsChecked == true)
            {
                petsList.Add("Dogs");
            }
            if (CbxPetsOther.IsChecked == true)
            {
                petsList.Add("Other");
            }
            string pets = string.Join<string>(",", petsList);
            //
            /* string continent = ComboContinent.Text;
            Console.WriteLine("Continent: " + continent);
            if (ComboContinent.SelectedIndex == 0)
            {
                MessageBox.Show(this, "You must select a continent", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } */
            string continent = ComboContinent.SelectedValue?.ToString(); // conditional call
            if (continent == null)
            {
                MessageBox.Show(this, "Please select a continent", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //
            double tempC = SliderTempC.Value;
            //
            string line = $"{name};{age};{pets};{continent};{tempC}";
            File.AppendAllText(@"..\..\output.txt", line + "\n");
            MessageBox.Show(this, "Data appended to file", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            // TODO: reset all inputs after successfully writing data to file
        }

    }
}
