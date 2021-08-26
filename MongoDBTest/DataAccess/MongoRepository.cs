using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBTest.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBTest
{
    public class MongoRepository<T> where T : ModelBase
    {
        private IMongoDatabase _database;

        private MongoClient _mongoClient;

        public MongoRepository(string connectionString, string databaseName)
        {
            this._mongoClient = new MongoClient(connectionString);
            this._database = _mongoClient.GetDatabase(databaseName);
        }

        public async Task<List<T>> GetDocuments(List<KeyValuePair<string, string>> filters = null)
        {
            var collection = GetCollection(typeof(T));
            var filterBuilder = Builders<T>.Filter;
            var filterDefinitions = filterBuilder.Empty;

            if (filters != null)
            {
                filters.ForEach(filterItem => {
                    var filterDefinition = filterBuilder.Eq(filterItem.Key, filterItem.Value);
                    filterDefinitions &= filterDefinition;
                });
            }

            return await collection.Find(filterDefinitions).ToListAsync();
        }

        public async Task AddDocument(T document)
        {
            var collection = GetCollection(typeof(T));
            await collection.InsertOneAsync(document);
        }

        public async Task AddDocuments(List<T> documents)
        {
            var collection = GetCollection(typeof(T));
            await collection.InsertManyAsync(documents);
        }

        public async Task DeleteDocument(ObjectId id)
        {
            var collection = GetCollection(typeof(T));
            await collection .DeleteOneAsync(document => document._id == id);
        }

        public async Task DeleteDocumentsById(List<ObjectId> ids)
        {
            var collection = GetCollection(typeof(T));
            await collection.DeleteManyAsync(document => ids.Contains(document._id.Value));
        }

        public async Task DeleteDocumentsByFilters(List<KeyValuePair<string, string>> filters)
        {
            var collection = GetCollection(typeof(T));
            var filterBuilder = Builders<T>.Filter;
            var filterDefinitions = filterBuilder.Empty;

            filters.ForEach(filterItem =>
            {
                var filterDefinition = filterBuilder.Eq(filterItem.Key, filterItem.Value);
                filterDefinitions &= filterDefinition;
            });

            await collection.DeleteManyAsync(filterDefinitions);
        }

        private IMongoCollection<T> GetCollection(Type type)
        {
            return _database.GetCollection<T>(type.Name);
        }
    }
}
