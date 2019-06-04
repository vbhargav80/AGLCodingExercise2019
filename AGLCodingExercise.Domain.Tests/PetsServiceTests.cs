using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGLCodingExercise.Domain.Abstractions;
using AGLCodingExercise.Domain.Model;
using AGLCodingExercise.Domain.Services;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AGLCodingExercise.Domain.Tests
{
    public class PetsServiceTests
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly ILogger<PetsService> _logger;

        public PetsServiceTests()
        {
            _peopleRepository = Substitute.For<IPeopleRepository>();
            _logger = Substitute.For<ILogger<PetsService>>();
        }

        [Fact]
        public async Task GetCatsGroupedByOwnerGender_SortsPetsByFirstname()
        {
            var mockPeopleData = GetMockPeopleData();

            _peopleRepository.GetPeople()
                .Returns(Task.FromResult(mockPeopleData));

            var petsService = new PetsService(_peopleRepository, _logger);
            var actualResult = await petsService.GetCatsGroupedByOwnerGender();

            var maleGroup = actualResult.First(a => a.OwnerGender == "Male");
            var femaleGroup = actualResult.First(a => a.OwnerGender == "Female");

            maleGroup.Pets.Count.Should().Be(2);
            femaleGroup.Pets.Count.Should().Be(4);

            maleGroup.Pets[0].Name.Should().Be("Andrew");
            maleGroup.Pets[1].Name.Should().Be("Billy");

            femaleGroup.Pets[0].Name.Should().Be("CatB");
            femaleGroup.Pets[1].Name.Should().Be("CatC");
            femaleGroup.Pets[2].Name.Should().Be("FelineA");
            femaleGroup.Pets[3].Name.Should().Be("FelineF");
        }

        [Fact]
        public async Task GetCatsGroupedByOwnerGender_ReturnsCatsOnly()
        {
            var mockPeopleData = GetMockPeopleData();

            _peopleRepository.GetPeople()
                .Returns(Task.FromResult(mockPeopleData));

            var petsService = new PetsService(_peopleRepository, _logger);
            var actualResult = await petsService.GetCatsGroupedByOwnerGender();

            foreach (var group in actualResult)
            {
                foreach (var pet in group.Pets)
                {
                    pet.Type.Should().Be(PetTypes.Cat);
                }
            }
        }

        private List<Person> GetMockPeopleData()
        {
            return new List<Person>
            {
                new Person()
                {
                    Gender = "Male",
                    Name = "Varun",
                    Pets = new List<Pet>()
                    {
                        new Pet() {Name = "Billy", Type = "Cat"},
                        new Pet() {Name = "Andrew", Type = "Cat"},
                        new Pet() {Name = "Tom", Type = "Dog"}
                    }
                },
                new Person()
                {
                    Gender = "Female",
                    Name = "Amy",
                    Pets = new List<Pet>()
                    {
                        new Pet() {Name = "CatC", Type = "Cat"},
                        new Pet() {Name = "CatB", Type = "Cat"},
                        new Pet() {Name = "DogA", Type = "Dog"}
                    }
                },
                new Person()
                {
                    Gender = "Female",
                    Name = "Edel",
                    Pets = new List<Pet>()
                    {
                        new Pet() {Name = "FelineF", Type = "Cat"},
                        new Pet() {Name = "FelineA", Type = "Cat"},
                        new Pet() {Name = "Mia", Type = "Dog"}
                    }
                }
            };
        }
    }
}
