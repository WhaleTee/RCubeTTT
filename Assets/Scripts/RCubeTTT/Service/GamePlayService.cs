using System.Collections.Generic;
using Common.EventBus;
using RCubeTTT.Scriptable;

namespace RCubeTTT.Service {
  public class GamePlayService {
    private readonly PlayerGameData playerXData;
    private readonly PlayerGameData playerOData;

    public GamePlayService(PlayerGameData playerXData, PlayerGameData playerOData) {
      this.playerXData = playerXData;
      this.playerOData = playerOData;

      EventBus<ScanMarksEvent>.Register(new EventBinding<ScanMarksEvent>(OnScanMarks));
    }

    private void OnScanMarks(ScanMarksEvent @event) {
      var winner = IfThereIsWinner(@event.marks);
      if (winner == MarkType.None) return;

      EventBus<PlayerWinConditionReachedEvent>.Raise(
        new PlayerWinConditionReachedEvent { player = winner == MarkType.X ? playerXData : playerOData }
      );
    }

    private static MarkType IfThereIsWinner(IReadOnlyList<MarkType> board) {
      return CheckWinCondition(board, MarkType.X) ? MarkType.X : CheckWinCondition(board, MarkType.O) ? MarkType.O : MarkType.None;
    }

    private static bool CheckWinCondition(IReadOnlyList<MarkType> board, MarkType mark) {
      // Check rows
      for (var row = 0; row < 3; row++) {
        var startIndex = row * 3;

        if (board[startIndex] == mark && board[startIndex + 1] == mark && board[startIndex + 2] == mark) {
          return true;
        }
      }

      // Check columns
      for (var col = 0; col < 3; col++) {
        if (board[col] == mark && board[col + 3] == mark && board[col + 6] == mark) {
          return true;
        }
      }

      // Check diagonals
      return (board[0] == mark && board[4] == mark && board[8] == mark) || (board[2] == mark && board[4] == mark && board[6] == mark);
    }
  }
}