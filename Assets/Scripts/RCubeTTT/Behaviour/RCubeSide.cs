using Common;
using Common.EventBus;
using Common.Extensions;
using Common.Scanner;
using Common.ServiceLocator;
using RCubeTTT.Component;
using UnityEngine;

namespace RCubeTTT.Behaviour {
  [RequireComponent(typeof(BoxCastScanner))]
  public class RCubeSide : MonoBehaviour, TargetInstanceIdProvider, ServiceInstaller {
    private const int MAX_EXPECTED_SIDE_PIECES_COUNT = 9;

    [Header("Scan pieces")] [SerializeField] private LayerMask cubePieceLayer;
    [Header("Drag rotation")] [SerializeField] private float speed;

    private BoxCastScanner boxCastScanner;
    private RCubeSideDragRotation dragRotation;

    public int GetTargetInstanceId() => gameObject.GetInstanceID();

    private void Awake() {
      Install();
      boxCastScanner = GetComponent<BoxCastScanner>();

      EventBus<DragBeginEvent>.Register(
        new EventBinding<DragBeginEvent>(
          ctx => {
            if (ctx.instanceId == GetTargetInstanceId()) {
              foreach (var piece in boxCastScanner.ScanForLayer(MAX_EXPECTED_SIDE_PIECES_COUNT, cubePieceLayer)) {
                piece.ReParent(gameObject.transform);
              }
            }
          }
        )
      );
    }

    public void Install() {
      ServiceLocator.For(this).Register(new RCubeSideDragRotation(transform, GetTargetInstanceId(), speed)).Get(out dragRotation);
    }
  }
}