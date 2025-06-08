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
    public class Tasks
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required PriorityEnum Priority { get; set; }
        public required StateEnum Status { get; set; }
        public required DateTime EstimatedTime { get; set; }
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string StoryId { get; set; }
        public string AssignedUserId { get; set; } = string.Empty;

    }
}
