using Common.EventSystem.Bus;
using RCubeTTT.Model;
using TMPro;
using UnityEngine;

namespace RCubeTTT.Controller {
  public class PlayerInfoPopupController : MonoBehaviour {
    [SerializeField] private string playerXTurnText;
    [SerializeField] private string playerOTurnText;

    private TextMeshProUGUI text;
    private bool gameOver;

    private void Awake() {
      text = GetComponent<TextMeshProUGUI>();

      EventBus<PlayerTurnEvent>.Register(new EventBinding<PlayerTurnEvent>(OnPlayerTurnStarted));
      EventBus<PlayerWinConditionReachedEvent>.Register(new EventBinding<PlayerWinConditionReachedEvent>(OnPlayerWinConditionReached));
    }

    private void OnPlayerWinConditionReached(PlayerWinConditionReachedEvent @event) {
      gameOver = true;
      text.text = $"Player {@event.player.markType} wins!";
    }

    private void OnPlayerTurnStarted(PlayerTurnEvent context) {
      if (!gameOver) text.text = context.player.markType == MarkType.X ? playerXTurnText : playerOTurnText;
    }
  }
}