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
using System.Windows.Forms.VisualStyles;

namespace modbus
{
    public partial class Form1 : Form
    {
        private Socket socket;
        Modbus modbus;

        public Form1()
        {
            InitializeComponent();
            modbus = new Modbus();
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
                catch (Exception ex)
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
            catch (Exception ex)
            {
                this.textBox1.AppendText("Deconnexion impossible: " + ex.Message + "\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var trameE = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x01, 0x03, 0x0C, 0x86, 0x00, 0x01 };
            try
            {
                this.socket.Send(trameE);
            }
            catch (Exception ex)
            {
                this.textBox1.AppendText("Envoi impossible: " + ex.Message + "\n");
            }

            var trameR = new byte[256];

            int bytesReceived = this.socket.Receive(trameR);
            int tensionRaw = (trameR[9] << 8) | trameR[10];
            double tension = tensionRaw / 10.0;
            textBox2.Text = tension.ToString();
        }
    }
}