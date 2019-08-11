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
        private Guid Id { get { return Guid.NewGuid(); } }

        
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

        public Task CreateTask(long userId, string taskText, DateTime taskDate)
        {
            this.UserId = userId;
            this.TaskDate = taskDate;
            this.TaskText = taskText;

            return this;
        }

        //TODO: Remove after test ends
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
