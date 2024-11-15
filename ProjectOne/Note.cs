using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectOne
{
    public class Note
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        public Note(string title, string text, DateTime createdDate)
        {
            Title = title;
            Text = text;
            CreatedDate = createdDate;
        }

        public static string NotesDirectory => Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Notes");


        public void SaveToFile()
        {
            Directory.CreateDirectory(NotesDirectory);
            string fileName = Path.Combine(NotesDirectory, $"{Title}.txt");

            string content = $"Title: {Title}\nCreated Date: {CreatedDate}\n\n{Text}";
            File.WriteAllText(fileName, content);

            Console.WriteLine($"File saved at: {Path.GetFullPath(fileName)}");
            MessageBox.Show($"File saved at: {Path.GetFullPath(fileName)}");

        }

        public static List<Note> LoadAllNotes()
        {
            var notes = new List<Note>();
            if (!Directory.Exists(NotesDirectory))
                return notes;

            var files = Directory.GetFiles(NotesDirectory, "*.txt");
            foreach (var file in files)
            {
                try
                {
                    string[] lines = File.ReadAllLines(file);
                    string title = lines.FirstOrDefault()?.Replace("Title: ", "").Trim() ?? "Untitled";
                    string createdDateStr = lines.Skip(1).FirstOrDefault()?.Replace("Created Date: ", "").Trim();
                    DateTime.TryParse(createdDateStr, out DateTime createdDate);

                    string text = string.Join("\n", lines.Skip(3));
                    notes.Add(new Note(title, text, createdDate));
                }
                catch
                {
                    // Ignore malformed files
                }
            }
            return notes.OrderByDescending(n => n.CreatedDate).ToList();
        }
    }
}
