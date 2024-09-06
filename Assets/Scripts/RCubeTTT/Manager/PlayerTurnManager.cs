using RCubeTTT.EventSystem;
using RCubeTTT.EventSystem.Event.RCubeEvent;
using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.EventSystem.EventInvoker.Impl;
using RCubeTTT.Model;
using UnityEngine;

namespace RCubeTTT.Manager
{
  /// <summary>
  /// Manages the turns and actions of players in a game.
  /// </summary>
  public sealed class PlayerTurnManager {
    private readonly PlayerTurnStartEventInvoker playerTurnStartEventInvoker = new PlayerTurnStartEventInvokerImpl();
    private readonly PlayerTurnEventInvoker playerTurnEventInvoker = new PlayerTurnEventInvokerImpl();

    private readonly PlayerPlayData playerXData;
    private readonly PlayerPlayData playerOData;

    private Quaternion faceInitLocalRotation;

    public PlayerTurnManager(PlayerPlayData playerXData, PlayerPlayData playerOData) {
      this.playerXData = playerXData;
      this.playerOData = playerOData;

      RCubeFaceEventManager.AddRotationStartListener(OnRCubeFaceRotationStart);
      RCubeFaceEventManager.AddRotationEndListener(OnRCubeFaceRotationEnd);
      EventManager.AddRCubePieceFaceMarkSetListener(OnRCubePieceFaceSetSign);

      EventManager.AddPlayerWinConditionReachedListener(
        _ => {
          DisableTurn(playerXData);
          DisableTurn(playerOData);
        }
      );

      EventManager.AddPlayerTurnStartInvoker(playerTurnStartEventInvoker);
    }

    /// <summary>
    /// Handles the rotation end event of a cube face and updates the player's ability to drag the cube face based on their turn.
    /// </summary>
    /// /// <param name="context">The context of the <see cref="RCubeFaceRotationEndEvent"/>.</param>
    private void OnRCubeFaceRotationEnd(RotationEventContext context) {
      if (Quaternion.Angle(faceInitLocalRotation, context.localRotation) > 0) {
        if (playerXData.isMyTurn) {
          playerXData.canDragCubeFace = false;
          playerTurnEventInvoker.Invoke(playerXData);
        } else if (playerOData.isMyTurn) {
          playerOData.canDragCubeFace = false;
          playerTurnEventInvoker.Invoke(playerOData);
        }
      }
    }
    

    /// <summary>
    /// Handles the rotation start event of a cube face and updates the active face initial rotation.
    /// </summary>
    /// /// <param name="context">The context of the <see cref="RCubeFaceRotationEndEvent"/>.</param>
    private void OnRCubeFaceRotationStart(RotationEventContext context) {
      faceInitLocalRotation = context.localRotation;
    }

    /// <summary>
    /// Handles the callback for a set sign in the piece's face.
    /// Switches the turn between two players in a game.
    /// </summary>
    private void OnRCubePieceFaceSetSign(RCubePieceFaceMarkSetEventContext context) {
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
      playerPlayData.canDragCubeFace = true;
      playerTurnStartEventInvoker.Invoke(playerPlayData);
    }

    private void DisableTurn(PlayerPlayData playerPlayData) {
      playerPlayData.isMyTurn = false;
      playerPlayData.canSetSign = false;
      playerPlayData.canDragCubeFace = false;
      playerTurnEventInvoker.Invoke(playerPlayData);
    }
  }
}