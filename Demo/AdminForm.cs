using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Demo
{
    public partial class AdminForm : Form
    {
        List<User> users;

        public AdminForm()
        {
            InitializeComponent();
        }

        public AdminForm(List<User> users) : this()
        {
            this.users = users;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }
        
        private void button1_Click(object sender, EventArgs e) // нажатие кнопки "добавить"
        {
            AddForm addForm = new AddForm(ref users);

            this.Hide();

            addForm.ShowDialog();

            this.Show();

            // RefreshTable();
        }

        private void button2_Click(object sender, EventArgs e) // нажатие кнопки "удалить"
        {
            var selectedRows = dataGridView1.SelectedRows;

            if (selectedRows != null)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Id == selectedRows[0].Cells[0].Value.ToString())
                    {
                        users.RemoveAt(i);
                    }
                }
            }
            RefreshTable();
        }
        
        private void button3_Click(object sender, EventArgs e) // нажатие кнопки изменить
        {
            var selectedRows = dataGridView1.SelectedRows;

            if (selectedRows != null)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Id == selectedRows[0].Cells[0].Value.ToString())
                    {
                        AddForm addForm = new AddForm(ref users, users[i]);

                        this.Hide();

                        addForm.ShowDialog();

                        this.Show();
                    }
                }
            }

            // RefreshTable();
        }

        private void button4_Click(object sender, EventArgs e) // выход из окна
        {
            this.Close();
        }

        void RefreshTable() // обновление таблицы
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < users.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = users[i].Id;
                dataGridView1.Rows[i].Cells[1].Value = users[i].Login;
                dataGridView1.Rows[i].Cells[2].Value = users[i].Password;
                dataGridView1.Rows[i].Cells[3].Value = users[i].Role;
                dataGridView1.Rows[i].Cells[4].Value = users[i].LastName + " " + users[i].Name;
            }

            try // выгрузка данных в файл
            {
                byte[] buffer;
                FileStream fs = new FileStream("data.csv", FileMode.Truncate);
                buffer = Encoding.Default.GetBytes("Id;Last_name;Name;Login;Password;Role;Result\n");
                fs.Write(buffer, 0, buffer.Length);
                foreach(User u in users)
                {
                    buffer = Encoding.Default.GetBytes($"{u.Id};{u.LastName};{u.Name};{u.Login};{u.Password};{u.Role};{u.Result}\n");
                    fs.Write(buffer, 0, buffer.Length);
                }
                fs.Close();
            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show($"Ошибка!\nФайла не существует!\n{exception.Message}");
                this.Close();
                return;
            }
        }
    }
}
