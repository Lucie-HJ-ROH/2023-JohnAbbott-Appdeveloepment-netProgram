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

namespace Day03ListGridViewPeople
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> peopleList = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();
            LvPeople.ItemsSource = peopleList;
        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            // if (!ArePersonInputsValid()) return;
            string name = TbxName.Text;
            int.TryParse(TbxAge.Text, out int age); // okay: no need to validate again
            peopleList.Add(new Person(name, age));
            LvPeople.Items.Refresh(); // tell ListView data has changed
            // ResetFields();
        }

        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
        }
        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void LoadDataFromFile() // call when window is loaded
        {
            // do your best with validation
            // data stored semicolon-separated, one per line:  Jerry;33

        }

        private void SaveDataToFile() // call when window is closing
        {
        }

    }
