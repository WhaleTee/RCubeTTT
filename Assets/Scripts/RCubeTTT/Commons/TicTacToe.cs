using RCubeTTT.Model;

namespace RCubeTTT.Commons
{
  public static class TicTacToe {
    public static bool CheckWinCondition(MarkType[] board, MarkType mark) {
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