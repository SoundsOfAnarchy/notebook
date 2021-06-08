using NotebookMVC.Controller;
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

namespace GuiView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public INotebookController Controller { get; set; }
        TimeSpan duration_for_search { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Controller = new NotebookController(@"your_file_path_to_bd");
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            TaskView tv = new TaskView(Controller);
            tv.ShowDialog();
        }

        private void dateTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dateTimePicker.SelectedDate != null)
            {
                listBox.ItemsSource = Controller.FindTasksByDate((DateTime)dateTimePicker.SelectedDate, TimeSpan.FromHours(24));                
            }
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem == null) MessageBox.Show("select task on listbox", "error");
            else
            {
                NotebookMVC.Model.Task task = (NotebookMVC.Model.Task)listBox.SelectedItem;
                ChangeTaskView tv = new ChangeTaskView(Controller, task.ID);
                tv.dateTimePicker1.SelectedDate = task.DateTime;
                tv.hourComboBox.SelectedIndex = task.DateTime.Hour;
                tv.minutesComboBox.SelectedIndex = task.DateTime.Minute;
                tv.durationTextBox.Text = task.Length.TotalMinutes.ToString();
                tv.completionComboBox.SelectedIndex = (int)task.Completion;
                tv.textTextBox.Text = task.TaskText;
                tv.ShowDialog();
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem == null) MessageBox.Show("select task on listbox", "error");
            else
            {
                NotebookMVC.Model.Task task = (NotebookMVC.Model.Task)listBox.SelectedItem;
                Controller.RemoveTask(task.ID);
            }
        }

        private void findTasksByDateButton_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void findCancelledTasksButton_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePicker.SelectedDate == null) MessageBox.Show("select date on dateTimePicker", "error");
            else
            {          
                TimeDurationWindow tdw = new TimeDurationWindow(this, Controller, NotebookMVC.Model.TaskCompletion.Canceled);
                tdw.ShowDialog();
            }
        }

        private void findTasksByTextButton_Click(object sender, RoutedEventArgs e)
        {
            TextSearchWindow tsw = new TextSearchWindow(this, Controller);
            tsw.ShowDialog();
        }
        private void findInProgressTasksButton_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePicker.SelectedDate == null) MessageBox.Show("select date on dateTimePicker", "error");
            else
            {
                TimeDurationWindow tdw = new TimeDurationWindow(this, Controller, NotebookMVC.Model.TaskCompletion.InProgress);
                tdw.ShowDialog();
            }
        }

        private void findCompleteTasksButton_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePicker.SelectedDate == null) MessageBox.Show("select date on dateTimePicker", "error");
            else
            {
                TimeDurationWindow tdw = new TimeDurationWindow(this, Controller, NotebookMVC.Model.TaskCompletion.Complete);
                tdw.ShowDialog();
            }
        }

        private void getAllTasksButton_Click(object sender, RoutedEventArgs e)
        {
            listBox.ItemsSource = Controller.GetAllTasks();
        }
    }
}
