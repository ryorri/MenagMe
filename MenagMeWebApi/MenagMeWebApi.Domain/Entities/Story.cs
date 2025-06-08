using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenagMeWebApi.Domain.Enums;

namespace MenagMeWebApi.Domain.Entities
{
    public class Story
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; } = null!;
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public required PriorityEnum Priority { get; set; }
        public required StateEnum Status { get; set; }
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public required string ProjectId { get; set; }

    }
}
