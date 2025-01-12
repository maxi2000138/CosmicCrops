using _Project.Scripts.Game.Level.Interface;
using _Project.Scripts.Game.Units.Character.Interface;

namespace _Project.Scripts.Game.Level.Model
{
  public class LevelModel
  {
    public ILevel Level { get; private set; }
    public ICharacter Character { get; private set; }

    
    public void SetCharacter(ICharacter character) => Character = character;
    public void SetLevel(ILevel level) => Level = level;
   
    public void Cleanup()
    {
      Character = null;
    }
  }
}