using System;
using UnityEngine;

namespace RCubeTTT.Commons
{
  /// <summary>
  /// Provides a globally unique identifier for an objects.
  /// </summary>
  [CreateAssetMenu(fileName = "GlobalIdentifier", menuName = "ScriptableObjects/GlobalIdentifier")]
  public sealed class GlobalIdentifier : ScriptableObject {
    /// <summary>
    /// Returns the globally unique identifier for the object.
    /// </summary>
    public string id { get; } = Guid.NewGuid().ToString();
  }
}