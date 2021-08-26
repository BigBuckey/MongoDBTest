using System.Collections.Generic;
using MongoDBTest.Models;

namespace MongoDBTest.MockData
{
    public static class Dogs
    {
        public static List<Dog> Get
        {
            get
            {
                return new List<Dog>
                {
                    new Dog() { Breed="Pit Bull", IsSpayedOrNeutered=false, MaleOrFemale=Dog.Gender.Male, Name="Biter"},
                    new Dog() { Breed="Pit Bull", IsSpayedOrNeutered=false, MaleOrFemale=Dog.Gender.Female, Name="Spider"},
                    new Dog() { Breed="Chihuaua", IsSpayedOrNeutered=false, MaleOrFemale=Dog.Gender.Unknown, Name="Turd"},
                    new Dog() { Breed="Chihuaua", IsSpayedOrNeutered=false, MaleOrFemale=Dog.Gender.Male, Name="Nerd"},
                    new Dog() { Breed="Chihuaua", IsSpayedOrNeutered=false, MaleOrFemale=Dog.Gender.Male, Name="Taco"},
                    new Dog() { Breed="Chihuaua", IsSpayedOrNeutered=false, MaleOrFemale=Dog.Gender.Female, Name="Butch"},
                    new Dog() { Breed="Chihuaua", IsSpayedOrNeutered=false, MaleOrFemale=Dog.Gender.Female, Name="Maximus"},
                    new Dog() { Breed="Chihuaua"}
                };
            }
        }
    }
}
