using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodoMinCuadrado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Cantidad de Datos
        public int CantD()
        {
            int x = 0;
            int cantidad = dgvDatos.RowCount + 2;
            for (int i = 1; i < cantidad; i++)
            {
                x = i;
            }
            return x;
        }
        
        //Boton de Agregar
        private void button1_Click(object sender, EventArgs e)
        {
            if ((txtX.Text == "") && (txtY.Text == ""))
            {
                MessageBox.Show("Las casillas no pueden estar vacias");
            }
            if ((txtX.Text != "") && (txtY.Text != ""))
            {
                DataGridViewRow file = new DataGridViewRow();
                file.CreateCells(dgvDatos);
                file.Cells[0].Value = CantD();
                file.Cells[1].Value = txtX.Text;
                file.Cells[2].Value = txtY.Text;
                dgvDatos.Rows.Add(file);
                txtX.Clear();
                txtY.Clear();
                ObtenerTotalXY();
            }
        }

        //Sumatoria del total de X e Y
        public void ObtenerTotalXY()
        {
            double totalX = 0;
            double totalY = 0;
            int contador = 0;
            contador = dgvDatos.RowCount;
            for (int i = 0; i < contador; i++)
            {
                totalX += double.Parse(dgvDatos.Rows[i].Cells[1].Value.ToString());
            }
            for (int i = 0; i < contador; i++)
            {
                totalY += double.Parse(dgvDatos.Rows[i].Cells[2].Value.ToString());
            }
            lblTotalX.Text = totalX.ToString();
            lblTotalY.Text = totalY.ToString();
        }

        //Boton de eliminar
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rppta = MessageBox.Show("Desea eliminar valores?", "Eliminacion",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rppta == DialogResult.Yes)
                {
                    dgvDatos.Rows.Remove(dgvDatos.CurrentRow);
                }
            }
            catch { }
            ObtenerTotalXY();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

//--------------------------------------------------------------------------------
//--------------- OPERACIONES AUXILIARES DE LA ECUACION --------------------------
//--------------------------------------------------------------------------------
        public double SumatoriaXY()   //Sum(xy)
        {
            double SumXY = 0;
            int n = dgvDatos.RowCount;
            for (int i = 0; i < n; i++)
            {
                SumXY += (double.Parse(dgvDatos.Rows[i].Cells[1].Value.ToString()) *
                    double.Parse(dgvDatos.Rows[i].Cells[2].Value.ToString()));
            }
            return SumXY;
        }
        public double SumXpSumY()   //Sum(x)*sum(y)
        {
            double SumXpY = double.Parse(lblTotalX.Text) * double.Parse(lblTotalY.Text);
            return SumXpY;
        }
        public double SumatoriaX2() //Sum(x2)
        {
            double SumX2 = 0;
            int n = dgvDatos.RowCount;
            for (int i = 0; i < n; i++)
            {
                SumX2 += (double.Parse(dgvDatos.Rows[i].Cells[1].Value.ToString()) *
                    double.Parse(dgvDatos.Rows[i].Cells[1].Value.ToString()));
            }
            return SumX2;
        }
        public double SumatoriaY2() //Sum(y2)
        {
            double SumY2 = 0;
            int n = dgvDatos.RowCount;
            for (int i = 0; i < n; i++)
            {
                SumY2 += (double.Parse(dgvDatos.Rows[i].Cells[2].Value.ToString()) *
                    double.Parse(dgvDatos.Rows[i].Cells[2].Value.ToString()));
            }
            return SumY2;
        }
        public double Sumx2()       //Sum(x)2
        {
            double X2 = (double.Parse(lblTotalX.Text) * double.Parse(lblTotalX.Text));
            return X2;
        }
        public double Sumy2()       //Sum(y)2
        {       
            double y2 = (double.Parse(lblTotalY.Text) * double.Parse(lblTotalY.Text));
            return y2;
        }
        public double ValorM()
        {
            int n = dgvDatos.RowCount;
            double B = (((n * SumatoriaXY()) - (SumXpSumY())) / ((n * SumatoriaX2()) - (Sumx2())));
            return B;
        }
        public double ValorB()
        {
            int n = dgvDatos.RowCount;
            double A = ((double.Parse(lblTotalY.Text) - (ValorM() * 
                (double.Parse(lblTotalX.Text)))) / n);
            return A;
        }

//--------------------------------------------------------------------------------
//---------------------- BOTONES DE CALCULAR --------------------------------------
//--------------------------------------------------------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            lblValorA.Text = "(" + ValorM() + ")" + "x" + " + " + "(" + ValorB() + ")";
        }

        private void buttonCorrelacion_Click(object sender, EventArgs e)
        {
            int n = dgvDatos.RowCount;
            double correlacion = 0;
            correlacion = (((n * SumatoriaXY()) - SumXpSumY()) / 
                (Math.Sqrt(((n * SumatoriaY2()) - Sumy2()) * ((n * SumatoriaX2()) - Sumx2()))));
            double raiz= Math.Sqrt(((n * SumatoriaY2()) - Sumy2()) * ((n * SumatoriaX2()) - Sumx2()));
            lblCorrelacion.Text = correlacion + "";
        }

        private void buttonError_Click(object sender, EventArgs e)
        {
            int n = dgvDatos.RowCount;
            double SumY = double.Parse(lblTotalY.Text);
            double S = Math.Sqrt((SumatoriaY2() - (ValorB() * SumY) -
                (ValorM() * SumatoriaXY())) / (n - 2));
            lblError.Text = S + "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
        
        }
    }
}
