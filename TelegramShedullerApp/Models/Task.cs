using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace TelegramShedullerApp.Models
{
    [DataContract]
    public class Task
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        private Guid Id { get { return Guid.NewGuid(); } set { Id = value; } }

        
        [BsonElement("UserId")]
        //[BsonRepresentation(BsonType.Int32)]
        [DataMember]
        private long UserId { get; set; }

        [BsonElement("TaskText")]
        //[BsonRepresentation(BsonType.String)]
        [DataMember]
        private string TaskText { get; set; }

        [BsonElement("TaskDate")]
        //[BsonRepresentation(BsonType.DateTime)]
        [DataMember]
        private DateTime TaskDate { get; set; }

        public Task CreateTask(long user_id, string task_text, DateTime task_date)
        {
            //Random r = new Random();
            //this.Id = r.Next(1000000000);
            this.UserId = user_id;
            this.TaskDate = task_date;
            this.TaskText = task_text;

            return this;
        }

        public bool CheckOnNull()
        {
            bool t = false;

            if (UserId == 0 && String.IsNullOrEmpty(TaskText))
            {
                t = true;
            }

            return t;
        }
    }
}
