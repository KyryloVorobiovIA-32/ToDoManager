using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ToDo.Models;
using ToDo.Repositories;
using Task = ToDo.Models.Task;

namespace ToDo.UI.ApiRepositories
{
    public class ApiTaskRepository : IRepository<Task>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5260/api/tasks"; 

        public ApiTaskRepository()
        {
            _httpClient = new HttpClient();
        }

        public IEnumerable<Task> GetAll()
        {
            try 
            {
                // Використовуємо синхронне очікування (.Result) для сумісності з інтерфейсом
                return _httpClient.GetFromJsonAsync<List<Task>>(_baseUrl).Result ?? new List<Task>();
            }
            catch (Exception)
            {
                // Якщо сервер вимкнений, повертаємо пустий список, щоб програма не впала
                return new List<Task>();
            }
        }

        public Task? GetById(int id)
        {
            try
            {
                return _httpClient.GetFromJsonAsync<Task>($"{_baseUrl}/{id}").Result;
            }
            catch
            {
                return null;
            }
        }

        public void Add(Task entity)
        {
            var response = _httpClient.PostAsJsonAsync(_baseUrl, entity).Result;
            response.EnsureSuccessStatusCode();

            // *** ВИПРАВЛЕННЯ: Оновлюємо ID завдання ***
            var createdEntity = response.Content.ReadFromJsonAsync<Task>().Result;
            if (createdEntity != null)
            {
                entity.TaskId = createdEntity.TaskId;
            }
        }

        public void Update(Task entity)
        {
            var response = _httpClient.PutAsJsonAsync($"{_baseUrl}/{entity.TaskId}", entity).Result;
            response.EnsureSuccessStatusCode();
        }

        public void Delete(int id)
        {
            var response = _httpClient.DeleteAsync($"{_baseUrl}/{id}").Result;
            response.EnsureSuccessStatusCode();
        }
    }
}