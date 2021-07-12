using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bulmacalar
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private string kelime;
        public char[] tahmin;
        public int can = 7;
        int maxcan = 7;
        List<string> resimler = new List<string>();


       

        public void ButtonPress(object sender, EventArgs e) // tahmin kontrolü
        {
            char text = char.Parse((sender as Button).Text);
            bool secim = false;
            for (int i = 0; i < kelime.Length; i++)
            {
                if (kelime[i] == text && tahmin[i] != text)
                {
                    secim = true;
                    tahmin[i] = text;
                }

            }
            string secili = "";
            foreach (var item in tahmin)
            {
                secili += item;
            }
            if (kelime == secili)
            {
                Thread.Sleep(1000);
                MessageBox.Show("Doğru!\nSıradaki Soru Geliyor");
                Restart();
            }
            if (secim)
            {
                this.label3.Text = "";
                string gecici = "";
                for (int i = 0; i < tahmin.Length; i++)
                {
                    if (tahmin[i] == '_')
                    {
                        gecici += '_' + " ";
                    }
                    else
                    {
                        gecici += tahmin[i];
                    }
                }
                this.label3.Text = gecici;
            }
            else
            {

                int gecicisayac = maxcan - can;
                can--;
                if (can == 0)
                {
                    this.pictureBox1.ImageLocation = resimler[maxcan - 1];
                    MessageBox.Show("Öldün");
                    Restart();
                }
                else
                {
                    this.pictureBox1.ImageLocation = resimler[gecicisayac];

                    this.label1.Text = "Kalan Can: " + can;
                }
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            List<string> path = Directory.GetFiles("sprites").OrderBy(p => int.Parse(p.Substring(8).Split('.')[0])).ToList();

            foreach (var item in path)
            {
                resimler.Add(item);
            }

            Restart();

        }

        public void Restart()
        {

            GelenKelime();
            this.pictureBox1.ImageLocation = "";
            can = maxcan;
            this.label1.Text = "Kalan Can: " + can;
            Thread.Sleep(1000);


        }
        public void GelenKelime()
        {
            using (StreamReader sr = new StreamReader("kelimeler.txt"))
            {
                string satir;
                List<string> kelimeler = new List<string>();
                while ((satir = sr.ReadLine()) != null)
                {
                    kelimeler.Add(satir);
                }
                Random r = new Random();
                kelime = kelimeler[r.Next(0, kelimeler.Count)].ToUpper();

                tahmin = new char[kelime.Length];
                this.label3.Text = "";
                for (int i = 0; i < kelime.Length; i++)
                {
                    tahmin[i] = '_';
                    this.label3.Text += "_ ";
                }
                if (1 == 1)
                {
                    this.Text = kelime;
                }
                
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
