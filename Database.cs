using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace project2233
{
    public class Database
    {
        private string connectionString = "Server=dbsrv\\dub2024;Database=oshkinng207b2; Integrated Security=True;";
        

        public void AddBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Book (Article, Title, Genre, Description, IssueDate, ReturnDate, Status, Reader) " +
                               "VALUES (@Article, @Title, @Genre, @Description, @IssueDate, @ReturnDate, @Status, @Reader)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Article", book.Article);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@Description", book.Description);
                command.Parameters.AddWithValue("@IssueDate", book.IssueDate);
                command.Parameters.AddWithValue("@ReturnDate", book.ReturnDate);
                command.Parameters.AddWithValue("@Status", book.Status);
                command.Parameters.AddWithValue("@Reader", book.Reader);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Book";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book
                    {
                        Article = Convert.ToInt32(reader["Article"]),
                        Title = reader["Title"].ToString(),
                        Genre = reader["Genre"].ToString(),
                        Description = reader["Description"].ToString(),
                        IssueDate = (DateTime)reader["IssueDate"],
                        ReturnDate = (DateTime)reader["ReturnDate"],
                        Status = reader["Status"].ToString(),
                        Reader = reader["Reader"].ToString()
                    };
                    books.Add(book);
                }
            }

            return books;
        }

        public void UpdateBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Book SET Title = @Title, Genre = @Genre, Description = @Description, " +
                               "IssueDate = @IssueDate, ReturnDate = @ReturnDate, Status = @Status, Reader = @Reader " +
                               "WHERE Article = @Article"; 

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Article", book.Article);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@Description", book.Description);
                command.Parameters.AddWithValue("@IssueDate", book.IssueDate);
                command.Parameters.AddWithValue("@ReturnDate", book.ReturnDate);
                command.Parameters.AddWithValue("@Status", book.Status);
                command.Parameters.AddWithValue("@Reader", book.Reader);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteBook(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Book WHERE Article = @Article"; 

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Article", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
