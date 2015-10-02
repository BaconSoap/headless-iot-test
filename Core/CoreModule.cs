using Autofac;

namespace Core
{
    public class CoreModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Runner>().AsSelf().InstancePerDependency();
        }
    }
}
