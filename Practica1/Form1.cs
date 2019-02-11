using System;
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
                        switch (concatenar){
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
                        }
                        break;
                    case 1:
                        if (concatenar == 'D') {
                            token += concatenar;
                            mover = 1;
                        } else if (concatenar.Equals('I')) {
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
                        break;
                    case 4:
                         if(concatenar == 'S')
                        {
                            token += concatenar;
                            mover = 5;
                            estadoInical = estadoInical - 1;
                        }
                        break;
                    case 5:
                        enviarToken(token);
                        token = "";
                        mover = 0;
                        break;
                }
            }
        }

        public void enviarToken(string token)
        {
            Console.WriteLine("Token correcto "+ token);
        }

        public void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //crearHtml();
            string texto = idTexto.Text;
            Analizador(texto);
        }
    }
}
