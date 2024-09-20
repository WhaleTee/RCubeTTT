using Common.EventSystem.Bus;
using RCubeTTT.Handler;
using RCubeTTT.Model;
using UnityEngine;

namespace RCubeTTT.Manager {
  public class GameManager : MonoBehaviour {
    [SerializeField] private PlayerPlayData playerXData;
    [SerializeField] private PlayerPlayData playerOData;

    private void Awake() {
      ActivatePlayers();
      _ = new PlayerWinConditionHandler(playerXData, playerOData);
      
      EventBus<PlayerPutMarkEvent>.Register(new EventBinding<PlayerPutMarkEvent>(OnPlayerPutMark));

      EventBus<PlayerWinConditionReachedEvent>.Register(
        new EventBinding<PlayerWinConditionReachedEvent>(
          _ => {
            DisableTurn(playerXData);
            DisableTurn(playerOData);
          }
        )
      );
    }

    private void ActivatePlayers() {
      playerXData.isMyTurn = true;
      playerXData.canSetSign = true;
      playerXData.canDragCubeSide = true;
      playerXData.canDragCube = true;

      playerOData.isMyTurn = false;
      playerOData.canSetSign = false;
      playerOData.canDragCubeSide = false;
      playerOData.canDragCube = true;

      EventBus<PlayerTurnEvent>.Raise(new PlayerTurnEvent { player = playerXData });
    }

    private void OnPlayerPutMark() {
      if (playerXData.isMyTurn) {
        DisableTurn(playerXData);
        EnableTurn(playerOData);
      } else if (playerOData.isMyTurn) {
        DisableTurn(playerOData);
        EnableTurn(playerXData);
      }
    }

    private void EnableTurn(PlayerPlayData playerPlayData) {
      playerPlayData.isMyTurn = true;
      playerPlayData.canSetSign = true;
      playerPlayData.canDragCubeSide = true;
      EventBus<PlayerTurnEvent>.Raise(new PlayerTurnEvent { player = playerPlayData });
    }

    private void DisableTurn(PlayerPlayData playerPlayData) {
      playerPlayData.isMyTurn = false;
      playerPlayData.canSetSign = false;
      playerPlayData.canDragCubeSide = false;
      EventBus<PlayerTurnEvent>.Raise(new PlayerTurnEvent { player = playerPlayData });
    }
  }
}