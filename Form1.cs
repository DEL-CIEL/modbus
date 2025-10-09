using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace modbus
{
    public partial class Form1 : Form
    {
        private Socket socket;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBoxAdresseIP.TextLength > 6)
            {
                string ipAdress = this.textBoxAdresseIP.Text;
                this.textBox1.AppendText("Connexion au serveur " + ipAdress + "\n");
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iped = new IPEndPoint(IPAddress.Parse(ipAdress), 502);
                try
                {
                    this.socket.Connect(iped);
                    this.textBox1.AppendText("Connexion ok\n");
                }
                catch (System.Exception ex)
                {
                    this.textBox1.AppendText("**Exception : Impossible de se connecter au serveur : " + ex.Message + "\r\n");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.socket.Close();
                this.textBox1.AppendText("Deconnexion reussie\n");
            }
            catch (System.Exception ex)
            {
                this.textBox1.AppendText("Deconnexion impossible: " + ex.Message + "\n");
            }
        }
    }
}