using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProjectOne
{
    public class Note
    {
        public string Text { get; set; }
        public DateOnly Date { get; set; }

        public Note(string text, DateOnly date)
        {
            Text = text;
            Date = date;
        }

        public void SaveToFile(string fileName)
        {
            var notes = LoadFromFile(fileName);
            notes.Add(this);

            string json = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, json);
        }

        public static List<Note> LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, "[]");
                return new List<Note>();
            }

            try
            {
                string json = File.ReadAllText(fileName);

                var notes = JsonSerializer.Deserialize<List<Note>>(json);
                return notes ?? new List<Note>();  
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new List<Note>();
            }
        }
    }
}
