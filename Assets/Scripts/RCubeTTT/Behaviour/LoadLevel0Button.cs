using UnityEngine;
using UnityEngine.SceneManagement;

namespace RCubeTTT.Behaviour {
  public class LoadLevel0Button : MonoBehaviour {
    public void OnClick() {
      SceneManager.LoadScene("level_0");
    }
  }
}