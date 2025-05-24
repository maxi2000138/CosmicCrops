using System;
using System.Collections.Generic;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using _Project.Scripts.Game.Features.AI.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.Services.AIReporter
{
  public interface IAIReporter
  {
    event Action<DecisionDetails> DecisionDetailsReported;
    event Action<DecisionScore> DecisionScoreReported;
    void ReportDecisionDetails(BattleAction battleAction, ITarget target, List<ScoreFactor> scoreFactors);
    void ReportDecisionScores(ITarget producer, List<ScoredAction> choices);
  }
}