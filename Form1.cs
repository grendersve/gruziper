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
using АИС_грузоперевозки;
using АИС_салона_по_аренде_автомобилей;

namespace АИС_грузоперевозки
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "user")
            {
                Form2 userForm = new Form2();
                userForm.ShowDialog();
                this.Close(); // Закрыть текущую форму после открытия формы пользователя
            }
            else if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                Form3 adminForm = new Form3();
                adminForm.ShowDialog();
                this.Close(); // Закрыть текущую форму после открытия формы администратора
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль. Пожалуйста, попробуйте снова.");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Заменяем каждый символ вводимого пароля на '*'
            textBox2.PasswordChar = '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //        Form2 form2 = new Form2(textBox1.Text, textBox2.Text);
        //        form2.ShowDialog();
        //        this.Close(); // Закрыть текущую форму после открытия формы Form4


        //}

    }
}
