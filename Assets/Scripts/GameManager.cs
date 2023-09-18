using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  [SerializeField]
  private PlayerPlayData playerXData;

  [SerializeField]
  private PlayerPlayData playerOData;

  private readonly PlayerTurnStartEventInvoker playerTurnStartEventInvoker = new PlayerTurnStartEventInvokerImpl();

  private void OnEnable() {
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  private void Awake() {
    EventManager.AddPlayerTurnStartInvoker(playerTurnStartEventInvoker);
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
    if (playerXData.isMyTurn) {
      playerTurnStartEventInvoker.Invoke(playerXData);
    } else if (playerOData.isMyTurn) {
      playerTurnStartEventInvoker.Invoke(playerOData);
    }
  }
}