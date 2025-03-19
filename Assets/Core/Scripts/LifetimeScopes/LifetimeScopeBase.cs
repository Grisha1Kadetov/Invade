using Core.Input;
using VContainer;
using VContainer.Unity;

namespace Core.Scripts.LifetimeScopes
{
    public class LifetimeScopeBase : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InputSystem>().AsSelf();
            base.Configure(builder);
        }
    }
}