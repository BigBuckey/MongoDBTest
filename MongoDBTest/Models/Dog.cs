namespace MongoDBTest.Models
{
    public class Dog : ModelBase
    {
        public string Name { get; set; }

        public string Breed { get; set; }

        public bool IsSpayedOrNeutered { get; set; }

        public Gender MaleOrFemale { get; set; }

        public enum Gender : byte
        {
            Unknown = 0,
            Male = 1,
            Female = 2
        }
    }
}
