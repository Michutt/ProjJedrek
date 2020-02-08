using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;
using MathNet.Numerics.Integration;

namespace jedrek
{

    public partial class Form1 : Form
    {
        public delegate double FUNC(double x);

        public Form1()
        {
            InitializeComponent();
        }

        double h(double x)
        {
            if (radioButton3.Checked) return -4 * Math.Pow(x, 3) + 2 * Math.Pow(x, 2) - 3;
            if (radioButton2.Checked) return Math.Sin(x*Math.PI/180) * Math.PI / 180;
            if (radioButton1.Checked) return x * Math.Pow(Math.E, 2*x);
            return 0;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton3.Checked = false;
            radioButton2.Checked = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton3.Checked = false;
            radioButton2.Checked = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton3.Checked = true;
            radioButton2.Checked = false;
        }

        private double MetodaProstokatow(FUNC f, double xp, double xk, int n)
        {
            double dx, calka;

            dx = (xk - xp) / n;

            calka = 0;
            for (int i = 1; i <= n; i++)
            {
                System.Console.WriteLine(calka);
                calka += f(xp + i * dx);
            }
           calka *= dx;

            return calka;
        }

        private double MetodaTrapezow(FUNC f, double xp, double xk, int n)
        {
            double dx, calka;

            dx = (xk - xp) / n;

            calka = 0;
            for (int i = 1; i < n; i++)
            {
                calka += f(xp + i * dx);
            }
            calka += (f(xp) + f(xk)) / 2;
            calka *= dx;

            return calka;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double xp = Double.Parse(textBox7.Text);
                double xk = Double.Parse(textBox5.Text);
                int n = Int32.Parse(textBox6.Text);
                if (n % 2 == 0 && xp >= xk)
                {
                    textBox1.Text = "" + MetodaProstokatow(h, xp, xk, n);
                    textBox3.Text = "" + MetodaTrapezow(h, xp, xk, n);
                    textBox4.Text = "" + Integrate.OnClosedInterval(h, xp, xk);
                    textBox2.Text = "" + MathNet.Numerics.Integration.Algorithms.SimpsonRule.IntegrateComposite(h, xp, xk, n);
                }
                else
                    MessageBox.Show("Zły przedział, lub granica górna jest mniejsza od dolnej");
            }
            catch (System.FormatException) { }

        }

    }
}
