using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1
{
    public partial class Form1 : Form
    {
        RichTextBox texto = new RichTextBox();
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        string path;
        int cont = 1;
        static ArrayList listToken = new ArrayList();
        static ArrayList errorToken = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        private void editorTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.idTexto.Visible = true;
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDe acer = new AcercaDe();
            acer.Show();
                 
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TextWriter txt = new StreamWriter("C:\\Users\\Armando\\Desktop\\Ejemplos\\demo.txt");
                txt.Write(idTexto.Text);
                txt.Close();
            }
        }
        public void crearHtml() {
            StreamWriter archivo = new StreamWriter("C:\\Users\\Armando\\Desktop\\Ejemplos\\ImagenUml.html");
            archivo.Write("<html>");
            archivo.Write("<head>");
            archivo.Write("</head>");
            archivo.Write("<body>");
            archivo.Write("<H1>Diagrama de clases</H1>");
            archivo.Write("<img src=\"C:\\Users\\Armando\\Desktop\\Ejemplos\\graf.png\"");
            archivo.Write("</body>");
            archivo.Write("</html>");
            archivo.Close();
            Console.WriteLine("Se ha generado el html");
            Process.Start(@"c:\Users\Armando\Desktop\Ejemplos\ImagenUml.html");
        }
        public void Analizador(string cadena){
            int estadoInical = 0;
            int mover = 0;
            char concatenar;
            string token="";
            for (estadoInical = 0; estadoInical < cadena.Length; estadoInical++){
                concatenar = cadena[estadoInical];
                switch (mover) {
                    case 0:
                        switch (concatenar) {
                            case ' ':
                            case '\r':
                            case '\t':
                            case '\n':
                            case '\f':
                                mover = 0;
                                break;
                            case 'D':
                                token += concatenar;
                                mover = 1;
                                break;
                            case 'C':
                                token += concatenar;
                                mover = 6;
                                break;
                            case '"':
                                token += concatenar;
                                mover = 12;
                                break;
                            case '[':
                                token += concatenar;
                                mover = 8;
                                estadoInical = estadoInical - 1;
                                break;
                            case ']':
                                token += concatenar;
                                mover = 9;
                                estadoInical = estadoInical - 1;
                                break;
                            case '*':
                                token += concatenar;
                                mover = 10;
                                estadoInical = estadoInical - 1;
                                break;
                            case 'N':
                                token += concatenar;
                                mover = 14;
                                break;
                            default:
                                if (Char.IsNumber(concatenar))
                                {
                                    token += concatenar;
                                    mover = 19;
                                    estadoInical = estadoInical - 1;
                                }
                                else {
                                    mover = 11;
                                    estadoInical = estadoInical - 1;
                                }
                               
                                break;
                        }
                        break;
                    case 1:
                        if (concatenar == 'D')
                        {
                            token += concatenar;
                            mover = 1;
                        }
                        else if (concatenar.Equals('I'))
                        {
                            token += concatenar;
                            mover = 1;
                        }
                        else if (concatenar.Equals('A'))
                        {
                            token += concatenar;
                            mover = 1;
                        }
                        else if (concatenar.Equals('G'))
                        {
                            token += concatenar;
                            mover = 1;
                        }
                        else if (concatenar.Equals('R'))
                        {
                            token += concatenar;
                            mover = 1;
                        }
                        else if (concatenar.Equals('A'))
                        {
                            token += concatenar;
                            mover = 1;
                        }
                        else if (concatenar.Equals('M'))
                        {
                            token += concatenar;
                            mover = 1;
                        }
                        else if (concatenar.Equals('A'))
                        {
                            token += concatenar;
                            mover = 1;
                        }
                        else if (concatenar == '_')
                        {
                            token += concatenar;
                            mover = 2;
                        }
                        else {
                            errores(token += concatenar);
                            token = "";
                            mover = 0;
                        }
                        break;
                    case 2:
                        if (concatenar == 'D')
                        {
                            token += concatenar;
                            mover = 2;
                        }
                        else if (concatenar.Equals('E'))
                        {
                            token += concatenar;
                            mover = 2;
                        }
                        else if (concatenar == '_')
                        {
                            token += concatenar;
                            mover = 3;
                        }
                        else
                        {
                            errores(token += concatenar);
                            token = "";
                            mover = 0;
                        }
                        break;
                    case 3:
                        if (concatenar == 'C')
                        {
                            token += concatenar;
                            mover = 3;
                        }
                        else if (concatenar.Equals('L'))
                        {
                            token += concatenar;
                            mover = 3;
                        }
                        else if (concatenar.Equals('A'))
                        {
                            token += concatenar;
                            mover = 3;
                        }
                        else if (concatenar.Equals('S'))
                        {
                            token += concatenar;
                            mover = 3;
                        }
                        else if (concatenar == 'E')
                        {
                            token += concatenar;
                            mover = 4;
                        }
                        else {
                            errores(token += concatenar);
                            token = "";
                            mover = 0;
                        }
                        break;
                    case 4:
                        if (concatenar == 'S')
                        {
                            token += concatenar;
                            mover = 5;
                            estadoInical = estadoInical - 1;
                        }
                        else {
                            errores(token += concatenar);
                            token = "";
                            mover = 0;
                        }
                        break;
                    case 5:
                        enviarToken(token, "RESERVADA");
                        token = "";
                        mover = 0;
                        break;
                    case 6:
                        if (concatenar == 'C') {
                            token += concatenar;
                            mover = 6;
                        }
                        else if (concatenar.Equals('L')) {
                            token += concatenar;
                            mover = 6;
                        }
                        else if (concatenar.Equals('A'))
                        {
                            token += concatenar;
                            mover = 6;
                        }
                        else if (concatenar.Equals('S'))
                        {
                            token += concatenar;
                            mover = 6;
                        }
                        else if (concatenar == 'O')
                        {
                            token += concatenar;
                            mover = 16;
                        }
                        else if (concatenar.Equals('E'))
                        {
                            token += concatenar;
                            mover = 7;
                            estadoInical = estadoInical - 1;
                        }
                        else
                        {
                            errores(token += concatenar);
                            token = "";
                            mover = 0;
                        }
                        break;
                    case 7:
                        enviarToken(token, "RESERVADA");
                        token = "";
                        mover = 0;
                        break;
                    case 8:
                        enviarToken(token, "SIMBOLO");
                        token = "";
                        mover = 0;
                        break;
                    case 9:
                        enviarToken(token, "SIMBOLO");
                        token = "";
                        mover = 0;
                        break;
                    case 10:
                        enviarToken(token, "SIMBOLO");
                        token = "";
                        mover = 0;
                        break;
                    case 11:
                        errores(token += concatenar);
                        token = "";
                        mover = 0;
                        break;
                    case 12:
                        if (Char.IsLetterOrDigit(concatenar)|| Char.IsSymbol(concatenar))
                        {
                            token += concatenar;
                        }
                        else if (concatenar == '"')
                        {
                            estadoInical = estadoInical - 1;
                            mover = 13;
                        }
                        else {
                            errores(token += concatenar);
                            token = "";
                            mover = 0;
                        }
                        break;
                    case 13:
                        enviarToken(token+ "\"", "IDENTIFICADOR");
                        token = "";
                        mover = 0;
                        break;
                    case 14:
                        if (concatenar=='N')
                        {
                            token += concatenar;
                            mover = 14;
                        }
                        else if (concatenar == 'O')
                        {
                            token += concatenar;
                            mover = 14;
                        }
                        else if (concatenar == 'M')
                        {
                            token += concatenar;
                            mover = 14;
                        }
                        else if (concatenar == 'B')
                        {
                            token += concatenar;
                            mover = 14;
                        }
                        else if (concatenar == 'R')
                        {
                            token += concatenar;
                            mover = 14;
                        }
                        else if (concatenar == 'E')
                        {
                            token += concatenar;
                            mover = 15;
                            estadoInical = estadoInical - 1;
                        }
                        else {
                            errores(token += concatenar);
                            token = "";
                            mover = 0;
                        }
                        break;
                    case 15:
                        enviarToken(token, "RESERVADA");
                        token = "";
                        mover = 0;
                        break;
                    case 16:
                        if (concatenar == 'D')
                        {
                            token += concatenar;
                            mover = 16;
                        }
                        else if (concatenar == 'I')
                        {
                            token += concatenar;
                            mover = 16;
                        }
                        else if (concatenar == 'G')
                        {
                            token += concatenar;
                            mover = 16;
                        }
                        else if (concatenar == 'O')
                        {
                            token += concatenar;
                            mover = 17;
                            estadoInical = estadoInical - 1;
                        }
                        else
                        {
                            errores(token += concatenar);
                            token = "";
                            mover = 0;
                        }
                        break;
                    case 17:
                        enviarToken(token, "RESERVADA");
                        token = "";
                        mover = 0;
                        break;
                    case 18:
                        if (Char.IsNumber(concatenar))
                        {
                            enviarToken(token, "DIGITO");
                            token = "";
                            mover = 0;
                        }
                        else {
                            errores(token += concatenar);
                            token = "";
                            mover = 0;
                        }                        
                        break;
                    case 19:
                        enviarToken(token, "DIGITO");
                        token = "";
                        mover = 0;
                        break;
                }
            }
        }

        public void enviarToken(string token,string tipo)
        {
            Console.WriteLine(token);
            if (tipo.Equals("RESERVADA"))
            {
                listToken.Add(new Token(cont, 1, token, "Reservada", 1, 1));
                cont++;
            }
            else if (tipo.Equals("SIMBOLO"))
            {
                listToken.Add(new Token(cont, 1, token, "SIMBOLO", 1, 1));
                cont++;
            }
            else if (tipo.Equals("IDENTIFICADOR"))
            {
                listToken.Add(new Token(cont, 1, token, "IDENTIFICADOR", 1, 1));
                cont++;
            }
            else if (tipo.Equals("DIGITO"))
            {
                listToken.Add(new Token(cont, 1, token, "DIGITO", 1, 1));
                cont++;
            }
            else {
                errores(token);
            }
            
          
        }
        public void errores(string token) {
            errorToken.Add(token);
        }
        public void generarTablaErrores() {
            int conts = 1;
            StreamWriter archivo = new StreamWriter("C:\\Users\\Armando\\Desktop\\Ejemplos\\tablaErrores.html");
            archivo.Write("<html>");
            archivo.Write("<head>");
            archivo.Write("<style>"
                    + "table{"
                    + "  font-family: arial, sans-serif; border-collapse: collapse;    width: 100%;}"
                    + "td, th{"
                    + "border: 1px solid #dddddd;text-align: left;  padding: 8px;}"
                    + "tr:nth-child(even){"
                    + " background-color: #dddddd;}"
                    + "</style>");
            archivo.Write("</head>");
            archivo.Write("<body>");
            archivo.Write("<H1>Tabla de Simbolos</H1>");
            archivo.Write("<br><br>");
            archivo.Write("<table>");
            archivo.Write("<tr><th>No</th><th>Error</th><th>Descripcion</th><th>fila</th><th>columna</th></tr>");
            foreach (string i in errorToken) {
                archivo.Write("<tr><td>" + conts + "</td><td>" + i + "</td><td>" + "Elemento Léxico Desconocido" + "</td><td>" + conts + "</td><td>" + conts + "</td></tr>" );
                conts++;
            }
            conts++;
            archivo.Write("</table>");
            archivo.Write("</body>");
            archivo.Write("</html>");
            archivo.Close();
            Process.Start(@"c:\Users\Armando\Desktop\Ejemplos\tablaErrores.html");
        }
        public void generarTablaSimbolos()
        {
            string nombre="";
            int conts = 0;
            StreamWriter archivo = new StreamWriter("C:\\Users\\Armando\\Desktop\\Ejemplos\\tablaSimbolos.html");
            archivo.Write("<html>");
            archivo.Write("<head>");
            archivo.Write("<style>"
                    + "table{"
                    + "  font-family: arial, sans-serif; border-collapse: collapse;    width: 100%;}"
                    + "td, th{"
                    + "border: 1px solid #dddddd;text-align: left;  padding: 8px;}"
                    + "tr:nth-child(even){"
                    + " background-color: #dddddd;}"
                    + "</style>");
            archivo.Write("</head>");
            archivo.Write("<body>");
            archivo.Write("<H1>Tabla de Simbolos</H1>");
            archivo.Write("<br><br>");
            archivo.Write("<table>");
            archivo.Write("<tr><th>No</th><th>Token</th><th>Lexema</th><th>Tipo</th><th>Fila</th><th>Columna</th></tr>");
            foreach (Token i in listToken)
            {
                conts += i.Tokens;
                archivo.Write("<tr><td>" + i.No + "</td><td>" + conts + "</td><td>" + i.Lexema + "</td><td>" + i.Tipo + "</td><td>" + i.Fila + "</td><td>" + i.Columna + "</td></tr>");
                nombre = i.Lexema;
            }
            archivo.Write("</table>");
            archivo.Write("</body>");
            archivo.Write("</html>");
            archivo.Close();
            Process.Start(@"c:\Users\Armando\Desktop\Ejemplos\tablaSimbolos.html");
        }

        public void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //crearHtml();
            string texto = idTexto.Text;
            Analizador(texto);
            generarTablaErrores();
            generarTablaSimbolos();
            pintarTexto();
        }
        public void pintarTexto() {
            int inicio = 0;
            string texto = idTexto.Text;
            idTexto.Text = "";
            idTexto.Text = texto;
            while (inicio< idTexto.Text.LastIndexOf("DIAGRAMA_DE_CLASES")) {
                idTexto.Find("DIAGRAMA_DE_CLASES", inicio, idTexto.TextLength, RichTextBoxFinds.None);
                idTexto.SelectionBackColor = Color.Blue;
                inicio = idTexto.Text.IndexOf("DIAGRAMA_DE_CLASES", inicio) + 1;
            }
           
        }
    }
}
