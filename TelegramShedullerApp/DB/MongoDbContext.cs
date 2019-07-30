using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace TelegramShedullerApp.DB
{
    public class MongoDbContext
    {
        private static IMongoDatabase MongoDatabase { get; set; }

        private string MongoCollectionName = "todotasks";

        //TODO: Add mongo db credentials
        public MongoDbContext()
        {
            if (MongoDatabase == null)
            {
                Console.WriteLine("{0} Initialize MongoConnection",DateTime.Now);

                MongoDatabase = MongoConnection.GetMongoDatabaseAsync();
            }
        }

        public async Task<string> InsertNewTask(TelegramShedullerApp.Models.Task myTask, IOptions<DB.MongoSettings> options)
        {
            var p = myTask.ToBsonDocument();

            Console.WriteLine("{0} Try to get collection",DateTime.Now);

            var collection = MongoDatabase.GetCollection<BsonDocument>(MongoCollectionName);
            Console.WriteLine("{0} Collection return",DateTime.Now);


            try
            {
                await collection.InsertOneAsync(p);
                return "Success";
            }
            catch(Exception)
            {
                throw;
            }
        }


    }
}
