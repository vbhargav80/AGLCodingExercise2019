using System;
using System.Collections.Generic;
using System.Text;

namespace AGLCodingExercise.Domain.Model
{
    public class PetsGroupingByOwnerGender
    {
        public string OwnerGender { get; set; }
        public List<Pet> Pets { get; set; }

        public PetsGroupingByOwnerGender()
        {
            Pets = new List<Pet>();
        }
    }
}
