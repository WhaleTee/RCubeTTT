using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPlayData", menuName = "ScriptableObjects/PlayerPlayData")]
public sealed class PlayerPlayData : ScriptableObject {
  [field: SerializeField]
  public MarkType markType { get; private set; }

  [field: SerializeField]
  public GameObject sign { get; private set; }

  [field: SerializeField]
  public bool isMyTurn { get; set; }

  [field: SerializeField]
  public bool canDragCube { get; set; }

  [field: SerializeField]
  public bool canDragCubeFace { get; set; }

  [field: SerializeField]
  public bool canSetSign { get; set; }
}