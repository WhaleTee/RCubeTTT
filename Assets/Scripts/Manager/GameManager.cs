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
    playerXData.isMyTurn = true;
    playerXData.canSetSign = true;
    playerXData.canDragCubeFace = true;
    playerXData.canDragCube = true;

    playerOData.isMyTurn = false;
    playerOData.canSetSign = false;
    playerOData.canDragCubeFace = false;
    playerOData.canDragCube = true;
    
    playerTurnStartEventInvoker.Invoke(playerXData);
  }
}