using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using ToDo.Models;
using ToDo.Repositories;

namespace ToDo.UI.ApiRepositories
{
    public class ApiUserRepository : IRepository<User>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5260/api/users"; // Твой порт

        public ApiUserRepository()
        {
            _httpClient = new HttpClient();
        }

        public IEnumerable<User> GetAll()
        {
            try {
                // Для упрощения: просто получаем всех юзеров
                // В реальности тут был бы метод GetCurrentUser, но пока так
                return _httpClient.GetFromJsonAsync<List<User>>(_baseUrl).Result ?? new List<User>();
            } catch { return new List<User>(); }
        }

        public User? GetById(int id) => null; // Не нужно для UI
        public void Add(User entity) { } // Регистрация идет через AuthService
        public void Update(User entity) { }
        public void Delete(int id) { }
    }
}