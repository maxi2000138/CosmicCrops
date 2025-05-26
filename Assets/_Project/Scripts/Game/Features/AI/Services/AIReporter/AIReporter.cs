using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using _Project.Scripts.Game.Features.AI.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.Services.AIReporter
{
  public class AIReporter : IAIReporter
  {
    
    public event Action<DecisionDetails> DecisionDetailsReported;
    public event Action<DecisionScore> DecisionScoreReported;

    public void ReportDecisionDetails(ITarget unit, BattleAction battleAction, List<ScoreFactor> scoreFactors)
    {
      DecisionDetails decisionDetails = new DecisionDetails
      {
        ProducerName = $"{ unit }",
        TargetName = $"{ battleAction.Target }",
        ActionName = $"{ battleAction.ActionType }",
        
        Scores = scoreFactors,
        FormattedLine = string.Join(Environment.NewLine, 
          scoreFactors.OrderByDescending(x => x.Score)
            .Select(x => x.ToString())
            .ToArray()),
      };
      
      DecisionDetailsReported?.Invoke(decisionDetails);
    }

    public void ReportDecisionScores(ITarget unit, List<ScoredAction> choices)
    {
      DecisionScore decisionScore = new DecisionScore
      {
        ProducerName = $"{ unit }",
        Choices = choices,
        
        FormattedLine = string.Join(Environment.NewLine, 
          choices.OrderByDescending(x => x.Score)
            .Select(x => x.ToString())
            .ToArray()),
      };
      
      DecisionScoreReported?.Invoke(decisionScore);
    }
  }
}