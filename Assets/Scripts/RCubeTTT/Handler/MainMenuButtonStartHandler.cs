using UnityEngine;
using UnityEngine.SceneManagement;

namespace RCubeTTT.Handler
{
  public class MainMenuButtonStartHandler : MonoBehaviour {
  
    [SerializeField]
    private string startSceneName;
  
    public void OnClick() {
      SceneManager.LoadScene(startSceneName);
    }
  }
}