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
        private Database dbHelper = new Database(); 

        public MainWindow()
        {
            InitializeComponent();
            LoadBooks(); 
        }

        private void LoadBooks()
        {
            books = dbHelper.GetBooks(); 
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

           
            if (!int.TryParse(ArticleTextBox.Text, out int articleId))
            {
                MessageBox.Show("Артикул должен быть числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

           
            Book newBook = new Book
            {
                Article = articleId, 
                Title = TitleTextBox.Text,
                Genre = GenreTextBox.Text,
                Description = DescriptionTextBox.Text,
                IssueDate = IssueDatePicker.SelectedDate ?? DateTime.Now,
                ReturnDate = ReturnDatePicker.SelectedDate ?? DateTime.Now,
                Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Reader = ReaderTextBox.Text
            };

            
            dbHelper.AddBook(newBook);
            LoadBooks(); 
            ClearInputs(); 
        }


        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook == null)
            {
                MessageBox.Show("Пожалуйста, выберите книгу для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedBook.Article = Convert.ToInt32(ArticleTextBox);
            selectedBook.Title = TitleTextBox.Text;
            selectedBook.Genre = GenreTextBox.Text;
            selectedBook.Description = DescriptionTextBox.Text;
            selectedBook.IssueDate = IssueDatePicker.SelectedDate ?? DateTime.Now;
            selectedBook.ReturnDate = ReturnDatePicker.SelectedDate ?? DateTime.Now;
            selectedBook.Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            selectedBook.Reader = ReaderTextBox.Text;

            
            dbHelper.UpdateBook(selectedBook); 
            LoadBooks(); 
            ClearInputs();
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook == null)
            {
                MessageBox.Show("Пожалуйста, выберите книгу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            dbHelper.DeleteBook(selectedBook.Article); 
            LoadBooks(); 
            ClearInputs();
        }

        private void BooksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBook = BooksDataGrid.SelectedItem as Book;
            if (selectedBook != null)
            {
               
                ArticleTextBox.Text = selectedBook.Article.ToString();
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
}
