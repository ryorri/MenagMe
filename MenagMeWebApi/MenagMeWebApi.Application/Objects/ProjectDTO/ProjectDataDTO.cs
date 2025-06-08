using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagMeWebApi.Application.Objects.ProjectDTO
{
    public class ProjectDataDTO
    {
        public required string Id { get; set; } 
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
