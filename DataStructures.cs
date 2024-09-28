using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlejandriaLogic
{
    public class DataStructures
    {
        public struct MyRating
        {
            Book Book;
            double Score;

            public MyRating(Book book, double score)
            {
                Book = book;
                Score = score;
            }

            public void UpdateRating(double score)
            {
                Score = score;
            }

            public double GetRating()
            {
              return Score;
            }

            public Book GetBook()
            {
                return Book;
            }
        }
    }
}
