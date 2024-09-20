using Common.EventSystem.Bus;
using RCubeTTT.Commons;
using RCubeTTT.Model;

namespace RCubeTTT.Handler {
  public class PlayerWinConditionHandler {
    private readonly PlayerPlayData playerXData;
    private readonly PlayerPlayData playerOData;

    public PlayerWinConditionHandler(PlayerPlayData playerXData, PlayerPlayData playerOData) {
      this.playerXData = playerXData;
      this.playerOData = playerOData;

      EventBus<ScanMarksEvent>.Register(new EventBinding<ScanMarksEvent>(OnScanMarks));
    }

    private void OnScanMarks(ScanMarksEvent @event) {
      if (TicTacToe.CheckWinCondition(@event.marks, playerXData.markType)) {
        EventBus<PlayerWinConditionReachedEvent>.Raise(new PlayerWinConditionReachedEvent { player = playerXData });
      }

      if (TicTacToe.CheckWinCondition(@event.marks, playerOData.markType)) {
        EventBus<PlayerWinConditionReachedEvent>.Raise(new PlayerWinConditionReachedEvent { player = playerOData });
      }
    }
  }
}