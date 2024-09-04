using System.Collections.Generic;
using RCubeTTT.EventSystem.EventInvoker;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem {
  public static class RCubeEventManager {
    #region input drag support

    #region drag start fields

    private static readonly List<IUnityEventInvoker> dragStartEventInvokers = new List<IUnityEventInvoker>();
    private static readonly List<UnityAction> dragStartListeners = new List<UnityAction>();

    #endregion

    #region drag performed fields

    private static readonly List<IUnityEventInvoker> dragPerformedInvokers = new List<IUnityEventInvoker>();
    private static readonly List<UnityAction> dragPerformedListeners = new List<UnityAction>();

    #endregion

    #region drag end fields

    private static readonly List<IUnityEventInvoker> dragEndInvokers = new List<IUnityEventInvoker>();
    private static readonly List<UnityAction> dragEndListeners = new List<UnityAction>();

    #endregion

    #region drag start methods

    public static void AddDragStartInvoker(IUnityEventInvoker invoker) {
      dragStartEventInvokers.Add(invoker);

      foreach (var listener in dragStartListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void RemoveDragStartInvoker(IUnityEventInvoker invoker) {
      dragStartEventInvokers.Remove(invoker);
    }

    public static void AddDragStartListener(UnityAction listener) {
      dragStartListeners.Add(listener);

      foreach (var invoker in dragStartEventInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region drag RCube methods

    public static void AddDragPerformedInvoker(IUnityEventInvoker invoker) {
      dragPerformedInvokers.Add(invoker);

      foreach (var listener in dragPerformedListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void RemoveDragPerformedInvoker(IUnityEventInvoker invoker) {
      dragPerformedInvokers.Remove(invoker);
    }

    public static void AddDragPerformedListener(UnityAction listener) {
      dragPerformedListeners.Add(listener);

      foreach (var invoker in dragPerformedInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region end drag RCube methods

    public static void AddDragEndInvoker(IUnityEventInvoker invoker) {
      dragEndInvokers.Add(invoker);

      foreach (var listener in dragEndListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void RemoveDragEndInvoker(IUnityEventInvoker invoker) {
      dragEndInvokers.Remove(invoker);
    }

    public static void AddDragEndListener(UnityAction listener) {
      dragEndListeners.Add(listener);

      foreach (var invoker in dragEndInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #endregion
  }
}