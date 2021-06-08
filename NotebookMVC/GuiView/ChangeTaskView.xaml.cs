using NotebookMVC.Controller;
using NotebookMVC.Model;
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
using System.Windows.Shapes;

namespace GuiView
{
    /// <summary>
    /// Логика взаимодействия для ChangeTaskView.xaml
    /// </summary>
    public partial class ChangeTaskView : Window
    {
        public INotebookController Controller { get; set; }
        private int _id;
        
        public ChangeTaskView(INotebookController controller, int id)
        {
            InitializeComponent();

            Controller = controller;
            _id = id;

            for (int i = 0; i < 24; ++i)
                hourComboBox.Items.Add(i);

            for (int i = 0; i < 60; ++i)
                minutesComboBox.Items.Add(i);

            completionComboBox.ItemsSource = Enum.GetValues(typeof(TaskCompletion));
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePicker1.SelectedDate == null)
            {
                MessageBox.Show("Date isn't seleceted");
                return;
            }
            if (hourComboBox.SelectedItem == null)
            {
                MessageBox.Show("Hour isn't seleceted");
                return;
            }
            if (minutesComboBox.SelectedItem == null)
            {
                MessageBox.Show("Minutes isn't seleceted");
                return;
            }
            if (durationTextBox.Text.Length == 0)
            {
                MessageBox.Show("Duration isn't enter");
                return;
            }
            if (completionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Completion isn't selected");
                return;
            }
            if (textTextBox.Text.Length == 0)
            {
                MessageBox.Show("Text isn't enter");
                return;
            }

            DateTime date = (DateTime)dateTimePicker1.SelectedDate;
            date = date.AddHours(Convert.ToDouble(hourComboBox.SelectedItem));
            date = date.AddMinutes(Convert.ToDouble(minutesComboBox.SelectedItem));
            double duration = Convert.ToDouble(durationTextBox.Text);
            TaskCompletion completion = (TaskCompletion)completionComboBox.SelectedItem;
            string text = textTextBox.Text;
            Controller.ChangeTask(_id, date, TimeSpan.FromMinutes(duration), completion, text);
            Close();
        }
    }
}
