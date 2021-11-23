using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildCounter2._0.Projects
{
    internal class ProjectModel
    {
        public string? Name { get; set; } 
        public string? Description { get; set; } 
        

    }

    internal class LoadedProjectModel : ProjectModel
    {
        public string SaveFileLocation { get; set; }

    }
}
