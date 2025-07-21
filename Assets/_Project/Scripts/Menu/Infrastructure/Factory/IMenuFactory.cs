using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Menu.Infrastructure.Factory
{
  public interface IMenuFactory
  {
    UniTask<CharacterPreviewComponent> CreateCharacterPreview();
  }
}