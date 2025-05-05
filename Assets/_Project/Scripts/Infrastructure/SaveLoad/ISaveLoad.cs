using _Project.Scripts.Infrastructure.Progress;

namespace _Project.Scripts.Infrastructure.SaveLoad
{
    public interface ISaveLoad<T> : IData<T>
    {
        void Save(T data);
        T Load();
    }
}