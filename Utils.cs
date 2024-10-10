using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlejandriaLogic
{
    internal class Utils
    {
        // Método para generar una clave a partir del objeto dado
        public int GenerateKey<T>(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            // Caso específico para Author
            if (obj is Author author)
            {
                // Generamos un hash basado en el nombre del autor y, si existe, un ID único.
                int hash = 17;
                hash = hash * 31 + (author.Name?.GetHashCode() ?? 0);
                hash = hash * 31 + (author.Surname?.GetHashCode() ?? 0);
                return hash;
            }
            else if (obj is string str)
            {
                // Si es un string, usamos su hash directamente
                return str.GetHashCode();
            }

            // Para otros tipos, usamos su hash general
            return obj.GetHashCode();
        }

    }
}
