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
        private string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
        private string connectionString;
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
            LoadGruzComboData();
            LoadSearchCriteria();
            LoadPostavData();
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

            //string q1 = "SELECT ID, [Фамилия] || ' ' || [Имя] AS Full FROM [Клиент]";
            //SQLiteDataAdapter da1 = new SQLiteDataAdapter(q1, connection);
            //DataTable dt1 = new DataTable();
            //da1.Fill(dt1);
            //comboBox1.DataSource = dt1;
            //comboBox1.DisplayMember = "Full"; // Отображаемое значение в комбобоксе
            //comboBox1.ValueMember = "ID";        // ID клиента

            string q4 = "SELECT ID, [Фамилия] || ' ' || [Имя] AS Full FROM [Клиент]";
            SQLiteDataAdapter da4 = new SQLiteDataAdapter(q4, connection);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            comboBox4.DataSource = dt4;
            comboBox4.DisplayMember = "Full"; // Отображаемое значение в комбобоксе
            comboBox4.ValueMember = "ID";   
            
            string query2 = "SELECT ID, [Марка] || ' ' || [Модель] AS Full FROM [Транспортное_средство]";
            SQLiteDataAdapter da2 = new SQLiteDataAdapter(query2, connection);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "Full"; // Отображаемое значение в комбобоксе
            comboBox2.ValueMember = "ID";     // ID автомобиля

            string query8 = "SELECT ID, [Фамилия] || ' ' || [Имя] AS Full FROM [Поставщик]";
            SQLiteDataAdapter da8 = new SQLiteDataAdapter(query8, connection);
            DataTable dt8 = new DataTable();
            da8.Fill(dt8);
            comboBox8.DataSource = dt8;
            comboBox8.DisplayMember = "Full"; // Отображаемое значение в комбобоксе
            comboBox8.ValueMember = "ID";

            string query9 = "SELECT ID, [Фамилия] || ' ' || [Имя] AS Full FROM [Водители]";
            SQLiteDataAdapter da9 = new SQLiteDataAdapter(query9, connection);
            DataTable dt9 = new DataTable();
            da9.Fill(dt9);
            comboBox9.DataSource = dt9;
            comboBox9.DisplayMember = "Full"; // Отображаемое значение в комбобоксе
            comboBox9.ValueMember = "ID";

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
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Новый SQL-запрос с правильными именами столбцов
                string query = @"
        SELECT 
            Договор.ID,
            Клиент.Фамилия || ' ' || Клиент.Имя || ' ' || Клиент.Отчество AS 'ФИО Клиента',  -- Объединяем Фамилию, Имя и Отчество
            Поставщик.Фамилия || ' ' || Поставщик.Имя || ' ' || Поставщик.Отчество AS 'ФИО Отправителя',  -- Для Поставщика
            Водители.Фамилия || ' ' || Водители.Имя || ' ' || Водители.Отчество AS 'ФИО Водителя',  -- Для Водителей
            Транспортное_средство.Марка || ' ' || Транспортное_средство.Модель AS 'Марка и Модель Авто',  -- Объединяем Марку и Модель
            Договор.Общая_стоимость,
            Договор.Дата_оформления_заказа,
            Договор.Номер_телефона_заказчика,
            Договор.Номер_телефона_водителя,
            Договор.Номер_телефона_поставщика
        FROM Договор
        LEFT JOIN Клиент ON Договор.ID_Клиента = Клиент.ID
        LEFT JOIN Поставщик ON Договор.ID_Отправителя = Поставщик.ID
        LEFT JOIN Водители ON Договор.ID_Водителя = Водители.ID
        LEFT JOIN Транспортное_средство ON Договор.ID_Автомобиля = Транспортное_средство.ID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable contractsTable = new DataTable();
                        adapter.Fill(contractsTable);
                        dataGridView3.DataSource = contractsTable;

                        // Скрываем внутренний ID договора
                        dataGridView3.Columns["ID"].Visible = false;
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
            MessageBox.Show("Данные успешно добавлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Данные успешно удалены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    command.CommandText = "UPDATE Клиент SET Фамилия = @surname, Имя = @name, Отчество = @lastName, [Серия_паспорта] = @seria, [Номер_паспорта] = @number, [Номер_телефона] = @telephone, Почта = @mail, Адрес = @address WHERE Фамилия = @surname"; // Используем фамилию
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

        // Метод проверки корректности объема (включая 0)
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
            string customer_name = comboBox4.Text;
            string driver_name = comboBox9.Text;
            string otp_name = comboBox8.Text;
            string numder_customer = textBox18.Text;
            string number_driver = textBox17.Text;
            string name_auto = comboBox2.SelectedItem?.ToString() ?? "Не выбрано";

            try
            {
                // Вставляем данные в базу данных
                InsertContractDataToDatabase(price, date_order, customer_name, driver_name, numder_customer, number_driver, name_auto);

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

        private void InsertContractDataToDatabase(string price, string date_order, string customer_name, string driver_name, string numder_customer, string number_driver, string name_auto)
        {
            throw new NotImplementedException();
        }

        private void button12_Click(object sender, EventArgs e)     /*Договор*/
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Получаем ID выбранной строки
                string id = dataGridView3.SelectedRows[0].Cells["ID"].Value.ToString();

                // Подтверждение удаления
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот договор?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Перемещение договора в архив
                    MoveContractToDeletedTable(id);

                    // Обновление таблицы договоров
                    LoadContractDataFromDatabase();
                }
            }
            else
            {
                MessageBox.Show("Выберите договор для удаления.");
            }
        }


        private void button9_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность полей
            if (string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox15.Text) || (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox18.Text) || string.IsNullOrEmpty(textBox17.Text) || string.IsNullOrEmpty(comboBox10.Text)||
                string.IsNullOrEmpty(textBox19.Text) || string.IsNullOrEmpty(textBox23.Text) ||
                string.IsNullOrEmpty(comboBox2.Text) || string.IsNullOrEmpty(comboBox8.Text) || string.IsNullOrEmpty(comboBox9.Text) ||
                string.IsNullOrEmpty(comboBox4.Text)))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

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
            string name_client = comboBox4.Text;
            string date_order = textBox15.Text;
            string customer_name = comboBox4.Text;  
            string driver_name = comboBox9.Text;
            string number_customer = textBox18.Text;
            string number_driver = textBox17.Text;
            string punct_otprav = textBox19.Text;
            string punct_polych = textBox23.Text;
            string name_auto = comboBox2.SelectedItem?.ToString() ?? "Не выбрано";
            string gruz = comboBox10.SelectedItem?.ToString() ?? "Не выбрано";

            try
            {
                // Вставляем данные в базу данных
                InsertContractDataToDatabase(price, name_client, date_order, customer_name, driver_name, number_customer, number_driver, name_auto, punct_otprav, punct_polych, gruz);

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

        private void InsertContractDataToDatabase(string price, string name_client, string date_order, string customer_name, string driver_name, string number_customer, string number_driver, string name_auto, string punct_otprav, string punct_polych, string gruz)
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
                    command.CommandText = "INSERT INTO Договор (Груз, Общая_стоимость, ID_Клиента, Дата_оформления_заказа , ID_Отправителя, ФИО_Водителя, Номер_телефона_заказчика, Номер_телефона_водителя, ID_Автомобиля, Пункт_отправления, Пункт_назначения) " +
                                          "VALUES (@gruz, @price, @clientId, @date_order, @senderId, @driver_name, @number_customer, @number_driver, @vehicleId, @punct_otprav, @punct_polych)";
                    command.Parameters.AddWithValue("@gruz", gruz);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@date_order", date_order);
                    command.Parameters.AddWithValue("@driver_name", driver_name);
                    command.Parameters.AddWithValue("@number_customer", number_customer);
                    command.Parameters.AddWithValue("@number_driver", number_driver);
                    command.Parameters.AddWithValue("@punct_otprav", punct_otprav);
                    command.Parameters.AddWithValue("@punct_polych", punct_polych);

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


        //// Пример функций для получения ID из базы данных
        //private int GetClientIdByName(string name)
        //{
        //    return GetIdByName("Клиент", "ID", "Name", name);
        //}

        //private int GetSenderIdByName(string name)
        //{
        //    return GetIdByName("Поставщик", "ID", "Name", name);
        //}

        //private int GetDriverIdByName(string name)
        //{
        //    return GetIdByName("Водители", "ID", "Name", name);
        //}

        //private int GetVehicleIdByName(string name)
        //{
        //    return GetIdByName("Транспортное_средство", "ID", "Name", name);
        //}

        //private int GetIdByName(string tableName, string idColumn, string nameColumn, string name)
        //{
        //    try
        //    {
        //        using (SQLiteCommand command = new SQLiteCommand($"SELECT {idColumn} FROM {tableName} WHERE {nameColumn} = @name", connection))
        //        {
        //            command.Parameters.AddWithValue("@name", name);

        //            object result = command.ExecuteScalar();
        //            return result != null ? Convert.ToInt32(result) : -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка при получении ID для {tableName}: " + ex.Message);
        //        return -1;
        //    }
        //}

        //private void UpdateContractDataInDatabase(string id, string price, string name_client, string date_order, string customer_name, string driver_name, string number_customer, string number_driver, string name_auto, string punct_otprav, string punct_polych, string gruz)
        //{
        //    try
        //    {
        //        // Проверка соединения
        //        if (connection.State != ConnectionState.Open)
        //        {
        //            connection.Open();
        //        }

        //        using (SQLiteCommand command = new SQLiteCommand(connection))
        //        {
        //            // SQL-запрос для обновления данных
        //            command.CommandText = "UPDATE Договор SET " +
        //                                  "Груз = @gruz, Общая_стоимость = @price, Дата_оформления_заказа = @date_order, " +
        //                                  "ФИО_Клиента = @name_client, ФИО_Отправителя = @customer_name, ФИО_Водителя = @driver_name, " +
        //                                  "Номер_телефона_заказчика = @number_customer, Номер_телефона_водителя = @number_driver, " +
        //                                  "Название_автомобиля = @name_auto, Пункт_отправления = @punct_otprav, Пункт_назначения = @punct_polych " +
        //                                  "WHERE ID = @id";

        //            // Установка параметров
        //            command.Parameters.AddWithValue("@gruz", gruz);
        //            command.Parameters.AddWithValue("@price", price);
        //            command.Parameters.AddWithValue("@date_order", date_order);
        //            command.Parameters.AddWithValue("@name_client", name_client);
        //            command.Parameters.AddWithValue("@customer_name", customer_name);
        //            command.Parameters.AddWithValue("@driver_name", driver_name);
        //            command.Parameters.AddWithValue("@number_customer", number_customer);
        //            command.Parameters.AddWithValue("@number_driver", number_driver);
        //            command.Parameters.AddWithValue("@name_auto", name_auto);
        //            command.Parameters.AddWithValue("@punct_otprav", punct_otprav);
        //            command.Parameters.AddWithValue("@punct_polych", punct_polych);
        //            command.Parameters.AddWithValue("@contractId", id);

        //            // Выполнение запроса
        //            command.ExecuteNonQuery();
        //        }

        //        MessageBox.Show("Данные успешно обновлены.");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
        //    }
        //    finally
        //    {
        //        // Закрытие соединения
        //        if (connection.State == ConnectionState.Open)
        //        {
        //            connection.Close();
        //        }
        //    }
        //}
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
                        comboBox10.Items.Clear();

                        while (reader.Read())
                        {
                            // Добавляем каждый тип груза в ComboBox
                            comboBox10.Items.Add(reader["Тип_груза"].ToString());
                        }
                    }
                }
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
            InsertRouteDataToDatabase(start, finish);

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
            if (string.IsNullOrWhiteSpace(textBox25.Text) ||
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
                command.CommandText = "INSERT INTO Маршрут (Начальная_точка, Конечная_точка) VALUES (@start, @finish)";
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

                        // Обновляем DataGridView
                        dataGridView1.DataSource = searchResults;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];

                // Заполняем текстовые поля данными из выбранной строки
                textBox1.Text = selectedRow.Cells["Общая_стоимость"].Value.ToString();
                textBox15.Text = selectedRow.Cells["Дата_оформления_заказа"].Value.ToString();
                textBox18.Text = selectedRow.Cells["Номер_телефона_заказчика"].Value.ToString();
                textBox17.Text = selectedRow.Cells["Номер_телефона_водителя"].Value.ToString();
                textBox8.Text = selectedRow.Cells["Номер_телефона_поставщика"].Value.ToString();
                string carName = selectedRow.Cells["Марка и Модель Авто"].Value?.ToString() ?? "";
                string otpName = selectedRow.Cells["ФИО Отправителя"].Value?.ToString() ?? "";
                string klName = selectedRow.Cells["ФИО Клиента"].Value?.ToString() ?? "";
                string vodName = selectedRow.Cells["ФИО Водителя"].Value?.ToString() ?? "";

                if (!string.IsNullOrEmpty(vodName))
                {
                    for (int i = 0; i < comboBox9.Items.Count; i++)
                    {
                        if (((DataRowView)comboBox9.Items[i])["ФИО Водителя"].ToString() == carName)
                        {
                            comboBox9.SelectedIndex = i;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(klName))
                {
                    for (int i = 0; i < comboBox7.Items.Count; i++)
                    {
                        if (((DataRowView)comboBox8.Items[i])["ФИО Клиента"].ToString() == carName)
                        {
                            comboBox7.SelectedIndex = i;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(otpName))
                {
                    for (int i = 0; i < comboBox8.Items.Count; i++)
                    {
                        if (((DataRowView)comboBox9.Items[i])["ФИО Отправителя"].ToString() == carName)
                        {
                            comboBox8.SelectedIndex = i;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(carName))
                {
                    for (int i = 0; i < comboBox2.Items.Count; i++)
                    {
                        if (((DataRowView)comboBox2.Items[i])["Марка и Модель Авто"].ToString() == carName)
                        {
                            comboBox2.SelectedIndex = i;
                            break;
                        }
                    }
                }
             
            }
        }


        //Заполнение текстовиков в Договоре
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];

                // Заполняем текстовые поля данными из выбранной строки
                textBox15.Text = selectedRow.Cells["Дата_оформления_заказа"].Value.ToString();
                textBox18.Text = selectedRow.Cells["Номер_телефона_заказчика"].Value.ToString();
                textBox17.Text = selectedRow.Cells["Номер_телефона_водителя"].Value.ToString();

                // Устанавливаем значение для comboBox2
                string selectedAuto = $"{selectedRow.Cells["Марка"].Value?.ToString()} и {selectedRow.Cells["Модель"].Value?.ToString()}";
                comboBox2.Items.Add(selectedAuto);
                // Имя столбца для авто

                // Проверяем, существует ли этот элемент в comboBox
                if (!string.IsNullOrEmpty(selectedAuto) && comboBox2.Items.Contains(selectedAuto))
                {
                    comboBox2.SelectedItem = selectedAuto;
                }
                //else
                //{
                //    // Если такого элемента нет, устанавливаем пустое значение
                //    comboBox2.SelectedIndex = -1;
                //    MessageBox.Show("Не найдено совпадение для автомобиля.");
                //}
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

        private void MoveContractToDeletedTable(string id)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                // Копирование договора в таблицу "Удалённые_договоры"
                using (SQLiteCommand copyCommand = new SQLiteCommand(connection))
                {
                    copyCommand.CommandText = "INSERT INTO Удалённые_договоры SELECT * FROM Договор WHERE ID = @id";
                    copyCommand.Parameters.AddWithValue("@id", id);
                    copyCommand.ExecuteNonQuery();
                }

                // Удаление договора из таблицы "Договор"
                using (SQLiteCommand deleteCommand = new SQLiteCommand(connection))
                {
                    deleteCommand.CommandText = "DELETE FROM Договор WHERE ID = @id";
                    deleteCommand.Parameters.AddWithValue("@id", id);
                    deleteCommand.ExecuteNonQuery();
                }

                MessageBox.Show("Договор успешно перемещён в архив.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при перемещении договора в архив: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
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
                                comboBox6.Items.Add(columnName1);
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
                            comboBox11.Items.Add(reader["name"].ToString());
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

        private void button25_Click(object sender, EventArgs e)
        {
            // Получаем выбранный критерий поиска
            string selectedCriteria = comboBox6.SelectedItem?.ToString();
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
            comboBox6.SelectedIndex = -1; // Сбрасываем выбор в ComboBox

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
    }
}


