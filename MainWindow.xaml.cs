using System;
using System.Collections.Generic;
using System.Linq; 
using System.Windows;
using System.Windows.Controls;

namespace project2233
{
    public partial class MainWindow : Window
    {
        private List<Book> books = new List<Book>(); 
        private Book selectedBook; 

        public MainWindow()
        {
            InitializeComponent();
            BooksDataGrid.ItemsSource = books; 
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(ArticleTextBox.Text) ||
                string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                string.IsNullOrWhiteSpace(GenreTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                StatusComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(ReaderTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            Book newBook = new Book
            {
                Article = ArticleTextBox.Text,
                Title = TitleTextBox.Text,
                Genre = GenreTextBox.Text,
                Description = DescriptionTextBox.Text,
                IssueDate = IssueDatePicker.SelectedDate ?? DateTime.Now,
                ReturnDate = ReturnDatePicker.SelectedDate ?? DateTime.Now,
                Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Reader = ReaderTextBox.Text
            };

            books.Add(newBook); 
            BooksDataGrid.Items.Refresh(); 
            ClearInputs(); 
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (selectedBook == null)
            {
                MessageBox.Show("Пожалуйста, выберите книгу для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedBook.Article = ArticleTextBox.Text;
            selectedBook.Title = TitleTextBox.Text;
            selectedBook.Genre = GenreTextBox.Text;
            selectedBook.Description = DescriptionTextBox.Text;
            selectedBook.IssueDate = IssueDatePicker.SelectedDate ?? DateTime.Now;
            selectedBook.ReturnDate = ReturnDatePicker.SelectedDate ?? DateTime.Now;
            selectedBook.Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            selectedBook.Reader = ReaderTextBox.Text;

            BooksDataGrid.Items.Refresh(); 
            ClearInputs(); 
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (selectedBook == null)
            {
                MessageBox.Show("Пожалуйста, выберите книгу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

           
            books.Remove(selectedBook);
            BooksDataGrid.Items.Refresh(); 
            ClearInputs(); 
        }

        private void BooksDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            selectedBook = BooksDataGrid.SelectedItem as Book;
            if (selectedBook != null)
            {
                ArticleTextBox.Text = selectedBook.Article;
                TitleTextBox.Text = selectedBook.Title;
                GenreTextBox.Text = selectedBook.Genre;
                DescriptionTextBox.Text = selectedBook.Description;
                IssueDatePicker.SelectedDate = selectedBook.IssueDate;
                ReturnDatePicker.SelectedDate = selectedBook.ReturnDate;
                StatusComboBox.SelectedItem = StatusComboBox.Items.OfType<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == selectedBook.Status);
                ReaderTextBox.Text = selectedBook.Reader;
            }
        }

        private void ClearInputs()
        {
       
            ArticleTextBox.Clear();
            TitleTextBox.Clear();
            GenreTextBox.Clear();
            DescriptionTextBox.Clear();
            IssueDatePicker.SelectedDate = null; 
            ReturnDatePicker.SelectedDate = null;
            StatusComboBox.SelectedItem = null; 
            ReaderTextBox.Clear();
            selectedBook = null;
        }
    }

    
    public class Book
    {
        public string Article { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }
        public string Reader { get; set; }
    }
}

