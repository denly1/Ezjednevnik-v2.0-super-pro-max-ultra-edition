using System.Windows;

namespace DailyPlanner
{
    public partial class NoteWindow : Window
    {
        public Note Note { get; private set; }

        public NoteWindow()
        {
            InitializeComponent();
            Note = new Note();
        }

        public NoteWindow(Note note)
        {
            InitializeComponent();
            Note = note;
            titleTextBox.Text = note.Title;
            descriptionTextBox.Text = note.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Note.Title = titleTextBox.Text;
            Note.Description = descriptionTextBox.Text;
            DialogResult = true;
        }
    }
}