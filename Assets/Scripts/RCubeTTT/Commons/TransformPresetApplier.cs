using UnityEngine;

namespace RCubeTTT.Commons
{
  public class TransformPresetApplier : MonoBehaviour {
    [SerializeField]
    private TransformPreset transformPreset;

    [SerializeField]
    private bool applyPosition;

    [SerializeField]
    private bool applyRotation;

    [SerializeField]
    private bool applyScale;

    private void Start() {
      if (applyPosition) {
        transform.position = transformPreset.position;
      }

      if (applyRotation) {
        transform.rotation = Quaternion.Euler(transformPreset.rotation);
      }

      if (applyScale) {
        transform.localScale = transformPreset.scale;
      }
    }
  }
}