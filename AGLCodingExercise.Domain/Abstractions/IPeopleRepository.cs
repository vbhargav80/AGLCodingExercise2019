using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AGLCodingExercise.Domain.Model;

namespace AGLCodingExercise.Domain.Abstractions
{
    public interface IPeopleRepository
    {
        Task<List<Person>> GetPeople();
    }
}
