using MenagMeWebApi.Application.Interfaces.DataInterfaces;
using MenagMeWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Infrastructure.Data
{
    public class MenagMeDbContext : IdentityDbContext<User>, IMenagMeDbContext
    {
        public MenagMeDbContext(DbContextOptions<MenagMeDbContext> options) : base(options) { }
        public override DbSet<User> Users { get; set; }
    }
}
