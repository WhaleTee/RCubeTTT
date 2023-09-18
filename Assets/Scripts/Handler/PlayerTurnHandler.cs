public sealed class PlayerTurnHandler {
  private readonly PlayerTurnStartEventInvoker playerTurnStartEventInvoker = new PlayerTurnStartEventInvokerImpl();
  private readonly PlayerTurnEventInvoker playerTurnEventInvoker = new PlayerTurnEventInvokerImpl();

  private readonly PlayerPlayData playerXData;
  private readonly PlayerPlayData playerOData;

  public PlayerTurnHandler(PlayerPlayData playerXData, PlayerPlayData playerOData) {
    this.playerXData = playerXData;
    this.playerOData = playerOData;

    EventManager.AddRCubeFaceDragListener(OnRCubeFaceDrag);
    EventManager.AddSignSetListener(OnSetSign);

    EventManager.AddPlayerTurnStartInvoker(playerTurnStartEventInvoker);
    EventManager.AddPlayerTurnInvoker(playerTurnEventInvoker);
  }

  private void OnSetSign() {
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

  private void OnRCubeFaceDrag(string faceGlobalId) {
    if (playerXData.isMyTurn) {
      playerXData.canDragCubeFace = false;
      playerTurnEventInvoker.Invoke(playerXData);
    } else if (playerOData.isMyTurn) {
      playerOData.canDragCubeFace = false;
      playerTurnEventInvoker.Invoke(playerOData);
    }
  }
}