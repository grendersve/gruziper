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



namespace АИС_грузоперевозки
{
    public partial class Form3 : Form
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter adapter;
        private DataTable dt;

        public Form3()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadClientData();
            LoadCargoData();
            LoadContractData();
            LoadRouteData();
            LoadCarData();
            LoadDriverData();
            LoadPostavData();
            LoadComboBox4FromAuto();
            LoadComboBox4FromClient();
            LoadComboBox4FromDriver();
            LoadComboBox4FromPost();
            LoadGruzComboData();
            LoadSearchCriteria();
            LoadDeleteContractData();
            InitializeFormComponents();
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            dataGridView3.ReadOnly = true;
            dataGridView4.ReadOnly = true;
            dataGridView5.ReadOnly = true;
            dataGridView6.ReadOnly = true;
            dataGridView7.ReadOnly = true;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;
            dataGridView3.SelectionChanged += dataGridView3_SelectionChanged;
            dataGridView4.SelectionChanged += dataGridView4_SelectionChanged;
            dataGridView5.SelectionChanged += dataGridView5_SelectionChanged;
            dataGridView6.SelectionChanged += dataGridView6_SelectionChanged;
            dataGridView7.SelectionChanged += dataGridView7_SelectionChanged;

            comboBox3.Items.AddRange(new object[] { "Mercedes-Benz", "Volvo", "Scania", "MAN", "DAF", "Renault", "Iveco", "Freightliner", "Kenworth", "Peterbilt", "ГАЗ", "ГАЗ", "ГАЗ", "LADA", "Peugeot" });
            comboBox5.Items.AddRange(new object[] { "Грузовой", "Легковой" });
            comboBox7.Items.AddRange(new object[] { "В рабочем состоянии", "Требует ремонта", "Находится на ремонте" });

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

        private void LoadCargoData()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Грузы";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable clientsTable = new DataTable();
                        adapter.Fill(clientsTable);
                        dataGridView2.DataSource = clientsTable;
                        dataGridView2.Columns["ID"].Visible = false;
                    }
                }
            }
        }

        private void LoadContractData()
        {
            string dbPath ="C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Договор";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable contractsTable = new DataTable();
                        adapter.Fill(contractsTable);
                        dataGridView3.DataSource = contractsTable;
                        dataGridView3.Columns["ID"].Visible = false;
                    }
                }
            }
        }

        private void LoadDeleteContractData()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Удалённые_договоры";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable contractsTable = new DataTable();
                        adapter.Fill(contractsTable);
                        dataGridView8.DataSource = contractsTable;
                        dataGridView8.Columns["ID"].Visible = false;
                    }
                }
            }
        }

        private void LoadRouteData()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Маршрут";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable clientsTable = new DataTable();
                        adapter.Fill(clientsTable);
                        dataGridView4.DataSource = clientsTable;
                        dataGridView4.Columns["ID"].Visible = false;
                    }
                }
            }
        }

        private void LoadCarData()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Транспортное_средство";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable clientsTable = new DataTable();
                        adapter.Fill(clientsTable);
                        dataGridView6.DataSource = clientsTable;
                        dataGridView6.Columns["ID"].Visible = false;
                    }
                }
            }
        }

        private void LoadDriverData()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Водители";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable clientsTable = new DataTable();
                        adapter.Fill(clientsTable);
                        dataGridView5.DataSource = clientsTable;
                        dataGridView5.Columns["ID"].Visible = false;
                    }
                }
            }
        }

        private void LoadPostavData()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Поставщик";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable clientsTable = new DataTable();
                        adapter.Fill(clientsTable);
                        dataGridView7.DataSource = clientsTable;
                        dataGridView7.Columns["ID"].Visible = false;
                    }
                }
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string surname = dataGridView1.SelectedRows[0].Cells["Фамилия"].Value.ToString(); // Используем фамилию как уникальный идентификатор
                string name = dataGridView1.SelectedRows[0].Cells["Имя"].Value.ToString();
                string lastName = dataGridView1.SelectedRows[0].Cells["Отчество"].Value.ToString();

                if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    !IsValidName(textBox2.Text) ||
                    !IsValidName(textBox3.Text) ||
                    !IsValidName(textBox4.Text))
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

                string phonePattern = @"^(\+7|8)\d{10}$";
                if (!Regex.IsMatch(textBox7.Text, phonePattern))
                {
                    MessageBox.Show("Неверный формат номера телефона.");
                    return;
                }

                UpdateClientDataInDatabase(
                    surname, // Используем фамилию как уникальный идентификатор
                    name,
                    lastName,
                    textBox5.Text,
                    textBox6.Text,
                    textBox7.Text,
                    textBox36.Text,
                    textBox37.Text
                );

                LoadClientData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для редактирования.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string surname = dataGridView1.SelectedRows[0].Cells["Фамилия"].Value.ToString(); // Используем фамилию как уникальный идентификатор

                bool isConnectionAlreadyOpen = connection.State == System.Data.ConnectionState.Open;

                if (!isConnectionAlreadyOpen)
                {
                    connection.Open();
                }

                try
                {
                    // Удаляем клиента
                    DeleteClientDataFromDatabase(surname); // Используем фамилию для удаления

                    // Перенумерация ID
                    using (SQLiteCommand reorderCmd = new SQLiteCommand("UPDATE Клиент SET ID = (SELECT COUNT(*) FROM Клиент k2 WHERE k2.ID < Клиент.ID) + 1", connection))
                    {
                        reorderCmd.ExecuteNonQuery();
                    }

                    // Обновление данных
                    LoadClientData();
                }
                finally
                {
                    if (!isConnectionAlreadyOpen)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для удаления.");
            }
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

        private void UpdateClientDataInDatabase(string surname, string name, string lastName, string seria, string number, string telephone, string mail, string address)
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
                    command.CommandText = "UPDATE Клиент SET Фамилия = @surname, Имя = @name, Отчество = @lastName, [Серия_паспорта] = @seria, [Номер_паспорта] = @number, [Номер_телефона] = @telephone, Почта = @mail, Адрес = @address WHERE Фамилия = @surname"; 
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

        private void DeleteClientDataFromDatabase(string surname)
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
                    command.CommandText = "DELETE FROM Клиент WHERE Фамилия = @surname"; // Используем фамилию
                    command.Parameters.AddWithValue("@surname", surname);
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

        private void button5_Click(object sender, EventArgs e)
        {
            // Получаем значения из полей ввода
            string weight = textBox9.Text;
            string type_cargo = textBox10.Text;
            string volume = textBox11.Text;
            string senderName = textBox12.Text;
            string recipient = textBox38.Text;

            // Валидация данных
            if (string.IsNullOrEmpty(weight) || string.IsNullOrEmpty(type_cargo) ||
                string.IsNullOrEmpty(senderName) || string.IsNullOrEmpty(recipient))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            // Проверка на корректность веса и объема (например, положительные числа с текстом)
            if (!IsValidWeightOrVolume(weight))
            {
                MessageBox.Show("Вес должен быть положительным числом, возможно с единицей измерения (например, 'кг').");
                return;
            }

            if (!IsValidWeightOrVolume(volume))
            {
                MessageBox.Show("Объем должен быть положительным числом, возможно с единицей измерения (например, 'литры').");
                return;
            }

            // Вставляем данные в базу данных
            InsertCargoDataToDatabase(weight, type_cargo, volume, senderName, recipient);

            // Обновляем DataGridView
            LoadCargoData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку в DataGridView
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // Проверка на заполненность полей
                string weight = textBox9.Text;
                string type_cargo = textBox10.Text;
                string volume = textBox11.Text;
                string cost = textBox12.Text;
                string quantity = textBox38.Text;

                if (string.IsNullOrEmpty(weight) || string.IsNullOrEmpty(type_cargo) ||
                    string.IsNullOrEmpty(cost) || string.IsNullOrEmpty(quantity))
                {
                    MessageBox.Show("Заполните все поля.");
                    return;
                }

                // Проверка на корректность
                if (!IsValidWeightOrVolume(weight))
                {
                    MessageBox.Show("Вес должен быть положительным числом, возможно с единицей измерения (например, 'кг').");
                    return;
                }

                // Проверяем, что объем — это число (включая 0)
                if (!IsValidVolume(volume))
                {
                    MessageBox.Show("Объем должен быть положительным числом или 0, возможно с единицей измерения (например, 'литры').");
                    return;
                }

                // Обновляем данные в базе данных
                UpdateCargoDataInDatabase(
                    selectedRow.Cells[0].Value.ToString(), // ID
                    weight,
                    type_cargo,
                    volume,
                    cost,
                    quantity
                );

                LoadCargoData();
            }
            else
            {
                MessageBox.Show("Выберите данные.");
            }
        }

        private bool IsValidVolume(string volume)
        {
            // Проверка, что значение является числом и может быть равно 0 или больше
            return decimal.TryParse(volume, out decimal result) && result >= 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку в DataGridView
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // Подтверждение удаления
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот элемент?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Удаляем данные из базы данных
                    DeleteCargoDataFromDatabase(selectedRow.Cells[0].Value.ToString());

                    // Обновляем DataGridView
                    LoadCargoData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
        }

        private bool IsValidWeightOrVolume(string input)
        {
            // Регулярное выражение для извлечения числовой части перед единицей измерения
            string numericPart = System.Text.RegularExpressions.Regex.Match(input, @"\d+(\.\d+)?").Value;

            // Проверка, что числовая часть является положительным числом
            if (decimal.TryParse(numericPart, out decimal result) && result > 0)
            {
                return true;
            }

            return false;
        }

        private void InsertCargoDataToDatabase(string weight, string type_cargo, string volume, string cost, string quantity)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Грузы (Вес, Тип_груза, Объем, Стоимость, Количество) VALUES (@weight, @type_cargo, @volume, @cost, @quantity)";
                command.Parameters.AddWithValue("@weight", weight);
                command.Parameters.AddWithValue("@type_cargo", type_cargo);
                command.Parameters.AddWithValue("@volume", volume);
                command.Parameters.AddWithValue("@cost", cost);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.ExecuteNonQuery();
            }
        }

        private void UpdateCargoDataInDatabase(string id, string weight, string type_cargo, string volume, string cost, string quantity)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Грузы SET Вес = @weight, Тип_груза = @type_cargo, Объем = @volume, Стоимость = @cost, Количество = @quantity WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@weight", weight);
                command.Parameters.AddWithValue("@type_cargo", type_cargo);
                command.Parameters.AddWithValue("@volume", volume);
                command.Parameters.AddWithValue("@cost", cost);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.ExecuteNonQuery();
            }
        }

        private void DeleteCargoDataFromDatabase(string id)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM Грузы WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // Заполняем текстовые поля данными из выбранной строки
                textBox9.Text = selectedRow.Cells["Вес"].Value.ToString();
                textBox10.Text = selectedRow.Cells["Тип_груза"].Value.ToString();
                textBox11.Text = selectedRow.Cells["Объем"].Value.ToString();
                textBox12.Text = selectedRow.Cells["Стоимость"].Value.ToString();
                textBox38.Text = selectedRow.Cells["Количество"].Value.ToString();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность полей
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox8.Text) ||
                string.IsNullOrEmpty(textBox17.Text) || string.IsNullOrEmpty(comboBox2.Text) ||
                string.IsNullOrEmpty(textBox15.Text) || string.IsNullOrEmpty(textBox18.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            // Валидация цены (положительное число)
            if (!decimal.TryParse(textBox1.Text, out decimal parsedPrice) || parsedPrice <= 0)
            {
                MessageBox.Show("Цена должна быть положительным числом.");
                return;
            }

            // Валидация даты (формат yyyy-MM-dd)
            if (!DateTime.TryParse(textBox15.Text, out DateTime parsedDate))
            {
                MessageBox.Show("Введите корректную дату в формате yyyy-MM-dd.");
                return;
            }

            // Получаем значения из полей ввода
            string price = textBox1.Text;
            string date_order = textBox15.Text;
            string customer_name = comboBox4.SelectedItem?.ToString() ?? "Не выбрано";
            string driver_name = comboBox9.SelectedItem?.ToString() ?? "Не выбрано";
            string otp_name = comboBox8.SelectedItem?.ToString() ?? "Не выбрано";
            string numder_customer = textBox18.Text;
            string number_driver = textBox17.Text;
            string name_auto = comboBox2.SelectedItem?.ToString() ?? "Не выбрано";
            string gruz = comboBox6.SelectedItem?.ToString() ?? "Не выбрано";
            string number_postav = textBox8.Text;
            string punct_otp = textBox19.Text;
            string punct_naz = textBox23.Text;

            // Получаем ID выбранного клиента из DataGridView
            DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];
            string clientId = selectedRow.Cells["ID"].Value.ToString();  // ID из скрытого столбца

            try
            {
                // Вставляем данные в базу данных
                UpdateContractDataInDatabase(clientId, price, date_order, customer_name, driver_name, otp_name, numder_customer, number_driver, name_auto, gruz, punct_otp, punct_naz, number_postav);

                // Обновляем DataGridView
                LoadContractData();

                // Очищаем поля ввода после успешного выполнения
                ClearInputFields();

                // Показываем сообщение об успешном обновлении
                MessageBox.Show("Данные успешно обновлены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
            }

        }

        private void button12_Click(object sender, EventArgs e) /*Договор*/
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Получаем ID выбранной строки
                string id = dataGridView3.SelectedRows[0].Cells["ID"].Value.ToString();

                // Подтверждение удаления
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот договор?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Подключение к базе данных
                    string connectionString = @"Data Source=C:\Users\Даниил\Desktop\Gruzoperevozki.db;Version=3;";

                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        // Получаем данные о договоре для добавления в таблицу Удалённые_договоры
                        string selectQuery = "SELECT * FROM Договор WHERE ID = @ID";
                        using (var selectCommand = new SQLiteCommand(selectQuery, connection))
                        {
                            selectCommand.Parameters.AddWithValue("@ID", id);
                            using (var reader = selectCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Вставляем данные в таблицу Удалённые_договоры
                                    string insertQuery = @"INSERT INTO Удалённые_договоры (
                        ID, ФИО_Клиента, ФИО_Отправителя, ФИО_Водителя, 
                        Марка_и_Модель_Авто, Груз, Общая_стоимость, 
                        Дата_оформления_заказа, Номер_телефона_заказчика, 
                        Номер_телефона_водителя, Номер_телефона_поставщика, 
                        Пункт_отправления, Пункт_назначения)
                        VALUES (
                        @ID, @ФИО_Клиента, @ФИО_Отправителя, @ФИО_Водителя, 
                        @Марка_и_Модель_Авто, @Груз, @Общая_стоимость, 
                        @Дата_оформления_заказа, @Номер_телефона_заказчика, 
                        @Номер_телефона_водителя, @Номер_телефона_поставщика, 
                        @Пункт_отправления, @Пункт_назначения)";

                                    using (var insertCommand = new SQLiteCommand(insertQuery, connection))
                                    {
                                        insertCommand.Parameters.AddWithValue("@ID", reader["ID"]);
                                        insertCommand.Parameters.AddWithValue("@ФИО_Клиента", reader["ФИО_Клиента"]);
                                        insertCommand.Parameters.AddWithValue("@ФИО_Отправителя", reader["ФИО_Поставщика"]);  // Исправлено на "ФИО_Отправителя"
                                        insertCommand.Parameters.AddWithValue("@ФИО_Водителя", reader["ФИО_Водителя"]);
                                        insertCommand.Parameters.AddWithValue("@Марка_и_Модель_Авто", reader["Марка_и_Модель_Авто"]);
                                        insertCommand.Parameters.AddWithValue("@Груз", reader["Груз"]);
                                        insertCommand.Parameters.AddWithValue("@Общая_стоимость", reader["Общая_стоимость"]);
                                        insertCommand.Parameters.AddWithValue("@Дата_оформления_заказа", reader["Дата_оформления_заказа"]);
                                        insertCommand.Parameters.AddWithValue("@Номер_телефона_заказчика", reader["Номер_телефона_заказчика"]);
                                        insertCommand.Parameters.AddWithValue("@Номер_телефона_водителя", reader["Номер_телефона_водителя"]);
                                        insertCommand.Parameters.AddWithValue("@Номер_телефона_поставщика", reader["Номер_телефона_поставщика"]);
                                        insertCommand.Parameters.AddWithValue("@Пункт_отправления", reader["Пункт_отправления"]);
                                        insertCommand.Parameters.AddWithValue("@Пункт_назначения", reader["Пункт_назначения"]);

                                        insertCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        // Удаление договора из таблицы Договор
                        string deleteQuery = "DELETE FROM Договор WHERE ID = @ID";
                        using (var deleteCommand = new SQLiteCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@ID", id);
                            deleteCommand.ExecuteNonQuery();
                        }
                    }

                    // Обновление таблицы договоров
                    LoadContractData();
                }
            }
            else
            {
                MessageBox.Show("Выберите договор для удаления.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            // Валидация цены
            if (!decimal.TryParse(textBox1.Text, out decimal parsedPrice) || parsedPrice <= 0)
            {
                MessageBox.Show("Цена должна быть положительным числом.");
                return;
            }

            // Валидация даты
            if (!DateTime.TryParse(textBox15.Text, out DateTime parsedDate))
            {
                MessageBox.Show("Введите корректную дату в формате yyyy-MM-dd.");
                return;
            }

            // Получаем значения из полей ввода
            string price = textBox1.Text;
            string date_order = textBox15.Text;
            string id_client = comboBox4.SelectedItem?.ToString() ?? "Не выбрано";
            string id_driver = comboBox9.SelectedItem?.ToString() ?? "Не выбрано";
            string id_otp = comboBox8.SelectedItem?.ToString() ?? "Не выбрано";
            string numder_customer = textBox18.Text;
            string number_driver = textBox17.Text;
            string id_auto = comboBox2.SelectedItem?.ToString() ?? "Не выбрано";
            string gruz = comboBox6.SelectedItem?.ToString() ?? "Не выбрано";
            string number_postav = textBox8.Text;
            string punct_otp = "Муром";
            string punct_naz = textBox23.Text;

            try
            {
                // Вставляем данные в базу данных
                InsertContractDataToDatabase(price, date_order, id_client, id_driver, id_otp, numder_customer, number_driver, id_auto, gruz, punct_otp, number_postav, punct_naz);

                // Обновляем DataGridView
                LoadContractData();

                // Очищаем поля ввода после успешного выполнения
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
        }

        private void ClearInputFields()
        {
            textBox1.Clear();
            textBox8.Clear();
            textBox15.Clear();
            textBox17.Clear();
            textBox18.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;
            comboBox9.SelectedIndex = -1;
        }

        private void InsertContractDataToDatabase(string price, string date_order, string id_client, string id_driver,string id_otp,string numder_customer,string number_driver, string id_auto,string gruz,string punct_otp,string number_postav,string punct_naz)
        {
            try
            {
                // Проверка, если соединение не открыто - открываем его
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();  // Открытие соединения
                }

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    // Убедитесь, что все параметры добавлены в команду
                    punct_otp = "Муром";
                    command.CommandText = "INSERT INTO Договор (Груз, Общая_стоимость, ФИО_Клиента, ФИО_Водителя, ФИО_Поставщика, Дата_оформления_заказа, Номер_телефона_заказчика, Марка_и_Модель_Авто, Номер_телефона_водителя, Номер_телефона_поставщика, Пункт_отправления, Пункт_назначения) " +
                                          "VALUES (@gruz, @price, @id_client, @id_driver, @id_otp, @date_order, @numder_customer, @id_auto, @number_driver, @number_postav, @punct_otp, @punct_naz)";

                    command.Parameters.AddWithValue("@gruz", gruz);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@date_order", date_order);
                    command.Parameters.AddWithValue("@id_client", id_client);  // ID клиента
                    command.Parameters.AddWithValue("@id_driver", id_driver);  // ID водителя
                    command.Parameters.AddWithValue("@id_otp", id_otp);  // ID поставщика
                    command.Parameters.AddWithValue("@numder_customer", numder_customer);
                    command.Parameters.AddWithValue("@number_driver", number_driver);
                    command.Parameters.AddWithValue("@id_auto", id_auto);  // ID транспортного средства
                    command.Parameters.AddWithValue("@number_postav", number_postav);
                    command.Parameters.AddWithValue("@punct_otp", punct_otp);
                    command.Parameters.AddWithValue("@punct_naz", punct_naz);

                    // Выполнение запроса
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных в базу: " + ex.Message);
            }
            finally
            {
                // Закрытие соединения, даже если произошла ошибка
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void UpdateContractDataInDatabase(string id, string price, string date_order, string id_client, string id_driver, string id_otp, string numder_customer, string number_driver, string id_auto, string gruz, string number_postav, string punct_otp, string punct_naz)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "UPDATE Договор SET Груз = @gruz, Общая_стоимость = @price, ФИО_Клиента = @id_client, ФИО_Водителя = @id_driver, ФИО_Поставщика = @id_otp, " +
                                          "Дата_оформления_заказа = @date_order, Номер_телефона_заказчика = @numder_customer, Марка_и_Модель_Авто = @id_auto, Номер_телефона_водителя = @number_driver, " +
                                          "Номер_телефона_поставщика = @number_postav, Пункт_отправления = @punct_otp, Пункт_назначения = @punct_naz " +
                                          "WHERE ID = @id";

                    // Добавление параметров
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@gruz", gruz);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@date_order", date_order);
                    command.Parameters.AddWithValue("@id_client", id_client);  // ID клиента
                    command.Parameters.AddWithValue("@id_driver", id_driver);  // ID водителя
                    command.Parameters.AddWithValue("@id_otp", id_otp);  // ID поставщика
                    command.Parameters.AddWithValue("@numder_customer", numder_customer);
                    command.Parameters.AddWithValue("@number_driver", number_driver);
                    command.Parameters.AddWithValue("@id_auto", id_auto);  // ID транспортного средства
                    command.Parameters.AddWithValue("@number_postav", number_postav);
                    command.Parameters.AddWithValue("@punct_otp", punct_otp);
                    command.Parameters.AddWithValue("@punct_naz", punct_naz);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении данных в базе: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];

                comboBox4.SelectedItem = selectedRow.Cells["ФИО_Клиента"].Value?.ToString() ?? "";
                comboBox8.SelectedItem = selectedRow.Cells["ФИО_Поставщика"].Value?.ToString() ?? "";
                comboBox9.SelectedItem = selectedRow.Cells["ФИО_Водителя"].Value?.ToString() ?? "";
                comboBox2.SelectedItem = selectedRow.Cells["Марка_и_Модель_Авто"].Value?.ToString() ?? "";
                comboBox6.SelectedItem = selectedRow.Cells["Груз"].Value?.ToString() ?? "";
                textBox1.Text = selectedRow.Cells["Общая_стоимость"].Value?.ToString() ?? "";
                textBox15.Text = selectedRow.Cells["Дата_оформления_заказа"].Value?.ToString() ?? "";
                textBox18.Text = selectedRow.Cells["Номер_телефона_заказчика"].Value?.ToString() ?? "";
                textBox17.Text = selectedRow.Cells["Номер_телефона_водителя"].Value?.ToString() ?? "";
                textBox8.Text = selectedRow.Cells["Номер_телефона_поставщика"].Value?.ToString() ?? "";
                textBox19.Text = "Муром";
                textBox23.Text = selectedRow.Cells["Пункт_назначения"].Value?.ToString() ?? "";

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность полей
            if (string.IsNullOrWhiteSpace(textBox25.Text) ||
                string.IsNullOrWhiteSpace(textBox26.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            // Получаем значения из полей ввода
            
            string start = textBox25.Text;
            string finish = textBox26.Text;
            

            // Вставляем данные в базу данных
            InsertRouteDataToDatabase( start, finish);

            // Обновляем DataGridView
            LoadRouteData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Проверка на выбор строки в DataGridView
            if (dataGridView4.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для обновления.");
                return;
            }

            // Проверка на заполненность полей
            if (
                string.IsNullOrWhiteSpace(textBox25.Text) ||
                string.IsNullOrWhiteSpace(textBox26.Text))
                
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            // Получаем выбранную строку в DataGridView
            DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

            // Обновляем данные в базе данных
            UpdateRouteDataInDatabase(
                selectedRow.Cells[0].Value.ToString(),
                textBox25.Text,
                textBox26.Text
            );

            // Обновляем DataGridView
            LoadRouteData();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Проверка на выбор строки в DataGridView
            if (dataGridView4.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления.");
                return;
            }

            // Получаем выбранную строку в DataGridView
            DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

            // Удаляем данные из базы данных
            DeleteRouteDataFromDatabase(selectedRow.Cells[0].Value.ToString());

            // Обновляем DataGridView
            LoadRouteData();
        }

        private void InsertRouteDataToDatabase(string start, string finish)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Маршрут (Начальная_точка, Конечная_точка, ) VALUES (@start, @finish)";
                command.Parameters.AddWithValue("@start", start);
                command.Parameters.AddWithValue("@finish", finish);
                command.ExecuteNonQuery();
            }
        }

        private void UpdateRouteDataInDatabase(string id, string start, string finish)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Маршрут SET Начальная_точка = @start, Конечная_точка = @finish WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@start", start);
                command.Parameters.AddWithValue("@finish", finish);
                command.ExecuteNonQuery();
            }
        }

        private void DeleteRouteDataFromDatabase(string id)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM Маршрут WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        //Транспортное средство
        private void button21_Click(object sender, EventArgs e)
        {
            // Проверка заполненности полей
            if (string.IsNullOrWhiteSpace(comboBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox20.Text) ||
                string.IsNullOrWhiteSpace(textBox16.Text) ||
                string.IsNullOrWhiteSpace(comboBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox13.Text) ||
                string.IsNullOrWhiteSpace(comboBox7.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка");
                return;
            }

            // Получаем значения из полей ввода
            string name_auto = comboBox3.Text;
            string name = textBox16.Text;
            string number_auto = textBox20.Text;
            string type_transport = comboBox5.Text;
            string load = textBox13.Text;
            string condition = comboBox7.Text;

            // Вставляем данные в базу данных
            InsertCarDataToDatabase(name_auto, name, number_auto, type_transport, load, condition);

            // Очистка всех текстовых полей и комбобоксов
            comboBox3.SelectedIndex = -1;
            textBox16.Clear();
            textBox20.Clear();
            comboBox5.SelectedIndex = -1;
            textBox13.Clear();
            comboBox7.SelectedIndex = -1;

            // Обновляем DataGridView
            LoadCarData();
        }


        private void InsertCarDataToDatabase(string name_auto, string name, string number_auto, string type_transport, string load, string condition)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Вставляем данные без ID, чтобы SQLite сама присваивала ID
                command.CommandText = "INSERT INTO Транспортное_средство (Марка, Модель, Госномер, Тип_транспорта, Грузоподъемность, Техническое_состояние) " +
                                       "VALUES (@name_auto, @name, @number_auto, @type_transport, @load, @condition)";
                command.Parameters.AddWithValue("@name_auto", name_auto);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@number_auto", number_auto);
                command.Parameters.AddWithValue("@type_transport", type_transport);
                command.Parameters.AddWithValue("@load", load);
                command.Parameters.AddWithValue("@condition", condition);

                command.ExecuteNonQuery();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (dataGridView6.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView6.SelectedRows[0];

                // Проверка заполненности полей
                if (string.IsNullOrWhiteSpace(comboBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox20.Text) ||
                string.IsNullOrWhiteSpace(textBox16.Text) ||
                string.IsNullOrWhiteSpace(comboBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox13.Text) ||
                string.IsNullOrWhiteSpace(comboBox7.Text))
                {
                    MessageBox.Show("Все поля должны быть заполнены для обновления данных.", "Ошибка");
                    return;
                }

                // Обновляем данные в базе данных
                UpdateCarDataInDatabase(
                    selectedRow.Cells[0].Value.ToString(),
                    comboBox3.Text,
                    textBox20.Text,
                    textBox16.Text,
                    comboBox5.Text,
                    textBox13.Text,
                    comboBox7.Text
                );

                // Обновляем DataGridView
                LoadCarData();
            }
            else
            {
                MessageBox.Show("Выберите строку для обновления данных.", "Ошибка");
            }
        }

        private void UpdateCarDataInDatabase(string id, string name_auto, string name, string number_auto, string type_transport, string load, string condition)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Транспортное_средство SET Марка = @name_auto, Модель=@name, Госномер = @number_auto, Тип_транспорта = @type_transport, " +
                                       "Грузоподъемность = @load, Техническое_состояние = @condition WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name_auto", name_auto);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@number_auto", number_auto);
                command.Parameters.AddWithValue("@type_transport", type_transport);
                command.Parameters.AddWithValue("@load", load);
                command.Parameters.AddWithValue("@condition", condition);

                command.ExecuteNonQuery();
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (dataGridView6.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView6.SelectedRows[0];

                // Удаляем данные из базы
                DeleteCarDataFromDatabase(selectedRow.Cells[0].Value.ToString());

                // Обновляем DataGridView
                LoadCarData();
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка");
            }
        }

        private void DeleteCarDataFromDatabase(string id)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM Транспортное_средство WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
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

                        // Проверяем, найдены ли данные
                        if (searchResults.Rows.Count == 0)
                        {
                            throw new Exception("Данные не найдены. Проверьте введённые критерии.");
                        }

                        // Обновляем DataGridView
                        dataGridView1.DataSource = searchResults;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            // Проверка, что строка выбрана
            if (dataGridView4.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

                // Заполняем текстовые поля данными из выбранной строки
                textBox25.Text = selectedRow.Cells["Начальная_точка"].Value.ToString();
                textBox26.Text = selectedRow.Cells["Конечная_точка"].Value.ToString();
            }
            else
            {
                // Очищаем поля, если строка не выбрана
                textBox25.Clear();
                textBox26.Clear();
            }
        }

        private void DataGridView6_SelectionChanged(object sender, EventArgs e)
        {
            // Проверка, что строка выбрана
            if (dataGridView6.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView6.SelectedRows[0];

                // Проверяем и заполняем comboBox3 (Марка)
                if (selectedRow.Cells["Марка"] != null && selectedRow.Cells["Марка"].Value != null)
                {
                    string selectedMark = selectedRow.Cells["Марка"].Value.ToString();
                    if (!string.IsNullOrEmpty(selectedMark) && comboBox3.Items.Contains(selectedMark))
                    {
                        comboBox3.SelectedItem = selectedMark;
                    }
                    else
                    {
                        comboBox3.SelectedIndex = -1;
                        MessageBox.Show("Не найдено совпадение для марки.");
                    }
                }

                // Заполняем textBox20 (Госномер)
                if (selectedRow.Cells["Госномер"] != null && selectedRow.Cells["Госномер"].Value != null)
                {
                    textBox20.Text = selectedRow.Cells["Госномер"].Value.ToString();
                }
                else
                {
                    textBox20.Clear();
                }

                // Проверяем и заполняем comboBox5 (Тип транспорта)
                if (selectedRow.Cells["Тип_транспорта"] != null && selectedRow.Cells["Тип_транспорта"].Value != null)
                {
                    string selectedType = selectedRow.Cells["Тип_транспорта"].Value.ToString();
                    if (!string.IsNullOrEmpty(selectedType) && comboBox5.Items.Contains(selectedType))
                    {
                        comboBox5.SelectedItem = selectedType;
                    }
                    else
                    {
                        comboBox5.SelectedIndex = -1;
                        MessageBox.Show("Не найдено совпадение для типа транспорта.");
                    }
                }

                // Проверяем и заполняем comboBox6 (Грузоподъемность)
                if (selectedRow.Cells["Грузоподъемность"] != null && selectedRow.Cells["Грузоподъемность"].Value != null)
                {
                    string selectedCapacity = selectedRow.Cells["Грузоподъемность"].Value.ToString();
                    if (!string.IsNullOrEmpty(selectedCapacity))
                    {
                        textBox13.Text = selectedCapacity;
                    }
                    else
                    {
                        textBox13.Clear();
                        MessageBox.Show("Поле должно быть заполнено.");
                    }
                }


                // Проверяем и заполняем comboBox7 (Техническое состояние)
                if (selectedRow.Cells["Техническое_состояние"] != null && selectedRow.Cells["Техническое_состояние"].Value != null)
                {
                    string selectedCondition = selectedRow.Cells["Техническое_состояние"].Value.ToString();
                    if (!string.IsNullOrEmpty(selectedCondition) && comboBox7.Items.Contains(selectedCondition))
                    {
                        comboBox7.SelectedItem = selectedCondition;
                    }
                    else
                    {
                        comboBox7.SelectedIndex = -1;
                        MessageBox.Show("Не найдено совпадение для технического состояния.");
                    }
                }
            }
            else
            {
                // Очищаем поля, если строка не выбрана
                comboBox3.SelectedIndex = -1;
                comboBox5.SelectedIndex = -1;
                textBox13.Clear();
                comboBox7.SelectedIndex = -1;
                textBox20.Clear();
            }
        }

        
        //Водители
        private void button14_Click(object sender, EventArgs e)
        {
            // Проверка корректности ввода
            if (!IsValidName(textBox28.Text) || !IsValidName(textBox22.Text) || !IsValidName(textBox29.Text))
            {
                MessageBox.Show("Фамилия, Имя и Отчество должны содержать только буквы.");
                return;
            }

            string phonePattern = @"^(\+7|8)\d{10}$";
            if (!Regex.IsMatch(textBox30.Text, phonePattern))
            {
                MessageBox.Show("Неверный формат номера телефона.");
                return;
            }

            if (!int.TryParse(textBox14.Text, out int experience) || experience < 0)
            {
                MessageBox.Show("Стаж должен быть числом и неотрицательным.");
                return;
            }

            // Вставка данных
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Проверка на открытое соединение
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                command.CommandText = "INSERT INTO Водители (Имя, Фамилия, Отчество, Телефон, Стаж) VALUES (@name, @surname, @lastName, @phone, @experience)";
                command.Parameters.AddWithValue("@name", textBox22.Text);
                command.Parameters.AddWithValue("@surname", textBox28.Text);
                command.Parameters.AddWithValue("@lastName", textBox29.Text);
                command.Parameters.AddWithValue("@phone", textBox30.Text);
                command.Parameters.AddWithValue("@experience", experience);

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Водитель успешно добавлен.");
            LoadDriverData(); // Обновление таблицы
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count > 0)
            {
                string id = dataGridView5.SelectedRows[0].Cells["ID"].Value?.ToString();

                // Проверка корректности ввода
                if (!IsValidName(textBox28.Text) || !IsValidName(textBox22.Text) || !IsValidName(textBox29.Text))
                {
                    MessageBox.Show("Фамилия, Имя и Отчество должны содержать только буквы.");
                    return;
                }

                string phonePattern = @"^(\+7|8)\d{10}$";
                if (!Regex.IsMatch(textBox30.Text, phonePattern))
                {
                    MessageBox.Show("Неверный формат номера телефона.");
                    return;
                }

                if (!int.TryParse(textBox14.Text, out int experience) || experience < 0)
                {
                    MessageBox.Show("Стаж должен быть числом и неотрицательным.");
                    return;
                }

                // Обновление данных
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();

                    command.CommandText = "UPDATE Водители SET Имя = @name, Фамилия = @surname, Отчество = @lastName, Телефон = @phone, Стаж = @experience WHERE ID = @id";
                    command.Parameters.AddWithValue("@name", textBox22.Text);
                    command.Parameters.AddWithValue("@surname", textBox28.Text);
                    command.Parameters.AddWithValue("@lastName", textBox29.Text);
                    command.Parameters.AddWithValue("@phone", textBox30.Text);
                    command.Parameters.AddWithValue("@experience", experience);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("Данные водителя успешно обновлены.");
                LoadDriverData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите водителя для редактирования.");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();

                // Удаление данных
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    connection.Open();
                    command.CommandText = "DELETE FROM Водители WHERE ID = @id";
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("Водитель успешно удалён.");
                LoadDriverData(); // Обновление таблицы
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите водителя для удаления.");
            }
        }

        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            // Проверка, что есть хотя бы одна выделенная строка
            if (dataGridView5.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView5.SelectedRows[0];

                // Заполнение текстбоксов данными из выбранной строки
                textBox22.Text = selectedRow.Cells["Имя"].Value?.ToString() ?? "";
                textBox28.Text = selectedRow.Cells["Фамилия"].Value?.ToString() ?? "";
                textBox29.Text = selectedRow.Cells["Отчество"].Value?.ToString() ?? "";
                textBox30.Text = selectedRow.Cells["Телефон"].Value?.ToString() ?? "";
                textBox14.Text = selectedRow.Cells["Стаж"].Value?.ToString() ?? "";
            }
        }

        //Отправители
        private void button18_Click(object sender, EventArgs e)
        {
            // Проверка корректности ввода
            if (!IsValidName(textBox31.Text) || !IsValidName(textBox32.Text) || !IsValidName(textBox33.Text))
            {
                MessageBox.Show("Фамилия, Имя и Отчество должны содержать только буквы.");
                return;
            }

            string phonePattern = @"^(\+7|8)\d{10}$";
            if (!Regex.IsMatch(textBox34.Text, phonePattern))
            {
                MessageBox.Show("Неверный формат номера телефона.");
                return;
            }

            // Вставка данных
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Проверка на открытое соединение
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                command.CommandText = "INSERT INTO Поставщик (Имя, Фамилия, Отчество, Телефон, Адрес) VALUES (@name, @surname, @lastName, @phone, @address)";
                command.Parameters.AddWithValue("@name", textBox32.Text);
                command.Parameters.AddWithValue("@surname", textBox31.Text);
                command.Parameters.AddWithValue("@lastName", textBox33.Text);
                command.Parameters.AddWithValue("@phone", textBox34.Text);
                command.Parameters.AddWithValue("@address", textBox35.Text);

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Поставщик успешно добавлен.");
            LoadPostavData(); // Обновление таблицы
        }

        // Изменить запись
        private void button19_Click(object sender, EventArgs e)
        {
            if (dataGridView7.SelectedRows.Count > 0)
            {
                string id = dataGridView7.SelectedRows[0].Cells["ID"].Value?.ToString();

                // Проверка корректности ввода
                if (!IsValidName(textBox31.Text) || !IsValidName(textBox32.Text) || !IsValidName(textBox33.Text))
                {
                    MessageBox.Show("Фамилия, Имя и Отчество должны содержать только буквы.");
                    return;
                }

                string phonePattern = @"^(\+7|8)\d{10}$";
                if (!Regex.IsMatch(textBox34.Text, phonePattern))
                {
                    MessageBox.Show("Неверный формат номера телефона.");
                    return;
                }

                // Обновление данных
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();

                    command.CommandText = "UPDATE Поставщик SET Имя = @name, Фамилия = @surname, Отчество = @lastName, Телефон = @phone, Адрес = @address WHERE ID = @id";
                    command.Parameters.AddWithValue("@name", textBox32.Text);
                    command.Parameters.AddWithValue("@surname", textBox31.Text);
                    command.Parameters.AddWithValue("@lastName", textBox33.Text);
                    command.Parameters.AddWithValue("@phone", textBox34.Text);
                    command.Parameters.AddWithValue("@address", textBox35.Text);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("Данные поставщика успешно обновлены.");
                LoadPostavData(); // Обновление таблицы
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите поставщика для редактирования.");
            }
        }

        // Удалить запись
        private void button20_Click(object sender, EventArgs e)
        {
            if (dataGridView7.SelectedRows.Count > 0)
            {
                string id = dataGridView7.SelectedRows[0].Cells["ID"].Value.ToString();

                try
                {
                    // Удаление данных
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open(); // Открываем соединение, только если оно закрыто
                        }

                        command.CommandText = "DELETE FROM Поставщик WHERE ID = @id";
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Поставщик успешно удалён.");
                    LoadPostavData(); // Обновление таблицы
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close(); // Закрываем соединение, если оно осталось открытым
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите поставщика для удаления.");
            }
        }


        // Заполнение текстбоксов при выборе строки
        private void dataGridView7_SelectionChanged(object sender, EventArgs e)
        {
            // Проверка, что есть хотя бы одна выделенная строка
            if (dataGridView7.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView7.SelectedRows[0];

                // Заполнение текстбоксов данными из выбранной строки
                textBox32.Text = selectedRow.Cells["Имя"].Value?.ToString() ?? "";
                textBox31.Text = selectedRow.Cells["Фамилия"].Value?.ToString() ?? "";
                textBox33.Text = selectedRow.Cells["Отчество"].Value?.ToString() ?? "";
                textBox34.Text = selectedRow.Cells["Телефон"].Value?.ToString() ?? "";
                textBox35.Text = selectedRow.Cells["Адрес"].Value?.ToString() ?? "";
            }
        }

        private void dataGridView6_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView6.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView6.SelectedRows[0];

                // Устанавливаем значения в textBox и comboBox
                comboBox3.Text = selectedRow.Cells["Марка"].Value?.ToString();       // Марка
                textBox16.Text = selectedRow.Cells["Модель"].Value?.ToString();     // Модель
                textBox20.Text = selectedRow.Cells["Госномер"].Value?.ToString();   // Госномер
                comboBox5.Text = selectedRow.Cells["Тип_транспорта"].Value?.ToString(); // Тип транспорта
                textBox13.Text = selectedRow.Cells["Грузоподъемность"].Value?.ToString(); // Грузоподъемность
                comboBox7.Text = selectedRow.Cells["Техническое_состояние"].Value?.ToString(); // Техническое состояние
            }
        }

        private void LoadComboBox4FromClient()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Запрос для объединения фамилии, имени и отчества
                    string query = "SELECT DISTINCT Фамилия || ' ' || Имя || ' ' || Отчество AS ФИО FROM Клиент";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        comboBox4.Items.Clear(); // Очистить ComboBox перед загрузкой данных
                        while (reader.Read())
                        {
                            // Добавляем объединённые ФИО в ComboBox
                            comboBox4.Items.Add(reader.GetString(0));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных в ComboBox1: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadComboBox4FromPost()
        {   
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Запрос для объединения фамилии, имени и отчества
                    string query = "SELECT DISTINCT Фамилия || ' ' || Имя || ' ' || Отчество AS ФИО FROM Поставщик";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        comboBox8.Items.Clear(); // Очистить ComboBox перед загрузкой данных
                        while (reader.Read())
                        {
                            // Добавляем объединённые ФИО в ComboBox
                            comboBox8.Items.Add(reader.GetString(0));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных в ComboBox1: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadComboBox4FromDriver()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Запрос для объединения фамилии, имени и отчества
                    string query = "SELECT DISTINCT Фамилия || ' ' || Имя || ' ' || Отчество AS ФИО FROM Водители";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        comboBox9.Items.Clear(); // Очистить ComboBox перед загрузкой данных
                        while (reader.Read())
                        {
                            // Добавляем объединённые ФИО в ComboBox
                            comboBox9.Items.Add(reader.GetString(0));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных в ComboBox1: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadComboBox4FromAuto()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Запрос для объединения фамилии, имени и отчества
                    string query = "SELECT DISTINCT Марка || ' ' || Модель AS Авто FROM Транспортное_средство";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        comboBox2.Items.Clear(); // Очистить ComboBox перед загрузкой данных
                        while (reader.Read())
                        {
                            // Добавляем объединённые ФИО в ComboBox
                            comboBox2.Items.Add(reader.GetString(0));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных в ComboBox1: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                        comboBox6.Items.Clear();

                        while (reader.Read())
                        {
                            // Добавляем каждый тип груза в ComboBox
                            comboBox6.Items.Add(reader["Тип_груза"].ToString());
                        }
                    }
                }
            }
            comboBox6.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
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
                string query1 = "PRAGMA table_info(Грузы)";
                string query2 = "PRAGMA table_info(Договор)";
                string query3 = "PRAGMA table_info(Маршрут)";
                string query4 = "PRAGMA table_info(Транспортное_средство)";
                string query5 = "PRAGMA table_info(Водители)";
                string query6 = "PRAGMA table_info(Поставщик)";
                string query7 = "PRAGMA table_info(Удалённые_договоры)";

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

                using (SQLiteCommand command = new SQLiteCommand(query7, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Добавляем имена столбцов в ComboBox
                            string columnName7 = reader["name"].ToString();

                            // Пропускаем столбец с именем "ID"
                            if (columnName7 != "ID")
                            {
                                comboBox16.Items.Add(columnName7);
                            }
                        }
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(query1, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Добавляем имена столбцов в ComboBox
                            string columnName1 = reader["name"].ToString();

                            // Пропускаем столбец с именем "ID"
                            if (columnName1 != "ID")
                            {
                                comboBox10.Items.Add(columnName1);
                            }
                        }
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(query2, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Добавляем имена столбцов в ComboBox
                            string columnName2 = reader["name"].ToString();

                            // Пропускаем столбец с именем "ID"
                            if (columnName2 != "ID")
                            {
                                comboBox11.Items.Add(columnName2);
                            }
                        }
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(query3, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Добавляем имена столбцов в ComboBox
                            string columnName3 = reader["name"].ToString();

                            // Пропускаем столбец с именем "ID"
                            if (columnName3 != "ID")
                            {
                                comboBox12.Items.Add(columnName3);
                            }
                        }
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(query4, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Добавляем имена столбцов в ComboBox
                            string columnName4 = reader["name"].ToString();

                            // Пропускаем столбец с именем "ID"
                            if (columnName4 != "ID")
                            {
                                comboBox13.Items.Add(columnName4);
                            }
                        }
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(query5, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Добавляем имена столбцов в ComboBox
                            string columnName5 = reader["name"].ToString();

                            // Пропускаем столбец с именем "ID"
                            if (columnName5 != "ID")
                            {
                                comboBox14.Items.Add(columnName5);
                            }
                        }
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(query6, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Добавляем имена столбцов в ComboBox
                            string columnName6 = reader["name"].ToString();

                            // Пропускаем столбец с именем "ID"
                            if (columnName6 != "ID")
                            {
                                comboBox15.Items.Add(columnName6);
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

        private void button25_Click(object sender, EventArgs e)
        {
            // Получаем выбранный критерий поиска
            string selectedCriteria = comboBox10.SelectedItem?.ToString();
            string searchValue = textBox24.Text.Trim();

            if (string.IsNullOrEmpty(selectedCriteria) || string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Выберите критерий и введите значение для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Формируем запрос поиска
                string query = $"SELECT * FROM Грузы WHERE {selectedCriteria} LIKE @searchValue";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable searchResults = new DataTable();
                        adapter.Fill(searchResults);

                        // Обновляем DataGridView
                        dataGridView2.DataSource = searchResults;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            // Очищаем поле поиска и сбрасываем выбранный критерий
            textBox24.Clear();
            comboBox10.SelectedIndex = -1; // Сбрасываем выбор в ComboBox

            try
            {
                // Формируем запрос для получения всех данных из таблицы Клиент
                string query = "SELECT * FROM Грузы";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable allData = new DataTable();
                        adapter.Fill(allData);

                        // Обновляем DataGridView с полными данными
                        dataGridView2.DataSource = allData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сбросе фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            // Получаем выбранный критерий поиска
            string selectedCriteria = comboBox11.SelectedItem?.ToString();
            string searchValue = textBox27.Text.Trim();

            if (string.IsNullOrEmpty(selectedCriteria) || string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Выберите критерий и введите значение для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Формируем запрос поиска
                string query = $"SELECT * FROM Договор WHERE {selectedCriteria} LIKE @searchValue";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable searchResults = new DataTable();
                        adapter.Fill(searchResults);

                        // Обновляем DataGridView
                        dataGridView3.DataSource = searchResults;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            // Очищаем поле поиска и сбрасываем выбранный критерий
            textBox27.Clear();
            comboBox11.SelectedIndex = -1; // Сбрасываем выбор в ComboBox

            try
            {
                // Формируем запрос для получения всех данных из таблицы Клиент
                string query = "SELECT * FROM Договор";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable allData = new DataTable();
                        adapter.Fill(allData);

                        // Обновляем DataGridView с полными данными
                        dataGridView3.DataSource = allData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сбросе фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            // Получаем выбранный критерий поиска
            string selectedCriteria = comboBox12.SelectedItem?.ToString();
            string searchValue = textBox39.Text.Trim();

            if (string.IsNullOrEmpty(selectedCriteria) || string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Выберите критерий и введите значение для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Формируем запрос поиска
                string query = $"SELECT * FROM Маршрут WHERE {selectedCriteria} LIKE @searchValue";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable searchResults = new DataTable();
                        adapter.Fill(searchResults);

                        // Обновляем DataGridView
                        dataGridView4.DataSource = searchResults;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            // Очищаем поле поиска и сбрасываем выбранный критерий
            textBox39.Clear();
            comboBox12.SelectedIndex = -1; // Сбрасываем выбор в ComboBox

            try
            {
                // Формируем запрос для получения всех данных из таблицы Клиент
                string query = "SELECT * FROM Маршрут";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable allData = new DataTable();
                        adapter.Fill(allData);

                        // Обновляем DataGridView с полными данными
                        dataGridView4.DataSource = allData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сбросе фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            // Получаем выбранный критерий поиска
            string selectedCriteria = comboBox13.SelectedItem?.ToString();
            string searchValue = textBox40.Text.Trim();

            if (string.IsNullOrEmpty(selectedCriteria) || string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Выберите критерий и введите значение для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Формируем запрос поиска
                string query = $"SELECT * FROM Транспортное_средство WHERE {selectedCriteria} LIKE @searchValue";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable searchResults = new DataTable();
                        adapter.Fill(searchResults);

                        // Обновляем DataGridView
                        dataGridView6.DataSource = searchResults;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            // Очищаем поле поиска и сбрасываем выбранный критерий
            textBox40.Clear();
            comboBox13.SelectedIndex = -1; // Сбрасываем выбор в ComboBox

            try
            {
                // Формируем запрос для получения всех данных из таблицы Клиент
                string query = "SELECT * FROM Транспортное_средство";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable allData = new DataTable();
                        adapter.Fill(allData);

                        // Обновляем DataGridView с полными данными
                        dataGridView6.DataSource = allData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сбросе фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            // Получаем выбранный критерий поиска
            string selectedCriteria = comboBox14.SelectedItem?.ToString();
            string searchValue = textBox41.Text.Trim();

            if (string.IsNullOrEmpty(selectedCriteria) || string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Выберите критерий и введите значение для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Формируем запрос поиска
                string query = $"SELECT * FROM Водители WHERE {selectedCriteria} LIKE @searchValue";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable searchResults = new DataTable();
                        adapter.Fill(searchResults);

                        // Обновляем DataGridView
                        dataGridView5.DataSource = searchResults;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            // Очищаем поле поиска и сбрасываем выбранный критерий
            textBox41.Clear();
            comboBox14.SelectedIndex = -1; // Сбрасываем выбор в ComboBox

            try
            {
                // Формируем запрос для получения всех данных из таблицы Клиент
                string query = "SELECT * FROM Водители";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable allData = new DataTable();
                        adapter.Fill(allData);

                        // Обновляем DataGridView с полными данными
                        dataGridView5.DataSource = allData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сбросе фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            // Получаем выбранный критерий поиска
            string selectedCriteria = comboBox15.SelectedItem?.ToString();
            string searchValue = textBox42.Text.Trim();

            if (string.IsNullOrEmpty(selectedCriteria) || string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Выберите критерий и введите значение для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Формируем запрос поиска
                string query = $"SELECT * FROM Поставщик WHERE {selectedCriteria} LIKE @searchValue";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable searchResults = new DataTable();
                        adapter.Fill(searchResults);

                        // Обновляем DataGridView
                        dataGridView7.DataSource = searchResults;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            // Очищаем поле поиска и сбрасываем выбранный критерий
            textBox42.Clear();
            comboBox15.SelectedIndex = -1; // Сбрасываем выбор в ComboBox

            try
            {
                // Формируем запрос для получения всех данных из таблицы Клиент
                string query = "SELECT * FROM Поставщик";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable allData = new DataTable();
                        adapter.Fill(allData);

                        // Обновляем DataGridView с полными данными
                        dataGridView7.DataSource = allData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сбросе фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) // Клиент
        {
            if (comboBox4.SelectedItem != null)
            {
                string selectedClient = comboBox4.SelectedItem.ToString();
                string connectionString = @"Data Source=C:\Users\Даниил\Desktop\Gruzoperevozki.db;Version=3;";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand("SELECT Номер_телефона FROM Клиент WHERE Фамилия || ' ' || Имя || ' ' || Отчество = @FullName", connection))
                    {
                        command.Parameters.AddWithValue("@FullName", selectedClient);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox18.Text = reader["Номер_телефона"].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e) // Водитель
        {
            if (comboBox9.SelectedItem != null)
            {
                string selectedDriver = comboBox9.SelectedItem.ToString();
                string connectionString = @"Data Source=C:\Users\Даниил\Desktop\Gruzoperevozki.db;Version=3;";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand("SELECT Телефон FROM Водители WHERE Фамилия || ' ' || Имя || ' ' || Отчество = @FullName", connection))
                    {
                        command.Parameters.AddWithValue("@FullName", selectedDriver);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox17.Text = reader["Телефон"].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e) // Поставщик
        {
            if (comboBox8.SelectedItem != null)
            {
                string selectedSupplier = comboBox8.SelectedItem.ToString();
                string connectionString = @"Data Source=C:\Users\Даниил\Desktop\Gruzoperevozki.db;Version=3;";


                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand("SELECT Телефон FROM Поставщик WHERE Фамилия || ' ' || Имя || ' ' || Отчество = @FullName", connection))
                    {
                        command.Parameters.AddWithValue("@FullName", selectedSupplier);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox8.Text = reader["Телефон"].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGruz = comboBox6.SelectedItem.ToString();
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
                            textBox43.Text = quantity.ToString();
                            textBox1.Text = (quantity * price).ToString("F2");
                        }
                    }
                }
            }
        }

        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox40_TextChanged(object sender, EventArgs e)
        {

        }

        private void button36_Click(object sender, EventArgs e)
        {
            // Получаем выбранный критерий поиска
            string selectedCriteria = comboBox16.SelectedItem?.ToString();
            string searchValue = textBox44.Text.Trim();

            if (string.IsNullOrEmpty(selectedCriteria) || string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Выберите критерий и введите значение для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Формируем запрос поиска
                string query = $"SELECT * FROM Удалённые_договоры WHERE {selectedCriteria} LIKE @searchValue";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable searchResults = new DataTable();
                        adapter.Fill(searchResults);

                        // Проверяем, найдены ли данные
                        if (searchResults.Rows.Count == 0)
                        {
                            throw new Exception("Данные не найдены. Проверьте введённые критерии.");
                        }

                        // Обновляем DataGridView
                        dataGridView8.DataSource = searchResults;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button37_Click(object sender, EventArgs e)
        {
            // Очищаем поле поиска и сбрасываем выбранный критерий
            textBox44.Clear();
            comboBox16.SelectedIndex = -1; // Сбрасываем выбор в ComboBox

            try
            {
                // Формируем запрос для получения всех данных из таблицы Клиент
                string query = "SELECT * FROM Удалённые_договоры";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable allData = new DataTable();
                        adapter.Fill(allData);

                        // Обновляем DataGridView с полными данными
                        dataGridView8.DataSource = allData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сбросе фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeFormComponents()
        {
            // Создаем объект MenuStrip
            MenuStrip menuStrip = new MenuStrip();

            // Создаем элементы меню
            ToolStripMenuItem refreshMenuItem = new ToolStripMenuItem("Обновить");
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Выход");

            // Привязываем обработчики событий к пунктам меню
            refreshMenuItem.Click += RefreshMenuItem_Click;
            exitMenuItem.Click += ExitMenuItem_Click;

            // Добавляем элементы меню в MenuStrip
            menuStrip.Items.Add(refreshMenuItem);
            menuStrip.Items.Add(exitMenuItem);

            // Устанавливаем MenuStrip для формы
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // Настройки формы
            this.Text = "Form3";
            this.Width = 600;
            this.Height = 300;
        }
        private void RefreshMenuItem_Click(object sender, EventArgs e)
        {
            // Логика для кнопки "Обновить"
            LoadClientData();
            LoadCargoData();
            LoadContractData();
            LoadRouteData();
            LoadCarData();
            LoadDriverData();
            LoadPostavData();
            MessageBox.Show("Данные обновлены", "Обновление");
        }
                
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form1 form1 = new Form1();  // Создаем новый экземпляр Form1
                form1.Show();  // Открываем Form1
                this.Hide();  // Скрываем текущую форму
            }
        }
    }
}

