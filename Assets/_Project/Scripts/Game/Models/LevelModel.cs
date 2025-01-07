using _Project.Scripts.Game.Interfaces;

namespace _Project.Scripts.Game.Models
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