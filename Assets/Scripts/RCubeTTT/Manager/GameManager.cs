using RCubeTTT.EventSystem;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.EventSystem.EventInvoker.Impl;
using RCubeTTT.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RCubeTTT.Manager
{
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
}