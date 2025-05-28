using System.Collections.Generic;
using _Project.Scripts.Game.Entities.Enemy.Actions;

namespace _Project.Scripts.Game.Features.AI.Services.AIReporter
{
  public class DecisionScore
  {
    public string ProducerName;
    public string FormattedLine;
    
    public List<ScoredAction> Choices;
  }
}