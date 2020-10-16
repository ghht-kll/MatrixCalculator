using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixCalculator
{
    abstract class Matrixll
    {
        protected float[,] resultMatrixrray;
        public bool states = false;
        protected TextBox[,] textBoxes;
        protected int sizeMatrixM;
        protected int sizeMatrixN;
        protected int x, y;

        public abstract void CreateMatrix(int m, int n);
        public abstract TextBox GetTextBox(int m, int n);
        public abstract void DeleteMatrix();

        public abstract int GetSizeMatrixM();
        public abstract int GetSizeMatrixN();
    }

    class Matrix : Matrixll
    {
        public Matrix(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override void CreateMatrix(int m, int n)
        {
            this.states = true;
            this.sizeMatrixM = m;
            this.sizeMatrixN = n;
            this.textBoxes = new TextBox[this.sizeMatrixM, this.sizeMatrixN];

            int _x = x, _y = y;
            for (int i = 0; i < this.sizeMatrixM; i++)
            {
                for (int j = 0; j < this.sizeMatrixN; j++)
                {
                    this.textBoxes[i, j] = new TextBox();
                    this.textBoxes[i, j].Multiline = true;
                    this.textBoxes[i, j].Height = 25;
                    this.textBoxes[i, j].Width = 25;
                    this.textBoxes[i, j].Location = new System.Drawing.Point(_x, _y);
                    _x += 25;
                }
                _x = this.x;
                _y += 25;
            }
        }
        public override TextBox GetTextBox(int m, int n)
        {
            return this.textBoxes[m, n];
        }

        public override void DeleteMatrix()
        {
            for (int i = 0; i < this.sizeMatrixM; i++)
            {
                for (int j = 0; j < this.sizeMatrixN; j++)
                {
                    this.textBoxes[i, j].Dispose();
                }
            }
            this.states = false;
        }

        public void CreateMatrixToArrat()
        {
            this.resultMatrixrray = new float[this.sizeMatrixM, this.sizeMatrixN];
            for (int i = 0; i < this.sizeMatrixM; i++)
            {
                for (int j = 0; j < this.sizeMatrixN; j++)
                {
                    this.resultMatrixrray[i, j] = float.Parse(this.textBoxes[i, j].Text);
                }
            }
        }
        public void MatrixTranspose()
        {
            if (this.sizeMatrixM == this.sizeMatrixN)
            {
                this.CreateMatrixToArrat();
                float temp;
                for (int i = 0; i < this.sizeMatrixM; i++)
                {
                    for (int j = i + 1; j < this.sizeMatrixN; j++)
                    {
                        temp = this.resultMatrixrray[i, j];
                        this.resultMatrixrray[i, j] = this.resultMatrixrray[j, i];
                        this.resultMatrixrray[j, i] = temp;
                    }
                }
            }
            this.ChangingTheCurrentStateBasedOnAnArray();
        }
        public void ChangingTheCurrentStateBasedOnAnArray()
        {
            for (int i = 0; i < this.sizeMatrixM; i++)
            {
                for (int j = 0; j < this.sizeMatrixN; j++)
                {
                    this.textBoxes[i, j].Text = this.resultMatrixrray[i, j].ToString();
                }
            }
        }
        public void MatrixMultiplicationByNumber(float value)
        {
            this.CreateMatrixToArrat();
            for (int i = 0; i < this.GetSizeMatrixM(); i++)
            {
                for (int j = 0; j < this.GetSizeMatrixN(); j++)
                {
                    this.resultMatrixrray[i, j] *= value;
                }
            }
            this.ChangingTheCurrentStateBasedOnAnArray();
        }

        public override int GetSizeMatrixM() => this.sizeMatrixM;
        public override int GetSizeMatrixN() => this.sizeMatrixN;
    }


    class ResultMatrix : Matrixll
    {
        public override void CreateMatrix(int m, int n)
        {
            this.states = true;
            this.sizeMatrixM = m;
            this.sizeMatrixN = n;
            this.textBoxes = new TextBox[this.sizeMatrixM, this.sizeMatrixN];

            int x = 586, y = 12;
            for (int i = 0; i < this.sizeMatrixM; i++)
            {
                for (int j = 0; j < this.sizeMatrixN; j++)
                {
                    this.textBoxes[i, j] = new TextBox();
                    this.textBoxes[i, j].Multiline = true;
                    this.textBoxes[i, j].Height = 25;
                    this.textBoxes[i, j].Width = 25;
                    this.textBoxes[i, j].Location = new System.Drawing.Point(x, y);
                    this.textBoxes[i, j].Text = this.resultMatrixrray[i, j].ToString();
                    x += 25;
                }
                x = 586;
                y += 25;
            }
        }
        public override TextBox GetTextBox(int m, int n)
        {
            return this.textBoxes[m, n];
        }

        public override void DeleteMatrix()
        {
            for (int i = 0; i < this.sizeMatrixM; i++)
            {
                for (int j = 0; j < this.sizeMatrixN; j++)
                {
                    this.textBoxes[i, j].Dispose();
                }
            }
            this.states = false;
        }

        public void SumMatrixAandB(Matrix matrixA, Matrix matrixB)
        {
            if (EqualityCheck(matrixA, matrixB))
            {

                this.resultMatrixrray = new float[matrixA.GetSizeMatrixM(), matrixA.GetSizeMatrixN()];
                this.sizeMatrixM = matrixA.GetSizeMatrixM();
                this.sizeMatrixN = matrixA.GetSizeMatrixN();
                for (int i = 0; i < matrixA.GetSizeMatrixM(); i++)
                {
                    for (int j = 0; j < matrixA.GetSizeMatrixN(); j++)
                    {
                        this.resultMatrixrray[i, j] = float.Parse(matrixA.GetTextBox(i, j).Text) + float.Parse(matrixB.GetTextBox(i, j).Text);
                    }
                }
            }
            else
            {
                MessageBox.Show("operation is impossible: n != m");
            }
        }
        public override int GetSizeMatrixM() => this.sizeMatrixM;
        public override int GetSizeMatrixN() => this.sizeMatrixN;

        public bool EqualityCheck(Matrix matrixA, Matrix matrixB)
        {
            if ((matrixA.GetSizeMatrixM() == matrixB.GetSizeMatrixM()) && (matrixA.GetSizeMatrixM() == matrixB.GetSizeMatrixN()))
                return true;
            else return false;
        }
    }
}
