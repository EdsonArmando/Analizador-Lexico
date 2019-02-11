using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1
{
    class Token
    {
        int no;
        int tokens;
        string lexema;
        string tipo;
        int fila;
        int columna;

        public int No { get => no; set => no = value; }
        public int Tokens { get => tokens; set => tokens = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int Fila { get => fila; set => fila = value; }
        public int Columna { get => columna; set => columna = value; }

        public Token() {
        }
        public Token(int no, int tokens,string lexema, string tipo,int fila,int columna) {
            this.no = no;
            this.tokens = tokens;
            this.lexema = lexema;
            this.tipo = tipo;
            this.fila = fila;
            this.columna = columna;
        }
    }
}
