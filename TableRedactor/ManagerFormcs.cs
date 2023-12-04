using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace TableRedactor
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
            SumTextBox.Text = "0";
        }

        DataTableCollection tableCollection;

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx| Excel 97-2003 Workbook|*.xls" })// создаём диалоговое окно с фильтрами
                                                                                                                                    // проверяем был ли выбран файл
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream)) // Запускаем поток чтения
                            {
                                DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = false }//Используем первые строки таблицы как заголовки DataGridView
                                });
                                tableCollection = dataSet.Tables;
                                toolStripComboBox1.Items.Clear();
                                foreach (DataTable table in tableCollection)
                                    toolStripComboBox1.Items.Add(table.TableName);// добавляем листы
                            }
                        }

                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = tableCollection[toolStripComboBox1.SelectedItem.ToString()];
                //dt.Columns.Add("Выбранные для оплаты товары");
                //DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
                //col1.DataPropertyName = "Выбранные для оплаты товары";
                //dataGridView1.Columns.Add(col1);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int cellPrice = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Общая сумма"].Value.ToString());

                int result = Convert.ToInt32(SumTextBox.Text) + cellPrice;

                SumTextBox.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            SumTextBox.Text = "0";
        }
    }
}
