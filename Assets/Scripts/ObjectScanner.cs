using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ObjectScanner : MonoBehaviour {
  #region fields

  private const float MAX_DISTANCE = .01f;

  #endregion

  #region serializable fields

  [SerializeField]
  private Vector3 center;

  [SerializeField]
  private Vector3 size;

  [SerializeField]
  private Vector3 direction;

  [SerializeField]
  private bool debug;

  #endregion

  #region properties

  private Vector3 position => transform.position + center;

  #endregion

  #region unity methods

  #if UNITY_EDITOR

  private void OnDrawGizmosSelected() {
    if (debug) {
      DebugExtensions.DrawBoxCastBox(
        position,
        size,
        transform.rotation,
        direction,
        MAX_DISTANCE,
        Color.red
      );
    }
  }

  #endif

  #endregion

  #region methods

  public IEnumerable<GameObject> ScanForLayer(int expectedObjectsCount, int layer) {
    var hits = new RaycastHit[expectedObjectsCount];

    Physics.BoxCastNonAlloc(
      position,
      size,
      direction,
      hits,
      transform.rotation,
      MAX_DISTANCE,
      layer
    );

    return hits.NotNullOrEmpty()
           ? hits.Where(hit => hit.collider != null).Select(hit => hit.collider.gameObject).ToArray()
           : Array.Empty<GameObject>();
  }

  #endregion
}