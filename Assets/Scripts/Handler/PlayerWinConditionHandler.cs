public class PlayerWinConditionHandler {
  private readonly PlayerWinConditionReachedEventInvoker winConditionReachedEventInvoker = new PlayerWinConditionReachedEventInvokerImpl();
  private readonly PlayerPlayData playerXData;
  private readonly PlayerPlayData playerOData;

  public PlayerWinConditionHandler(PlayerPlayData playerXData, PlayerPlayData playerOData) {
    this.playerXData = playerXData;
    this.playerOData = playerOData;

    EventManager.AddRCubeFacePiecesFacesRaycastHitListener(OnRCubeFacePiecesFacesRaycastHit);

    EventManager.AddPlayerWinConditionReachedInvoker(winConditionReachedEventInvoker);
  }

  private void OnRCubeFacePiecesFacesRaycastHit(RCubeFacePiecesFacesRaycastHitEventContext context) {
    if (TicTacToe.CheckWinCondition(context.scannedMarks, playerXData.markType)) {
      winConditionReachedEventInvoker.Invoke(new PlayerWinConditionReachedEventContext(playerXData, context.facePositionType));
    }

    if (TicTacToe.CheckWinCondition(context.scannedMarks, playerOData.markType)) {
      winConditionReachedEventInvoker.Invoke(new PlayerWinConditionReachedEventContext(playerOData, context.facePositionType));
    }
  }
}