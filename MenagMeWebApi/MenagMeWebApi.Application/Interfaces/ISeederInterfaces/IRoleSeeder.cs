using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Application.Interfaces.ISeederInterfaces
{
    public interface IRoleSeeder
    {
        Task SeedRolesAsync();
    }
}
