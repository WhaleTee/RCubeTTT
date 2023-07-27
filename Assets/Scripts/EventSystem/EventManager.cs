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
  
  #endregion

  #region initializer

  static EventManager() { }

  #endregion
}