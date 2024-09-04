using RCubeTTT.Model;

namespace RCubeTTT.EventSystem.EventContext
{
  public sealed class PlayerWinConditionReachedEventContext {
    public PlayerPlayData activePlayer { get; private set; }
    public RCubeFacePositionType facePositionType { get; private set; }

    public PlayerWinConditionReachedEventContext(PlayerPlayData activePlayer, RCubeFacePositionType facePositionType) {
      this.activePlayer = activePlayer;
      this.facePositionType = facePositionType;
    }
  }
}