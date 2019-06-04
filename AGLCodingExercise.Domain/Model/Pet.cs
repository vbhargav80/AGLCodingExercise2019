namespace AGLCodingExercise.Domain.Model
{
    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Person Owner { get; set; }
    }
}
