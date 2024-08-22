using bookDemo.Models;
namespace bookDemo.Data
{
    public static class ApplicationContex
    {
        public static List<Book> Books { get; set; }
        static ApplicationContex()
        {

            Books = new List<Book>()
            {
                new Book(){ Id = 1, Title = "Karagöz ve Hacivat", Price = 70 },
                new Book(){ Id = 2, Title = "Mesnevi", Price = 150 },
                new Book(){ Id = 3, Title = "Dede Korkut", Price = 70 }
            };
        }
    }
}
