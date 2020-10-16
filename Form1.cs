using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixCalculator
{
    public partial class Form1 : Form
    {
        Matrix matrixA;
        Matrix matrixB;
        ResultMatrix resultMatrix;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.numericUpDownMatrixAM.Minimum = 2;
            this.numericUpDownMatrixAM.Maximum = 11;
            this.numericUpDownMatrixAN.Minimum = 2;
            this.numericUpDownMatrixAN.Maximum = 11;
            this.numericUpDownMatrixBM.Minimum = 2;
            this.numericUpDownMatrixBM.Maximum = 11;
            this.numericUpDownMatrixBN.Minimum = 2;
            this.numericUpDownMatrixBN.Maximum = 11;
            this.matrixA = new Matrix(12,12);
            this.matrixB = new Matrix(299,12);
            this.resultMatrix = new ResultMatrix();
            
        }

        private void CreateMatrixA_Click(object sender, EventArgs e)
        {
            if (this.matrixA.states)
            {
                this.matrixA.DeleteMatrix();
            }
            ShowAllMatrix(this.matrixA, numericUpDownMatrixAM, numericUpDownMatrixAN);
        }
        private void ShowAllMatrix(Matrix matrixLocal, NumericUpDown numericUpDownAmLocal, NumericUpDown numericUpDownAnLocal)
        {
            matrixLocal.CreateMatrix(int.Parse(numericUpDownAmLocal.Value.ToString()), int.Parse(numericUpDownAnLocal.Value.ToString()));
            for (int i = 0; i < int.Parse(numericUpDownAmLocal.Value.ToString()); i++)
            {
                for (int j = 0; j < int.Parse(numericUpDownAnLocal.Value.ToString()); j++)
                {
                    this.Controls.Add(matrixLocal.GetTextBox(i, j));
                }
            }
        }

        private void ShowAllMatrixExtraAll(ResultMatrix matrixAll)
        {
            for (int i = 0; i < matrixAll.GetSizeMatrixM(); i++)
            {
                for (int j = 0; j < matrixAll.GetSizeMatrixN(); j++)
                {
                    this.Controls.Add(matrixAll.GetTextBox(i, j));
                }
            }
        }
        private void buttonDeleteMatrixA_Click(object sender, EventArgs e)
        {
            this.matrixA.DeleteMatrix();
        }

        private void CreateMatrixB_Click(object sender, EventArgs e)
        {
            if (this.matrixB.states)
            {
                this.matrixB.DeleteMatrix();
            }
            ShowAllMatrix(this.matrixB, numericUpDownMatrixBM, numericUpDownMatrixBN);
        }

        private void DeleteMatrixB_Click(object sender, EventArgs e)
        {
            this.matrixB.DeleteMatrix();
        }

        private void ResultMatrixSumAB_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.resultMatrix.states)
                {
                    this.resultMatrix.DeleteMatrix();
                }
                this.resultMatrix.SumMatrixAandB(this.matrixA, this.matrixB);
                this.resultMatrix.CreateMatrix(this.resultMatrix.GetSizeMatrixM(), this.resultMatrix.GetSizeMatrixN());
                this.ShowAllMatrixExtraAll(this.resultMatrix);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void TransposeMatrixA_Click(object sender, EventArgs e)
        {
            try
            {
                this.matrixA.MatrixTranspose();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            
        }

        private void TransposeMatrixB_Click(object sender, EventArgs e)
        {
            try
            {
                 this.matrixB.MatrixTranspose();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
           
        }

        private void MultiplyByMatrixA_Click(object sender, EventArgs e)
        {
            try
            {
                this.matrixA.MatrixMultiplicationByNumber(float.Parse(this.numericUpDownMatrixmultiplicationbynumber.Value.ToString()));
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void MultiplyByMatrixB_Click(object sender, EventArgs e)
        {
            try
            {
                this.matrixB.MatrixMultiplicationByNumber(float.Parse(this.numericUpDownMatrixmultiplicationbynumberB.Value.ToString()));
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
