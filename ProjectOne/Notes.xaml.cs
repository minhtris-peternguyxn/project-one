using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace ProjectOne
{
    public partial class Notes : Window
    {
        private NotifyIcon trayIcon;
        private List<Note> notes;

        public Notes()
        {
            InitializeComponent();
            notes = Note.LoadFromFile("notes.json");
            InitializeTrayIcon();
        }

        private void InitializeTrayIcon()
        {
            trayIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon("Resources/notes.ico"),
                Visible = true,
                Text = "Notes App"
            };
            trayIcon.DoubleClick += TrayIcon_DoubleClick;
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Activate();
        }

        protected override void OnClosed(EventArgs e)
        {
            trayIcon.Visible = false;
            base.OnClosed(e);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            NotesTextBox.Clear();
        }
        private void CalendarControl_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateOnly selectedDate = DateOnly.FromDateTime(CalendarControl.SelectedDate ?? DateTime.Now);
            var selectedNotes = notes.Where(n => n.Date == selectedDate).ToList();

            NotesListBox.ItemsSource = selectedNotes;

            if (selectedNotes.Any())
            {
                NotesTextBox.Text = selectedNotes.First().Text;
                NotesListBox.SelectedItem = null;
            }
            else
            {
                NotesTextBox.Clear();
                NotesListBox.SelectedItem = null;
            }
        }

        private void RefreshCalendar()
        {
            DateOnly selectedDate = DateOnly.FromDateTime(CalendarControl.SelectedDate ?? DateTime.Now);
            var selectedNotes = notes.Where(n => n.Date == selectedDate).ToList();

            NotesListBox.ItemsSource = selectedNotes;

            if (selectedNotes.Any())
            {
                NotesTextBox.Text = selectedNotes.First().Text;
                NotesListBox.SelectedItem = null;
            }
            else
            {
                NotesTextBox.Clear();
                NotesListBox.SelectedItem = null;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NotesTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter a note.");
                return;
            }

            Note? selectedNote = NotesListBox.SelectedItem as Note;

            if (selectedNote != null)
            {
                selectedNote.Text = NotesTextBox.Text;

                NotesListBox.ItemsSource = notes.Where(n => n.Date == selectedNote.Date).ToList();

                NotesListBox.SelectedItem = selectedNote;

                SaveNotesToFile();
            }
            else
            {
                DateOnly selectedDate = DateOnly.FromDateTime(CalendarControl.SelectedDate ?? DateTime.Now);
                Note newNote = new Note(NotesTextBox.Text, selectedDate);

                notes.Add(newNote);

                NotesListBox.ItemsSource = notes.Where(n => n.Date == selectedDate).ToList();

                NotesListBox.SelectedItem = newNote;

                newNote.SaveToFile("notes.json");
            }

            NotesTextBox.Clear();

            RefreshCalendar();
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotesListBox.SelectedItem != null)
            {
                Note? selectedNote = NotesListBox.SelectedItem as Note;
                if (selectedNote != null)
                {
                    notes.Remove(selectedNote);

                    DateOnly selectedDate = DateOnly.FromDateTime(CalendarControl.SelectedDate ?? DateTime.Now);
                    NotesListBox.ItemsSource = notes.Where(n => n.Date == selectedDate).ToList();

                    SaveNotesToFile();
                }
            }

            RefreshCalendar();
        }

        private void SaveNotesToFile()
        {
            string json = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("notes.json", json);
        }

        private void NotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotesListBox.SelectedItem != null)
            {
                Note? selectedNote = NotesListBox.SelectedItem as Note;
                if (selectedNote != null)
                {
                    NotesTextBox.Text = selectedNote.Text;
                }
            }
        }
    }
}
