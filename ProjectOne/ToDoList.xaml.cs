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
        public ToDoList()
        {
            InitializeComponent();
            todos = new Todos();
            DataContext = todos;
        }

        private void AddTodoButton_clicked(object sender, RoutedEventArgs e)
        {
            Todo todo = new Todo()
            {
                Desc = NewTodoTextBox.Text
            };
            todos.AllTodos.Add(todo);
        }
    }
}
