using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public static class EventManager {
  #region fields
  
  // mouse drag cube input support
  private static readonly List<MouseDragCubeInputInvoker> MOUSE_DRAG_CUBE_INPUT_INVOKERS = new List<MouseDragCubeInputInvoker>();
  private static readonly List<UnityAction<InputAction.CallbackContext>> MOUSE_DRAG_CUBE_INPUT_LISTENERS = new List<UnityAction<InputAction.CallbackContext>>();
  
  // mouse right click down support
  private static readonly List<MouseRightClickDownInputInvoker> MOUSE_RIGHT_CLICK_DOWN_INPUT_INVOKERS = new List<MouseRightClickDownInputInvoker>();
  private static readonly List<UnityAction<InputAction.CallbackContext>> MOUSE_RIGHT_CLICK_DOWN_INPUT_LISTENERS = new List<UnityAction<InputAction.CallbackContext>>();
  
  // mouse right click up support
  private static readonly List<MouseRightClickUpInputInvoker> MOUSE_RIGHT_CLICK_UP_INPUT_INVOKERS = new List<MouseRightClickUpInputInvoker>();
  private static readonly List<UnityAction<InputAction.CallbackContext>> MOUSE_RIGHT_CLICK_UP_INPUT_LISTENERS = new List<UnityAction<InputAction.CallbackContext>>();

  #endregion

  #region mouse drag cube input support

  public static void AddMouseDragCubeInputInvoker(MouseDragCubeInputInvoker invoker) {
    MOUSE_DRAG_CUBE_INPUT_INVOKERS.Add(invoker);

    foreach (var listener in MOUSE_DRAG_CUBE_INPUT_LISTENERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }
  
  public static void AddMouseDragCubeInputListener(UnityAction<InputAction.CallbackContext> listener) {
    MOUSE_DRAG_CUBE_INPUT_LISTENERS.Add(listener);

    foreach (var invoker in MOUSE_DRAG_CUBE_INPUT_INVOKERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }

  #endregion
  
  #region mouse right click down input support

  public static void AddMouseRightClickDownInputInvoker(MouseRightClickDownInputInvoker invoker) {
    MOUSE_RIGHT_CLICK_DOWN_INPUT_INVOKERS.Add(invoker);
    
    foreach (var listener in MOUSE_RIGHT_CLICK_DOWN_INPUT_LISTENERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }
  
  public static void AddMouseRightClickDownInputListener(UnityAction<InputAction.CallbackContext> listener) {
    MOUSE_RIGHT_CLICK_DOWN_INPUT_LISTENERS.Add(listener);
    
    foreach (var invoker in MOUSE_RIGHT_CLICK_DOWN_INPUT_INVOKERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }

  #endregion
  
  #region mouse right click up input support
  
  public static void AddMouseRightClickUpInputInvoker(MouseRightClickUpInputInvoker invoker) {
    MOUSE_RIGHT_CLICK_UP_INPUT_INVOKERS.Add(invoker);
    
    foreach (var listener in MOUSE_RIGHT_CLICK_UP_INPUT_LISTENERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }
  
  public static void AddMouseRightClickUpInputListener(UnityAction<InputAction.CallbackContext> listener) {
    MOUSE_RIGHT_CLICK_UP_INPUT_LISTENERS.Add(listener);
    
    foreach (var invoker in MOUSE_RIGHT_CLICK_UP_INPUT_INVOKERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }
  
  #endregion
}