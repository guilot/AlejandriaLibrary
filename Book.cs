using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlejandriaLogic
{
    public class Book
    {
        // Atributos privados
        private string title;
        private Author author;
        private string genre = "none";
        private int year = -1;
        private int numPages = -1;
        private string duration = "none";
        private double score = -1.0;
        private int numRatings = 0;
        private string coverImage = "none";

        // Basic Constructor
        public Book(string title, Author author)
        {
            this.title = title;
            this.author = author;
        }

        // Medium Constructor
        public Book(string title, Author author, string genre, int year)
        {
            this.title = title;
            this.author = author;
            this.genre = genre;
            this.year = year;
        }

        // Large Constructor
        public Book(string title, Author author, string genre, int year, int numPages, string coverImage)
        {
            this.title = title;
            this.author = author;
            this.genre = genre;
            this.year = year;
            this.numPages = numPages;
            this.coverImage = coverImage;

            // Logic for duration
            duration = numPages < 140 ? "Short" : numPages <= 320 ? "Medium" :"Long";
        }

        // Getters y setters (propiedades)
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public Author Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public int NumPages
        {
            get { return numPages; }
            set 
            { 
                numPages = value;
                // Logic for duration
                duration = numPages < 140 ? "Short" : numPages <= 250 ? "Medium" : "Long";
            }
        }

        public string Duration
        {
            get { return duration; }
        }

        public double Score
        {
            get { return score; }
            set { score = value; }
        }

        public int NumRatings
        {
            get { return numRatings; }
            set { numRatings = value; }
        }

        public string CoverImage
        {
            get { return coverImage; }
            set { coverImage = value; }
        }

    }
}
