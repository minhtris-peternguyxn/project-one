using System;
using System.Linq;
using System.Windows;

namespace ProjectOne
{
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
          
            if (!string.IsNullOrEmpty(NewTodoTextBox.Text))
            {
                Todo todo = new Todo(
                    NewTodoTextBox.Text,  
                    false,                
                    DateTime.Now           
                );
                todos.Add(todo); 

               
                Todos.SaveTodosToFile(todos.AllTodos);

                NewTodoTextBox.Clear(); 
            }
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

                    Todos.SaveTodosToFile(todos.AllTodos);
                }
            }
        }

        private void SuggestTaskButton_Clicked(object sender, RoutedEventArgs e)
        {
          
            string suggestedTask = aiHelper.SuggestTask(todos.AllTodos.ToList());
            System.Windows.MessageBox.Show("Suggest Task: " + suggestedTask);
        }
    }
}
