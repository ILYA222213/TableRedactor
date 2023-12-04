using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using ExcelDataReader;
using System.Data;

namespace TableRedactor
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {

        }
        DataTableCollection tableCollection;

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using(OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx| Excel 97-2003 Workbook|*.xls" })
                // проверяем был ли выбран файл
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using(var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using(IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true}
                            });
                            tableCollection = dataSet.Tables;
                            toolStripComboBox1.Items.Clear();
                            foreach (DataTable table in tableCollection)
                                toolStripComboBox1.Items.Add(table.TableName);// добавляем листы
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[toolStripComboBox1.SelectedItem.ToString()];
            dataGridView1.DataSource = dt;
        }

        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application exApp = new Excel.Application();

                exApp.Workbooks.Add();
                Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;
                int i, j;
                for (i = 0; i <= dataGridView1.RowCount - 2; i++)
                {
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        wsh.Cells[1, j+1]= dataGridView1.Columns[j].HeaderText.ToString();
                        wsh.Cells[i + 2, j + 1] = dataGridView1[j, i].Value.ToString();
                    }
                }
                exApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
