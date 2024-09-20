using Common;
using Common.EventSystem.Bus;
using RCubeTTT.Commons;
using UnityEngine;

namespace RCubeTTT.Controller {
  [RequireComponent(typeof(BoxCastScanner))]
  public class RCubeFacePiecesController : MonoBehaviour, TargetInstanceIdProvider {
    [SerializeField] private LayerMask cubePieceLayer;
    [SerializeField] private GameObject target;

    private BoxCastScanner boxCastScanner;
    private RCubeFacePiecesAssigner rCubeFacePiecesAssigner;

    public int targetInstanceId => targetGameObject.GetInstanceID();
    private GameObject targetGameObject => target ? target.gameObject : gameObject;

    private void Awake() {
      boxCastScanner = GetComponent<BoxCastScanner>();
      rCubeFacePiecesAssigner = new RCubeFacePiecesAssigner(boxCastScanner, cubePieceLayer);

      EventBus<DragBeginEvent>.Register(
        new EventBinding<DragBeginEvent>(
          ctx => {
            if (ctx.instanceId == targetInstanceId) rCubeFacePiecesAssigner.FindAndAssignPiecesToFace(targetGameObject);
          }
        )
      );
    }
  }
}