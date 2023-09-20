using TMPro;
using UnityEngine;

public class PlayerInfoPopupController : MonoBehaviour {
  [SerializeField]
  private string playerXTurnText;

  [SerializeField]
  private string playerOTurnText;

  private TextMeshProUGUI text;

  private void Awake() {
    EventManager.AddPlayerTurnStartListener(OnPlayerTurnStarted);
    EventManager.AddPlayerWinConditionReachedListener(OnPlayerWinConditionReached);
    text = GetComponent<TextMeshProUGUI>();
  }

  private void OnPlayerWinConditionReached(PlayerWinConditionReachedEventContext context) {
    text.text = $"Player {context.activePlayer.markType} win on {context.facePositionType} side";
  }

  private void OnPlayerTurnStarted(PlayerPlayData context) {
    text.text = context.markType == MarkType.X ? playerXTurnText : playerOTurnText;
  }
}