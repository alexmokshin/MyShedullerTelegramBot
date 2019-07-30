using System;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace TelegramShedullerApp.DB
{
    public static class MongoConnection
    {


        private static string UserName { get; set; } = "dbAdmin";

        private static string Password { get; set; } = "ab123456";

        private static string Url { get; set; } = "cluster0-pcf2p.mongodb.net";

        static string Datatbase { get; set; } = "shedullerbotdb";

       // private static IOptions<MongoSettings> options;


        
        /*private static Task<MongoClient> GetDbClient ()
        {
            /*if (!String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(url) && !String.IsNullOrEmpty(datatbase))
            {
                UserName = username;
                Password = password;
                Url = url;
                Datatbase = datatbase;
            }*/


     /*       StringBuilder connectionStringBuilder = new StringBuilder();
            connectionStringBuilder.AppendFormat(@"mongodb+srv://{0}:{1}@{2}/test?retryWrites=true",UserName,Password,Url);
            

            var client = new MongoDB.Driver.MongoClient(connectionStringBuilder.ToString());
            
            Console.WriteLine("Get client");

            return client;
        }*/

        public static IMongoDatabase GetMongoDatabaseAsync ()
        {
            StringBuilder connectionStringBuilder = new StringBuilder();
            connectionStringBuilder.AppendFormat(@"mongodb+srv://{0}:{1}@{2}/test?retryWrites=true", UserName, Password, Url);

            var mongoDatabase = Task<MongoClient>.Factory.StartNew( ()  => {
                var client = new MongoClient(connectionStringBuilder.ToString());
                return client;
            } );

            Console.WriteLine("{0} Try to get Mongoclient database",DateTime.Now);
            var databaseConn = mongoDatabase.Result.GetDatabase(Datatbase);
            Console.WriteLine("{0} Mongoclient database return",DateTime.Now);
            return databaseConn;
        }
    }


}
