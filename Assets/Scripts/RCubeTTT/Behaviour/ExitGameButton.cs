using UnityEngine;

namespace RCubeTTT.Behaviour {
  public class ExitGameButton : MonoBehaviour {
    public void OnClick() {
      Application.Quit();
    }
  }
}