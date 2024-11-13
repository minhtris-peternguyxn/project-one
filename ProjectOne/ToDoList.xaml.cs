using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;  // Đây là WPF Button
// Không cần 'using System.Windows.Forms;' nếu không sử dụng WinForms'


namespace ProjectOne
{
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
            if (!string.IsNullOrWhiteSpace(NewTodoTextBox.Text))
            {
                Todo todo = new Todo()
                {
                    Desc = NewTodoTextBox.Text
                };
                todos.AllTodos.Add(todo);
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
                }
            }
        }

        private void PlayYouTubeButton_Clicked(object sender, RoutedEventArgs e)
        {
            string url = YouTubeUrlTextBox.Text;

            if (!string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                // Extract video ID from the URL if it is a full YouTube link
                var videoId = GetYouTubeVideoId(url);

                if (!string.IsNullOrEmpty(videoId))
                {
                    // Embed URL format for YouTube video
                    string embedUrl = $"https://www.youtube.com/embed/{videoId}?autoplay=1";

                    // Navigate the WebBrowser to the embed URL
                    YouTubePlayer.Visibility = Visibility.Visible;
                    YouTubePlayer.Navigate(embedUrl);
                }
                else
                {
                    // Chỉ rõ MessageBox từ WPF
                    System.Windows.MessageBox.Show("URL YouTube không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Chỉ rõ MessageBox từ WPF
                System.Windows.MessageBox.Show("Vui lòng nhập một URL hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Helper method to extract YouTube video ID from URL
        private string GetYouTubeVideoId(string url)
        {
            // Sample full URL format: https://www.youtube.com/watch?v=VIDEO_ID
            Uri uri = new Uri(url);
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            return query["v"];
        }
    }
}
