using ArchitecturalStudioTradition.Contract.v1.EventBus.Messages;
using ArchitecturalStudioTradition.Domain.Projects;
using DotNetCore.CAP;

namespace ArchitecturalStudioTradition.FileStorage.WebApi.EventBus;

[CapSubscribe("projects")]
public class ProjectSubscriberService : ICapSubscribe
{
    [CapSubscribe("create", isPartial: true)]
    public async Task Create(Project project)
    {
        
    }
}
