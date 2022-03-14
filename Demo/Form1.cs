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
    public partial class Form1 : Form
    {
        List<User> users = new List<User>(); // список пользователей

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> info = new List<string>(); // список информации в string
            try 
            {
                FileStream fs = new FileStream("data.csv", FileMode.Open); // открытие файла
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                info = Encoding.Default.GetString(buffer).Split(new char[] {'\n', ';'}).ToList<string>(); // переводим информацию из byte в список string
                fs.Close();
            }
            catch (FileNotFoundException exception) // если файла не существует
            {
                MessageBox.Show($"Ошибка!\nФайла не существует!\n{exception.Message}");
                this.Close();
                return;
            }

            // info.ForEach(inf => MessageBox.Show(inf));

            for(int i = 1; i < info.Count / 7; i++)
            {
                users.Add(new User(info[i * 7], info[i * 7 + 1], info[i * 7 + 2], info[i * 7 + 3],
                    info[i * 7 + 4], info[i * 7 + 5], info[i * 7 + 6]));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User temp = new User(textBox1.Text, maskedTextBox1.Text); // данные, которые ввел пользователь

            // users.ForEach(user => MessageBox.Show(user.Login + user.Password));

            foreach(User u in users) 
            {
                if (u.IsEqual(temp)) // сверяемся, есть ли пользователь в базе, данные которого ввели
                {
                    switch (u.Role) // просмотр роли и вывод соответствующего окна
                    {
                        case "worker": 

                            WorkerForm workerForm = new WorkerForm(u, users);

                            this.Hide();

                            workerForm.ShowDialog();

                            this.Show();

                            return;
                        case "admin":

                            AdminForm adminForm = new AdminForm(users);

                            this.Hide();

                            adminForm.ShowDialog();

                            this.Show();

                            return;
                        case "director":

                            DirectorForm directorForm = new DirectorForm(users);

                            this.Hide();

                            directorForm.ShowDialog();

                            this.Show();

                            return;
                    }

                    break;
                }
            }

            MessageBox.Show("Пользователь не найден!");
        }

        private void button2_Click(object sender, EventArgs e) // кнопка выхода
        {
            this.Close();
        }
    }
}
