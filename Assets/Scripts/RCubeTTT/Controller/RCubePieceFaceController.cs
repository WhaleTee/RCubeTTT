using RCubeTTT.EventSystem;
using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.EventSystem.EventInvoker.Impl;
using RCubeTTT.Model;
using Support;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInputManager = RCubeTTT.InputSystem.PlayerInputManager;

namespace RCubeTTT.Controller
{
  /// <summary>
  /// Controls the behavior of the Rubik's Cube Piece Face.
  /// </summary>
  public class RCubePieceFaceController : MonoBehaviour {
    #region serializable fields

    [SerializeField]
    private Camera raycastCamera;

    [SerializeField]
    private RCubeFacePositionType facePositionType;

    #endregion

    #region fields

    private readonly RCubePieceFaceMarkSetEventInvoker rCubePieceFaceMarkSetEventInvoker = new RCubePieceFaceMarkSetEventInvokerImpl();

    private PlayerPlayData activePlayer;
    private Vector2 pointerPosition;
    private GameObject previousPieceFaceClicked;
    private bool isAlreadyMarked;

    #endregion

    #region unity methods

    private void Awake() {
      PlayerInputManager.mouse.PointerPosition.performed += OnMousePositionPerformed;
      PlayerInputManager.mouse.DoubleClick.started += OnMouseLeftButtonDoubleClickStarted;
      PlayerInputManager.mouse.DoubleClick.performed += OnMouseLeftButtonDoubleClickPerformed;

      EventManager.AddPlayerTurnStartListener(OnPlayerTurnStarted);

      EventManager.AddRCubePieceFaceMarkSetInvoker(rCubePieceFaceMarkSetEventInvoker);
    }

    #endregion

    #region methods

    private void OnPlayerTurnStarted(PlayerPlayData context) {
      activePlayer = context;
    }

    /// <summary>
    /// Handles the callback for a double-click performed with the left mouse button. 
    /// Performs a raycast from the <see cref="raycastCamera"/>'s position to the <see cref="pointerPosition"/>, 
    /// and if the hit object is this object and <see cref="previousPieceFaceClicked"/> is the same as hit object, sets a sign on it.
    /// </summary>
    /// <param name="context">The input action callback context.</param>
    private void OnMouseLeftButtonDoubleClickPerformed(InputAction.CallbackContext context) {
      if (!isAlreadyMarked && activePlayer.isMyTurn && activePlayer.canSetSign) {
        if (Physics.Raycast(
              raycastCamera.ScreenPointToRay(pointerPosition),
              out var hit,
              float.PositiveInfinity,
              LayerMaskUtils.GetMask(gameObject.layer)
            )) {
          var hitPieceFace = hit.collider.gameObject;

          if (previousPieceFaceClicked == hitPieceFace && hitPieceFace == gameObject) {
            SetSign();
          }
        }
      }
    }

    /// <summary>
    /// Handles the callback for when the left mouse button double click is started.
    /// Performs a raycast from the <see cref="raycastCamera"/>'s position to the <see cref="pointerPosition"/>, and if it hits a Rubik's Cube piece face
    /// that is the same as this, updates the <see cref="previousPieceFaceClicked"/>.
    /// </summary>
    /// <param name="context">The input action callback context.</param>
    private void OnMouseLeftButtonDoubleClickStarted(InputAction.CallbackContext context) {
      if (!isAlreadyMarked && activePlayer.isMyTurn && activePlayer.canSetSign) {
        if (Physics.Raycast(
              raycastCamera.ScreenPointToRay(pointerPosition),
              out var hit,
              float.PositiveInfinity,
              LayerMaskUtils.GetMask(gameObject.layer)
            )) {
          if (hit.collider.gameObject == gameObject) {
            previousPieceFaceClicked = gameObject;
          }
        }
      }
    }

    /// <summary>
    /// Handles the mouse position change event.
    /// Updates the pointer position based on the mouse position.
    /// </summary>
    /// <param name="context">The input action callback context.</param>
    private void OnMousePositionPerformed(InputAction.CallbackContext context) => pointerPosition = context.ReadValue<Vector2>();

    private void SetSign() {
      Instantiate(activePlayer.sign, transform);
      isAlreadyMarked = true;
      rCubePieceFaceMarkSetEventInvoker.Invoke(new RCubePieceFaceMarkSetEventContext(facePositionType));
    }

    #endregion
  }
}