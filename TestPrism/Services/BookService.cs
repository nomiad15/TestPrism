using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestPrism.Entities;

namespace TestPrism.Services
{
    public interface IBookService
    {
        #region # Task<List<Book>>
        Task<List<Book>> GetBooks();
        //List<Book> GetBooks();
        #endregion
    }

    public class BookService : IBookService
    {
        //public Realm _realm => Realm.GetInstance();

        #region # Task<List<Book>>
        public async Task<List<Book>> GetBooks()
        //public List<Book> GetBooks()
        {
            //var ListFr = _realm.All<Friend>().ToList();

            //return Get();

            var name = typeof(BookService).AssemblyQualifiedName.Split(',')[1].Trim();
            var assembly = Assembly.Load(new AssemblyName(name));
            var stream = assembly.GetManifestResourceStream(name + ".Data.Booklist1.json");

            using (StreamReader r = new StreamReader(stream))
            {
                string json = await r.ReadToEndAsync();
                //string json = r.ReadToEnd();
                JObject obj = JObject.Parse(json);
                JArray items = (JArray)obj["Books"];

                List<Book> books = items.ToObject<List<Book>>();
                return books;
            }
        }
        #endregion

        #region # Get()
        private List<Book> Get()
        {
            List<Book> resultList = new List<Book>()
            {
                new Book
                {
                    Id = "13",
                    Title = "Knockemstiff",
                    Author = "Pollock",
                    FirstName = "Donald R.",
                    Year = 2008,
                    Read = true
                },
                new Book
                {
                    Id = "14",
                    Title = "Fear of flying",
                    Author = "Jong",
                    FirstName = "Erica",
                    Year = 2003,
                    Read = true
                }
            };

            return resultList;
        }
        #endregion

    }
}
