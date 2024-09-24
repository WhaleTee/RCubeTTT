using Common.EventBus;
using Common.Extensions;
using RCubeTTT.Scriptable;
using UnityEngine;

namespace RCubeTTT.Behaviour {
  public class RCubePieceFace : MonoBehaviour {
    private Camera raycastCamera;
    private PlayerGameData activePlayer;
    private Vector2 pointerPosition;
    private GameObject previousPieceFaceClicked;
    private bool isAlreadyMarked;

    private void Awake() {
      raycastCamera = Camera.main;
      
      EventBus<PlayerTurnEvent>.Register(new EventBinding<PlayerTurnEvent>(OnPlayerTurnStarted));
      EventBus<PointerDoubleClickBeginEvent>.Register(new EventBinding<PointerDoubleClickBeginEvent>(OnMouseLeftButtonDoubleClickBegin));
      EventBus<PointerDoubleClickEvent>.Register(new EventBinding<PointerDoubleClickEvent>(OnMouseLeftButtonDoubleClickPerformed));
      EventBus<PointerPositionEvent>.Register(new EventBinding<PointerPositionEvent>(ctx => pointerPosition = ctx.screenPosition));
    }

    private void OnPlayerTurnStarted(PlayerTurnEvent @event) {
      if (@event.player.isMyTurn) activePlayer = @event.player;
    }

    private void OnMouseLeftButtonDoubleClickPerformed(PointerDoubleClickEvent @event) {
      if (!isAlreadyMarked && activePlayer.isMyTurn && activePlayer.canSetSign) {
        if (Physics.Raycast(
              raycastCamera.ScreenPointToRay(pointerPosition),
              out var hit,
              float.PositiveInfinity,
              gameObject.layer.GetMask()
            )) {
          var hitPieceFace = hit.collider.gameObject;

          if (previousPieceFaceClicked == hitPieceFace && hitPieceFace == gameObject) {
            SetSign();
          }
        }
      }
    }

    private void OnMouseLeftButtonDoubleClickBegin(PointerDoubleClickBeginEvent @event) {
      if (!isAlreadyMarked && activePlayer.isMyTurn && activePlayer.canSetSign) {
        if (Physics.Raycast(
              raycastCamera.ScreenPointToRay(pointerPosition),
              out var hit,
              float.PositiveInfinity,
              gameObject.layer.GetMask()
            )) {
          if (hit.collider.gameObject == gameObject) {
            previousPieceFaceClicked = gameObject;
          }
        }
      }
    }

    private void SetSign() {
      Instantiate(activePlayer.sign, transform);
      isAlreadyMarked = true;
      EventBus<PlayerPutMarkEvent>.Raise(new PlayerPutMarkEvent { player = activePlayer });
    }
  }
}