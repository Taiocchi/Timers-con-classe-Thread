using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Text;
using System.Timers;

namespace Thread
{
    //Timer timerX = new Thread;

    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer[] timer;
        private System.Windows.Forms.Label[] lblTempo;
        private int cont = 0;
        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer = new System.Windows.Forms.Timer[100];
            lblTempo = new System.Windows.Forms.Label[100];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblTempo[cont] = new System.Windows.Forms.Label();
            lblTempo[cont].Location = new Point((cont % 10) * 100, cont / 10 * 100);
            lblTempo[cont].Font = new Font(FontFamily.Families[0], 36);
            lblTempo[cont].Size = new Size(100, 100);
            this.Controls.Add(lblTempo[cont]);
            lblTempo[cont].Text = numericUpDown1.Value.ToString();

            timer[cont] = new System.Windows.Forms.Timer();
            timer[cont].Interval = 1000;
            timer[cont].Tick += timer1_Tick;
            timer[cont].Tag = cont;
            timer[cont].Start();
            cont++;
            //numericUpDown1.Visible = false;
            //button1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int n = Convert.ToInt16((sender as System.Windows.Forms.Timer).Tag);
            lblTempo[n].Text = (Convert.ToInt16(lblTempo[n].Text) - 1).ToString();
            if (lblTempo[n].Text == "0")
            {
                timer[n].Stop();
                MessageBox.Show("BOOM");
            }
        }

    }
}
//AGGIUNGI IMMAGINE BOOM E THREAD
