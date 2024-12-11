using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace АИС_грузоперевозки
{
    public partial class Form2 : Form
    {
        private SQLiteConnection connection;

        public Form2()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            ConnectToDatabase();
            LoadKlientData();
        }

        private void ConnectToDatabase()
        {
            string dbPath = "C:\\Users\\Даниил\\Desktop\\Gruzoperevozki.db";
            connection = new SQLiteConnection($"Data Source={dbPath}");
            connection.Open();
        }

        private void LoadKlientData()
        {
            connection.Open();

            string query = "SELECT " +
                           "c.ID_auto, " +
                           "co.Country AS CountryName, " +
                           "s.Title, " +
                           "s.Color, " +
                           "c.Price, " +
                           "c.Availability " +
                           "FROM Cars c " +
                           "JOIN Country co ON c.ID_country = co.ID " +
                           "JOIN Specificatoins s ON c.ID_auto = s.ID";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
