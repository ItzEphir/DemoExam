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
    public partial class WorkerForm : Form
    {
        List<User> users;
        User Worker;

        public WorkerForm()
        {
            InitializeComponent();
        }

        public WorkerForm(User worker, List<User> users) : this()
        {
            this.Worker = worker;
            this.users = users;
        }

        private void WorkerForm_Load(object sender, EventArgs e) // загрузка приветствия и результативности работника
        {
            label1.Text += Worker.Result;
            label2.Text += $"{this.Worker.LastName} {this.Worker.Name}";
        }

        private void button1_Click(object sender, EventArgs e) // нажатие функциональной кнопки
        {
            this.Worker.PressedResult();
            RefreshLabel();
        }

        void RefreshLabel() // обновление результата
        {
            label1.Text = "Ваш результат: " + this.Worker.Result;
            try
            {
                byte[] buffer;
                FileStream fs = new FileStream("data.csv", FileMode.Truncate);
                buffer = Encoding.Default.GetBytes("Id;Last_name;Name;Login;Password;Role;Result\n");
                fs.Write(buffer, 0, buffer.Length);
                foreach (User u in users)
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

        private void button2_Click(object sender, EventArgs e) // выход из программы
        {
            this.Close();
        }
    }
}
