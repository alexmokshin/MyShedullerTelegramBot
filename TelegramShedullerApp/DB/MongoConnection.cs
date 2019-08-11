using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Options;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace TelegramShedullerApp.DB
{
    public class MongoConnection
    {
        private static MongoSettings MongoSettings;
        private IMongoDatabase MongoDatabase;
        public IMongoDatabase GetMongoDatabaseAsync ()
        {
            StringBuilder connectionStringBuilder = new StringBuilder();
            connectionStringBuilder.AppendFormat(@"mongodb+srv://{0}:{1}@{2}/test?retryWrites=true", MongoSettings.Username, MongoSettings.Password, MongoSettings.Url);

            var mongoClient = Task<MongoClient>.Factory.StartNew( ()  => {
                var client = new MongoClient(connectionStringBuilder.ToString());
                return client;
            } );

            Console.WriteLine("Try to get Mongoclient database {0}",MongoSettings.Database);
            try
            {
                MongoDatabase = mongoClient.Result.GetDatabase(MongoSettings.Database);
                Console.WriteLine("Mongoclient database {0} return successful",MongoSettings.Database);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error to return Mongoclient database: {0}",ex.Message);
            }

            return MongoDatabase;
        }

        public MongoConnection (MongoSettings settings)
        {
            Console.WriteLine("Call constructor for {0}",this.ToString());
            MongoSettings = settings;
        }
    }


}
