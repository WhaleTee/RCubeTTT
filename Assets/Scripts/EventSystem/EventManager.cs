using System.Collections.Generic;
using UnityEngine.Events;

public static class EventManager {
  #region fields

  #region drag RCube support

  // start drag RCube support
  private static readonly List<StartDragRCubeInvoker> startDragRCubeInvokers = new List<StartDragRCubeInvoker>();
  private static readonly List<UnityAction> startDragRCubeListeners = new List<UnityAction>();

  // drag RCube support
  private static readonly List<DragRCubeInvoker> dragRCubeInvokers = new List<DragRCubeInvoker>();
  private static readonly List<UnityAction> dragRCubeListeners = new List<UnityAction>();

  // end drag RCube support
  private static readonly List<EndDragRCubeInvoker> endDragRCubeInvokers = new List<EndDragRCubeInvoker>();
  private static readonly List<UnityAction> endDragRCubeListeners = new List<UnityAction>();

  #endregion

  #region drag RCube face support

  // start drag RCube face support
  private static readonly List<StartDragRCubeFaceInvoker> startDragRCubeFaceInvokers = new List<StartDragRCubeFaceInvoker>();
  private static readonly List<UnityAction> startDragRCubeFaceListeners = new List<UnityAction>();

  // drag RCube face support
  private static readonly List<DragRCubeFaceInvoker> dragRCubeFaceInvokers = new List<DragRCubeFaceInvoker>();
  private static readonly List<UnityAction> dragRCubeFaceListeners = new List<UnityAction>();

  // end drag RCube face support
  private static readonly List<EndDragRCubeFaceInvoker> endDragRCubeFaceInvokers = new List<EndDragRCubeFaceInvoker>();
  private static readonly List<UnityAction> endDragRCubeFaceListeners = new List<UnityAction>();

  #endregion

  #region cube face rotation support

  // start cube face rotation support
  private static readonly List<StartRCubeFaceRotationInvoker> startRCubeFaceRotationInvokers = new List<StartRCubeFaceRotationInvoker>();
  private static readonly List<UnityAction<string>> startRCubeFaceRotationListeners = new List<UnityAction<string>>();

  // cube face rotation support
  private static readonly List<RCubeFaceRotationInvoker> cubeFaceRotationInvokers = new List<RCubeFaceRotationInvoker>();
  private static readonly List<UnityAction<string>> cubeFaceRotationListeners = new List<UnityAction<string>>();

  // end cube face rotation support
  private static readonly List<EndRCubeFaceRotationInvoker> endRCubeFaceRotationInvokers = new List<EndRCubeFaceRotationInvoker>();
  private static readonly List<UnityAction<string>> endRCubeFaceRotationListeners = new List<UnityAction<string>>();

  #endregion

  #endregion

  #region methods

  #region start drag RCube support

  public static void AddStartDragRCubeInvoker(StartDragRCubeInvoker invoker) {
    startDragRCubeInvokers.Add(invoker);

    foreach (var listener in startDragRCubeListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveStartDragRCubeInvoker(StartDragRCubeInvoker invoker) {
    startDragRCubeInvokers.Remove(invoker);
  }

  public static void AddStartDragRCubeListener(UnityAction listener) {
    startDragRCubeListeners.Add(listener);

    foreach (var invoker in startDragRCubeInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region drag RCube support

  public static void AddDragRCubeInvoker(DragRCubeInvoker invoker) {
    dragRCubeInvokers.Add(invoker);

    foreach (var listener in dragRCubeListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveDragRCubeInvoker(DragRCubeInvoker invoker) {
    dragRCubeInvokers.Remove(invoker);
  }

  public static void AddDragRCubeListener(UnityAction listener) {
    dragRCubeListeners.Add(listener);

    foreach (var invoker in dragRCubeInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region end drag RCube support

  public static void AddEndDragRCubeInvoker(EndDragRCubeInvoker invoker) {
    endDragRCubeInvokers.Add(invoker);

    foreach (var listener in endDragRCubeListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveEndDragRCubeInvoker(EndDragRCubeInvoker invoker) {
    endDragRCubeInvokers.Remove(invoker);
  }

  public static void AddEndDragRCubeListener(UnityAction listener) {
    endDragRCubeListeners.Add(listener);

    foreach (var invoker in endDragRCubeInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region start drag RCube face support

  public static void AddStartDragRCubeFaceInvoker(StartDragRCubeFaceInvoker invoker) {
    startDragRCubeFaceInvokers.Add(invoker);

    foreach (var listener in startDragRCubeFaceListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveStartDragRCubeFaceInvoker(StartDragRCubeFaceInvoker invoker) {
    startDragRCubeFaceInvokers.Remove(invoker);
  }

  public static void AddStartDragRCubeFaceListener(UnityAction listener) {
    startDragRCubeFaceListeners.Add(listener);

    foreach (var invoker in startDragRCubeFaceInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region drag RCube face support

  public static void AddDragRCubeFaceInvoker(DragRCubeFaceInvoker invoker) {
    dragRCubeFaceInvokers.Add(invoker);

    foreach (var listener in dragRCubeFaceListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveDragRCubeFaceInvoker(DragRCubeFaceInvoker invoker) {
    dragRCubeFaceInvokers.Remove(invoker);
  }

  public static void AddDragRCubeFaceListener(UnityAction listener) {
    dragRCubeFaceListeners.Add(listener);

    foreach (var invoker in dragRCubeFaceInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region end drag RCube face support

  public static void AddEndDragRCubeFaceInvoker(EndDragRCubeFaceInvoker invoker) {
    endDragRCubeFaceInvokers.Add(invoker);

    foreach (var listener in endDragRCubeFaceListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveEndDragRCubeFaceInvoker(EndDragRCubeFaceInvoker invoker) {
    endDragRCubeFaceInvokers.Remove(invoker);
  }

  public static void AddEndDragRCubeFaceListener(UnityAction listener) {
    endDragRCubeFaceListeners.Add(listener);

    foreach (var invoker in endDragRCubeFaceInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region start cube face rotation support

  public static void AddStartRCubeFaceRotationInvoker(StartRCubeFaceRotationInvoker invoker) {
    startRCubeFaceRotationInvokers.Add(invoker);

    foreach (var listener in startRCubeFaceRotationListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveStartRCubeFaceRotationInvoker(StartRCubeFaceRotationInvoker invoker) {
    startRCubeFaceRotationInvokers.Remove(invoker);
  }

  public static void AddStartRCubeFaceRotationListener(UnityAction<string> listener) {
    startRCubeFaceRotationListeners.Add(listener);

    foreach (var invoker in startRCubeFaceRotationInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region cube face rotation support

  public static void AddRCubeFaceRotationInvoker(RCubeFaceRotationInvoker invoker) {
    cubeFaceRotationInvokers.Add(invoker);

    foreach (var listener in cubeFaceRotationListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceRotationInvoker(RCubeFaceRotationInvoker invoker) {
    cubeFaceRotationInvokers.Remove(invoker);
  }

  public static void AddRCubeFaceRotationListener(UnityAction<string> listener) {
    cubeFaceRotationListeners.Add(listener);

    foreach (var invoker in cubeFaceRotationInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region end cube face rotation support

  public static void AddEndRCubeFaceRotationInvoker(EndRCubeFaceRotationInvoker invoker) {
    endRCubeFaceRotationInvokers.Add(invoker);

    foreach (var listener in endRCubeFaceRotationListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveEndRCubeFaceRotationInvoker(EndRCubeFaceRotationInvoker invoker) {
    endRCubeFaceRotationInvokers.Remove(invoker);
  }

  public static void AddEndRCubeFaceRotationListener(UnityAction<string> listener) {
    endRCubeFaceRotationListeners.Add(listener);

    foreach (var invoker in endRCubeFaceRotationInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #endregion

  #region initializer

  static EventManager() { }

  #endregion
}