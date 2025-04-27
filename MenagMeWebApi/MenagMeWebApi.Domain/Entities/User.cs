using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Domain.Entities
{
    public class User: IdentityUser
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }
}
