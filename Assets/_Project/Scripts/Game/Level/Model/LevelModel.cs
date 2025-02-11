using System.Collections.Generic;
using _Project.Scripts.Game.Level.Interface;
using _Project.Scripts.Game.Units.Character.Interface;
using _Project.Scripts.Game.Units.Loot.Interface;
using ObservableCollections;
using R3;

namespace _Project.Scripts.Game.Level.Model
{
  public class LevelModel
  {
    private readonly ObservableList<ILoot> _loot = new ();

    public ILevel Level { get; private set; }
    public ICharacter Character { get; private set; }
    public ReactiveProperty<ILoot> CurrentLoot { get; } = new ReactiveProperty<ILoot>();
    public IReadOnlyObservableList<ILoot> Loot => _loot;


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