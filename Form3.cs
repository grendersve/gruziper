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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace АИС_грузоперевозки
{
    public partial class Form3 : Form
    {
        private SQLiteConnection connection;

        public Form3()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadClientData();
            LoadAutomobileData();
            LoadContractData();
            LoadRouteData();
            LoadCarData();
            string q1 = "SELECT ID, Фамилия || ' ' || Имя AS Full FROM Клиент";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(q1, connection);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "Full"; // Отображаемое значение в комбобоксе
            comboBox1.ValueMember = "ID";        // ID клиента
        }

        private void ConnectToDatabase()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            connection = new SQLiteConnection($"Data Source={dbPath}");
            connection.Open();

            //    // Добавляем внешние ключи
            //    using (SQLiteCommand command = new SQLiteCommand(connection))
            //    {
            //        command.CommandText = @"
            //    ALTER TABLE cars
            //    ADD FOREIGN KEY (ID_auto) REFERENCES specifications(ID) ON UPDATE CASCADE ON DELETE SET NULL,
            //    ADD FOREIGN KEY (ID_country) REFERENCES country(ID) ON UPDATE CASCADE ON DELETE SET NULL;
            //";
            //        command.ExecuteNonQuery();
            //    }
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

        private void LoadAutomobileData()
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
            // Получаем значения из полей ввода
            string surname = textBox2.Text;
            string name = textBox3.Text;
            string lastName = textBox4.Text;
            string seria = textBox5.Text;
            string number = textBox6.Text;
            string telephone = textBox7.Text;
            string mail = textBox36.Text;
            string address = textBox37.Text;

            // Вставляем данные в базу данных
            InsertKlientDataToDatabase(surname, name, lastName, seria, number, telephone, mail, address);

            // Обновляем DataGridView
            LoadClientData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получаем выбранную строку в DataGridView
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // Обновляем данные в базе данных
            UpdateCarDataInDatabase(
                selectedRow.Cells[0].Value.ToString(),
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox6.Text,
                textBox7.Text,
                textBox36.Text,
                textBox37.Text
            );

            // Обновляем DataGridView
            LoadClientData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                DeleteKlientDataFromDatabase(selectedRow.Cells[0].Value.ToString());

                LoadClientData();
            }
            else
            {
                // Добавьте обработку случая, когда не выбрана ни одна строка в DataGridView
                MessageBox.Show("Выберите строку для удаления.");
            }
        }



        private void InsertKlientDataToDatabase(string surname, string name, string lastName, string seria, string number, string telephone, string mail, string address)
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

        private void UpdateCarDataInDatabase(string id, string surname, string name, string lastName, string seria, string number, string telephone, string mail, string address)
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

        private void DeleteKlientDataFromDatabase(string id)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM Клиент WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
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

            // Проверка на заполненность полей (необязательно, но полезно)
            if (string.IsNullOrEmpty(weight) || string.IsNullOrEmpty(type_cargo) || string.IsNullOrEmpty(volume) ||
                string.IsNullOrEmpty(senderName) || string.IsNullOrEmpty(recipient))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            // Вставляем данные в базу данных
            InsertCargoDataToDatabase(weight, type_cargo, volume, senderName, recipient);

            // Обновляем DataGridView
            LoadCargoData();
        }


        private void LoadCargoData()
        {
            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Грузы", connection))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView2.DataSource = table;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку в DataGridView
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // Обновляем данные в базе данных
                UpdateCargoDataInDatabase(
                    selectedRow.Cells[0].Value.ToString(), // ID
                    textBox9.Text,
                    textBox10.Text,
                    textBox11.Text,
                    textBox12.Text,
                    textBox38.Text
                );

                // Обновляем DataGridView
                LoadCargoData();
            }
            else
            {
                MessageBox.Show("Выберите строку для обновления.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку в DataGridView
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // Удаляем данные из базы данных
                DeleteCargoDataFromDatabase(selectedRow.Cells[0].Value.ToString());

                // Обновляем DataGridView
                LoadCargoData();
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
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


        //private void button13_Click(object sender, EventArgs e)
        //{
        //    // Проверка на заполненность полей
        //    if (string.IsNullOrEmpty(textBox13.Text) || string.IsNullOrEmpty(textBox16.Text) || string.IsNullOrEmpty(textBox19.Text) ||
        //        string.IsNullOrEmpty(textBox14.Text) || string.IsNullOrEmpty(textBox17.Text) || string.IsNullOrEmpty(textBox20.Text) ||
        //        string.IsNullOrEmpty(textBox15.Text) || string.IsNullOrEmpty(textBox18.Text))
        //    {
        //        MessageBox.Show("Заполните все поля.");
        //        return;
        //    }

        //    // Получаем значения из полей ввода
        //    string id = textBox13.Text;
        //    string price = textBox14.Text;
        //    string date_order = textBox15.Text;
        //    string customers_name = textBox16.Text;
        //    string driver_name = textBox19.Text;
        //    string numder_customer = textBox18.Text;
        //    string number_driver = textBox17.Text;
        //    string name_auto = textBox20.Text;

        //    // Вставляем данные в базу данных
        //    InsertContractDataToDatabase(id, price, date_order, customers_name, driver_name, numder_customer, number_driver, name_auto);

        //    // Обновляем DataGridView
        //    LoadContractDataFromDatabase();
        //}

        //private void button15_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView3.SelectedRows.Count > 0)
        //    {
        //        // Получаем выбранную строку
        //        DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];

        //        // Проверка на заполненность полей
        //        if (string.IsNullOrEmpty(textBox13.Text) || string.IsNullOrEmpty(textBox16.Text) || string.IsNullOrEmpty(textBox19.Text) ||
        //        string.IsNullOrEmpty(textBox14.Text) || string.IsNullOrEmpty(textBox17.Text) || string.IsNullOrEmpty(textBox20.Text) ||
        //        string.IsNullOrEmpty(textBox15.Text) || string.IsNullOrEmpty(textBox18.Text))
        //        {
        //            MessageBox.Show("Заполните все поля.");
        //            return;
        //        }

        //        // Обновляем данные
        //        UpdateContractDataInDatabase(
        //            selectedRow.Cells[0].Value.ToString(), // ID из DataGridView
        //            textBox14.Text,
        //            textBox15.Text,
        //            textBox16.Text,
        //            textBox17.Text,
        //            textBox18.Text,
        //            textBox19.Text,
        //            textBox20.Text
        //        );

        //        // Обновляем DataGridView
        //        LoadContractDataFromDatabase();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Выберите строку для обновления.");
        //    }
        //}

        //private void button16_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView3.SelectedRows.Count > 0)
        //    {
        //        // Получаем ID выбранной строки
        //        string id = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();

        //        // Удаляем данные
        //        DeleteContractDataFromDatabase(id);

        //        // Обновляем DataGridView
        //        LoadContractDataFromDatabase();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Выберите строку для удаления.");
        //    }
        //}

        private void InsertContractDataToDatabase(string id, string price, string date_order, string customers_name, string driver_name, string numder_customer, string number_driver, string name_auto)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Договор (ID, Цена_груза, Дата_оформления_заказа, ФИО_Заказчика, ФИО_Водителя, Номер_телефона_заказчика, Номер_телефона_водителя, Название_автомобиля) VALUES (@id, @price, @date_order, @customers_name, @driver_name, @numder_customer, @number_driver, @name_auto)";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@date_order", date_order);
                command.Parameters.AddWithValue("@customers_name", customers_name);
                command.Parameters.AddWithValue("@driver_name", driver_name);
                command.Parameters.AddWithValue("@numder_customer", numder_customer);
                command.Parameters.AddWithValue("@number_driver", number_driver);
                command.Parameters.AddWithValue("@name_auto", name_auto);
                command.ExecuteNonQuery();
            }
        }

        private void UpdateContractDataInDatabase(string id, string price, string date_order, string customers_name, string driver_name, string numder_customer, string number_driver, string name_auto)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Договор SET Цена_груза = @price, Дата_оформления_заказа = @date_order, ФИО_Заказчика = @customers_name, ФИО_Водителя = @driver_name, Номер_телефона_заказчика = @numder_customer, Номер_телефона_водителя = @number_driver, Название_автомобиля = @name_auto WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@date_order", date_order);
                command.Parameters.AddWithValue("@customers_name", customers_name);
                command.Parameters.AddWithValue("@driver_name", driver_name);
                command.Parameters.AddWithValue("@numder_customer", numder_customer);
                command.Parameters.AddWithValue("@number_driver", number_driver);
                command.Parameters.AddWithValue("@name_auto", name_auto);
                command.ExecuteNonQuery();
            }
        }

        private void DeleteContractDataFromDatabase(string id)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM Договор WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        private void LoadContractDataFromDatabase()
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



        //private void button13_Click(object sender, EventArgs e)
        //{
        //    // Проверка на заполненность полей
        //    if (string.IsNullOrWhiteSpace(textBox23.Text) ||
        //        string.IsNullOrWhiteSpace(textBox24.Text) ||
        //        string.IsNullOrWhiteSpace(textBox25.Text) ||
        //        string.IsNullOrWhiteSpace(textBox26.Text) ||
        //        string.IsNullOrWhiteSpace(textBox27.Text))
        //    {
        //        MessageBox.Show("Все поля должны быть заполнены.");
        //        return;
        //    }

        //    // Получаем значения из полей ввода
        //    string id = textBox23.Text;
        //    string time_on_road = textBox24.Text;
        //    string start = textBox25.Text;
        //    string finish = textBox26.Text;
        //    string distance = textBox27.Text;

        //    // Вставляем данные в базу данных
        //    InsertRouteDataToDatabase(id, time_on_road, start, finish, distance);

        //    // Обновляем DataGridView
        //    LoadRouteData();
        //}

        //private void button15_Click(object sender, EventArgs e)
        //{
        //    // Проверка на выбор строки в DataGridView
        //    if (dataGridView4.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show("Выберите строку для обновления.");
        //        return;
        //    }

        //    // Проверка на заполненность полей
        //    if (string.IsNullOrWhiteSpace(textBox24.Text) ||
        //        string.IsNullOrWhiteSpace(textBox25.Text) ||
        //        string.IsNullOrWhiteSpace(textBox26.Text) ||
        //        string.IsNullOrWhiteSpace(textBox27.Text))
        //    {
        //        MessageBox.Show("Все поля должны быть заполнены.");
        //        return;
        //    }

        //    // Получаем выбранную строку в DataGridView
        //    DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

        //    // Обновляем данные в базе данных
        //    UpdateRouteDataInDatabase(
        //        selectedRow.Cells[0].Value.ToString(),
        //        textBox24.Text,
        //        textBox25.Text,
        //        textBox26.Text,
        //        textBox27.Text
        //    );

        //    // Обновляем DataGridView
        //    LoadRouteData();
        //}

        //private void button16_Click(object sender, EventArgs e)
        //{
        //    // Проверка на выбор строки в DataGridView
        //    if (dataGridView4.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show("Выберите строку для удаления.");
        //        return;
        //    }

        //    // Получаем выбранную строку в DataGridView
        //    DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

        //    // Удаляем данные из базы данных
        //    DeleteRouteDataFromDatabase(selectedRow.Cells[0].Value.ToString());

        //    // Обновляем DataGridView
        //    LoadRouteData();
        //}

        private void InsertRouteDataToDatabase(string id, string time_on_road, string start, string finish, string distance)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Маршрут (ID, Время_в_пути, Начальная_точка, Конечная_точка, Дистанция) VALUES (@id, @time_on_road, @start, @finish, @distance)";
                command.Parameters.AddWithValue("@id", id);
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
            // Получаем значения из полей ввода
            string id = textBox44.Text;
            string name_auto = textBox43.Text;
            string number_auto = textBox42.Text;
            string type_transport = textBox41.Text;
            string load = textBox40.Text;
            string condition = textBox39.Text;

            // Вставляем данные в базу данных
            InsertCarDataToDatabase(id, name_auto, number_auto, type_transport, load, condition);

            // Обновляем DataGridView
            LoadCarData();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (dataGridView6.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку в DataGridView
                DataGridViewRow selectedRow = dataGridView6.SelectedRows[0];

                // Проверяем, что все поля заполнены
                if (string.IsNullOrWhiteSpace(textBox44.Text) ||
                    string.IsNullOrWhiteSpace(textBox43.Text) ||
                    string.IsNullOrWhiteSpace(textBox42.Text) ||
                    string.IsNullOrWhiteSpace(textBox41.Text) ||
                    string.IsNullOrWhiteSpace(textBox40.Text) ||
                    string.IsNullOrWhiteSpace(textBox39.Text))
                {
                    MessageBox.Show("Все поля должны быть заполнены для обновления данных.", "Ошибка");
                    return;
                }

                // Обновляем данные в базе данных
                UpdateCarDataInDatabase(
                    selectedRow.Cells[0].Value.ToString(), 
                    textBox43.Text, 
                    textBox42.Text, 
                    textBox41.Text, 
                    textBox40.Text,
                    textBox39.Text 
                );

                // Обновляем DataGridView
                LoadCarData();
            }
            else
            {
                MessageBox.Show("Выберите строку для обновления данных.", "Ошибка");
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (dataGridView6.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку в DataGridView
                DataGridViewRow selectedRow = dataGridView6.SelectedRows[0];

                // Удаляем данные из базы
                DeleteCarDataFromDatabase(selectedRow.Cells[0].Value.ToString());

                // Обновляем DataGridView с данными о машинах
                LoadCarData();
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка");
            }
        }

        private void InsertCarDataToDatabase(string id, string name_auto, string number_auto, string type_transport, string load, string condition)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Транспортное_средство (ID, Марка, Госномер, Тип_транспорта, Грузоподъемность, Техническое_состояние) VALUES (@id, @name_auto, @number_auto, @type_transport, @load, @condition)";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name_auto", name_auto);
                command.Parameters.AddWithValue("@number_auto", number_auto);
                command.Parameters.AddWithValue("@type_transport", type_transport);
                command.Parameters.AddWithValue("@load", load);
                command.Parameters.AddWithValue("@condition", condition);
                command.ExecuteNonQuery();
            }
        }

        private void UpdateCarDataInDatabase(string id, string name_auto, string number_auto, string type_transport, string load, string condition)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "UPDATE Транспортное_средство SET Марка = @name_auto, Госномер = @number_auto, Тип_транспорта = @type_transport, Грузоподъемность = @load, Техническое_состояние = @condition WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name_auto", name_auto);
                command.Parameters.AddWithValue("@number_auto", number_auto);
                command.Parameters.AddWithValue("@type_transport", type_transport);
                command.Parameters.AddWithValue("@load", load);
                command.Parameters.AddWithValue("@condition", condition);
                command.ExecuteNonQuery();
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

        //Код для кнопок в разделе "Договор"
        private void button10_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность полей
            if (string.IsNullOrEmpty(textBox13.Text) || string.IsNullOrEmpty(textBox16.Text) || string.IsNullOrEmpty(textBox19.Text) ||
                string.IsNullOrEmpty(textBox14.Text) || string.IsNullOrEmpty(textBox17.Text) || string.IsNullOrEmpty(textBox20.Text) ||
                string.IsNullOrEmpty(textBox15.Text) || string.IsNullOrEmpty(textBox18.Text))
            {
                MessageBox.Show("Заполните все поля.");
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
            string name_auto = textBox20.Text;

            // Вставляем данные в базу данных
            InsertContractDataToDatabase(id, price, date_order, customers_name, driver_name, numder_customer, number_driver, name_auto);

            // Обновляем DataGridView
            LoadContractDataFromDatabase();
        }


        private void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Получаем ID выбранной строки
                string id = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();

                // Удаляем данные
                DeleteContractDataFromDatabase(id);

                // Обновляем DataGridView
                LoadContractDataFromDatabase();
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность полей
            if (string.IsNullOrEmpty(textBox13.Text) || string.IsNullOrEmpty(textBox16.Text) || string.IsNullOrEmpty(textBox19.Text) ||
                string.IsNullOrEmpty(textBox14.Text) || string.IsNullOrEmpty(textBox17.Text) || string.IsNullOrEmpty(textBox20.Text) ||
                string.IsNullOrEmpty(textBox15.Text) || string.IsNullOrEmpty(textBox18.Text))
            {
                MessageBox.Show("Заполните все поля.");
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
            string name_auto = textBox20.Text;

            // Вставляем данные в базу данных
            InsertContractDataToDatabase(id, price, date_order, customers_name, driver_name, numder_customer, number_driver, name_auto);

            // Обновляем DataGridView
            LoadContractDataFromDatabase();
        }

        //Код для кнопок в "Маршрут"
        private void button3_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность полей
            if (string.IsNullOrWhiteSpace(textBox23.Text) ||
                string.IsNullOrWhiteSpace(textBox24.Text) ||
                string.IsNullOrWhiteSpace(textBox25.Text) ||
                string.IsNullOrWhiteSpace(textBox26.Text) ||
                string.IsNullOrWhiteSpace(textBox27.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            // Получаем значения из полей ввода
            string id = textBox23.Text;
            string time_on_road = textBox24.Text;
            string start = textBox25.Text;
            string finish = textBox26.Text;
            string distance = textBox27.Text;

            // Вставляем данные в базу данных
            InsertRouteDataToDatabase(id, time_on_road, start, finish, distance);

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

        private void button13_Click(object sender, EventArgs e)
        {
            {
                string query;
                if (string.IsNullOrEmpty(textBox9.Text))
                {
                    // Если поле поиска пустое, загрузить все данные
                    query = "SELECT * FROM Specifications";
                }
                else
                {
                    //Поиск по выбранному критерию
                    string selectedField = comboBox1.SelectedItem.ToString();
                    string searchText = textBox9.Text;
                    query = $"SELECT * FROM Specifications WHERE {selectedField} LIKE '%{searchText}%'";
                }
                adapter = new SQLiteDataAdapter(query, connection);
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridView5.DataSource = dt;
            }


        }
    }
}

