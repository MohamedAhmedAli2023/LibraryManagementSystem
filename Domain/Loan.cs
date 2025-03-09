namespace LibraryManagementSystem.Domain
{
    public class Loan
    {
        public Book Book { get; set; }
        public Patron Patron { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Loan(Book book, Patron patron, DateTime borrowDate, DateTime dueDate)
        {
            Book = book;
            Patron = patron;
            BorrowDate = borrowDate;
            DueDate = dueDate;
            ReturnDate = null;
        }
    }
}