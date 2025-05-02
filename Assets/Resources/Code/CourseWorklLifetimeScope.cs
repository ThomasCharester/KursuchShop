using Resources.Code;
using VContainer;
using VContainer.Unity;

public class CourseWorklLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<UIQuerySender>();
        builder.RegisterComponentInHierarchy<SimpleTCPClient>();
    }
}
