using VContainer;

namespace _Project.Scripts._Infrastructure.MVVM.UI
{
    public abstract class UIRouter
    {
        protected readonly IObjectResolver ObjectResolver;

        protected UIRouter(IObjectResolver objectResolver)
        {
            ObjectResolver = objectResolver;
        }
    }
}