using MenagMeWebApi.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Application.Objects.StoriesDTO
{
    public class StoryDataDTO
    {
        public required string  Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public required string Priority { get; set; }
        public required string Status { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required string UserId { get; set; }
        public required string ProjectId { get; set; }
    }
}
