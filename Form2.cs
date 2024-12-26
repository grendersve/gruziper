using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace АИС_салона_по_аренде_автомобилей
{
    public partial class Form2 : Form
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter adapter;
        private DataTable dt;

        public Form2()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadClientData();
            LoadSearchCriteria();
            LoadGruzComboData();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void ConnectToDatabase()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            connection = new SQLiteConnection($"Data Source={dbPath}");
            connection.Open();

        }

        private void LoadClientData()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Клиент";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable clientsTable = new DataTable();
                        adapter.Fill(clientsTable);
                        dataGridView1.DataSource = clientsTable;
                        dataGridView1.Columns["ID"].Visible = false;
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Обработка изменений выбранной строки
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                textBox2.Text = selectedRow.Cells["Фамилия"].Value.ToString();
                textBox3.Text = selectedRow.Cells["Имя"].Value.ToString();
                textBox4.Text = selectedRow.Cells["Отчество"].Value.ToString();
                textBox5.Text = selectedRow.Cells["Серия_паспорта"].Value.ToString();
                textBox6.Text = selectedRow.Cells["Номер_паспорта"].Value.ToString();
                textBox7.Text = selectedRow.Cells["Номер_телефона"].Value.ToString();
                textBox36.Text = selectedRow.Cells["Почта"].Value.ToString();
                textBox37.Text = selectedRow.Cells["Адрес"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsValidName(textBox2.Text) || !IsValidName(textBox3.Text) || !IsValidName(textBox4.Text))
            {
                MessageBox.Show("Фамилия, Имя и Отчество должны содержать только буквы.");
                return;
            }

            if (textBox5.Text.Length != 4 || !int.TryParse(textBox5.Text, out _))
            {
                MessageBox.Show("Серия паспорта должна содержать 4 цифры.");
                return;
            }

            if (textBox6.Text.Length != 6 || !int.TryParse(textBox6.Text, out _))
            {
                MessageBox.Show("Номер паспорта должен содержать 6 цифр.");
                return;
            }

            if (!IsUniqueSeriaNumber(textBox5.Text, textBox6.Text))
            {
                MessageBox.Show("Такое сочетание серии и номера паспорта уже существует.");
                return;
            }

            string phonePattern = @"^(\+7|8)\d{10}$";
            if (!Regex.IsMatch(textBox7.Text, phonePattern))
            {
                MessageBox.Show("Неверный формат номера телефона.");
                return;
            }

            if (!IsUniqueTelephone(textBox7.Text))
            {
                MessageBox.Show("Такой номер телефона уже существует.");
                return;
            }

            InsertClientDataToDatabase(
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox6.Text,
                textBox7.Text,
                textBox36.Text,
                textBox37.Text
            );

            LoadClientData();
        }

        private bool IsValidName(string name)
        {
            return Regex.IsMatch(name, "^[А-Яа-яЁё]+$");
        }

        private bool IsUniqueSeriaNumber(string seria, string number)
        {
            bool isConnectionAlreadyOpen = connection.State == System.Data.ConnectionState.Open;

            if (!isConnectionAlreadyOpen)
            {
                connection.Open();
            }

            try
            {
                using (SQLiteCommand command = new SQLiteCommand("SELECT COUNT(*) FROM Клиент WHERE [Серия_паспорта] = @seria AND [Номер_паспорта] = @number", connection))
                {
                    command.Parameters.AddWithValue("@seria", seria);
                    command.Parameters.AddWithValue("@number", number);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count == 0;
                }
            }
            finally
            {
                if (!isConnectionAlreadyOpen)
                {
                    connection.Close();
                }
            }
        }

        private bool IsUniqueTelephone(string telephone)
        {
            bool isConnectionAlreadyOpen = connection.State == System.Data.ConnectionState.Open;

            if (!isConnectionAlreadyOpen)
            {
                connection.Open();
            }

            try
            {
                using (SQLiteCommand command = new SQLiteCommand("SELECT COUNT(*) FROM Клиент WHERE [Номер_телефона] = @telephone", connection))
                {
                    command.Parameters.AddWithValue("@telephone", telephone);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count == 0;
                }
            }
            finally
            {
                if (!isConnectionAlreadyOpen)
                {
                    connection.Close();
                }
            }
        }

        private void InsertClientDataToDatabase(string surname, string name, string lastName, string seria, string number, string telephone, string mail, string address)
        {
            bool isConnectionAlreadyOpen = connection.State == System.Data.ConnectionState.Open;

            if (!isConnectionAlreadyOpen)
            {
                connection.Open();
            }

            try
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Клиент (Фамилия, Имя, Отчество, [Серия_паспорта], [Номер_паспорта], [Номер_телефона], Почта, Адрес) VALUES (@surname, @name, @lastName, @seria, @number, @telephone, @mail, @address)";
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@seria", seria);
                    command.Parameters.AddWithValue("@number", number);
                    command.Parameters.AddWithValue("@telephone", telephone);
                    command.Parameters.AddWithValue("@mail", mail);
                    command.Parameters.AddWithValue("@address", address);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (!isConnectionAlreadyOpen)
                {
                    connection.Close();
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Получаем выбранный критерий поиска
            string selectedCriteria = comboBox1.SelectedItem?.ToString();
            string searchValue = textBox21.Text.Trim();

            if (string.IsNullOrEmpty(selectedCriteria) || string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Выберите критерий и введите значение для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Формируем запрос поиска
                string query = $"SELECT * FROM Клиент WHERE {selectedCriteria} LIKE @searchValue";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable searchResults = new DataTable();
                        adapter.Fill(searchResults);

                        // Проверяем, найдены ли результаты
                        if (searchResults.Rows.Count == 0)
                        {
                            MessageBox.Show("Данные не найдены. Проверьте критерий и значение поиска.", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Обновляем DataGridView
                            dataGridView1.DataSource = searchResults;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            // Очищаем поле поиска и сбрасываем выбранный критерий
            textBox21.Clear();
            comboBox1.SelectedIndex = -1; // Сбрасываем выбор в ComboBox

            try
            {
                // Формируем запрос для получения всех данных из таблицы Клиент
                string query = "SELECT * FROM Клиент";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable allData = new DataTable();
                        adapter.Fill(allData);

                        // Обновляем DataGridView с полными данными
                        dataGridView1.DataSource = allData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сбросе фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSearchCriteria()
        {
            try
            {
                // SQL-запрос для получения имен столбцов таблицы Клиент
                string query0 = "PRAGMA table_info(Клиент)"; // SQLite: возвращает информацию о столбцах таблицы

                using (SQLiteCommand command = new SQLiteCommand(query0, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Добавляем имена столбцов в ComboBox
                            string columnName = reader["name"].ToString();

                            // Пропускаем столбец с именем "ID"
                            if (columnName != "ID")
                            {
                                comboBox1.Items.Add(columnName);
                            }
                        }
                    }
                }
                // Установить значение по умолчанию, если список не пуст
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить критерии поиска. Таблица не содержит столбцов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке критериев: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGruzComboData()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Тип_груза FROM Грузы";  // Запрос для получения всех типов груза

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // Очистить текущие элементы в ComboBox
                        comboBox2.Items.Clear();

                        while (reader.Read())
                        {
                            // Добавляем каждый тип груза в ComboBox
                            comboBox2.Items.Add(reader["Тип_груза"].ToString());
                        }
                    }
                }
            }

            // Привязать обработчик события для изменения выбора
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGruz = comboBox2.SelectedItem.ToString();
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Количество, Стоимость FROM Грузы WHERE Тип_груза = @Тип_груза";  // Запрос для получения данных выбранного груза

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Тип_груза", selectedGruz);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Получаем данные из базы
                            int quantity = Convert.ToInt32(reader["Количество"]);
                            decimal price = Convert.ToDecimal(reader["Стоимость"]);

                            // Устанавливаем значения в TextBox
                            textBox1.Text = quantity.ToString();
                            textBox8.Text = (quantity * price).ToString("F2");
                        }
                    }
                }
            }
        }

    }
}
