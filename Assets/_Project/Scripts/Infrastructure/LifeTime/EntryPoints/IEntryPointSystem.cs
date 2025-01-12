using System;
using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.LifeTime.EntryPoints
{
    public interface IEntryPointSystem : IInitializable, IStartable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
    }
}