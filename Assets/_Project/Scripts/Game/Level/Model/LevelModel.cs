using System.Collections.Generic;
using _Project.Scripts.Game.Level.Interface;
using _Project.Scripts.Game.Loot.Interface;
using _Project.Scripts.Game.Units.Character.Interface;

namespace _Project.Scripts.Game.Level.Model
{
  public class LevelModel
  {
    private readonly List<ILoot> _loot = new ();

    public ILevel Level { get; private set; }
    public ICharacter Character { get; private set; }
    public IReadOnlyList<ILoot> Loot => _loot;

    
    public void SetCharacter(ICharacter character) => Character = character;
    public void SetLevel(ILevel level) => Level = level;
    public void AddLoot(ILoot enemy) => _loot.Add(enemy);
    public void RemoveLoot(ILoot enemy) => _loot.Remove(enemy);
   
    public void Cleanup()
    {
      Character = null;
      Level = null;
      _loot.Clear();
    }
  }
}