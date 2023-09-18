/// <summary>
/// Manages the turns and actions of players in a game.
/// </summary>
public sealed class PlayerTurnManager {
  private readonly PlayerTurnStartEventInvoker playerTurnStartEventInvoker = new PlayerTurnStartEventInvokerImpl();
  private readonly PlayerTurnEventInvoker playerTurnEventInvoker = new PlayerTurnEventInvokerImpl();

  private readonly PlayerPlayData playerXData;
  private readonly PlayerPlayData playerOData;

  public PlayerTurnManager(PlayerPlayData playerXData, PlayerPlayData playerOData) {
    this.playerXData = playerXData;
    this.playerOData = playerOData;

    EventManager.AddRCubeFaceDragListener(OnRCubeFaceDrag);
    EventManager.AddSignSetListener(OnSetSign);

    EventManager.AddPlayerTurnStartInvoker(playerTurnStartEventInvoker);
    EventManager.AddPlayerTurnInvoker(playerTurnEventInvoker);
  }
  
  /// <summary>
  /// Handles the callback for a set sign in the piece's face.
  /// Switches the turn between two players in a game.
  /// </summary>
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

  /// <summary>
  /// Handles the drag event on a cube face and updates the player's ability to drag the cube face based on their turn.
  /// </summary>
  /// /// <param name="faceGlobalId">The context of the <see cref="RCubeFaceDragEvent"/> that represents Rubik's Cube's face global UUID.</param>
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