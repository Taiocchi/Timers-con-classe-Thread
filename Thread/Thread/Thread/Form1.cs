using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.IO;

namespace ThreadApp
{
    public class FormImmagine : Form
    {
        private PictureBox pictureBox;
        private Label labelMessage;
        private Button buttonOk;

        public FormImmagine(string message, string imagePath)
        {
            this.Size = new Size(200, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Immagine Box";

            pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Size = new Size(150, 150);
            pictureBox.Location = new Point(20, 20);

            try
            {
                pictureBox.Image = Image.FromFile(imagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore nel caricamento dell'immagine: " + ex.Message);
            }

            buttonOk = new Button();
            buttonOk.Text = "OK";
            buttonOk.Location = new Point(60, 200);
            buttonOk.Click += (sender, e) => this.Close();

            this.Controls.Add(pictureBox);
            this.Controls.Add(buttonOk);
        }
    }

    public partial class Form1 : Form
    {
        private Thread[] threads; 
        private System.Windows.Forms.Label[] lblTempo;
        private int cont = 0;
        private object lockObject = new object(); 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            threads = new Thread[100]; 
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

            threads[cont] = new Thread(new ParameterizedThreadStart(Countdown));
            threads[cont].Start(cont); 
            cont++;
        }

        private void Countdown(object index)
        {
            int n = (int)index;
            int timeLeft = Convert.ToInt16(lblTempo[n].Text);

            while (timeLeft > 0)
            {
                Thread.Sleep(1000); 

                this.Invoke((MethodInvoker)delegate {
                    timeLeft--;
                    lblTempo[n].Text = timeLeft.ToString();
                });
            }

            if (timeLeft == 0)
            {
                this.Invoke((MethodInvoker)delegate {
                    string imgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Boom-1258x686.png");
                    FormImmagine formImmagine = new FormImmagine("BOOM!", imgPath);
                    formImmagine.ShowDialog(); 
                });
            }
        }
    }
}
