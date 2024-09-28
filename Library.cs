using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlejandriaLogic.DataStructures;

namespace AlejandriaLogic
{
    internal class Library
    {
        // Diccionario de libros en la biblioteca, con la clave generada a partir del título del libro
        private Dictionary<int, Book> bookMap;
        private Dictionary<int, User> userMap;
        private Dictionary<int, List<Book>> genreIndex;
        private Dictionary<int, List<Book>> authorIndex;
        private Dictionary<int, List<Book>> durationIndex;
        private readonly Utils utils;

        // Constructor
        public Library()
        {
            bookMap = new Dictionary<int, Book>();
            userMap = new Dictionary<int, User>();
            genreIndex = new Dictionary<int, List<Book>>();
            authorIndex = new Dictionary<int, List<Book>>();
            durationIndex = new Dictionary<int, List<Book>>();
            utils = new Utils();

        }

        // Método para agregar un usuario a la biblioteca
        public void AddUser(User user)
        {
            if (!userMap.ContainsKey(user.UserId))
            {
                userMap.Add(user.UserId, user);
                Console.WriteLine("usuario: " + user.UserName + " anadido correctamente al mapa de usuarios.");
            }
            else 
            {
                Console.WriteLine("El Usuario ya ha sido registrado en el mapa.");
            }
        }

        // Método para eliminar un usuario a la biblioteca
        public void RemoveUser(int userId) 
        {
            if (userMap.ContainsKey(userId))
            {
                User myUser = userMap[userId];
                if (myUser != null)
                {
                    // Eliminamos todos sus ratings ya que afectan a Score de los libros. 
                    foreach(MyRating rating in myUser.GetMyRatings()) 
                    {
                        myUser.DeletePuntuation(rating.GetBook());
                    }
                    
                }
                userMap.Remove(userId);
                Console.WriteLine("Usuario: eliminado correctamente de biblioteca.");
            }
            else
            {
                Console.WriteLine("El Usuario no existe en la biblioteca.");
            }
        }

        // Método para agregar un libro a la biblioteca
        public void AddBook(Book book)
        {
            int key = utils.GenerateKey(book.Title);
            if (!bookMap.ContainsKey(key))
            {
                bookMap.Add(key, book);
                UpdateGenreIndex(book);
                UpdateAuthorIndex(book);
                UpdateDurationIndex(book);

                Console.WriteLine("Libro: " + book.Title + " anadido correctamente a la biblioteca.");
            }
            else
            {
                // Por ejemplo, lanzando una excepción o ignorando el libro duplicado
                Console.WriteLine("El libro ya existe en la biblioteca.");
            }
        }

        // Método para eliminar un libro de la biblioteca
        public void RemoveBook(Book book)
        {
            int key = utils.GenerateKey(book.Title);
            if (bookMap.ContainsKey(key))
            {
                bookMap.Remove(key);
                RemoveFromAuthorIndex(book);
                RemoveFromGenreIndex(book);
                RemoveFromDurationIndex(book);

                Parallel.ForEach(userMap, kvp =>
                {
                    kvp.Value.DeleteLibroLeido(book);
                    kvp.Value.DeletePuntuation(book);
                });

                Console.WriteLine("Libro: " + book.Title + " eliminado correctamente de biblioteca.");
            }
            else
            {
                Console.WriteLine("El libro no existe en la biblioteca.");
            }
        }

        // Método para buscar un libro por título
        public Book FindBookByTitle(string title)
        {
            int key = utils.GenerateKey(title);

            if(bookMap.ContainsKey(key))
            {
                return bookMap[key];
            }
            else
            {
                Console.WriteLine("El libro buscado no existe en la biblioteca.");
                // Retorna null si no se encuentra el libro especificado
                return null;
            }
        }

        // Método para buscar un libro por autor
        public List<Book> FindBookByAuthor(Author author)
        {
            int key = utils.GenerateKey(author);
            if (authorIndex.ContainsKey(key))
            {
                return authorIndex[key];
            }
            else
            {
                Console.WriteLine("No existen libros del autor.");
                // Retorna null si no se encuentran libros para el autor especificado
                return null;
            }
        }

        public List<Book> FindBooksByGenre(string genreName)
        {
            int key = utils.GenerateKey(genreName);
            if (genreIndex.ContainsKey(key))
            {
                return genreIndex[key];
            }
            else
            {
                Console.WriteLine("No existen libros del genero deseado");
                // Retorna null si no se encuentran libros para el género especificado
                return null; 
            }
        }

        // Método para buscar un libro por duracion
        public List<Book> FindBookByDuration(string duration)
        {
            int key = utils.GenerateKey(duration);
            if (durationIndex.ContainsKey(key))
            {
                return durationIndex[key];
            }
            else
            {
                Console.WriteLine("No existen libros de duracion " + duration + ".");
                // Retorna null si no se encuentran libros para la duracion especificada
                return null;
            }
        }

        // Método para obtener todos los libros de la biblioteca
        public List<Book> GetAllBooks()
        {
            return new List<Book>(bookMap.Values);
        }

        // Metodo para anadir libro a mapa de Genero
        private void UpdateGenreIndex(Book book)
        {
            string genre = book.Genre;
            int key = utils.GenerateKey(genre);

            if (!genreIndex.ContainsKey(key))
            {
                genreIndex.Add(key, new List<Book>());
            }
            genreIndex[key].Add(book);
        }

        // Metodo para anadir libro a mapa de Autor
        private void UpdateAuthorIndex(Book book)
        {
            int key = utils.GenerateKey(book.Author);
            if (!authorIndex.ContainsKey(key))
            {
                authorIndex.Add(key, new List<Book>());
            }
            authorIndex[key].Add(book);
        }

        // Metodo para anadir libro a mapa de duracion
        private void UpdateDurationIndex(Book book)
        {
            int key = utils.GenerateKey(book.Duration);
            if (!durationIndex.ContainsKey(key))
            {
                durationIndex.Add(key, new List<Book>());
            }
            durationIndex[key].Add(book);
        }

        // Metodo para eliminar libro a mapa de Gener
        private void RemoveFromGenreIndex(Book book)
        {
            int key = utils.GenerateKey(book.Genre);
            if (genreIndex.ContainsKey(key))
            {
                genreIndex[key].Remove(book);
                if (genreIndex[key].Count == 0)
                {
                    genreIndex.Remove(key);
                }
            }
        }

        // Metodo para eliminar libro a mapa de Autor
        private void RemoveFromAuthorIndex(Book book)
        {
            int key = utils.GenerateKey(book.Author);
            if (authorIndex.ContainsKey(key))
            {
                authorIndex[key].Remove(book);
                if (authorIndex[key].Count == 0)
                {
                    authorIndex.Remove(key);
                }
            }
        }

        // Metodo para eliminar libro a mapa de duracion
        private void RemoveFromDurationIndex(Book book)
        {
            int key = utils.GenerateKey(book.Author);
            if (durationIndex.ContainsKey(key))
            {
                durationIndex[key].Remove(book);
                if (durationIndex[key].Count == 0)
                {
                    durationIndex.Remove(key);
                }
            }
        }

    }
}
