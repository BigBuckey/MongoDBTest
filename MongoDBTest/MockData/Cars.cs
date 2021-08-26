using System;
using System.Collections.Generic;
using MongoDBTest.Models;

namespace MongoDBTest.MockData
{
    public static class Cars
    {
        public static List<Car> Get
        {
            get
            {
                return new List<Car>()
                {
                    new Car() { CreatedDate = DateTimeOffset.UtcNow, Make = "Ford", Mileage = 212121.1m, ModelYear=1998 },
                    new Car() { CreatedDate = DateTimeOffset.UtcNow.AddDays(-1), Make = "Chevy", Mileage = 123456, ModelYear=2001 },
                    new Car() { CreatedDate = DateTimeOffset.UtcNow.AddDays(-2), Make = "Chevy", Mileage = 1255.2m, ModelYear=2003 },
                    new Car() { CreatedDate = DateTimeOffset.UtcNow.AddDays(-3), Make = "Chevrolet", Mileage = 54053.8m, ModelYear=2005 },
                    new Car() { CreatedDate = DateTimeOffset.UtcNow.AddDays(-4), Make = "Ford", Mileage = 1245678.1m, ModelYear=1992},
                    new Car() { CreatedDate = DateTimeOffset.UtcNow, Make = "Ford", Mileage = 987654, ModelYear=2006 },
                    new Car() { CreatedDate = DateTimeOffset.UtcNow, Make = "Ford", Mileage = -10, ModelYear=2012 },
                    new Car() { CreatedDate = DateTimeOffset.UtcNow, Make = "Ford", Mileage = 0, ModelYear=2021 }
                };
            }
        }
    }
}
