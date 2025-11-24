namespace ToDo.UI;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TextBox txtLogin;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Button btnLogin;
    private System.Windows.Forms.Button btnRegister;
    private System.Windows.Forms.Label lblLogin;
    private System.Windows.Forms.Label lblPassword;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.txtLogin = new System.Windows.Forms.TextBox();
        this.txtPassword = new System.Windows.Forms.TextBox();
        this.btnLogin = new System.Windows.Forms.Button();
        this.btnRegister = new System.Windows.Forms.Button();
        this.lblLogin = new System.Windows.Forms.Label();
        this.lblPassword = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // txtLogin
        // 
        this.txtLogin.Location = new System.Drawing.Point(30, 40);
        this.txtLogin.Size = new System.Drawing.Size(220, 23);
        // 
        // txtPassword
        // 
        this.txtPassword.Location = new System.Drawing.Point(30, 90);
        this.txtPassword.Size = new System.Drawing.Size(220, 23);
        this.txtPassword.PasswordChar = '*'; // Ховаємо пароль
        // 
        // btnLogin
        // 
        this.btnLogin.Location = new System.Drawing.Point(30, 130);
        this.btnLogin.Size = new System.Drawing.Size(100, 30);
        this.btnLogin.Text = "Увійти";
        this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
        // 
        // btnRegister
        // 
        this.btnRegister.Location = new System.Drawing.Point(150, 130);
        this.btnRegister.Size = new System.Drawing.Size(100, 30);
        this.btnRegister.Text = "Реєстрація";
        this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
        // 
        // Labels
        // 
        this.lblLogin.Location = new System.Drawing.Point(30, 20);
        this.lblLogin.Text = "Логін:";
        this.lblLogin.AutoSize = true;
        
        this.lblPassword.Location = new System.Drawing.Point(30, 70);
        this.lblPassword.Text = "Пароль:";
        this.lblPassword.AutoSize = true;

        this.ClientSize = new System.Drawing.Size(284, 181);
        this.Controls.Add(this.lblPassword);
        this.Controls.Add(this.lblLogin);
        this.Controls.Add(this.btnRegister);
        this.Controls.Add(this.btnLogin);
        this.Controls.Add(this.txtPassword);
        this.Controls.Add(this.txtLogin);
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Авторизація";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}