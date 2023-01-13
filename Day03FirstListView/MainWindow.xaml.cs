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

namespace Day03FirstListView
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

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            string name = TbxName.Text;
            int.TryParse(TbxAge.Text, out int age); // FIXME: if ...
            LsvPeopleStrings.Items.Add($"{name} is {age} y/o");
            // clear the inputs
            TbxName.Text = "";
            TbxAge.Text = "";

            // List of Objects
            LsvPeopleObjects.Items.Add(new Person(name, age));
        }
    }
}
