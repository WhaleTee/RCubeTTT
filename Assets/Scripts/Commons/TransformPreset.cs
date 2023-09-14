using UnityEngine;

[CreateAssetMenu(fileName = "TransformPreset", menuName = "ScriptableObjects/TransformPreset")]
public sealed class TransformPreset : ScriptableObject {
  [field: SerializeField]
  public Vector3 position{ get; private set; }
  
  [field: SerializeField]
  public Vector3 rotation{ get; private set; }

  [field: SerializeField]
  public Vector3 scale { get; private set; }
}
