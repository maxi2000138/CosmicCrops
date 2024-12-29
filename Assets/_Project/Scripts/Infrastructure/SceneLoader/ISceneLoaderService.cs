using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Loader
{
    public interface ISceneLoaderService
    {
        UniTask Load(string name);
    }
}