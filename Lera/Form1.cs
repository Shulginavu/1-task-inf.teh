using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace Lera
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowCount = 2;
            dataGridView1.ColumnCount = 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[,] array = readFromDataGridView(dataGridView1);
            array = sortMass(array);       
            writeToDataGridView(array, dataGridView2);
        }
         
        private int[,] sortMass(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++){
                int sum1 = sum(array, i);
                for (int j = i; j < array.GetLength(0); j++) {
                    int sum2 = sum(array, j);
                    if (sum1 >= sum2) {
                        array = swapMass(array, i, j);
                        sum1 = sum2;
                    }
                }
            }
            return array;
        }

        private int sum(int[,] array, int i)
        {
            int sum = 0;
            for (int j = 0; j < array.GetLength(1); j++)
            {
                sum += array[i,j];
            }
            return sum;
        }
        private int[,] swapMass(int[,] array, int k, int m)
        {
            for (int i = 0; i < array.GetLength(1); i++) {
                int swap = array[k,i];
                array[k,i] = array[m,i];
                array[m,i] = swap;
            }
            return array;
        }

        public static int[,] readFromDataGridView(DataGridView dataGridView)
        {
            int[,] matrix;
            //создаём новый массив размера dataGridView.RowCount на dataGridView.ColumnCount
            //где RowCount количество строк у элемента, а ColumnCount количество столбцов
            matrix = new int[dataGridView.RowCount, dataGridView.ColumnCount];
            try//отлов исключений
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView.ColumnCount; j++)
                    {
                        //Преобразуем значения из ячеек в числа, и пишем в массив
                        //Если не число то происходит вызов исключения и его обработка
                        matrix[i,j] = Convert.ToInt32(dataGridView.Rows[i].Cells[j].Value);
                    }
                }
            }
            catch (System.Exception e)//обработка пойманного исключения
            {       
                 MessageBox.Show(e.Message + "\n(Использование букв и символов недопустимо!)", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return matrix;
        }

        public static void writeToDataGridView(int[,] matr, DataGridView dataGridView)
        {
            //указываем контроллу в который пишем количество строк и столбцов
            dataGridView.RowCount = matr.GetLength(0);
            dataGridView.ColumnCount = matr.GetLength(1);
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    //пишем значения из массива в ячейки контролла
                    dataGridView.Rows[i].Cells[j].Value = matr[i, j];
                }
            }

        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            StringBuilder sb = new StringBuilder();
            int[,] array = readFromDataGridView(dataGridView2);
            for (int i=0;i<array.GetLength(0);i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    sb.Append(array[i, j]);
                    sb.Append(" ");
                }
                sb.AppendLine("");
            }
            System.IO.File.WriteAllText(filename, sb.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;

            string[] lines = System.IO.File.ReadAllLines(filename); 
            int[,] array = new int[lines.Length, lines[0].Split(' ').Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                for (int j = 0; j < temp.Length; j++)
                    array[i, j] = Convert.ToInt32(temp[j]);
            }

            writeToDataGridView(array, dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount++;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount - 1 < 2)
            {
                return;
            }
            dataGridView1.RowCount--;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount++;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ColumnCount - 1 < 2)
            {
                return;
            }
            dataGridView1.ColumnCount--;
        }

    }
}
