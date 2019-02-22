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
        string textDiagr;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        string path;
        int cont = 1;
        int conAnali = 0;
        int error = 1;
        string nombre2="";
        static ArrayList listToken = new ArrayList();
        static ArrayList errorToken = new ArrayList();
        string []tokensReservadas = { "DIAGRMA_DE_CLASES","NOMBRE","CLASE","CODIGO", "ATRIBUTOS","ATRIBUTO","VISIBILIDAD","TIPO","METODOS","METODO","RELACIONES","RELACION",
        "ENLACE"};
        StreamWriter dotArchivo;
        string[] Simbolos = { "[","]","*"};
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

        public void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string texto = idTexto.Text;
            Analizador(texto);
            generarTablaErrores();
            generarTablaSimbolos();
            //pintarTexto();
            //generarArchivo(texto);
            //crearHtml();
            conAnali++;
            listToken.Clear();
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
            archivo.Write("<H1>Diagrama de clases: "+ nombre2 + " </H1>");
            archivo.Write("<img src=\"C:\\Users\\Armando\\source\\repos\\Practica1\\Practica1\\bin\\Debug\\diagrama.png\"");
            archivo.Write("</body>");
            archivo.Write("</html>");
            archivo.Close();
            Process.Start(@"c:\Users\Armando\Desktop\Ejemplos\ImagenUml.html");
        }
        public void Analizador(string cadena){
            int estadoInical = 0;
            int mover = 0;
            int fila = 1;
            int columna = 1;
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
                            case '\f':
                                mover = 0;
                                break;
                            case '\n':
                                fila++;
                                columna = 1;
                                mover = 0;
                                break;
                            case '"':
                                token += concatenar;
                                mover = 1;
                                break;
                            case '[':
                                token += concatenar;
                                mover = 2;
                                estadoInical = estadoInical - 1;
                                break;
                            case ']':
                                token += concatenar;
                                mover = 3;
                                estadoInical = estadoInical - 1;
                                break;
                            case '*':
                                token += concatenar;
                                mover = 4;
                                estadoInical = estadoInical - 1;
                                break;
                            default:
                                if (Char.IsNumber(concatenar))
                                {
                                    token += concatenar;
                                    mover = 5;
                                    estadoInical = estadoInical - 1;
                                }
                                else {
                                    mover = 6;
                                    estadoInical = estadoInical - 1;
                                }
                                break;
                        }
                        break;
                    case 1:
                        if (Char.IsLetterOrDigit(concatenar) || Char.IsSymbol(concatenar))
                        {
                            token += concatenar;
                        }
                        else if (concatenar == '"')
                        {
                            estadoInical = estadoInical - 1;
                            mover = 7;
                        }
                        else
                        {
                            errores(token += concatenar,fila,columna);
                            columna++;
                            token = "";
                            mover = 0;
                        }
                        break;
                    case 2:
                        enviarToken(token, "SIMBOLO",fila,columna);
                        columna++;
                        token = "";
                        mover = 0;
                        break;
                    case 3:
                        enviarToken(token, "SIMBOLO",fila, columna);
                        columna++;
                        token = "";
                        mover = 0;
                        break;
                    case 4:
                        enviarToken(token, "SIMBOLO",fila, columna);
                        columna++;
                        token = "";
                        mover = 0;
                        break;
                    case 6:
                        errores(token += concatenar, fila, columna);
                        columna++;
                        token = "";
                        mover = 0;
                        break;
                    case 5:
                        if (Char.IsNumber(concatenar))
                        {
                            enviarToken(token, "DIGITO",fila, columna);
                            columna++;
                            token = "";
                            mover = 0;
                        }
                        else
                        {
                            errores(token += concatenar, fila, columna);
                            columna++;
                            token = "";
                            mover = 0;
                        }
                        break;
                    case 7:
                        enviarToken(token+ "\"", "IDENTIFICADOR",fila, columna);
                        columna++;
                        token = "";
                        mover = 0;
                        break;
                   
                }
            }
        }

        public void enviarToken(string token,string tipo,int fila,int columna)
        {
            //Console.WriteLine(token);
            if (tipo.Equals("RESERVADA"))
            {
                listToken.Add(new Token(cont, 1, token, "Reservada", fila, columna));
                cont++;
            }
            else if (tipo.Equals("SIMBOLO"))
            {
                listToken.Add(new Token(cont, 1, token, "SIMBOLO", fila, columna));
                cont++;
            }
            else if (tipo.Equals("IDENTIFICADOR"))
            {
                listToken.Add(new Token(cont, 1, token, "IDENTIFICADOR", fila, columna));
                cont++;
            }
            else if (tipo.Equals("DIGITO"))
            {
                listToken.Add(new Token(cont, 1, token, "DIGITO", fila, columna));
                cont++;
            }
            else {
                //errores(token);
            }
            
          
        }
        public void errores(string token,int fila,int columna) {
            errorToken.Add(new ErrorToken(error,token,"Error lexico desconocido",fila,columna));
        }
        public void generarArchivo(string cadena) {
            nombre2 = "";
            dotArchivo = new StreamWriter(@"C:\Users\Armando\source\repos\Practica1\Practica1\bin\Debug\diagrama.dot");
            int cont5 = -1;
            int pos = 0;
            int pos2=0,contMetodo=0;
            int tipo3 = 0,nom=0,tipo2=0;
            string cadenas="";
            char variable;
            string codigo="", enlace = "";
            string nombre="", visibilidad = "",nombreAt="",tipo="";
            string tipoRela = "",relacion="";
            Boolean fin=false;
            string visMetodo = "", nombreMetodo = "", tipoMetodo = "", parametro="";
            textDiagr= "digraph D{\n";
            //pos2 = 62;
            for (pos=0;pos<cadena.Length;pos++) {
                variable = cadena[pos];
                cadenas += variable;
                if (cadenas.Equals("*CODIGO]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("*NOMBRE]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[DIAGRAMA_DE_CLASES]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("*ENLACE]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[ATRIBUTOS]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[RELACIONES]"))
                {
                    tipo2 =1;
                    cadenas = "";
                }
                else if (cadenas.Equals("[RELACION]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[*RELACION]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[*RELACIONES]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[CLASE]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[ATRIBUTO]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("*NOMBRE]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("*PARAMETROS]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[*ATRIBUTO]"))
                {

                    cadenas = "";
                    //textDiagr = textDiagr + visibilidad+" ";  // = textDiagr + nombreAt + ": "; //textDiagr = textDiagr + tipo + "\\n" + "\n";
                    textDiagr = textDiagr + visibilidad + " " + nombreAt + ": " + tipo + "\\n" + "\n";
                    visibilidad = "";
                    nombreAt = "";
                    tipo = "";
                }
                else if (cadenas.Equals("[*ATRIBUTOS]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[*CLASE]"))
                {
                    textDiagr = textDiagr + "}\"];"+"\n";
                    escribir(textDiagr,"",true);
                    textDiagr = "";
                    cadenas = "";
                    nombre = "";
                    codigo = "";
                    enlace = "";
                    nombreAt = "";
                    tipo = "";
                    visibilidad = "";
                    cont5 = 0;
                    nom = 0;
                    tipoRela = "";
                    tipo3 = 0;
                    tipo2 = 0;
                    visMetodo = ""; nombreMetodo = ""; tipoMetodo = ""; parametro = "";
                }
                else if (cadenas.Equals("*TIPO]"))
                {
                    cadenas = "";
                } else if (cadenas.Equals("[*DIAGRAMA_DE_CLASES]")) {
                    escribir("", relacion+ "}",false);
                    cadenas = "";
                    textDiagr = "";
                    cadenas = "";
                    nombre = "";
                    codigo = "";
                    enlace = "";
                    nombreAt = "";
                    tipo = "";
                    visibilidad = "";
                    cont5 = 0;
                    nom = 0;
                    tipoRela = "";
                    tipo3 = 0;
                    tipo2 = 0;
                    visMetodo = ""; nombreMetodo = ""; tipoMetodo = ""; parametro = "";
                }
                else if (cadenas.Equals("*VISIBILIDAD]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[METODOS]"))
                {
                    textDiagr = textDiagr + "| ";
                    cadenas = "";
                    tipo2 = 2;
                    cont5 = 2;
                    contMetodo = 1;
                }
                else if (cadenas.Equals("[*METODOS]"))
                {
                    tipo3++;
                    textDiagr = textDiagr + " " + nombreMetodo + "(" + parametro + ")" + " : " + tipoMetodo + "\n";
                    if (tipo3 > 0 || nom > 0)
                    {
                        tipoMetodo = "";
                        nombreMetodo = "";
                        parametro = "";
                    }
                    cadenas = "";
                }
                else if (cadenas.Equals("[METODO]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[*METODO]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[ARAMETROS]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("[*PARAMETROS]"))
                {
                    cadenas = "";
                }
                else if (cadenas.Equals("\n") || cadenas.Equals("\r") || cadenas.Equals("\t") || cadenas.Equals("\f") || cadenas.Equals(" "))
                {
                    cadenas = "";
                }
                switch (pos2) {
                    case 0:
                        switch (cadenas) {                        
                            case "[NOMBRE]":
                                if (cont5 == 0) {
                                    pos2 = 2;
                                    cadenas = "";
                                    cont5++;
                                } else if (cont5==1) {
                                    pos2 = 4;
                                    cadenas = "";
                                }
                                else if (cont5 == 2)
                                {
                                    pos2 = 8;
                                    cadenas = "";
                                }
                                else if (cont5 == -1)
                                {
                                    pos2 = 12;
                                    cadenas = "";
                                }
                                break;
                            case "[CODIGO]":
                                pos2 = 1;
                                cadenas = "";
                                break;
                            case "[VISIBILIDAD]":
                               if (contMetodo == 0) {
                                    pos2 = 3;
                                    cadenas = "";
                                } else if (contMetodo == 1) {
                                    pos2 = 9;
                                    cadenas = "";
                                }
                                break;
                            case "[TIPO]":
                                if (tipo2 == 0) {
                                    pos2 = 5;
                                    cadenas = "";
                                } else if (tipo2==1) {
                                    pos2 = 6;
                                    cadenas = "";
                                }
                                else if (tipo2 == 2)
                                {
                                    pos2 = 10;
                                    cadenas = "";
                                }
                                break;
                            case "[ENLACE]":
                                pos2 = 7;
                                cadenas = "";
                                break;
                            case "[PARAMETROS]":
                                pos2 = 11;
                                cadenas = "";
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        if (Char.IsLetter(variable)) 
                        {
                                nombre += variable;
                                pos2 = 2;
                        }
                        else if(variable == '[')
                        {
                            textDiagr = textDiagr +"label = \"{" + nombre + "| \n";
                            cadenas = "";
                            pos2 = 0;
                        }
                        break;
                    case 1 :
                        if (Char.IsDigit(variable))
                        {
                            codigo += variable;
                            pos2 =1;
                        }
                        else if (variable == '[')
                        {
                            textDiagr = textDiagr+ "clase" +codigo+"  "+ "[shape=record\n";
                            cadenas = "";
                            pos2 = 0;
                        }
                        break;
                    case 3:
                        if (cadenas.Equals("\"private\""))
                        {
                            visibilidad = "-";
                        } else if (cadenas.Equals("\"public\"")) {
                            visibilidad = "+";
                        
                        }
                        else if (variable == '[')
                        {
                            //textDiagr = textDiagr + visibilidad+" ";
                            cadenas = "";
                            pos2 = 0;
                        }
                        break;
                    case 4:
                        if (Char.IsLetter(variable))
                        {
                            nombreAt += variable;
                            pos2 = 4;
                        }
                        else if (variable == '[')
                        {
                            // = textDiagr + nombreAt + ": ";
                            cadenas = "";
                            pos2 = 0;
                            nom++;
                            if (nom>0) {
                                //nombreAt = "";
                            }
                            cont5 = 1;
                        }
                        break;
                    case 5:
                        if (Char.IsLetter(variable))
                        {
                            tipo += variable;
                            pos2 = 5;
                        }
                        else if (variable == '[')
                        {
                            tipo3++;
                            //textDiagr = textDiagr + tipo + "\\n" + "\n";
                            cadenas = "";
                            pos2 = 0;
                            if (tipo3 > 0) {
                                //tipo = "";
                            }
                            tipo2 = 0;
                        }
                        break;
                    case 6:
                        if (Char.IsLetter(variable))
                        {
                            tipoRela += variable;
                            pos2 = 6;
                        }
                        else if (variable == '[')
                        {
                            if (tipoRela.Equals("Asociacion")) {
                                relacion ="[arrowhead=empty];"+ relacion;
                            } else if (tipoRela.Equals("Agregacion")) {
                                relacion = "[arrowhead=diamond];"+"\n"+ relacion;
                            }
                            cadenas = "";
                            pos2 = 0; 
                        }
                        break;
                    case 7:
                        if (Char.IsDigit(variable))
                        {
                            enlace += variable;
                            pos2 = 7;
                        }
                        else if (variable == '[')
                        {
                            relacion = "clase"+codigo+ "->"+"clase"+enlace+" "+ relacion + "\n";
                            cadenas = "";
                            pos2 = 0;
                        }
                        break;
                    case 8:
                        if (Char.IsLetter(variable))
                        {
                            nombreMetodo += variable;
                            pos2 = 8;
                        }
                        else if (variable == '[')
                        {
                            //textDiagr =  textDiagr +" "+ nombreMetodo+"()"+" : " ;
                            cadenas = "";
                            pos2 = 0;
                            nom++;
                            /*if (nom > 0)
                            {
                                nombreMetodo = "";
                            }*/
                        }
                        break;
                    case 9:
                        if (cadenas.Equals("\"private\""))
                        {
                            visMetodo = "-";
                        }
                        else if (cadenas.Equals("\"public\""))
                        {
                            visMetodo = "+";

                        }
                        else if (variable == '[')
                        {
                            textDiagr = textDiagr + visMetodo + " ";
                            cadenas = "";
                            pos2 = 0;
                        }
                        break;
                    case 10:
                        if (Char.IsLetter(variable))
                        {
                            tipoMetodo += variable;
                            pos2 = 10;
                        }
                        else if (variable == '[')
                        {
                            tipo3++;
                            //textDiagr = textDiagr + tipoMetodo + "\n";
                            cadenas = "";
                            pos2 = 0;
                           /* if (tipo3 > 0)
                            {
                                tipoMetodo = "";
                            }*/
                        }
                        break;
                    case 11:
                        if (Char.IsLetter(variable))
                        {
                            parametro += variable;
                            pos2 = 11;
                        }
                        else if (variable == '[')
                        {
                           
                            cadenas = "";
                            pos2 = 0;
                           
                        }
                        break;
                    case 12:
                        if (Char.IsLetter(variable))
                        {
                            nombre2 += variable;
                            pos2 = 12;
                        }
                        else if (variable == '[')
                        {
                            cadenas = "";
                            pos2 = 0;
                            cont5 = 0;
                        }
                        break;
                }
            }
        }
        public void escribir(string text, string relacion, bool terminado)
        {
            text = text + "" + relacion;
            if (terminado == true)
            {
                dotArchivo.Write(text + "" + relacion);
            }
            else if (terminado == false)
            {
                dotArchivo.Write("" + relacion);
                dotArchivo.Close();
                ExecuteCommand(@"dot -Tpng diagrama.dot -o diagrama.png");
            }
        }

        static void ExecuteCommand(string _Command)
        {
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + _Command);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = false;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            string result = proc.StandardOutput.ReadToEnd();
            Console.WriteLine(result);
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
            archivo.Write("<H1>Tabla de Errores</H1>");
            archivo.Write("<br><br>");
            archivo.Write("<table>");
            archivo.Write("<tr><th>No</th><th>Error</th><th>Descripcion</th><th>fila</th><th>columna</th></tr>");
            foreach (ErrorToken i in errorToken) {
                archivo.Write("<tr><td>" + i.No + "</td><td>" + i.Tokens + "</td><td>" + "Elemento Léxico Desconocido" + "</td><td>" + i.Fila + "</td><td>" + i.Columna + "</td></tr>" );
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
