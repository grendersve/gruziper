using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace АИС_грузоперевозки
{
    public partial class Form5 : Form
    {
        // Строка подключения к базе данных
        private string connectionString = "Data Source=Gruzoperevozki.db";

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadDeletedContracts();
        }

        private void LoadDeletedContracts()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Запрос для получения удалённых договоров
                    string query = "SELECT * FROM Удалённые_договоры";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                        {
                            DataTable deletedContractsTable = new DataTable();
                            adapter.Fill(deletedContractsTable);

                            // Заполнение DataGridView1 данными
                            dataGridView1.DataSource = deletedContractsTable;

                            // Скрытие колонки ID, если она есть
                            if (dataGridView1.Columns.Contains("ID"))
                            {
                                dataGridView1.Columns["ID"].Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

