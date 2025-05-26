using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.AIReporter;
using _Project.Scripts.Game.Entities.Unit.Actions;

namespace _Project.Scripts.Game._Editor
{
    [CustomEditor(typeof(UnitAIComponent))]
    public class UnitAIComponentEditor : UnityEditor.Editor
    {
        private DecisionScore _latestScore;
        private readonly Dictionary<string, DecisionDetails> _detailsMap = new();
        private Vector2 _scroll;
        private string _targetUnitName;

        private void OnEnable()
        {
            _targetUnitName = (target as UnitAIComponent)?.GetComponent<UnitComponent>()?.ToString();
            
            if (EditorBridge.AiReporter != null)
            {
                EditorBridge.AiReporter.DecisionScoreReported += OnDecisionScoreReported;
                EditorBridge.AiReporter.DecisionDetailsReported += OnDecisionDetailsReported;
            }
        }

        private void OnDisable()
        {
            if (EditorBridge.AiReporter != null)
            {
                EditorBridge.AiReporter.DecisionScoreReported -= OnDecisionScoreReported;
                EditorBridge.AiReporter.DecisionDetailsReported -= OnDecisionDetailsReported;
            }
        }

        private void OnDecisionScoreReported(DecisionScore decisionScore)
        {
            if (decisionScore.ProducerName == _targetUnitName)
            {
                _latestScore = decisionScore;
                Repaint();
            }
        }

        private void OnDecisionDetailsReported(DecisionDetails decisionDetails)
        {
            if (decisionDetails.ProducerName == _targetUnitName)
            {
                string key = GetDetailKey(decisionDetails.ProducerName, decisionDetails.ActionName, decisionDetails.TargetName);
                _detailsMap[key] = decisionDetails;
                Repaint();
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(10);
            EditorGUILayout.LabelField("🧠 Utility AI Debug", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Tracking: {_targetUnitName}", EditorStyles.miniLabel);

            if (_latestScore == null || _latestScore.Choices == null || _latestScore.Choices.Count == 0)
            {
                EditorGUILayout.HelpBox($"No data from AIReporter for {_targetUnitName}. Run the scene and wait for the decisions.", MessageType.Info);
                return;
            }

            var sortedChoices = _latestScore.Choices.OrderByDescending(x => x.Score).ToList();
            var bestAction = sortedChoices.First();

            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField("🏆 Best Decision", EditorStyles.boldLabel);
            DrawActionBlock(bestAction, true);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("📋 Other Variants", EditorStyles.boldLabel);

            foreach (var choice in sortedChoices.Skip(1))
            {
                DrawActionBlock(choice, false);
                EditorGUILayout.Space(4);
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawActionBlock(ScoredAction choice, bool isBest)
        {
            string key = GetDetailKey(_latestScore.ProducerName, choice.ActionType.ToString(), choice.Target.ToString());

            Color originalBg = GUI.backgroundColor;
            GUI.backgroundColor = isBest ? new Color(0.15f, 0.15f, 0.85f) : new Color(0.95f, 0.95f, 0.95f);

            GUIStyle boxStyle = new GUIStyle(GUI.skin.box)
            {
                margin = new RectOffset(4, 4, 2, 2),
                padding = new RectOffset(8, 8, 6, 6),
                fontSize = 12
            };

            EditorGUILayout.BeginVertical(boxStyle);
            GUI.backgroundColor = originalBg;

            EditorGUILayout.LabelField($"{choice.ActionType} → {choice.Score:F2}", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Target:", choice.Target.ToString());

            if (_detailsMap.TryGetValue(key, out var details))
            {
                EditorGUILayout.Space(2);
                EditorGUILayout.LabelField("Factors:", EditorStyles.miniBoldLabel);
                EditorGUI.indentLevel++;

                foreach (var factor in details.Scores.OrderByDescending(x => x.Score))
                {
                    EditorGUILayout.LabelField($"• {factor.Name}", $"Score: {factor.Score:F2}");
                }

                EditorGUI.indentLevel--;
            }
            else
            {
                EditorGUILayout.HelpBox("No factors data", MessageType.None);
            }

            EditorGUILayout.EndVertical();
        }

        private string GetDetailKey(string producer, string action, string target)
        {
            return $"{producer}-{action}-{target}";
        }
    }
}