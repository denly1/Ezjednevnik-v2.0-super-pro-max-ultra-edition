using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace DailyPlanner
{
    public partial class MainWindow : Window
    {
        private List<Note> notes;
        private string dataFilePath = "notes.json";

        public MainWindow()
        {
            InitializeComponent();

            datePicker.SelectedDate = DateTime.Today;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadNotes();
        }

        private void NotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (notesListBox.SelectedIndex != -1)
            {
                Note selectedNote = (Note)notesListBox.SelectedItem;
                MessageBox.Show($"Title: {selectedNote.Title}\nDescription: {selectedNote.Description}");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NoteWindow noteWindow = new NoteWindow();
            if (noteWindow.ShowDialog() == true)
            {
                var note = noteWindow.Note;
                note.Date = (DateTime)datePicker.SelectedDate;
                notes.Add(note);
                SaveNotes();
                LoadNotes();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (notesListBox.SelectedIndex != -1)
            {
                Note selectedNote = (Note)notesListBox.SelectedItem;
                NoteWindow noteWindow = new NoteWindow(selectedNote);
                if (noteWindow.ShowDialog() == true)
                {
                    SaveNotes();
                    LoadNotes();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (notesListBox.SelectedIndex != -1)
            {
                Note selectedNote = (Note)notesListBox.SelectedItem;
                notes.Remove(selectedNote);
                SaveNotes();
                LoadNotes();
            }
        }

        private void LoadNotes()
        {
            if (File.Exists(dataFilePath))
            {
                string jsonData = File.ReadAllText(dataFilePath);
                notes = JsonConvert.DeserializeObject<List<Note>>(jsonData).Where(n => n.Date == datePicker.SelectedDate).ToList();
            }
            else
            {
                notes = new List<Note>();
            }
            notesListBox.ItemsSource = notes;
        }

        private void SaveNotes()
        {
            string jsonData = JsonConvert.SerializeObject(notes);
            File.WriteAllText(dataFilePath, jsonData);
        }
    }
}