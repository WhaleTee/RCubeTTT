using System.Collections.Generic;
using Common.EventSystem.Context;
using Common.EventSystem.Invoker;
using UnityEngine.Events;

namespace Common.InputSystem {
  public static class PlayerInputEventManager {
    #region pointer position

    private static readonly List<IEventInvoker<PositionContext>> pointerPositionInvokers = new List<IEventInvoker<PositionContext>>();
    private static readonly List<UnityAction<PositionContext>> pointerPositionListeners = new List<UnityAction<PositionContext>>();

    public static void AddPointerPositionInvoker(IEventInvoker<PositionContext> invoker) {
      pointerPositionInvokers.Add(invoker);

      foreach (var listener in pointerPositionListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddPointerPositionListener(UnityAction<PositionContext> listener) {
      pointerPositionListeners.Add(listener);

      foreach (var invoker in pointerPositionInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region pointer delta

    private static readonly List<IEventInvoker<DeltaContext>> pointerDeltaInvokers = new List<IEventInvoker<DeltaContext>>();
    private static readonly List<UnityAction<DeltaContext>> pointerDeltaListeners = new List<UnityAction<DeltaContext>>();

    public static void AddPointerDeltaInvoker(IEventInvoker<DeltaContext> invoker) {
      pointerDeltaInvokers.Add(invoker);

      foreach (var listener in pointerDeltaListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddPointerDeltaListener(UnityAction<DeltaContext> listener) {
      pointerDeltaListeners.Add(listener);

      foreach (var invoker in pointerDeltaInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region pointer click

    private static readonly List<IEventInvoker> pointerClickInvokers = new List<IEventInvoker>();
    private static readonly List<UnityAction> pointerClickListeners = new List<UnityAction>();

    public static void AddPointerClickInvoker(IEventInvoker invoker) {
      pointerClickInvokers.Add(invoker);

      foreach (var listener in pointerClickListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddPointerClickListener(UnityAction listener) {
      pointerClickListeners.Add(listener);

      foreach (var invoker in pointerClickInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region pointer click up

    private static readonly List<IEventInvoker> pointerClickUpInvokers = new List<IEventInvoker>();
    private static readonly List<UnityAction> pointerClickUpListeners = new List<UnityAction>();

    public static void AddPointerClickUpInvoker(IEventInvoker invoker) {
      pointerClickUpInvokers.Add(invoker);

      foreach (var listener in pointerClickUpListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddPointerClickUpListener(UnityAction listener) {
      pointerClickUpListeners.Add(listener);

      foreach (var invoker in pointerClickUpInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region pointer double click

    private static readonly List<IEventInvoker> pointerDoubleClickInvokers = new List<IEventInvoker>();
    private static readonly List<UnityAction> pointerDoubleClickListeners = new List<UnityAction>();

    public static void AddPointerDoubleClickInvoker(IEventInvoker invoker) {
      pointerDoubleClickInvokers.Add(invoker);

      foreach (var listener in pointerDoubleClickListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddPointerDoubleClickListener(UnityAction listener) {
      pointerDoubleClickListeners.Add(listener);

      foreach (var invoker in pointerDoubleClickInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region mouse right click

    private static readonly List<IEventInvoker> mouseRightClickInvokers = new List<IEventInvoker>();
    private static readonly List<UnityAction> mouseRightClickListeners = new List<UnityAction>();

    public static void AddMouseRightClickInvoker(IEventInvoker invoker) {
      mouseRightClickInvokers.Add(invoker);

      foreach (var listener in mouseRightClickListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddMouseRightClickListener(UnityAction listener) {
      mouseRightClickListeners.Add(listener);

      foreach (var invoker in mouseRightClickInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region mouse middle click

    private static readonly List<IEventInvoker> mouseMiddleClickInvokers = new List<IEventInvoker>();
    private static readonly List<UnityAction> mouseMiddleClickListeners = new List<UnityAction>();

    public static void AddMouseMiddleClickInvoker(IEventInvoker invoker) {
      mouseMiddleClickInvokers.Add(invoker);

      foreach (var listener in mouseMiddleClickListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddMouseMiddleClickListener(UnityAction listener) {
      mouseMiddleClickListeners.Add(listener);

      foreach (var invoker in mouseMiddleClickInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region mouse wheel scroll

    private static readonly List<IEventInvoker<DeltaContext>> mouseWheelScrollInvokers = new List<IEventInvoker<DeltaContext>>();
    private static readonly List<UnityAction<DeltaContext>> mouseWheelScrollListeners = new List<UnityAction<DeltaContext>>();

    public static void AddMouseWheelScrollInvoker(IEventInvoker<DeltaContext> invoker) {
      mouseWheelScrollInvokers.Add(invoker);

      foreach (var listener in mouseWheelScrollListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddMouseWheelScrollListener(UnityAction<DeltaContext> listener) {
      mouseWheelScrollListeners.Add(listener);

      foreach (var invoker in mouseWheelScrollInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion
  }
}