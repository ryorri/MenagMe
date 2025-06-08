using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Domain.Entities
{
    public class User: MongoUser
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required string RefreshToken { get; set; }
    }
}
