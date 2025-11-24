using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDo.Models;

namespace ToDo.UI.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5260/api/users";

        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        public User? CurrentUser { get; private set; }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var loginData = new User { Username = username, PasswordHash = password };
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    CurrentUser = await response.Content.ReadFromJsonAsync<User>();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка з'єднання з сервером: " + ex.Message);
            }
            return false;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            try
            {
                var newUser = new User { Username = username, PasswordHash = password };
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/register", newUser);

                if (response.IsSuccessStatusCode)
                {
                    CurrentUser = await response.Content.ReadFromJsonAsync<User>();
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка реєстрації: " + ex.Message);
            }
            return false;
        }
    }
}