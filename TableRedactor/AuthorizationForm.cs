using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableRedactor
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private string ManagerLogin = "Manager";
        private string ManagerPassword = "Mq2w3e4r5";

        private string UserLogin = "User";
        private string UserPassword = "Ur5e4w3q2";

        private void AuthorizationButton_Click(object sender, EventArgs e)
        {
            string EnteredLogin = LoginTextBox.Text.ToString();
            string EnteredPassword = PasswordTextBox.Text.ToString();

            if(EnteredLogin == ManagerLogin)
            {
                if (EnteredPassword == ManagerPassword)
                {
                    ManagerForm managerForm = new ManagerForm();
                    managerForm.Show();
                    this.Hide();
                }
                else{
                    MessageBox.Show("Вы ввели неверный пароль", "Ошибка:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (EnteredLogin == UserLogin)
            {
                if (EnteredPassword == UserPassword)
                {
                    UserForm userForm = new UserForm();
                    userForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный пароль", "Ошибка:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Вы ввели несуществующий логин", "Ошибка:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
