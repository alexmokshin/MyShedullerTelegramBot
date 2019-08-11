using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace TelegramShedullerApp.DB
{
    public class MongoDbContext
    {
        private static Stopwatch _stopWatch;
        private static IMongoDatabase MongoDatabase { get; set; }

        private static string MongoCollectionName;
        
        public MongoDbContext(MongoSettings settings)
        {
            Console.WriteLine("Call constructor for {0}",this.ToString());
            if (MongoDatabase == null)
            {
                Console.WriteLine("Try to get MongoConnection");
                MongoCollectionName = settings.Collection;
                var connection = new MongoConnection(settings);
                MongoDatabase = connection.GetMongoDatabaseAsync();
            }
        }

        public async Task InsertNewTask(TelegramShedullerApp.Models.UserTask myUserTask)
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            var p = myUserTask.ToBsonDocument();
            Console.WriteLine("Time to boxing class {0} or {1}ticks",_stopWatch.ElapsedMilliseconds, _stopWatch.ElapsedTicks);

            //Console.WriteLine("{0} Try to get collection",DateTime.Now);
            _stopWatch.Restart();
            var collection = MongoDatabase.GetCollection<BsonDocument>(MongoCollectionName);
            Console.WriteLine("Collection return {0}ms or {1}ticks",_stopWatch.ElapsedMilliseconds,_stopWatch.ElapsedTicks);

            _stopWatch.Restart();
            try
            { 
                await collection.InsertOneAsync(p);
            }
            catch(Exception)
            {
                throw;
            }
            Console.WriteLine("Elapsed time to insert doc {0}",_stopWatch.ElapsedMilliseconds);
        }

        public async Task<List<Models.UserTask>> GetTasksForUser(long userId)
        {
            List<Models.UserTask> tasks = new List<Models.UserTask>();

            var collection = MongoDatabase.GetCollection<Models.UserTask>(MongoCollectionName);
            var userFilter = new BsonDocument("$and", new BsonArray
            {
                new BsonDocument("UserId", userId),
                new BsonDocument("TaskDate", new BsonDocument("$gte",DateTime.Now))
            });
            if (collection != null)
                tasks = await collection.FindAsync(userFilter).Result.ToListAsync();

            return tasks;
        }


    }
}
