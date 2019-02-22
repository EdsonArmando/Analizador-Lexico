using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1
{
    class ErrorToken
    {
        int no;
        string tokens;
        string lexema;
        int fila;
        int columna;

        public int No { get => no; set => no = value; }
        public string Tokens { get => tokens; set => tokens = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public int Fila { get => fila; set => fila = value; }
        public int Columna { get => columna; set => columna = value; }

        public ErrorToken()
        {
        }
        public ErrorToken(int no, string tokens, string lexema,  int fila, int columna)
        {
            this.no = no;
            this.tokens = tokens;
            this.lexema = lexema;
            this.fila = fila;
            this.columna = columna;
        }
    }
}

