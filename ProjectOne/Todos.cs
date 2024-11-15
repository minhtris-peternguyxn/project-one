using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.ComponentModel;

namespace ProjectOne
{
    public class Todos : INotifyPropertyChanged
    {
        private ObservableCollection<Todo> _allTodos;

        public Todos()
        {
            _allTodos = new ObservableCollection<Todo>();
            LoadTodosFromFile();  // Tải danh sách todos từ file khi khởi tạo
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Todo> AllTodos
        {
            get { return _allTodos; }
            set
            {
                if (_allTodos != value)
                {
                    _allTodos = value;
                    OnPropertyChanged(nameof(AllTodos)); // Notify when AllTodos is replaced.
                }
            }
        }

        public void Add(Todo todo)
        {
            _allTodos.Add(todo);
            OnPropertyChanged(nameof(AllTodos)); // Notify after addition
            SaveTodosToFile(_allTodos); // Lưu lại danh sách todos sau khi thêm
        }

        public void Remove(Todo todo)
        {
            _allTodos.Remove(todo);
            OnPropertyChanged(nameof(AllTodos)); // Notify after removal
            SaveTodosToFile(_allTodos); // Lưu lại danh sách todos sau khi xóa
        }

     
        public static void SaveTodosToFile(ObservableCollection<Todo> todos)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "todos.json");

          
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

       
            string jsonContent = JsonConvert.SerializeObject(todos, Formatting.Indented);

            // Lưu JSON vào file
            File.WriteAllText(filePath, jsonContent);
        }

    
        public void LoadTodosFromFile()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "todos.json");

            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                var todos = JsonConvert.DeserializeObject<List<Todo>>(jsonContent);

                if (todos != null)
                {
                    _allTodos = new ObservableCollection<Todo>(todos);
                    OnPropertyChanged(nameof(AllTodos)); 
                }
            }
        }
    }
}
