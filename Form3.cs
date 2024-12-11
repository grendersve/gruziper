using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
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
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;
            dataGridView3.SelectionChanged += dataGridView3_SelectionChanged;
            dataGridView4.SelectionChanged += dataGridView4_SelectionChanged;
            dataGridView6.SelectionChanged += dataGridView6_SelectionChanged;

            string q1 = "SELECT ID, Фамилия || ' ' || Имя AS Full FROM Клиент";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(q1, connection);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "Full"; // Отображаемое значение в комбобоксе
            comboBox1.ValueMember = "ID";        // ID клиента
            comboBox2.Items.AddRange(new object[] { "Mercedes-Benz Actros", "Volvo FH", "Scania R-series", "MAN TGX", "DAF XF", "Renault Trucks T", "Iveco Stralis", "Freightliner Cascadia", "Kenworth T680", "Peterbilt 579" });
            comboBox3.Items.AddRange(new object[] { "Mercedes-Benz Actros", "Volvo FH", "Scania R-series", "MAN TGX", "DAF XF", "Renault Trucks T", "Iveco Stralis", "Freightliner Cascadia", "Kenworth T680", "Peterbilt 579", "ГАЗ-3302", "ГАЗ-33023 «ГАЗель-фермер»", "ГАЗ-3221", "LADA LARGUS" });
            comboBox5.Items.AddRange(new object[] { "Грузовой", "Легковой" });
            comboBox6.Items.AddRange(new object[] { "Менее 5 тонн", "Более 35 тонн" });
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
                    }
                }
            }
        }

        private void LoadContractData()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Договор";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable clientsTable = new DataTable();
                        adapter.Fill(clientsTable);
                        dataGridView3.DataSource = clientsTable;
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
                string clientId = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();

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
                    clientId,
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
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для редактирования.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string clientId = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();

                bool isConnectionAlreadyOpen = connection.State == System.Data.ConnectionState.Open;

                if (!isConnectionAlreadyOpen)
                {
                    connection.Open();
                }

                try
                {
                    // Удаляем клиента
                    DeleteClientDataFromDatabase(clientId);

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

        private void UpdateClientDataInDatabase(string id, string surname, string name, string lastName, string seria, string number, string telephone, string mail, string address)
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
                    command.CommandText = "UPDATE Клиент SET Фамилия = @surname, Имя = @name, Отчество = @lastName, [Серия_паспорта] = @seria, [Номер_паспорта] = @number, [Номер_телефона] = @telephone, Почта = @mail, Адрес = @address WHERE ID = @id";
                    command.Parameters.AddWithValue("@id", id);
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


        private void DeleteClientDataFromDatabase(string id)
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
                    command.CommandText = "DELETE FROM Клиент WHERE ID = @id";
                    command.Parameters.AddWithValue("@id", id);
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
                string senderName = textBox12.Text;
                string recipient = textBox38.Text;

                if (string.IsNullOrEmpty(weight) || string.IsNullOrEmpty(type_cargo) ||
                    string.IsNullOrEmpty(senderName) || string.IsNullOrEmpty(recipient))
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

                if (!IsValidWeightOrVolume(volume))
                {
                    MessageBox.Show("Объем должен быть положительным числом, возможно с единицей измерения (например, 'литры').");
                    return;
                }


                // Обновляем данные в базе данных
                UpdateCargoDataInDatabase(
                    selectedRow.Cells[0].Value.ToString(), // ID
                    weight,
                    type_cargo,
                    volume,
                    senderName,
                    recipient
                );

                LoadCargoData();
            }
            else
            {
                MessageBox.Show("Выберите данные.");
            }
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

        // Функция для проверки веса и объема
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


        private void InsertCargoDataToDatabase(string weight, string type_cargo, string volume, string senderName, string recipient)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Грузы (Вес, Тип_груза, Объем, Отправитель, Получатель) VALUES (@weight, @type_cargo, @volume, @senderName, @recipient)";
                command.Parameters.AddWithValue("@weight", weight);
                command.Parameters.AddWithValue("@type_cargo", type_cargo);
                command.Parameters.AddWithValue("@volume", volume);
                command.Parameters.AddWithValue("@senderName", senderName);
                command.Parameters.AddWithValue("@recipient", recipient);
                command.ExecuteNonQuery();
            }
        }

        private void UpdateCargoDataInDatabase(string id, string weight, string type_cargo, string volume, string senderName, string recipient)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Грузы SET Вес = @weight, Тип_груза = @type_cargo, Объем = @volume, Отправитель = @senderName, Получатель = @recipient WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@weight", weight);
                command.Parameters.AddWithValue("@type_cargo", type_cargo);
                command.Parameters.AddWithValue("@volume", volume);
                command.Parameters.AddWithValue("@senderName", senderName);
                command.Parameters.AddWithValue("@recipient", recipient);
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
                textBox12.Text = selectedRow.Cells["Отправитель"].Value.ToString();
                textBox38.Text = selectedRow.Cells["Получатель"].Value.ToString();
            }
        }



        private void button10_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность полей
            if (string.IsNullOrEmpty(textBox13.Text) || string.IsNullOrEmpty(textBox16.Text) || string.IsNullOrEmpty(textBox19.Text) ||
                string.IsNullOrEmpty(textBox14.Text) || string.IsNullOrEmpty(textBox17.Text) || string.IsNullOrEmpty(comboBox2.Text) ||
                string.IsNullOrEmpty(textBox15.Text) || string.IsNullOrEmpty(textBox18.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            // Валидация цены (положительное число)
            if (!decimal.TryParse(textBox14.Text, out decimal parsedPrice) || parsedPrice <= 0)
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
            string id = textBox13.Text;
            string price = textBox14.Text;
            string date_order = textBox15.Text;
            string customers_name = textBox16.Text;
            string driver_name = textBox19.Text;
            string numder_customer = textBox18.Text;
            string number_driver = textBox17.Text;
            string name_auto = comboBox2.SelectedItem?.ToString() ?? "Не выбрано";

            try
            {
                // Вставляем данные в базу данных
                InsertContractDataToDatabase(price, date_order, customers_name, driver_name, numder_customer, number_driver, name_auto);

                // Обновляем DataGridView
                LoadContractDataFromDatabase();

                // Очищаем поля ввода после успешного выполнения
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Получаем ID выбранной строки из первого столбца (предположим, что это ID)
                string id = dataGridView3.SelectedRows[0].Cells["ID"].Value.ToString();

                // Подтверждение удаления
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот элемент?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Проверяем, что соединение с базой данных открыто
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open(); // Открываем соединение, если оно закрыто
                        }

                        // Удаление записи из базы данных
                        using (SQLiteCommand command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "DELETE FROM Договор WHERE ID = @id";
                            command.Parameters.AddWithValue("@id", id);

                            // Выполняем запрос
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Запись успешно удалена.");
                            }
                            else
                            {
                                MessageBox.Show("Запись не найдена или не была удалена.");
                            }
                        }

                        // Обновляем DataGridView после удаления
                        LoadContractDataFromDatabase();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении данных: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close(); // Закрываем соединение
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
        }


        private void button9_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность полей
            if (string.IsNullOrEmpty(textBox14.Text) || string.IsNullOrEmpty(textBox15.Text) ||
                string.IsNullOrEmpty(textBox16.Text) || string.IsNullOrEmpty(textBox19.Text) ||
                string.IsNullOrEmpty(textBox18.Text) || string.IsNullOrEmpty(textBox17.Text) ||
                string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            // Валидация цены
            if (!decimal.TryParse(textBox14.Text, out decimal parsedPrice) || parsedPrice <= 0)
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
            string price = textBox14.Text;
            string date_order = textBox15.Text;
            string customers_name = textBox16.Text;
            string driver_name = textBox19.Text;
            string number_customer = textBox18.Text;
            string number_driver = textBox17.Text;
            string name_auto = comboBox2.SelectedItem?.ToString() ?? "Не выбрано";

            try
            {
                // Вставляем данные в базу данных
                InsertContractDataToDatabase(price, date_order, customers_name, driver_name, number_customer, number_driver, name_auto);

                // Обновляем DataGridView
                LoadContractDataFromDatabase();

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
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox19.Clear();
            comboBox2.SelectedIndex = -1;
        }

        private void InsertContractDataToDatabase(string price, string date_order, string customers_name, string driver_name, string number_customer, string number_driver, string name_auto)
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
                    command.CommandText = "INSERT INTO Договор (Цена_груза, Дата_оформления_заказа , ФИО_Заказчика, ФИО_Водителя, Номер_телефона_заказчика, Номер_телефона_водителя, Название_автомобиля) VALUES (@price, @date_order, @customers_name, @driver_name, @number_customer, @number_driver, @name_auto)";
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@date_order", date_order);
                    command.Parameters.AddWithValue("@customers_name", customers_name);
                    command.Parameters.AddWithValue("@driver_name", driver_name);
                    command.Parameters.AddWithValue("@number_customer", number_customer);
                    command.Parameters.AddWithValue("@number_driver", number_driver);
                    command.Parameters.AddWithValue("@name_auto", name_auto);

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

        private void UpdateContractDataInDatabase(string id, string price, string date_order, string customers_name, string driver_name, string number_customer, string number_driver, string name_auto)
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
                    command.CommandText = "UPDATE Договор SET Цена_груза = @price, Дата_оформления_заказа = @date_order, ФИО_Заказчика = @customers_name, ФИО_Водителя = @driver_name, Номер_телефона_заказчика = @number_customer, Номер_телефона_водителя = @number_driver, Название_автомобиля = @name_auto WHERE ID = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@date_order", date_order);
                    command.Parameters.AddWithValue("@customers_name", customers_name);
                    command.Parameters.AddWithValue("@driver_name", driver_name);
                    command.Parameters.AddWithValue("@number_customer", number_customer);
                    command.Parameters.AddWithValue("@number_driver", number_driver);
                    command.Parameters.AddWithValue("@name_auto", name_auto);

                    // Выполнение запроса
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении данных в базе: " + ex.Message);
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

        private void LoadContractDataFromDatabase()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Договор", connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView3.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }




        private void button3_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность полей
            if (string.IsNullOrWhiteSpace(textBox24.Text) ||
                string.IsNullOrWhiteSpace(textBox25.Text) ||
                string.IsNullOrWhiteSpace(textBox26.Text) ||
                string.IsNullOrWhiteSpace(textBox27.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            // Получаем значения из полей ввода
            string time_on_road = textBox24.Text;
            string start = textBox25.Text;
            string finish = textBox26.Text;
            string distance = textBox27.Text;

            // Вставляем данные в базу данных
            InsertRouteDataToDatabase(time_on_road, start, finish, distance);

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
            if (string.IsNullOrWhiteSpace(textBox24.Text) ||
                string.IsNullOrWhiteSpace(textBox25.Text) ||
                string.IsNullOrWhiteSpace(textBox26.Text) ||
                string.IsNullOrWhiteSpace(textBox27.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            // Получаем выбранную строку в DataGridView
            DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

            // Обновляем данные в базе данных
            UpdateRouteDataInDatabase(
                selectedRow.Cells[0].Value.ToString(),
                textBox24.Text,
                textBox25.Text,
                textBox26.Text,
                textBox27.Text
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

        private void InsertRouteDataToDatabase(string time_on_road, string start, string finish, string distance)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Маршрут (Время_в_пути, Начальная_точка, Конечная_точка, Дистанция) VALUES (@time_on_road, @start, @finish, @distance)";
                command.Parameters.AddWithValue("@time_on_road", time_on_road);
                command.Parameters.AddWithValue("@start", start);
                command.Parameters.AddWithValue("@finish", finish);
                command.Parameters.AddWithValue("@distance", distance);
                command.ExecuteNonQuery();
            }
        }

        private void UpdateRouteDataInDatabase(string id, string time_on_road, string start, string finish, string distance)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Маршрут SET Время_в_пути = @time_on_road, Начальная_точка = @start, Конечная_точка = @finish, Дистанция = @distance WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@time_on_road", time_on_road);
                command.Parameters.AddWithValue("@start", start);
                command.Parameters.AddWithValue("@finish", finish);
                command.Parameters.AddWithValue("@distance", distance);
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

        private void button21_Click(object sender, EventArgs e)
        {
            // Проверка заполненности полей
            if (string.IsNullOrWhiteSpace(comboBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox20.Text) ||
                string.IsNullOrWhiteSpace(comboBox5.Text) ||
                string.IsNullOrWhiteSpace(comboBox6.Text) ||
                string.IsNullOrWhiteSpace(comboBox7.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка");
                return;
            }

            // Получаем значения из полей ввода
            string name_auto = comboBox3.Text;
            string number_auto = textBox20.Text;
            string type_transport = comboBox5.Text;
            string load = comboBox6.Text;
            string condition = comboBox7.Text;

            // Вставляем данные в базу данных
            InsertCarDataToDatabase(name_auto, number_auto, type_transport, load, condition);

            // Обновляем DataGridView
            LoadCarData();
        }

        private void InsertCarDataToDatabase(string name_auto, string number_auto, string type_transport, string load, string condition)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Вставляем данные без ID, чтобы SQLite сама присваивала ID
                command.CommandText = "INSERT INTO Транспортное_средство (Марка, Госномер, Тип_транспорта, Грузоподъемность, Техническое_состояние) " +
                                       "VALUES (@name_auto, @number_auto, @type_transport, @load, @condition)";
                command.Parameters.AddWithValue("@name_auto", name_auto);
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
                    string.IsNullOrWhiteSpace(comboBox5.Text) ||
                    string.IsNullOrWhiteSpace(comboBox6.Text) ||
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
                    comboBox5.Text,
                    comboBox6.Text,
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

        private void UpdateCarDataInDatabase(string id, string name_auto, string number_auto, string type_transport, string load, string condition)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Транспортное_средство SET Марка = @name_auto, Госномер = @number_auto, Тип_транспорта = @type_transport, " +
                                       "Грузоподъемность = @load, Техническое_состояние = @condition WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name_auto", name_auto);
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
            {
                string query;
                if (string.IsNullOrEmpty(textBox21.Text))
                {
                    // Если поле поиска пустое, загрузить все данные
                    query = "SELECT * FROM Клиент";
                }
                else
                {
                    //Поиск по выбранному критерию
                    string selectedField = comboBox1.SelectedItem.ToString();
                    string searchText = textBox21.Text;
                    query = $"SELECT * FROM Клиент WHERE {selectedField} LIKE '%{searchText}%'";
                }
                adapter = new SQLiteDataAdapter(query, connection);
                dt = new DataTable();
                int v = adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }


        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

       //Заполнение текстовиков в Договорах
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // Заполняем текстовые поля данными из выбранной строки
                textBox14.Text = selectedRow.Cells["Цена_груза"].Value.ToString();
                textBox15.Text = selectedRow.Cells["Дата_оформления_заказа"].Value.ToString();
                textBox16.Text = selectedRow.Cells["ФИО_Заказчика"].Value.ToString();
                textBox19.Text = selectedRow.Cells["ФИО_Водителя"].Value.ToString();
                textBox18.Text = selectedRow.Cells["Номер_телефона_заказчика"].Value.ToString();
                textBox17.Text = selectedRow.Cells["Номер_телефона_водителя"].Value.ToString();
                comboBox2.Text = selectedRow.Cells["Название_автомобиля"].Value.ToString();
            }
        }

        //Заполнение текстовиков в Грузах
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];

                // Заполняем текстовые поля данными из выбранной строки
                textBox13.Text = selectedRow.Cells["ID"].Value.ToString();
                textBox14.Text = selectedRow.Cells["Цена_груза"].Value.ToString();
                textBox15.Text = selectedRow.Cells["Дата_оформления_заказа"].Value.ToString();
                textBox16.Text = selectedRow.Cells["ФИО_Заказчика"].Value.ToString();
                textBox19.Text = selectedRow.Cells["ФИО_Водителя"].Value.ToString();
                textBox18.Text = selectedRow.Cells["Номер_телефона_заказчика"].Value.ToString();
                textBox17.Text = selectedRow.Cells["Номер_телефона_водителя"].Value.ToString();

                // Устанавливаем значение для comboBox2
                string selectedAuto = selectedRow.Cells["Название_автомобиля"].Value?.ToString();

                // Проверяем, существует ли этот элемент в comboBox
                if (!string.IsNullOrEmpty(selectedAuto) && comboBox2.Items.Contains(selectedAuto))
                {
                    comboBox2.SelectedItem = selectedAuto;
                }
                else
                {
                    // Если такого элемента нет, устанавливаем пустое значение
                    comboBox2.SelectedIndex = -1;
                    MessageBox.Show("Не найдено совпадение для автомобиля.");
                }
            }
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            // Проверка, что строка выбрана
            if (dataGridView4.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

                // Заполняем текстовые поля данными из выбранной строки
                textBox24.Text = selectedRow.Cells["Время_в_пути"].Value.ToString();
                textBox25.Text = selectedRow.Cells["Начальная_точка"].Value.ToString();
                textBox26.Text = selectedRow.Cells["Конечная_точка"].Value.ToString();
                textBox27.Text = selectedRow.Cells["Дистанция"].Value.ToString();
            }
            else
            {
                // Очищаем поля, если строка не выбрана
                textBox24.Clear();
                textBox25.Clear();
                textBox26.Clear();
                textBox27.Clear();
            }
        }

        private void dataGridView6_SelectionChanged(object sender, EventArgs e)
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
                    if (!string.IsNullOrEmpty(selectedCapacity) && comboBox6.Items.Contains(selectedCapacity))
                    {
                        comboBox6.SelectedItem = selectedCapacity;
                    }
                    else
                    {
                        comboBox6.SelectedIndex = -1;
                        MessageBox.Show("Не найдено совпадение для грузоподъемности.");
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
                comboBox6.SelectedIndex = -1;
                comboBox7.SelectedIndex = -1;
                textBox20.Clear();
            }
        }



    }
}

