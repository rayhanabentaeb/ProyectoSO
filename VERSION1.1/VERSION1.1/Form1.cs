using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace VERSION1._1
{
    public partial class Form1 : Form
    {
        
        Socket server;
        Thread atender;
        public Form1(Socket server = null)
        {
            InitializeComponent();
            
            this.server = server;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje;

                
                switch (codigo)
                {
                    case 1:  // respuesta a longitud
                        mensaje = trozos[2].Split('\0')[0];

                        break;
                    case 2:      //respuesta a si mi nombre es bonito
                        mensaje = trozos[2].Split('\0')[0];

                        break;
                    case 3:       //Recibimos la respuesta de si soy alto
                        mensaje = trozos[2].Split('\0')[0];
                        break;

                }
            }
        }

        private void BotonAceptar_Click(object sender, EventArgs e)
        {
            string mensaje = "1/" + nombreusuario.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            mensaje = "2/" + correo.Text;
            // Enviamos al servidor el nombre tecleado
            msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            mensaje = "3/" + contrasena.Text;
            // Enviamos al servidor el nombre tecleado
            msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        private void botonconectar_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");
                //pongo en marcha el thread que atender� los mensajes del servidor
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show($"No he podido conectar con el servidor.");
                return;
            }
        }

        private void botondesconectar_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexi�n
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        
    }
}
