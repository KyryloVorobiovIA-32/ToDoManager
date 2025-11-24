using System;
using System.Windows.Forms;
using ToDo.UI.Services;

namespace ToDo.UI
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;

        // Отримуємо сервіс через конструктор
        public LoginForm(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            bool success = await _authService.LoginAsync(txtLogin.Text, txtPassword.Text);
            if (success)
            {
                this.DialogResult = DialogResult.OK; // Успіх!
                this.Close();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введіть логін і пароль.");
                return;
            }

            bool success = await _authService.RegisterAsync(txtLogin.Text, txtPassword.Text);
            if (success)
            {
                MessageBox.Show("Реєстрація успішна! Вхід виконано.", "Успіх");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}