using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

/// <summary>
/// Abstract class for scanning a specified layer for game objects using an implemented cast.
/// </summary>
public abstract class CastScanner : MonoBehaviour {
  #region fields

  /// <summary>
  /// The maximum distance to check for collisions.
  /// </summary>
  protected const float MAX_DISTANCE = .01f;

  #endregion

  #region serializable fields

  [SerializeField]
  protected Vector3 center;

  [SerializeField]
  protected Vector3 direction;

  #endregion

  /// <summary>
  /// Scans the specified layer for game objects using a implemented cast and returns the result as an enumerable collection.
  /// </summary>
  /// <param name="expectedObjectsCount">The expected number of objects to be found. Use a negative value for an unlimited count.</param>
  /// <param name="layer">The layer to scan for objects.</param>
  /// <returns>An enumerable collection of game objects found in the specified layer.</returns>
  public IEnumerable<GameObject> ScanForLayer(int expectedObjectsCount, LayerMask layer) {
    var hits = new RaycastHit[expectedObjectsCount < 0 ? int.MaxValue : expectedObjectsCount];
    CastNonAlloc(hits, layer);

    return hits.NotNullOrEmpty()
           ? hits.Where(hit => hit.collider != null).Select(hit => hit.collider.gameObject).ToArray()
           : Array.Empty<GameObject>();
  }

  /// <summary>
  /// Performs the cast and stores the results in the provided array.
  /// </summary>
  /// <param name="hits">The array to store the cast results in.</param>
  /// <param name="layer">The layer to cast against.</param>
  protected abstract void CastNonAlloc(RaycastHit[] hits, LayerMask layer);
}