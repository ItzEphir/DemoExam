using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class AddForm : Form
    {
        List<User> users;
        User change;

        public AddForm()
        {
            InitializeComponent();
        }

        public AddForm(ref List<User> users) : this()
        {
            this.users = users;
        }

        public AddForm(ref List<User> users, User changing) : this(ref users)
        {
            this.change = changing;
        }
        
        private void AddForm_Load(object sender, EventArgs e) // загрузка таблицы
        {
            if(this.change != null)
            {
                this.textBox1.Text = change.Login;
                this.textBox2.Text = change.Password;
                this.textBox3.Text = change.Name;
                this.textBox4.Text = change.LastName;
                this.textBox5.Text = change.Role;
            }
        }

        private void button1_Click(object sender, EventArgs e) // нажатие кнопки "принять"
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                if (this.change != null)
                {
                    for(int i = 0; i < this.users.Count; i++)
                    {
                        if(this.users[i].Id == this.change.Id)
                        {
                            users[i].Change(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                            break;
                        }
                    }
                }
                else
                {
                    this.users.Add(new User((int.Parse(this.users[this.users.Count - 1].Id) + 1).ToString(), textBox4.Text, textBox3.Text, textBox1.Text, textBox2.Text, textBox5.Text, "0"));
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
