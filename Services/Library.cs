using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Domain;

namespace LibraryManagementSystem.Services
{
    public class Library
    {
        private List<Book> books = new List<Book>();
        private List<User> users = new List<User>();
        private List<Loan> loans = new List<Loan>();

        public void AddBook(Book book) => books.Add(book);
        public void RemoveBook(Book book) => books.Remove(book);
        public void RegisterUser(User user) => users.Add(user);

        public void BorrowBook(Patron patron, Book book)
        {
            if (book.IsAvailable)
            {
                book.IsAvailable = false;
                Loan loan = new Loan(book, patron, DateTime.Now, DateTime.Now.AddDays(14));
                loans.Add(loan);
            }
            else
            {
                Console.WriteLine("Book is not available.");
            }
        }

        public void ReturnBook(Loan loan)
        {
            loan.Book.IsAvailable = true;
            loan.ReturnDate = DateTime.Now;
        }

        public List<Book> SearchBooks(string query)
        {
            return books.Where(b => b.Title.Contains(query) || b.Author.Contains(query) || b.ISBN.Contains(query)).ToList();
        }

        public List<Loan> GetLoansByPatron(Patron patron)
        {
            return loans.Where(l => l.Patron == patron && l.ReturnDate == null).ToList();
        }

        public User GetUserById(string id)
        {
            return users.FirstOrDefault(u => u.ID == id);
        }
    }
}
