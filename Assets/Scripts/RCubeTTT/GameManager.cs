using System;
using Common.DragSystem.Service;
using Common.EventBus;
using Common.InputSystem;
using Common.ServiceLocator;
using RCubeTTT.Scriptable;
using RCubeTTT.Service;
using UnityEngine;

namespace RCubeTTT {
  public class GameManager : MonoBehaviour, ServiceInstaller {
    [SerializeField] private PlayerGameData playerXData;
    [SerializeField] private PlayerGameData playerOData;
    
    [Header("Drag")]
    [SerializeField] private LayerMask draggableLayers;
    [SerializeField] [Range(0, 8)] private int maxHits = 1;
    [SerializeField] private Camera raycastCamera;
    
    private InputService inputService;
    private DragService dragService;
    private GamePlayService gamePlayService;

    private void Awake() {
      Install();

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

    private void Start() {
      ActivatePlayers();
    }

    public void Install() {
      ServiceLocator.global.Register(new InputService()).Get(out inputService);
      ServiceLocator.global.Register(new DragService(draggableLayers, maxHits, raycastCamera)).Get(out dragService);
      ServiceLocator.global.Register(new GamePlayService(playerXData, playerOData)).Get(out gamePlayService);
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

    private void EnableTurn(PlayerGameData playerGameData) {
      playerGameData.isMyTurn = true;
      playerGameData.canSetSign = true;
      playerGameData.canDragCubeSide = true;
      EventBus<PlayerTurnEvent>.Raise(new PlayerTurnEvent { player = playerGameData });
    }

    private void DisableTurn(PlayerGameData playerGameData) {
      playerGameData.isMyTurn = false;
      playerGameData.canSetSign = false;
      playerGameData.canDragCubeSide = false;
      EventBus<PlayerTurnEvent>.Raise(new PlayerTurnEvent { player = playerGameData });
    }
  }
}