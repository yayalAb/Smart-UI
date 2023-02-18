using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Project.Query
{
    public class ProjectsDto : IMapFrom<ProjectModel>
    {
    public int Id { get; set; }
    public string ProjectName { get; set; }
    }
}
