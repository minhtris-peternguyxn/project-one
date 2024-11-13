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

namespace ProjectOne
{
    /// <summary>
    /// Interaction logic for ToDoList.xaml
    /// </summary>
    public partial class ToDoList : Window
    {
        private Todos todos;
        private AIHelper aiHelper;
        public ToDoList()
        {
            InitializeComponent();
            todos = new Todos();
            DataContext = todos;
            aiHelper = new AIHelper();

        }

        private void AddTodoButton_clicked(object sender, RoutedEventArgs e)
        {
            Todo todo = new Todo()
            {
                Desc = NewTodoTextBox.Text
            };
            todos.AllTodos.Add(todo);
        }

        private void DeleteTodoButton_Clicked(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var todo = button.CommandParameter as Todo;
                if (todo != null)
                {
                    todos.Remove(todo);
                }
            }
        }
        private void SuggestTaskButton_Clicked(object sender, RoutedEventArgs e)
        {
            // Sử dụng đối tượng aiHelper để gọi phương thức SuggestTask
            string suggestedTask = aiHelper.SuggestTask(todos.AllTodos.ToList());
            System.Windows.MessageBox.Show("Suggest Task: " + suggestedTask);
        }
    }
}
