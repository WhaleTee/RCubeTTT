using UnityEngine;

namespace RCubeTTT.Scriptable
{
  [CreateAssetMenu(fileName = "PlayerPlayData", menuName = "ScriptableObjects/PlayerPlayData")]
  public sealed class PlayerGameData : ScriptableObject {
    [field: SerializeField]
    public MarkType markType { get; private set; }

    [field: SerializeField]
    public GameObject sign { get; private set; }

    [field: SerializeField]
    public bool isMyTurn { get; set; }

    [field: SerializeField]
    public bool canDragCube { get; set; }

    [field: SerializeField]
    public bool canDragCubeSide { get; set; }

    [field: SerializeField]
    public bool canSetSign { get; set; }
  }
}