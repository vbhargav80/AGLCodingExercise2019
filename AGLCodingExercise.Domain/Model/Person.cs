using System;
using System.Collections.Generic;
using System.Text;

namespace AGLCodingExercise.Domain.Model
{
    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<Pet> Pets { get; set; }

        public Person()
        {
            Pets = new List<Pet>();
        }
    }
}
