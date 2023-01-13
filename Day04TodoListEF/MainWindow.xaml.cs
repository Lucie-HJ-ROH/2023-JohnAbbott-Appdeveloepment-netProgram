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

namespace Day04TodoListEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DataFileName = @"..\..\todo.txt";
        List<Todo> todoList = new List<Todo>();

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new TodoDbContext(); // Exceptions
                LvToDos.ItemsSource = Globals.dbContext.Todos.ToList(); // equivalent of SELECT * FROM people
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

            string task = TaskInput.Text;
            int difficulty = (int)DifficultySlider.Value;
            DateTime dueDate = (DateTime)DueDatePicker.SelectedDate;
            Todo.StatusEnum status = (Todo.StatusEnum)StatusComboBox.SelectedIndex;

            Globals.dbContext.Todos.Add(new Todo(task, difficulty, dueDate, status));
            Globals.dbContext.SaveChanges();
            LvToDos.ItemsSource = Globals.dbContext.Todos.ToList();
            ResetFields();

        }

        private void ResetFields()
        {
            TaskInput.Text = "";

        }

        private void LvTodos_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Todo currSelTodo = LvToDos.SelectedItem as Todo;
            BtnDelete.IsEnabled = (currSelTodo != null);
            BtnUpdate.IsEnabled = (currSelTodo != null);


            if (currSelTodo != null)
            {
                TaskInput.Text = currSelTodo.Task;
                DifficultySlider.Value = currSelTodo.Difficulty;
                StatusComboBox.SelectedIndex = (int)currSelTodo.Status;

            }
        }

        private void SaveDataToFile() // call when window is closing
        {
            List<string> lines = new List<string>();
            foreach (Todo p in todoList)
            {
                lines.Add($"{p.Id};{p.Task};{p.Difficulty};{p.DueDate};{p.Status}");
            }
            try
            {
                File.WriteAllLines(DataFileName, lines);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error writing to file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadDataFromFile() // call when window is loaded
        {
            try
            {
                if (!File.Exists(DataFileName)) { return; }
                List<String> errorsList = new List<string>();
                string[] lineArray = File.ReadAllLines(DataFileName);
                for (int i = 0; i < lineArray.Length; i++)
                {
                    string line = lineArray[i];
                    var data = line.Split(';');
                    if (data.Length > 5)
                    {
                        errorsList.Add($"Each line must have exactly 5 fields (line {i + 1})" +
                                "\n" + line);
                        continue;
                    }
                    int.TryParse(data[0], out int id);
                    string task = data[1];
                    int.TryParse(data[2], out int difficuty);
                    DateTime dueDate = DateTime.Parse(data[3]);
                    Todo.StatusEnum status = (Todo.StatusEnum)Enum.Parse(typeof(Todo.StatusEnum), data[4]);

                    todoList.Add(new Todo(task, difficuty, dueDate, status));
                }

            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // FIXME: if saving fails the program should not exit, it should ask the user
            SaveDataToFile();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataFromFile();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Todo currSelTodo = LvToDos.SelectedItem as Todo;
            if (currSelTodo == null) { return; }
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?\n" + currSelTodo, "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) { return; }

            todoList.Remove(currSelTodo);
            LvToDos.Items.Refresh();
            LvToDos.SelectedIndex = -1;
            ResetFields();

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Todo currSelTodo = LvToDos.SelectedItem as Todo;
            if (currSelTodo == null) { return; }
            
            currSelTodo.Task = TaskInput.Text;
            currSelTodo.Difficulty = (int)DifficultySlider.Value;
            currSelTodo.DueDate = (DateTime)DueDatePicker.SelectedDate;
            currSelTodo.Status = (Todo.StatusEnum)StatusComboBox.SelectedIndex;
            LvToDos.Items.Refresh();
            LvToDos.SelectedIndex = -1;
            ResetFields();

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
