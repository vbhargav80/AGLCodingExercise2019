using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AGLCodingExercise.Domain.Abstractions;
using AGLCodingExercise.Domain.Model;
using Microsoft.Extensions.Logging;

namespace AGLCodingExercise.Data
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PeopleRepository> _logger;

        public PeopleRepository(HttpClient httpClient, ILogger<PeopleRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Person>> GetPeople()
        {
            _logger.LogDebug("Entered PeopleRepository:GetPeople");

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync("people.json");
            var people = await response.Content.ReadAsAsync<List<Person>>();

            foreach (var person in people.Where(p => p.Pets != null))
            {
                foreach (var pet in person.Pets)
                {
                    pet.Owner = person;
                }
            }

            _logger.LogDebug("Exited PeopleRepository:GetPeople");
            return people;
        }
    }
}
