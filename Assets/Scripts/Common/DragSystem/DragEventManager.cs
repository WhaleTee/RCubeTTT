using System.Collections.Generic;
using Common.EventSystem.Context;
using Common.EventSystem.Invoker;
using UnityEngine.Events;

namespace Common.DragSystem {
  public static class DragEventManager {
    #region drag start

    private static readonly List<IEventInvoker<RaycastHitContext>> dragStartInvokers = new List<IEventInvoker<RaycastHitContext>>();
    private static readonly List<UnityAction<RaycastHitContext>> dragStartListeners = new List<UnityAction<RaycastHitContext>>();

    public static void AddDragStartInvoker(IEventInvoker<RaycastHitContext> invoker) {
      dragStartInvokers.Add(invoker);

      foreach (var listener in dragStartListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddDragStartListener(UnityAction<RaycastHitContext> listener) {
      dragStartListeners.Add(listener);

      foreach (var invoker in dragStartInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region drag

    private static readonly List<IEventInvoker<GameObjectContext>> dragInvokers = new List<IEventInvoker<GameObjectContext>>();
    private static readonly List<UnityAction<GameObjectContext>> dragListeners = new List<UnityAction<GameObjectContext>>();

    public static void AddDragInvoker(IEventInvoker<GameObjectContext> invoker) {
      dragInvokers.Add(invoker);

      foreach (var listener in dragListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddDragListener(UnityAction<GameObjectContext> listener) {
      dragListeners.Add(listener);

      foreach (var invoker in dragInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region drag end

    private static readonly List<IEventInvoker<GameObjectContext>> dragEndInvokers = new List<IEventInvoker<GameObjectContext>>();
    private static readonly List<UnityAction<GameObjectContext>> dragEndListeners = new List<UnityAction<GameObjectContext>>();

    public static void AddDragEndInvoker(IEventInvoker<GameObjectContext> invoker) {
      dragEndInvokers.Add(invoker);

      foreach (var listener in dragEndListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void AddDragEndListener(UnityAction<GameObjectContext> listener) {
      dragEndListeners.Add(listener);

      foreach (var invoker in dragEndInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion
  }
}