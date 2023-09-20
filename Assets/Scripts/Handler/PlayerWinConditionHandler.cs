public class PlayerWinConditionHandler {
  private readonly PlayerWinConditionReachedEventInvoker winConditionReachedEventInvoker = new PlayerWinConditionReachedEventInvokerImpl();
  private PlayerPlayData activePlayer;

  public PlayerWinConditionHandler() {
    EventManager.AddPlayerTurnStartListener(ctx => activePlayer = ctx);
    EventManager.AddRCubeFacePiecesFacesRaycastHitListener(OnRCubeFacePiecesFacesRaycastHit);

    EventManager.AddPlayerWinConditionReachedInvoker(winConditionReachedEventInvoker);
  }

  private void OnRCubeFacePiecesFacesRaycastHit(RCubeFacePiecesFacesRaycastHitEventContext context) {
    if (TicTacToe.CheckWinCondition(context.scannedSigns, activePlayer.markType)) {
      winConditionReachedEventInvoker.Invoke(new PlayerWinConditionReachedEventContext(activePlayer, context.facePositionType));
    }
  }
}