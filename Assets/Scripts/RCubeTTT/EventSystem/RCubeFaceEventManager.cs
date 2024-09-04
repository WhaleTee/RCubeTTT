using System.Collections.Generic;
using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.Model;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem {
  public static class RCubeFaceEventManager {
    #region rotation support
    
    #region rotation start

    private static readonly List<IRotationEventInvoker> rotationStartInvokers = new List<IRotationEventInvoker>();
    private static readonly List<UnityAction<RotationEventContext>> rotationStartListeners = new List<UnityAction<RotationEventContext>>();

    public static void AddRotationStartInvoker(IRotationEventInvoker invoker) {
      rotationStartInvokers.Add(invoker);

      foreach (var listener in rotationStartListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void RemoveRotationStartInvoker(IRotationEventInvoker invoker) {
      rotationStartInvokers.Remove(invoker);
    }

    public static void AddRotationStartListener(UnityAction<RotationEventContext> listener) {
      rotationStartListeners.Add(listener);

      foreach (var invoker in rotationStartInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region rotation performed

    private static readonly List<IRotationEventInvoker> rotationPerformedInvokers = new List<IRotationEventInvoker>();
    private static readonly List<UnityAction<RotationEventContext>> rotationPerformedListeners = new List<UnityAction<RotationEventContext>>();

    public static void AddRotationPerformedInvoker(IRotationEventInvoker invoker) {
      rotationPerformedInvokers.Add(invoker);

      foreach (var listener in rotationPerformedListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void RemoveRotationPerformedInvoker(IRotationEventInvoker invoker) {
      rotationPerformedInvokers.Remove(invoker);
    }

    public static void AddRotationPerformedListener(UnityAction<RotationEventContext> listener) {
      rotationPerformedListeners.Add(listener);

      foreach (var invoker in rotationPerformedInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region rotation end

    private static readonly List<IRotationEventInvoker> rotationEndInvokers = new List<IRotationEventInvoker>();
    private static readonly List<UnityAction<RotationEventContext>> rotationEndListeners = new List<UnityAction<RotationEventContext>>();

    public static void AddRotationEndInvoker(IRotationEventInvoker eventInvoker) {
      rotationEndInvokers.Add(eventInvoker);

      foreach (var listener in rotationEndListeners) {
        eventInvoker.AddListener(listener);
      }
    }

    public static void RemoveRotationEndInvoker(IRotationEventInvoker eventInvoker) {
      rotationEndInvokers.Remove(eventInvoker);
    }

    public static void AddRotationEndListener(UnityAction<RotationEventContext> listener) {
      rotationEndListeners.Add(listener);

      foreach (var invoker in rotationEndInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #endregion

    #region drag support

    #region drag start

    private static readonly List<InputHitObjectEventInvoker> dragStartInvokers = new List<InputHitObjectEventInvoker>();
    private static readonly List<UnityAction<InputHitObjectEventContext>> dragStartListeners = new List<UnityAction<InputHitObjectEventContext>>();

    public static void AddDragStartInvoker(InputHitObjectEventInvoker invoker) {
      dragStartInvokers.Add(invoker);

      foreach (var listener in dragStartListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void RemoveDragStartInvoker(InputHitObjectEventInvoker invoker) {
      dragStartInvokers.Remove(invoker);
    }

    public static void AddDragStartListener(UnityAction<InputHitObjectEventContext> listener) {
      dragStartListeners.Add(listener);

      foreach (var invoker in dragStartInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region drag performed

    private static readonly List<IDragEventInvoker> dragPerformedInvokers = new List<IDragEventInvoker>();
    private static readonly List<UnityAction<ObjectInstanceContext>> dragPerformedListeners = new List<UnityAction<ObjectInstanceContext>>();

    public static void AddDragPerformedInvoker(IDragEventInvoker invoker) {
      dragPerformedInvokers.Add(invoker);

      foreach (var listener in dragPerformedListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void RemoveDragPerformedInvoker(IDragEventInvoker invoker) {
      dragPerformedInvokers.Remove(invoker);
    }

    public static void AddDragPerformedListener(UnityAction<ObjectInstanceContext> listener) {
      dragPerformedListeners.Add(listener);

      foreach (var invoker in dragPerformedInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region drag end

    private static readonly List<IDragEventInvoker> dragEndInvokers = new List<IDragEventInvoker>();
    private static readonly List<UnityAction<ObjectInstanceContext>> dragEndListeners = new List<UnityAction<ObjectInstanceContext>>();

    public static void AddDragEndInvoker(IDragEventInvoker invoker) {
      dragEndInvokers.Add(invoker);

      foreach (var listener in dragEndListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void RemoveDragEndInvoker(IDragEventInvoker invoker) {
      dragEndInvokers.Remove(invoker);
    }

    public static void AddDragEndListener(UnityAction<ObjectInstanceContext> listener) {
      dragEndListeners.Add(listener);

      foreach (var invoker in dragEndInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #endregion
  }
}