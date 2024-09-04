using UnityEngine;

namespace Common.EventSystem.Context {
  public readonly struct GameObjectContext {
    public GameObject gameObject { get; }

    public GameObjectContext(GameObject gameObject) {
      this.gameObject = gameObject;
    }
  }
}