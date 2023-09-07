using System;
using UnityEngine;

/// <summary>
/// Provides a globally unique identifier for a MonoBehaviour in Unity.
/// </summary>
public sealed class GlobalIdentifier : MonoBehaviour {
  /// <summary>
  /// Gets the globally unique identifier for the MonoBehaviour.
  /// </summary>
  public string id { get; } = Guid.NewGuid().ToString();
}