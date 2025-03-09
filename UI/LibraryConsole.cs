using System;
using LibraryManagementSystem.Domain;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.UI
{
    public class LibraryConsole
    {
        private Library library;

        public LibraryConsole(Library library)
        {
            this.library = library;
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("Are you a librarian or a patron? (L/P) Or type 'exit' to quit:");
                string? input = Console.ReadLine().ToUpper();
                if (input == "L") LibrarianMenu();
                else if (input == "P") PatronMenu();
                else if (input == "EXIT") break;
                else Console.WriteLine("Invalid choice.");
            }
        }

        private void LibrarianMenu()
        {
            Console.WriteLine("Enter your ID:");
            string? id = Console.ReadLine();
            User user = library.GetUserById(id);
            if (user is Librarian)
            {
                while (true)
                {
                    Console.WriteLine("\nLibrarian Menu:");
                    Console.WriteLine("1. Add Book");
                    Console.WriteLine("2. Remove Book");
                    Console.WriteLine("3. List All Books");
                    Console.WriteLine("4. Back");
                    string? choice = Console.ReadLine();
                    if (choice == "1") AddBook();
                    else if (choice == "2") RemoveBook();
                    else if (choice == "3") ListBooks();
                    else if (choice == "4") break;
                    else Console.WriteLine("Invalid choice.");
                }
            }
            else
            {
                Console.WriteLine("Librarian not found.");
            }
        }

        private void PatronMenu()
        {
            Console.WriteLine("Enter your ID:");
            string? id = Console.ReadLine();
            User user = library.GetUserById(id);
            if (user is Patron patron)
            {
                while (true)
                {
                    Console.WriteLine("\nPatron Menu:");
                    Console.WriteLine("1. Borrow Book");
                    Console.WriteLine("2. Return Book");
                    Console.WriteLine("3. List My Borrowed Books");
                    Console.WriteLine("4. Back");
                    string? choice = Console.ReadLine();
                    if (choice == "1") BorrowBook(patron);
                    else if (choice == "2") ReturnBook(patron);
                    else if (choice == "3") ListBorrowedBooks(patron);
                    else if (choice == "4") break;
                    else Console.WriteLine("Invalid choice.");
                }
            }
            else
            {
                Console.WriteLine("Patron not found.");
            }
        }

        private void AddBook()
        {
            Console.WriteLine("Enter book title:");
            string? title = Console.ReadLine();
            Console.WriteLine("Enter author:");
            string? author = Console.ReadLine();
            Console.WriteLine("Enter ISBN:");
            string? isbn = Console.ReadLine();
            Book book = new Book(title, author, isbn);
            library.AddBook(book);
            Console.WriteLine("Book added successfully.");
        }

        private void RemoveBook()
        {
            Console.WriteLine("Enter book ISBN to remove:");
            string? isbn = Console.ReadLine();
            var book = library.SearchBooks(isbn).FirstOrDefault();
            if (book != null)
            {
                library.RemoveBook(book);
                Console.WriteLine("Book removed successfully.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        private void ListBooks()
        {
            var books = library.SearchBooks("");
            if (books.Count > 0)
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN}, Available: {book.IsAvailable})");
                }
            }
            else
            {
                Console.WriteLine("No books in the library.");
            }
        }

        private void BorrowBook(Patron patron)
        {
            Console.WriteLine("Enter book ISBN to borrow:");
            string? isbn = Console.ReadLine();
            var book = library.SearchBooks(isbn).FirstOrDefault(b => b.IsAvailable);
            if (book != null)
            {
                library.BorrowBook(patron, book);
                Console.WriteLine("Book borrowed successfully.");
            }
            else
            {
                Console.WriteLine("Book not available or not found.");
            }
        }

        private void ReturnBook(Patron patron)
        {
            var loans = library.GetLoansByPatron(patron);
            if (loans.Count > 0)
            {
                Console.WriteLine("Enter ISBN of book to return:");
                string? isbn = Console.ReadLine();
                var loan = loans.FirstOrDefault(l => l.Book.ISBN == isbn);
                if (loan != null)
                {
                    library.ReturnBook(loan);
                    Console.WriteLine("Book returned successfully.");
                }
                else
                {
                    Console.WriteLine("Loan not found.");
                }
            }
            else
            {
                Console.WriteLine("You have no borrowed books.");
            }
        }

        private void ListBorrowedBooks(Patron patron)
        {
            var loans = library.GetLoansByPatron(patron);
            if (loans.Count > 0)
            {
                foreach (var loan in loans)
                {
                    Console.WriteLine($"{loan.Book.Title} (Due: {loan.DueDate.ToShortDateString()})");
                }
            }
            else
            {
                Console.WriteLine("No borrowed books.");
            }
        }
    }
}