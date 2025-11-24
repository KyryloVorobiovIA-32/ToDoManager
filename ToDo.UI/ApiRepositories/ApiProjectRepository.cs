using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using ToDo.Models;
using ToDo.Repositories;

namespace ToDo.UI.ApiRepositories
{
    public class ApiProjectRepository : IRepository<Project>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5260/api/projects";

        public ApiProjectRepository()
        {
            _httpClient = new HttpClient();
        }

        public IEnumerable<Project> GetAll()
        {
            try {
                return _httpClient.GetFromJsonAsync<List<Project>>(_baseUrl).Result ?? new List<Project>();
            } catch { return new List<Project>(); }
        }

        public void Add(Project entity)
        {
            var response = _httpClient.PostAsJsonAsync(_baseUrl, entity).Result;
            response.EnsureSuccessStatusCode();
            
            // *** ВИПРАВЛЕННЯ: Зчитуємо створений об'єкт, щоб отримати його ID ***
            var createdEntity = response.Content.ReadFromJsonAsync<Project>().Result;
            if (createdEntity != null)
            {
                entity.ProjectId = createdEntity.ProjectId;
            }
        }
        
        // Остальные методы пока пустые, они нам не нужны для лабы
        public Project? GetById(int id) => null;
        public void Update(Project entity) { }
        public void Delete(int id) { }
    }
}