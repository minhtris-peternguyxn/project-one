using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjectOne
{
    public partial class Notes : Window
    {
        private List<Note> notes;

        public Notes()
        {
            InitializeComponent();
            notes = Note.LoadAllNotes();
            RefreshNotesList();
        }

        private void RefreshNotesList()
        {
            NotesListBox.ItemsSource = notes.Select(n => new { n.Title, CreatedDate = n.CreatedDate.ToString("g") }).ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NotesTextBox.Text) || string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter both title and content.");
                return;
            }

            // Kiểm tra nếu title đã tồn tại
            var existingNote = notes.FirstOrDefault(n => n.Title == TitleTextBox.Text);
            if (existingNote != null)
            {
                // Nếu có note với title này, update nội dung
                existingNote.Text = NotesTextBox.Text;
                existingNote.CreatedDate = DateTime.Now;//thêm time
                existingNote.SaveToFile();
            }
            else
            {
                // Nếu không có, tạo mới
                var newNote = new Note(TitleTextBox.Text, NotesTextBox.Text, DateTime.Now);
                notes.Add(newNote);
                newNote.SaveToFile();
            }

            TitleTextBox.Clear();
            NotesTextBox.Clear();
            RefreshNotesList();
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotesListBox.SelectedItem is not null)
            {
                var selectedTitle = ((dynamic)NotesListBox.SelectedItem).Title;
                var note = notes.FirstOrDefault(n => n.Title == selectedTitle);

                if (note != null)
                {
                    string filePath = System.IO.Path.Combine(Note.NotesDirectory, $"{note.Title}.txt");
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    notes.Remove(note);
                    RefreshNotesList();
                }
            }
        }



            private void NotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotesListBox.SelectedItem is not null)
            {
                var selectedTitle = ((dynamic)NotesListBox.SelectedItem).Title;
                var note = notes.FirstOrDefault(n => n.Title == selectedTitle);

                if (note != null)
                {
                    TitleTextBox.Text = note.Title;
                    NotesTextBox.Text = note.Text;
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TitleTextBox.Clear();
            NotesTextBox.Clear();
        }

        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Kiểm tra nội dung trong TextBox và ẩn hoặc hiển thị placeholder
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                PlaceholderText.Visibility = Visibility.Visible;
            }
            else
            {
                PlaceholderText.Visibility = Visibility.Collapsed;
            }
        }

    }
}
