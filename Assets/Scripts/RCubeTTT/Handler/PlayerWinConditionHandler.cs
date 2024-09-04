using RCubeTTT.Commons;
using RCubeTTT.EventSystem;
using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.EventSystem.EventInvoker.Impl;
using RCubeTTT.Model;

namespace RCubeTTT.Handler
{
  public class PlayerWinConditionHandler {
    private readonly PlayerWinConditionReachedEventInvoker winConditionReachedEventInvoker = new PlayerWinConditionReachedEventInvokerImpl();
    private readonly PlayerPlayData playerXData;
    private readonly PlayerPlayData playerOData;

    public PlayerWinConditionHandler(PlayerPlayData playerXData, PlayerPlayData playerOData) {
      this.playerXData = playerXData;
      this.playerOData = playerOData;

      EventManager.AddRCubeFacePiecesFacesRaycastHitListener(OnRCubeFacePiecesFacesRayCastHit);

      EventManager.AddPlayerWinConditionReachedInvoker(winConditionReachedEventInvoker);
    }

    private void OnRCubeFacePiecesFacesRayCastHit(RCubeFacePiecesFacesRaycastHitEventContext context) {
      if (TicTacToe.CheckWinCondition(context.scannedMarks, playerXData.markType)) {
        winConditionReachedEventInvoker.Invoke(new PlayerWinConditionReachedEventContext(playerXData, context.facePositionType));
      }

      if (TicTacToe.CheckWinCondition(context.scannedMarks, playerOData.markType)) {
        winConditionReachedEventInvoker.Invoke(new PlayerWinConditionReachedEventContext(playerOData, context.facePositionType));
      }
    }
  }
}