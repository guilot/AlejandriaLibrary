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
            int hash = 17;
            hash ^= obj.GetHashCode();
            return (hash << 5) + hash;
        }
    }
}
