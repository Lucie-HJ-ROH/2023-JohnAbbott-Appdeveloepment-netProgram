using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        const string filePath = @"../../data.txt";

        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromFile();
            LvPeople.ItemsSource = peopleList;

        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            // if (!ArePersonInputsValid()) return;
            string name = TbxName.Text;
            string ageStr = TbxAge.Text;


            string strRegex = @"(^[0-9]{0,3}$)";
            Regex regex = new Regex(strRegex);


            string error = null;
            if (Person.IsNameValid(name, out error))
            {

                int.TryParse(TbxAge.Text, out int age); // okay: no need to validate again
                if (Person.IsAgeValid(age, out error) && regex.IsMatch(ageStr))
                {
                    peopleList.Add(new Person(name, age));
                    LvPeople.Items.Refresh(); // tell ListView data has changed
                                              // ResetFields();
                }
                else
                {
                    MessageBox.Show("Age must not be empty and only digit 0-150", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
            else
            {
                MessageBox.Show("Name must not be empty and 2-50 chars", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void TxtPersonReset()
        {
            TbxName.Clear();
            TbxAge.Clear();
        }


        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {

            peopleList.RemoveAt(LvPeople.SelectedIndex);
            LvPeople.SelectedItem = null;
            LvPeople.Items.Refresh();

        }
        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            string name = TbxName.Text;
            string ageStr = TbxAge.Text;

            string strRegex = @"(^[0-9]{0,3}$)";
            Regex regex = new Regex(strRegex);


            string error = null;
            if (Person.IsNameValid(name, out error))
            {

                int.TryParse(TbxAge.Text, out int age); // okay: no need to validate again
                if (Person.IsAgeValid(age, out error) && regex.IsMatch(ageStr))
                {
                    peopleList.ElementAt(LvPeople.SelectedIndex).Name = name;
                    peopleList.ElementAt(LvPeople.SelectedIndex).Age = age;
                    LvPeople.Items.Refresh(); // tell ListView data has changed
                                              // ResetFields();
                }
                else
                {
                    MessageBox.Show("Age must not be empty and only digit 0-150", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Name must not be empty and 2-50 chars", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

          

        }

        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LvPeople.SelectedItem != null)
            {
                TbxName.Text = peopleList.ElementAt(LvPeople.SelectedIndex).Name;
                TbxAge.Text = $"{peopleList.ElementAt(LvPeople.SelectedIndex).Age}";
            }
            
        }

        private void LoadDataFromFile() // call when window is loaded
        {
            // do your best with validation
            // data stored semicolon-separated, one per line:  Jerry;33
            string line;
            try{
                if (!File.Exists(filePath)) {
                    return;
                }    
                using(StreamReader reader = new StreamReader(filePath))
                {
                    line = reader.ReadLine();
                    if(line == null) { return; }
                    while (line != null)
                    {
                        try
                        {
                            string[] loadPerson = line.Split(';');
                            int.TryParse(loadPerson[1], out int age);
                            Person person = new Person(loadPerson[0], age);
                            peopleList.Add(person);
                            line = reader.ReadLine() ;
                                       
                        }catch(Exception ex) when (ex is ArgumentException || ex is FormatException)
                        {
                            MessageBox.Show(ex.Message, "Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }catch(Exception ex) when (ex is IOException)
            {
                MessageBox.Show(ex.Message, "IO Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void SaveDataToFile(string fileName) // call when window is closing
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath + fileName))
                {
                    peopleList.ForEach(person =>
                    {
                        writer.WriteLine($"{person.Name};{person.Age}");
                    });
                }
            }
            catch (Exception e) when (e is IOException || e is SystemException)
            {
                MessageBox.Show(this, "Error : Fail writing file", "Fail to save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MiExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Data File (*.data)|*.data|All File (*.*)|*.*|Text File (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    peopleList.ForEach(person =>
                    {
                        writer.WriteLine($"{person.Name};{person.Age}");
                    });
                }
            }
        }

        private void MiSortByAge_Click(object sender, RoutedEventArgs e)
        {
            peopleList = peopleList.OrderBy(person => person.Age).ToList();
            LvPeople.ItemsSource = peopleList;
            LvPeople.Items.Refresh();
            LblStatus.Text = "Sorted by Age";
        }

        private void MiSortByName_Click(object sender, RoutedEventArgs e)
        {
            peopleList = peopleList.OrderBy(person => person.Name).ToList();
            LvPeople.ItemsSource = peopleList;
            LvPeople.Items.Refresh();
            LblStatus.Text = "Sorted by Name";
        }

        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            SaveDataToFile("people.txt");
            Environment.Exit(0);
        }
    }
}
