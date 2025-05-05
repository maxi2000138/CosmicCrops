using System;
using VContainer.Unity;

namespace _Project.Scripts.Infrastructure.LifeTime.EntryPoints.Core
{
    public interface IEntryPoint : IStartable, IDisposable
    {
    }
}