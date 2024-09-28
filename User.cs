using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AlejandriaLogic.DataStructures;

namespace AlejandriaLogic
{
    public class User
    {

        private int userId;
        private string username;
        private string password;
        private string email;

        private List<Book> librosLeidos;
        private Dictionary<int, MyRating> myRatings;
        private readonly Utils utils;

        public User(int userId, string username, string password, string email)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.email = email; 
            librosLeidos = new List<Book>();
            myRatings = new Dictionary<int, MyRating>();
            utils = new Utils();
        }
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public string UserName
        { 
            get { return username; } 
            set {  username = value; } 
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public List<Book> GetLibrosLeidos()
        {
            return librosLeidos;
        }

        public void AddLibroLeido(Book book)
        {
            librosLeidos.Add(book);
        }

        public void DeleteLibroLeido(Book book)
        {
            librosLeidos.Remove(book);
        }

        public List<MyRating> GetMyRatings()
        {
            return new List<MyRating>(myRatings.Values);
        }

        public void AddPuntuation(Book book, double score)
        {
            int key = utils.GenerateKey(book.Title);
            
            if(!myRatings.ContainsKey(key))
            {
                MyRating myRating = new MyRating(book, score);
                myRatings.Add(key, myRating);
                if (book.Score == -1.0)
                {
                    book.Score = score;
                    book.NumRatings = 1;
                }
                else
                {
                    // Recalculamos el promedio ponderado al añadir una nueva puntuación
                    double totalScore = book.Score * book.NumRatings;
                    totalScore += score;  // Sumamos la nueva puntuación
                    book.NumRatings++;    // Incrementamos el número de valoraciones
                    book.Score = totalScore / book.NumRatings;  // Promedio
                }

                Console.WriteLine("Tras añadir la puntuación, el libro '" + book.Title + "' tiene un Score: " + book.Score);
            }
            else
            {
                Console.WriteLine("El libro ya tiene una puntuación asignada por este usuario. Pasamos a modificarla.");
                UpdatePuntuation(book, score);
            }

        }
        public void UpdatePuntuation(Book book, double score)
        {
            int key = utils.GenerateKey(book.Title);
            MyRating myRating = myRatings[key];
            // Solo recalculamos si había más de una valoración
            if (book.NumRatings > 1)
            {
                double totalScore = book.Score * book.NumRatings;
                totalScore -= myRating.GetRating();  // Restamos la puntuación eliminada
                totalScore += score;
                book.Score = totalScore / book.NumRatings;  // Promedio recalculado
            }
            else
            {
                // Si era la única puntuación, reseteamos los valores
                book.Score = score;
            }
            // actualizamos mi lista de scores.
            myRating.UpdateRating(score);
            myRatings[key] = myRating;
        }

        public void DeletePuntuation(Book book)
        {
            int key = utils.GenerateKey(book.Title);
            if (myRatings.ContainsKey(key))
            {
                // Obtenemos el score que deseamos eliminar
                double removeScore = myRatings[key].GetRating();

                // Lo eliminamos de mi lista de ratings
                myRatings.Remove(key);

                // Solo recalculamos si había más de una valoración
                if (book.NumRatings > 1)
                {
                    double totalScore = book.Score * book.NumRatings;
                    totalScore -= removeScore;  // Restamos la puntuación eliminada
                    book.NumRatings--;          // Reducimos el número de valoraciones
                    book.Score = totalScore / book.NumRatings;  // Promedio recalculado
                }
                else
                {
                    // Si era la única puntuación, reseteamos los valores
                    book.Score = -1.0;
                    book.NumRatings = 0;
                }

                Console.WriteLine("Tras borrar la puntuación, el libro '" + book.Title + "' tiene un Score: " + book.Score);
            }
               
            else
                Console.WriteLine("El libro no esta puntuado.");
        }
    }
}
