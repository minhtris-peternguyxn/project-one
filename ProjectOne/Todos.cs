using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ProjectOne
{
    public class Todos : INotifyPropertyChanged
    {
        public Todos()
        {
            _allTodos = new ObservableCollection<Todo>()
            {
                new Todo() { Desc = "an sang" },
                new Todo() { Desc = "hoc PRN thay hoang" }
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Todo> _allTodos;
        public ObservableCollection<Todo> AllTodos
        {
            get { return _allTodos; }
            set
            {
                _allTodos = value;
                OnPropertyChanged(nameof(AllTodos));
            }
        }

        // Method to add a new Todo
        public void Add(Todo todo)
        {
            _allTodos.Add(todo);
        }

        // Method to remove a Todo item
        public void Remove(Todo todo)
        {
            _allTodos.Remove(todo);
        }
    }
}
