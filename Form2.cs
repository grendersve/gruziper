using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace АИС_грузоперевозки
{
    public partial class Form2 : Form
    {
        private string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
        private string connectionString => $"Data Source={dbPath}";

        public Form2()
        {
            InitializeComponent();
            LoadComboBoxes();
            LoadContractData();

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            textBox4.TextChanged += TextBox4_TextChanged;

            // Добавляем обработчик для SelectionChanged
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Проверяем, что выбрана хотя бы одна строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Заполняем текстовые поля из выбранной строки
                textBox1.Text = selectedRow.Cells["Фамилия"].Value.ToString();
                textBox2.Text = selectedRow.Cells["Имя"].Value.ToString();
                textBox3.Text = selectedRow.Cells["Отчество"].Value.ToString();
                textBox5.Text = selectedRow.Cells["Общая_стоимость"].Value.ToString();
                textBox6.Text = selectedRow.Cells["Пункт_назначения"].Value.ToString();

                // Заполняем комбобоксы
                string selectedCargo = selectedRow.Cells["Груз"].Value.ToString();
                comboBox1.SelectedIndex = comboBox1.FindStringExact(selectedCargo);

                string selectedDriver = selectedRow.Cells["Водитель"].Value.ToString();
                comboBox2.SelectedIndex = comboBox2.FindStringExact(selectedDriver);

                string selectedVehicle = selectedRow.Cells["Автомобиль"].Value.ToString();
                comboBox3.SelectedIndex = comboBox3.FindStringExact(selectedVehicle);
            }
        }

        private void LoadContractData()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
        SELECT 
            Договор.ID,
            Клиент.Фамилия,
            Клиент.Имя,
            Клиент.Отчество,
            Договор.Груз,
            Договор.Общая_стоимость,
            Договор.Пункт_назначения,
            Водители.Фамилия || ' ' || Водители.Имя || ' ' || Водители.Отчество AS 'Водитель',
            Транспортное_средство.Марка || ' ' || Транспортное_средство.Модель AS 'Автомобиль'
        FROM Договор
        LEFT JOIN Клиент ON Договор.ID_Клиента = Клиент.ID
        LEFT JOIN Водители ON Договор.ID_Водителя = Водители.ID
        LEFT JOIN Транспортное_средство ON Договор.ID_Автомобиля = Транспортное_средство.ID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable contractsTable = new DataTable();
                    adapter.Fill(contractsTable);
                    dataGridView1.DataSource = contractsTable;

                    // Убедиться, что все поля отображаются корректно
                    dataGridView1.Columns["ID"].Visible = false; // Скрыть колонку ID
                    dataGridView1.Columns["Фамилия"].HeaderText = "Фамилия";
                    dataGridView1.Columns["Имя"].HeaderText = "Имя";
                    dataGridView1.Columns["Отчество"].HeaderText = "Отчество";
                    dataGridView1.Columns["Груз"].HeaderText = "Груз";
                    dataGridView1.Columns["Общая_стоимость"].HeaderText = "Общая стоимость";
                    dataGridView1.Columns["Пункт_назначения"].HeaderText = "Пункт назначения";
                    dataGridView1.Columns["Водитель"].HeaderText = "Водитель";
                    dataGridView1.Columns["Автомобиль"].HeaderText = "Автомобиль";
                }
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = @"
                UPDATE Договор SET 
                    ID_Клиента = @ID_Клиента,
                    ID_Водителя = @ID_Водителя,
                    ID_Автомобиля = @ID_Автомобиля,
                    Груз = @Груз,
                    Общая_стоимость = @Общая_стоимость,
                    Пункт_назначения = @Пункт_назначения
                WHERE ID = @ID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    int clientId = GetClientId(textBox1.Text, textBox2.Text, textBox3.Text);
                    command.Parameters.AddWithValue("@ID_Клиента", clientId);
                    command.Parameters.AddWithValue("@ID_Водителя", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("@ID_Автомобиля", comboBox3.SelectedValue);
                    command.Parameters.AddWithValue("@Груз", comboBox1.Text);
                    command.Parameters.AddWithValue("@Общая_стоимость", textBox5.Text);
                    command.Parameters.AddWithValue("@Пункт_назначения", textBox6.Text);
                    command.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells["ID"].Value);

                    command.ExecuteNonQuery();
                }
            }

            LoadContractData();
            MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null) return;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Стоимость FROM Грузы WHERE ID = @ID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", comboBox1.SelectedValue);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        textBox5.Text = result.ToString();
                        CalculateTotalCost();
                    }
                }
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalCost();
        }

        private void CalculateTotalCost()
        {
            if (decimal.TryParse(textBox4.Text, out decimal quantity) &&
                decimal.TryParse(textBox5.Text, out decimal unitCost))
            {
                textBox5.Text = (quantity * unitCost).ToString("0.00");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Проверяем, существует ли клиент
                int clientId = GetClientId(connection, textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim());
                if (clientId == -1)
                {
                    // Если клиент не существует, добавляем его
                    string insertClientQuery = @"
        INSERT INTO Клиент (Фамилия, Имя, Отчество)
        VALUES (@Фамилия, @Имя, @Отчество);
        SELECT last_insert_rowid();";

                    using (SQLiteCommand command = new SQLiteCommand(insertClientQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Фамилия", textBox1.Text.Trim());
                        command.Parameters.AddWithValue("@Имя", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@Отчество", textBox3.Text.Trim());

                        clientId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                // Добавляем запись в таблицу Договор
                string insertContractQuery = @"
    INSERT INTO Договор (
        ID_Клиента, ID_Водителя, ID_Автомобиля, Груз, Общая_стоимость, Пункт_назначения, Пункт_отправления)
    VALUES (
        @ID_Клиента, @ID_Водителя, @ID_Автомобиля, @Груз, @Общая_стоимость, @Пункт_назначения, @Пункт_отправления);";

                using (SQLiteCommand command = new SQLiteCommand(insertContractQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID_Клиента", clientId);
                    command.Parameters.AddWithValue("@ID_Водителя", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("@ID_Автомобиля", comboBox3.SelectedValue);
                    command.Parameters.AddWithValue("@Груз", comboBox1.Text);
                    command.Parameters.AddWithValue("@Общая_стоимость", textBox5.Text.Trim());
                    command.Parameters.AddWithValue("@Пункт_назначения", textBox6.Text.Trim());
                    command.Parameters.AddWithValue("@Пункт_отправления", textBox4.Text.Trim()); // Добавил параметр для пункта отправления

                    command.ExecuteNonQuery();
                }

                // Обновляем таблицу данных
                LoadContractData();
                MessageBox.Show("Запись успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Метод для проверки существования клиента
        private int GetClientId(SQLiteConnection connection, string lastName, string firstName, string middleName)
        {
            string query = @"
SELECT ID
FROM Клиент
WHERE Фамилия = @Фамилия AND Имя = @Имя AND Отчество = @Отчество;"; // Заменено на "ID"

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Фамилия", lastName);
                command.Parameters.AddWithValue("@Имя", firstName);
                command.Parameters.AddWithValue("@Отчество", middleName);


                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }




        private int GetClientId(string фамилия, string имя, string отчество)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                SELECT ID FROM Клиент 
                WHERE Фамилия = @Фамилия AND Имя = @Имя AND Отчество = @Отчество";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Фамилия", фамилия);
                    command.Parameters.AddWithValue("@Имя", имя);
                    command.Parameters.AddWithValue("@Отчество", отчество);
                    object result = command.ExecuteScalar();
                    return result == null ? -1 : Convert.ToInt32(result);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Вы уверены, что хотите удалить эту запись?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Договор WHERE ID = @ID";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells["ID"].Value);
                        command.ExecuteNonQuery();
                    }
                }

                LoadContractData();
                MessageBox.Show("Запись успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadComboBoxes()
        {
            // Очищаем все ComboBox перед загрузкой данных
            comboBox1.DataSource = null;
            comboBox2.DataSource = null;
            comboBox3.DataSource = null;

            // Загружаем данные для ComboBox1 (например, список типов груза)
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ID, Тип_груза FROM Грузы"; // Загружаем только ID и Тип_груза для ComboBox1

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Загружаем данные в ComboBox1
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Добавляем псевдозначение для пустого выбора
                    DataRow row = dt.NewRow();
                    row["Тип_груза"] = "Выберите тип груза";  // Строка с пустым значением
                    row["ID"] = -1;  // Устанавливаем ID для "пустого" выбора
                    dt.Rows.InsertAt(row, 0);

                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "Тип_груза"; // Название отображаемого элемента
                    comboBox1.ValueMember = "ID"; // Значение, которое будет использоваться при выборе

                    // Устанавливаем индекс на первое пустое значение
                    comboBox1.SelectedIndex = 0;
                }
            }

            // Загружаем данные для ComboBox2 (например, список водителей)
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ID, Фамилия || ' ' || Имя || ' ' || Отчество AS Водитель FROM Водители"; // Пример запроса для ComboBox2

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Загружаем данные в ComboBox2
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Добавляем псевдозначение для пустого выбора
                    DataRow row = dt.NewRow();
                    row["Водитель"] = "Выберите водителя";  // Строка с пустым значением
                    row["ID"] = -1;  // Устанавливаем ID для "пустого" выбора
                    dt.Rows.InsertAt(row, 0);

                    comboBox2.DataSource = dt;
                    comboBox2.DisplayMember = "Водитель"; // Отображаемое имя водителя
                    comboBox2.ValueMember = "ID"; // Значение, которое будет использоваться при выборе

                    // Устанавливаем индекс на первое пустое значение
                    comboBox2.SelectedIndex = 0;
                }
            }

            // Загружаем данные для ComboBox3 (например, список автомобилей)
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ID, Марка || ' ' || Модель AS Автомобиль FROM Транспортное_средство"; // Пример запроса для ComboBox3

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Загружаем данные в ComboBox3
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Добавляем псевдозначение для пустого выбора
                    DataRow row = dt.NewRow();
                    row["Автомобиль"] = "Выберите автомобиль";  // Строка с пустым значением
                    row["ID"] = -1;  // Устанавливаем ID для "пустого" выбора
                    dt.Rows.InsertAt(row, 0);

                    comboBox3.DataSource = dt;
                    comboBox3.DisplayMember = "Автомобиль"; // Отображаемое имя автомобиля
                    comboBox3.ValueMember = "ID"; // Значение, которое будет использоваться при выборе

                    // Устанавливаем индекс на первое пустое значение
                    comboBox3.SelectedIndex = 0;
                }
            }
        }
    }
}
