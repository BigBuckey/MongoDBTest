using MongoDB.Bson;
using MongoDBTest.MockData;
using MongoDBTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBTest
{
    class Program
    {
        private const string _connectionString = "mongodb://127.0.0.1:27017";

        private const string _databaseName = "test";

        static void Main(string[] args)
        {
            Console.WriteLine("Beginning tests");
            ExecuteTests().Wait();
            Console.WriteLine("Tests completed!");
            Console.Read();
        }

        public async static Task ExecuteTests()
        {
            // Get mock data
            var mockDogs = Dogs.Get;
            var mockCars = Cars.Get;

            // Initialize repositories
            var dogRepository = new MongoRepository<Dog>(_connectionString, _databaseName);
            var carRepository = new MongoRepository<Car>(_connectionString, _databaseName);

            // Add Dogs individually and Cars in bulk
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await AddDataSingle(mockDogs, dogRepository);
            Console.WriteLine($"Dogs added one record at a time in {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.Restart();
            await AddDataList(mockCars, carRepository);
            Console.WriteLine($"Cars added in bulk in {stopwatch.ElapsedMilliseconds}ms");

            // Get all records from the database
            stopwatch.Restart();
            var dogs = await dogRepository.GetDocuments();
            Console.WriteLine($"{dogs.Count} dogs retrieved in {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.Restart();
            var cars = await carRepository.GetDocuments();
            Console.WriteLine($"{cars.Count} cars retrieved in {stopwatch.ElapsedMilliseconds}ms");

            // Get only the Fords
            stopwatch.Restart();
            var fordFilter = new List<KeyValuePair<string, string>>();
            fordFilter.Add(new KeyValuePair<string, string>("Make", "Ford"));
            var fords = await carRepository.GetDocuments(fordFilter);
            fords.ForEach(ford =>
            {
                Console.WriteLine($"Ford retrieved: {ford.ModelYear} {ford.Make} {ford.Mileage} created {ford.CreatedDate} with ID = {ford._id}");
            });

            Console.WriteLine($"Fords retrieved in {stopwatch.ElapsedMilliseconds}ms");

            // Send all dogs to heaven
            stopwatch.Restart();
            foreach (var dog in dogs)
            {
                await dogRepository.DeleteDocument(dog._id.Value);
            }

            Console.WriteLine($"Dogs deleted individually in {stopwatch.ElapsedMilliseconds}ms");

            // Delete only the Fords
            stopwatch.Restart();
            await carRepository.DeleteDocumentsByFilters(fordFilter);
            Console.WriteLine($"Fords deleted in {stopwatch.ElapsedMilliseconds}ms");

            // Delete the rest of the cars in bulk
            stopwatch.Restart();
            var carIDs = cars.Where(car => car.Make != "Ford").Select(car => car._id).Cast<ObjectId>().ToList();
            await carRepository.DeleteDocumentsById(carIDs);
            Console.WriteLine($"The rest of the cars deleted in {stopwatch.ElapsedMilliseconds}ms");

            // Make sure they were deleted
            stopwatch.Restart();
            dogs = await dogRepository.GetDocuments();
            Console.WriteLine($"{dogs.Count} dogs retrieved in {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.Restart();
            cars = await carRepository.GetDocuments();
            Console.WriteLine($"{cars.Count} cars retrieved in {stopwatch.ElapsedMilliseconds}ms");
        }

        public static async Task AddDataSingle<T>(List<T> data, MongoRepository<T> repository) where T : ModelBase
        {
            foreach (var item in data)
            {
                await repository.AddDocument(item);
            }
        }

        public static async Task AddDataList<T>(List<T> data, MongoRepository<T> repository) where T : ModelBase
        {
            await repository.AddDocuments(data);
        }
    }
}
