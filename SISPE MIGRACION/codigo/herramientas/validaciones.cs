using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas
{
    class validaciones
    {
        public static bool alfaNumericos(char caracter) {
            bool aux = false;
            if((caracter >= '0' && caracter <= '9') || (caracter >= 'A' && caracter <= 'Z') || (caracter >= 'a' && caracter <= 'z') || caracter == 8 || caracter == 32) {
                aux = true;
            }
            return aux;
        }

        public static bool numerico(char caracter) {
            bool aux = false;
            if ((caracter >= '0' && caracter <= '9') || caracter == '.' || caracter == 8 || caracter == 32)
            {
                aux = true;
            }
            return aux;
        }
    }
}
