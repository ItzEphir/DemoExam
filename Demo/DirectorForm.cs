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
    public partial class DirectorForm : Form
    {
        List<User> users;

        public DirectorForm()
        {
            InitializeComponent();
        }

        public DirectorForm(List<User> users) : this()
        {
            this.users = users;
        }

        private void button1_Click(object sender, EventArgs e) // выход из окна
        {
            this.Close();
        }

        private void DirectorForm_Load(object sender, EventArgs e) // загрузка таблицы и диаграммы
        {
            for(int i = 0; i < users.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = users[i].Id;
                dataGridView1.Rows[i].Cells[1].Value = users[i].LastName + " " + users[i].Name;
                dataGridView1.Rows[i].Cells[2].Value = users[i].Result;
            }

            for(int i = 0; i<users.Count; i++)
            {
                chart1.Series[0].Points.Add();
                chart1.Series[0].Points[i].XValue = int.Parse(users[i].Id);
                if (float.Parse(users[i].Result) < 0)
                {
                    chart1.Series[0].Points[i].SetValueY(0);
                }
                else
                {
                    chart1.Series[0].Points[i].SetValueY(float.Parse(users[i].Result));
                }
            }
        }
    }
}

