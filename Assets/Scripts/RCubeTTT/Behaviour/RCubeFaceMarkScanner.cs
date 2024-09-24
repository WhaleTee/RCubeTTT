using System.Linq;
using Common.EventBus;
using Common.Scanner;
using UnityEngine;

namespace RCubeTTT.Behaviour {
  public class RCubeFaceSignsScanner : CastScanner {
    [SerializeField] private LayerMask cubePieceFaceLayer;
    [SerializeField] private GameObject[] rays;
    [SerializeField] private FaceType faceType;

    [SerializeField]
    [Tooltip("Whether to draw the ray casts gizmo or not.")]
    private bool debug;

    private MarkType[] scannedMarks;

    private void Awake() {
      scannedMarks = new MarkType[rays.Length];

      EventBus<DragEndEvent>.Register(new EventBinding<DragEndEvent>(ScanForFaces));
      EventBus<PlayerPutMarkEvent>.Register(new EventBinding<PlayerPutMarkEvent>(ScanForFaces));
    }

    private void Start() {
      ScanForFaces();
    }

    private void OnDrawGizmosSelected() {
      if (debug) {
        foreach (var ray in rays) {
          var rayTransform = ray.transform;
          Debug.DrawRay(rayTransform.position, rayTransform.forward, Color.red);
        }
      }
    }

    private void ScanForFaces() {
      scannedMarks = ScanForLayer(rays.Length, cubePieceFaceLayer)
      .Select(
        go => {
          var markController = go.GetComponentInChildren<RCubePieceFaceMark>();
          return markController ? markController.markType : MarkType.None;
        }
      )
      .ToArray();

      EventBus<ScanMarksEvent>.Raise(new ScanMarksEvent { marks = scannedMarks, faceType = faceType });
    }

    protected override void CastNonAlloc(in RaycastHit[] hits, LayerMask layer) {
      for (var i = 0; i < rays.Length; i++) {
        var rayTransform = rays[i].transform;
        Physics.Raycast(rayTransform.position, rayTransform.forward, out var hit, float.PositiveInfinity, cubePieceFaceLayer);
        hits[i] = hit;
      }
    }
  }
}