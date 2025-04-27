using MenagMeWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Application.Interfaces.DataInterfaces
{
    public interface IMenagMeDbContext
    {
        DbSet<User> Users { get; set; }
    }
}
