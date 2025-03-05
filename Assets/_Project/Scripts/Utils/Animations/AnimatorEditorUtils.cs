using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Utils.Animations
{
    public static class AnimatorEditorUtils
    {
        public static Dictionary<int, float> GetAnimationDurations(Animator animator)
        {
            var result = new Dictionary<int, float>();
#if UNITY_EDITOR
            
            var clips = animator.runtimeAnimatorController.animationClips;
            var clipsDict = DictionaryPool<string, AnimationClip>.Get();

            foreach (var clip in clips)
                clipsDict.Add(clip.name, clip);

            var animatorController = animator.runtimeAnimatorController as AnimatorController;

            foreach (AnimatorControllerLayer controllerLayer in animatorController.layers)
            {
                var states = controllerLayer.stateMachine.states;

                foreach (var state in states)
                {
                    var motion = state.state.motion;
                    if (motion == null)
                        continue;

                    var motionLength = clipsDict[motion.name].length;

                    var stateName = state.state.name;
                    var stateNameHash = Animator.StringToHash(stateName);
                    result.Add(stateNameHash, motionLength);
                }
            }

            DictionaryPool<string, AnimationClip>.Release(clipsDict);
#endif

            return result;
        }
    }
}