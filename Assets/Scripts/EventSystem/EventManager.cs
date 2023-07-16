using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public static class EventManager {
  #region fields
  
  // mouse drag support
  private static readonly List<MouseDragInputInvoker> MOUSE_DRAG_INPUT_INVOKERS = new List<MouseDragInputInvoker>();
  private static readonly List<UnityAction<InputAction.CallbackContext>> MOUSE_DRAG_INPUT_LISTENERS = new List<UnityAction<InputAction.CallbackContext>>();
  
  // mouse down support
  private static readonly List<MouseDownInputInvoker> MOUSE_DOWN_INPUT_INVOKERS = new List<MouseDownInputInvoker>();
  private static readonly List<UnityAction<InputAction.CallbackContext>> MOUSE_DOWN_INPUT_LISTENERS = new List<UnityAction<InputAction.CallbackContext>>();
  
  // mouse up support
  private static readonly List<MouseUpInputInvoker> MOUSE_UP_INPUT_INVOKERS = new List<MouseUpInputInvoker>();
  private static readonly List<UnityAction<InputAction.CallbackContext>> MOUSE_UP_INPUT_LISTENERS = new List<UnityAction<InputAction.CallbackContext>>();

  #endregion

  #region mouse input support

  public static void AddMouseDragInputInvoker(MouseDragInputInvoker invoker) {
    MOUSE_DRAG_INPUT_INVOKERS.Add(invoker);

    foreach (var listener in MOUSE_DRAG_INPUT_LISTENERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }
  
  public static void AddMouseDragInputListener(UnityAction<InputAction.CallbackContext> listener) {
    MOUSE_DRAG_INPUT_LISTENERS.Add(listener);

    foreach (var invoker in MOUSE_DRAG_INPUT_INVOKERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }

  #endregion
  
  #region mouse down input support

  public static void AddMouseDownInputInvoker(MouseDownInputInvoker invoker) {
    MOUSE_DOWN_INPUT_INVOKERS.Add(invoker);
    
    foreach (var listener in MOUSE_DOWN_INPUT_LISTENERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }
  
  public static void AddMouseDownInputListener(UnityAction<InputAction.CallbackContext> listener) {
    MOUSE_DOWN_INPUT_LISTENERS.Add(listener);
    
    foreach (var invoker in MOUSE_DOWN_INPUT_INVOKERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }

  #endregion
  
  #region mouse up input support
  
  public static void AddMouseUpInputInvoker(MouseUpInputInvoker invoker) {
    MOUSE_UP_INPUT_INVOKERS.Add(invoker);
    
    foreach (var listener in MOUSE_UP_INPUT_LISTENERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }
  
  public static void AddMouseUpInputListener(UnityAction<InputAction.CallbackContext> listener) {
    MOUSE_UP_INPUT_LISTENERS.Add(listener);
    
    foreach (var invoker in MOUSE_UP_INPUT_INVOKERS) {
      ((InputInvoker)invoker).AddListener(listener);
    }
  }
  
  #endregion
}