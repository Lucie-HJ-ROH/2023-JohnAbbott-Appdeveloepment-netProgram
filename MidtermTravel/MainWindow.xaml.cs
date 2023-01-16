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
using static System.Net.Mime.MediaTypeNames;

namespace MidtermTravel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DataFileName = @"..\..\trip.txt";
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new TripDBContextNew();
                LvTrip.ItemsSource = Globals.dbContext.Trips.ToList();
             }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string destination = DestinationInput.Text;
            string name = NameInput.Text;
            string passport = PassportInput.Text;
            DateTime departureDate = DepartureDate.SelectedDate.Value;
            DateTime returnDate = ReturnDate.SelectedDate.Value;
            try
            {
                if (DepartureDate.SelectedDate == null)
                {
                    throw new ArgumentException("Please select a due date");
                }
                if (ReturnDate.SelectedDate == null)
                {
                    throw new ArgumentException("Please select a due date");
                }
                // public Trip(string destination, string name, string passport, DateTime departureDate, DateTime returnDate)
                Trip newTrip = new Trip(destination, name, passport, departureDate, returnDate);
                Globals.dbContext.Trips.Add(newTrip);
                Globals.dbContext.SaveChanges();

                LvTrip.ItemsSource = Globals.dbContext.Trips.ToList();
            }
            catch (ArgumentException ex) {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ResetFields();
        }
        private void ResetFields()
        {
            DestinationInput.Text = "";
            NameInput.Text = "";
            PassportInput.Text = "";
            DepartureDate.SelectedDate = DateTime.Today;
            ReturnDate.SelectedDate = DateTime.Today;

        }

        private void LvTrip_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trip currSelTrip = LvTrip.SelectedItem as Trip;

            int cnt = LvTrip.SelectedItems.Count;
            if (cnt > 1)
            {
                BtnDelete.IsEnabled = false;
                BtnUpdate.IsEnabled = false;
            }
            else {
                BtnDelete.IsEnabled = (currSelTrip != null);
                BtnUpdate.IsEnabled = (currSelTrip != null);
            }
            



            if(currSelTrip!= null)
            {
                DestinationInput.Text = currSelTrip.Destination;
                NameInput.Text = currSelTrip.TravlerName;
                PassportInput.Text = currSelTrip.TravlerPassport;
                DepartureDate.SelectedDate = currSelTrip.DepartureDate;
                ReturnDate.SelectedDate = currSelTrip.ReturnDate;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Trip currSelTrip = LvTrip.SelectedItem as Trip;
            if(currSelTrip== null) { return; }


            var result = MessageBox.Show(this, "Are you sure you want to delete this item?\n" + currSelTrip, "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) { return; }

            Globals.dbContext.Trips.Remove(currSelTrip);
            Globals.dbContext.SaveChanges();

            LvTrip.ItemsSource = Globals.dbContext.Trips.ToList();
            LvTrip.Items.Refresh();
            ResetFields();

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Trip currSelTrip = LvTrip.SelectedItem as Trip;
            if (currSelTrip == null) { return; }

            currSelTrip.Destination = DestinationInput.Text;
            currSelTrip.TravlerName = NameInput.Text;
            currSelTrip.TravlerPassport = PassportInput.Text;
            // currSelTrip.DepartureDate = (DateTime)DepartureDate.SelectedDate;
            //currSelTrip.ReturnDate = (DateTime)ReturnDate.SelectedDate;
            currSelTrip.SetDepartureReturnDates((DateTime)DepartureDate.SelectedDate, (DateTime)ReturnDate.SelectedDate);

            Globals.dbContext.SaveChanges();
            LvTrip.ItemsSource = Globals.dbContext.Trips.ToList();
            LvTrip.Items.Refresh();
            ResetFields();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            List<String> lines = new List<String>();
            int cnt = LvTrip.SelectedItems.Count;

            for (int i = 0; i < cnt; i++)
            {
                Trip currSelTrip = LvTrip.SelectedItems[i] as Trip;

                lines.Add($"{currSelTrip.Destination};{currSelTrip.TravlerName}{currSelTrip.TravlerPassport}{currSelTrip.DepartureDate}{currSelTrip.ReturnDate}");

            }
            try
            {
                File.WriteAllLines(DataFileName, lines);
                MessageBox.Show("Save Success!");
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error writing to file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
