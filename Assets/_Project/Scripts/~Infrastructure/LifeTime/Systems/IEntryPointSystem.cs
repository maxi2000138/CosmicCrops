using System;
using VContainer.Unity;

namespace _Project.Scripts._Infrastructure.LifeTime.Systems
{
    public interface IEntryPointSystem : IInitializable, IStartable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
    }
}