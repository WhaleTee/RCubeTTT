using System.Collections.Generic;
using UnityEngine.Events;

public static class EventManager {
  #region fields

  #region RCube drag support

  // start drag RCube support
  private static readonly List<RCubeDragStartEventInvoker> rCubeDragStartInvokers = new List<RCubeDragStartEventInvoker>();
  private static readonly List<UnityAction> rCubeDragStartListeners = new List<UnityAction>();

  // drag RCube support
  private static readonly List<RCubeDragEventInvoker> rCubeDragInvokers = new List<RCubeDragEventInvoker>();
  private static readonly List<UnityAction> rCubeDragListeners = new List<UnityAction>();

  // end drag RCube support
  private static readonly List<RCubeDragEndEventInvoker> rCubeDragEndInvokers = new List<RCubeDragEndEventInvoker>();
  private static readonly List<UnityAction> rCubeDragEndListeners = new List<UnityAction>();

  #endregion

  #region RCube Face drag support

  // start drag RCube face support
  private static readonly List<RCubeFaceDragStartEventInvoker> rCubeFaceDragStartInvokers = new List<RCubeFaceDragStartEventInvoker>();
  private static readonly List<UnityAction<string>> rCubeFaceDragStartListeners = new List<UnityAction<string>>();

  // drag RCube face support
  private static readonly List<RCubeFaceDragEventInvoker> rCubeFaceDragInvokers = new List<RCubeFaceDragEventInvoker>();
  private static readonly List<UnityAction<string>> rCubeFaceDragListeners = new List<UnityAction<string>>();

  // end drag RCube face support
  private static readonly List<RCubeFaceDragEndEventInvoker> rCubeFaceDragEndInvokers = new List<RCubeFaceDragEndEventInvoker>();
  private static readonly List<UnityAction<string>> rCubeFaceDragEndListeners = new List<UnityAction<string>>();

  #endregion

  #region RCube Face rotation support

  // start cube face rotation support
  private static readonly List<RCubeFaceRotationStartEventInvoker> rCubeFaceRotationStartInvokers = new List<RCubeFaceRotationStartEventInvoker>();
  private static readonly List<UnityAction<string>> rCubeFaceRotationStartListeners = new List<UnityAction<string>>();

  // cube face rotation support
  private static readonly List<RCubeFaceRotationEventInvoker> rCubeFaceRotationInvokers = new List<RCubeFaceRotationEventInvoker>();
  private static readonly List<UnityAction<string>> rCubeFaceRotationListeners = new List<UnityAction<string>>();

  // end cube face rotation support
  private static readonly List<RCubeFaceRotationEndEventInvoker> rCubeFaceRotationEndInvokers = new List<RCubeFaceRotationEndEventInvoker>();
  private static readonly List<UnityAction<string>> rCubeFaceRotationEndListeners = new List<UnityAction<string>>();

  #endregion

  #endregion

  #region methods

  #region start drag RCube support

  public static void AddRCubeDragStartInvoker(RCubeDragStartEventInvoker invoker) {
    rCubeDragStartInvokers.Add(invoker);

    foreach (var listener in rCubeDragStartListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeDragStartInvoker(RCubeDragStartEventInvoker invoker) {
    rCubeDragStartInvokers.Remove(invoker);
  }

  public static void AddRCubeDragStartListener(UnityAction listener) {
    rCubeDragStartListeners.Add(listener);

    foreach (var invoker in rCubeDragStartInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region drag RCube support

  public static void AddRCubeDragInvoker(RCubeDragEventInvoker invoker) {
    rCubeDragInvokers.Add(invoker);

    foreach (var listener in rCubeDragListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeDragInvoker(RCubeDragEventInvoker invoker) {
    rCubeDragInvokers.Remove(invoker);
  }

  public static void AddRCubeDragListener(UnityAction listener) {
    rCubeDragListeners.Add(listener);

    foreach (var invoker in rCubeDragInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region end drag RCube support

  public static void AddRCubeDragEndInvoker(RCubeDragEndEventInvoker invoker) {
    rCubeDragEndInvokers.Add(invoker);

    foreach (var listener in rCubeDragEndListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeDragEndInvoker(RCubeDragEndEventInvoker invoker) {
    rCubeDragEndInvokers.Remove(invoker);
  }

  public static void AddRCubeDragEndListener(UnityAction listener) {
    rCubeDragEndListeners.Add(listener);

    foreach (var invoker in rCubeDragEndInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region start drag RCube face support

  public static void AddRCubeFaceDragStartInvoker(RCubeFaceDragStartEventInvoker invoker) {
    rCubeFaceDragStartInvokers.Add(invoker);

    foreach (var listener in rCubeFaceDragStartListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceDragStartInvoker(RCubeFaceDragStartEventInvoker invoker) {
    rCubeFaceDragStartInvokers.Remove(invoker);
  }

  public static void AddRCubeFaceDragStartListener(UnityAction<string> listener) {
    rCubeFaceDragStartListeners.Add(listener);

    foreach (var invoker in rCubeFaceDragStartInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region drag RCube face support

  public static void AddRCubeFaceDragInvoker(RCubeFaceDragEventInvoker invoker) {
    rCubeFaceDragInvokers.Add(invoker);

    foreach (var listener in rCubeFaceDragListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceDragInvoker(RCubeFaceDragEventInvoker invoker) {
    rCubeFaceDragInvokers.Remove(invoker);
  }

  public static void AddRCubeFaceDragListener(UnityAction<string> listener) {
    rCubeFaceDragListeners.Add(listener);

    foreach (var invoker in rCubeFaceDragInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region end drag RCube face support

  public static void AddRCubeFaceDragEndInvoker(RCubeFaceDragEndEventInvoker invoker) {
    rCubeFaceDragEndInvokers.Add(invoker);

    foreach (var listener in rCubeFaceDragEndListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceDragEndInvoker(RCubeFaceDragEndEventInvoker invoker) {
    rCubeFaceDragEndInvokers.Remove(invoker);
  }

  public static void AddRCubeFaceDragEndListener(UnityAction<string> listener) {
    rCubeFaceDragEndListeners.Add(listener);

    foreach (var invoker in rCubeFaceDragEndInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region start cube face rotation support

  public static void AddRCubeFaceRotationStartInvoker(RCubeFaceRotationStartEventInvoker invoker) {
    rCubeFaceRotationStartInvokers.Add(invoker);

    foreach (var listener in rCubeFaceRotationStartListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceRotationStartInvoker(RCubeFaceRotationStartEventInvoker invoker) {
    rCubeFaceRotationStartInvokers.Remove(invoker);
  }

  public static void AddRCubeFaceRotationStartListener(UnityAction<string> listener) {
    rCubeFaceRotationStartListeners.Add(listener);

    foreach (var invoker in rCubeFaceRotationStartInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region cube face rotation support

  public static void AddRCubeFaceRotationInvoker(RCubeFaceRotationEventInvoker invoker) {
    rCubeFaceRotationInvokers.Add(invoker);

    foreach (var listener in rCubeFaceRotationListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceRotationInvoker(RCubeFaceRotationEventInvoker invoker) {
    rCubeFaceRotationInvokers.Remove(invoker);
  }

  public static void AddRCubeFaceRotationListener(UnityAction<string> listener) {
    rCubeFaceRotationListeners.Add(listener);

    foreach (var invoker in rCubeFaceRotationInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region end cube face rotation support

  public static void AddRCubeFaceRotationEndInvoker(RCubeFaceRotationEndEventInvoker eventInvoker) {
    rCubeFaceRotationEndInvokers.Add(eventInvoker);

    foreach (var listener in rCubeFaceRotationEndListeners) {
      eventInvoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceRotationEndInvoker(RCubeFaceRotationEndEventInvoker eventInvoker) {
    rCubeFaceRotationEndInvokers.Remove(eventInvoker);
  }

  public static void AddRCubeFaceRotationEndListener(UnityAction<string> listener) {
    rCubeFaceRotationEndListeners.Add(listener);

    foreach (var invoker in rCubeFaceRotationEndInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #endregion

  #region initializer

  static EventManager() { }

  #endregion
}