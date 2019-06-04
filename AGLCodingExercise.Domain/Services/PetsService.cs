using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGLCodingExercise.Domain.Abstractions;
using AGLCodingExercise.Domain.Model;
using Microsoft.Extensions.Logging;

namespace AGLCodingExercise.Domain.Services
{
    public class PetsService : IPetsService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly ILogger<PetsService> _logger;

        public PetsService(IPeopleRepository peopleRepository, ILogger<PetsService> logger)
        {
            _peopleRepository = peopleRepository;
            _logger = logger;
        }

        public async Task<IList<PetsGroupingByOwnerGender>> GetCatsGroupedByOwnerGender()
        {
            _logger.LogDebug("Entered PetsService:GetCatsGroupedByOwnerGender");

            var groupings = new List<PetsGroupingByOwnerGender>();
            var people = await _peopleRepository.GetPeople();

            var distinctGendersOfPetOwners = people.Select(a => a.Gender).Distinct();

            foreach (var gender in distinctGendersOfPetOwners)
            {
                var grouping = new PetsGroupingByOwnerGender { OwnerGender = gender };
                foreach (var person in people)
                {
                    if (person.Gender == gender && person.Pets != null)
                    {
                        grouping.Pets.AddRange(person.Pets.Where(x => string.Equals(x.Type, PetTypes.Cat, StringComparison.OrdinalIgnoreCase)));
                    }
                }

                grouping.Pets = grouping.Pets.OrderBy(a => a.Name).ToList();
                groupings.Add(grouping);
            }

            _logger.LogDebug("Exited PetsService:GetCatsGroupedByOwnerGender");
            return groupings;
        }
    }
}
