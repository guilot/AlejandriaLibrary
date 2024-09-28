using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlejandriaLogic
{
    public class Author
    {
        // Atributos privados para el autor
        private string name;
        private string surname;
        private string nationality;

        // Constructor
        public Author(string name, string surname, string nationality)
        {
            this.name = name;
            this.surname = surname;
            this.nationality = nationality;
        }

        // Constructor
        public Author(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }


        // Getters y setters (propiedades) para el autor
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
    }
}
