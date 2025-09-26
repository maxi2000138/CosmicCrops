using System.Collections.Generic;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.Services.AIReporter
{
  public class DecisionDetails
  {
    public string ProducerName;
    public string TargetName;
    public string ActionName;

    public string FormattedLine;

    public List<ScoreFactor> Scores;
  }
}