using LibraryManagementSystem.Domain;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.UI;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            // initial users and books
            library.RegisterUser(new Librarian("Admin", "L001"));
            library.RegisterUser(new Patron("User1", "P001"));
            library.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", "1234567890"));
            library.AddBook(new Book("1984", "George Orwell", "0987654321"));
            LibraryConsole console = new LibraryConsole(library);
            console.Start();
        }
    }
}