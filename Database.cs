using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace project2233
{
    public class Database
    {
        private readonly DatabaseContext _context;

        public Database()
        {
            _context = new DatabaseContext();

            
            _context.Database.EnsureCreated();

            
            if (!_context.Books.Any())
            {
                SeedInitialData();
            }
        }

        private void SeedInitialData()
        {
            try
            {
                var initialBooks = new List<Book>
                {
                    new Book
                    {
                        Article = 1,
                        Title = "123",
                        Genre = "123",
                        Description = "123",
                        IssueDate = new DateTime(2023, 1, 1),
                        ReturnDate = new DateTime(2023, 1, 15),
                        Status = "Доступна",
                        Reader = "123"
                    },
                    new Book
                    {
                        Article = 2,
                        Title = "222",
                        Genre = "222",
                        Description = "222",
                        IssueDate = new DateTime(2023, 2, 1),
                        ReturnDate = new DateTime(2023, 2, 15),
                        Status = "Выдана",
                        Reader = "222"
                    }
                };

                _context.Books.AddRange(initialBooks);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Ошибка при инициализации данных: {ex.Message}");
            }
        }

        public void AddBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении книги: {ex.Message}");
                throw;
            }
        }

        public List<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public void UpdateBook(Book book)
        {
            try
            {
                var existingBook = _context.Books.Find(book.Id);
                if (existingBook != null)
                {
                    _context.Entry(existingBook).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении книги: {ex.Message}");
                throw;
            }
        }

        public void DeleteBook(int id)
        {
            try
            {
                var book = _context.Books.Find(id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении книги: {ex.Message}");
                throw;
            }
        }
    }
}