using Common.InputSystem;

namespace RCubeTTT.InputSystem
{
  public static class PlayerInputManager {
    #region fields

    private static readonly InputActions playerInput = new InputActions();

    public static InputActions.PlayerActions mouse = playerInput.Player;

    #endregion

    #region methods

    public static void EnableMouseInput() {
      mouse.Enable();
    }

    public static void DisableMouseInput() {
      mouse.Disable();
    }

    #endregion
  }
}