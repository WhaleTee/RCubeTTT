using System;
using UnityEngine;

public sealed class GlobalIdentifier : MonoBehaviour {
  public string id { get; } = Guid.NewGuid().ToString();
}