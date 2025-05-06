using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Infrastructure.Curtain
{
  public class ProgressBar : MonoBehaviour
  {
    [SerializeField] private Slider _slider;
    public bool Disabled => gameObject.activeSelf == false;

    public void Enable() => gameObject.SetActive(true);

    public void Disable() => gameObject.SetActive(false);

    public void SetProgress(float value)
    {
      _slider.value = value;
    }
  }
}