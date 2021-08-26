using System;

namespace MongoDBTest.Models
{
    public class Car : ModelBase
    {
        public string Make { get; set; }

        public int ModelYear { get; set; }

        public decimal Mileage { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}
