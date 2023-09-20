using System.Collections.Generic;
using UnityEngine.Events;

public static class EventManager {
  #region RCube drag support

  #region start drag RCube support

  private static readonly List<RCubeDragStartEventInvoker> rCubeDragStartInvokers = new List<RCubeDragStartEventInvoker>();
  private static readonly List<UnityAction> rCubeDragStartListeners = new List<UnityAction>();

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

  private static readonly List<RCubeDragEventInvoker> rCubeDragInvokers = new List<RCubeDragEventInvoker>();
  private static readonly List<UnityAction> rCubeDragListeners = new List<UnityAction>();

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

  private static readonly List<RCubeDragEndEventInvoker> rCubeDragEndInvokers = new List<RCubeDragEndEventInvoker>();
  private static readonly List<UnityAction> rCubeDragEndListeners = new List<UnityAction>();

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

  #endregion

  #region RCube Face drag support

  #region start drag RCube face support

  private static readonly List<RCubeFaceDragStartEventInvoker> rCubeFaceDragStartInvokers = new List<RCubeFaceDragStartEventInvoker>();

  private static readonly List<UnityAction<RCubeFaceDragStartEventContext>> rCubeFaceDragStartListeners =
  new List<UnityAction<RCubeFaceDragStartEventContext>>();

  public static void AddRCubeFaceDragStartInvoker(RCubeFaceDragStartEventInvoker invoker) {
    rCubeFaceDragStartInvokers.Add(invoker);

    foreach (var listener in rCubeFaceDragStartListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceDragStartInvoker(RCubeFaceDragStartEventInvoker invoker) {
    rCubeFaceDragStartInvokers.Remove(invoker);
  }

  public static void AddRCubeFaceDragStartListener(UnityAction<RCubeFaceDragStartEventContext> listener) {
    rCubeFaceDragStartListeners.Add(listener);

    foreach (var invoker in rCubeFaceDragStartInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region drag RCube face support

  private static readonly List<RCubeFaceDragEventInvoker> rCubeFaceDragInvokers = new List<RCubeFaceDragEventInvoker>();
  private static readonly List<UnityAction<string>> rCubeFaceDragListeners = new List<UnityAction<string>>();

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

  private static readonly List<RCubeFaceDragEndEventInvoker> rCubeFaceDragEndInvokers = new List<RCubeFaceDragEndEventInvoker>();
  private static readonly List<UnityAction<string>> rCubeFaceDragEndListeners = new List<UnityAction<string>>();

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

  #endregion

  #region RCube Face rotation support

  #region start cube face rotation support

  private static readonly List<RCubeFaceRotationStartEventInvoker> rCubeFaceRotationStartInvokers = new List<RCubeFaceRotationStartEventInvoker>();
  private static readonly List<UnityAction<RCubeFaceRotationStartEventContext>> rCubeFaceRotationStartListeners = new List<UnityAction<RCubeFaceRotationStartEventContext>>();

  public static void AddRCubeFaceRotationStartInvoker(RCubeFaceRotationStartEventInvoker invoker) {
    rCubeFaceRotationStartInvokers.Add(invoker);

    foreach (var listener in rCubeFaceRotationStartListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceRotationStartInvoker(RCubeFaceRotationStartEventInvoker invoker) {
    rCubeFaceRotationStartInvokers.Remove(invoker);
  }

  public static void AddRCubeFaceRotationStartListener(UnityAction<RCubeFaceRotationStartEventContext> listener) {
    rCubeFaceRotationStartListeners.Add(listener);

    foreach (var invoker in rCubeFaceRotationStartInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region cube face rotation support

  private static readonly List<RCubeFaceRotationEventInvoker> rCubeFaceRotationInvokers = new List<RCubeFaceRotationEventInvoker>();
  private static readonly List<UnityAction<RCubeFaceRotationEventContext>> rCubeFaceRotationListeners = new List<UnityAction<RCubeFaceRotationEventContext>>();

  public static void AddRCubeFaceRotationInvoker(RCubeFaceRotationEventInvoker invoker) {
    rCubeFaceRotationInvokers.Add(invoker);

    foreach (var listener in rCubeFaceRotationListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceRotationInvoker(RCubeFaceRotationEventInvoker invoker) {
    rCubeFaceRotationInvokers.Remove(invoker);
  }

  public static void AddRCubeFaceRotationListener(UnityAction<RCubeFaceRotationEventContext> listener) {
    rCubeFaceRotationListeners.Add(listener);

    foreach (var invoker in rCubeFaceRotationInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region end cube face rotation support

  private static readonly List<RCubeFaceRotationEndEventInvoker> rCubeFaceRotationEndInvokers = new List<RCubeFaceRotationEndEventInvoker>();
  private static readonly List<UnityAction<RCubeFaceRotationEndEventContext>> rCubeFaceRotationEndListeners = new List<UnityAction<RCubeFaceRotationEndEventContext>>();

  public static void AddRCubeFaceRotationEndInvoker(RCubeFaceRotationEndEventInvoker eventInvoker) {
    rCubeFaceRotationEndInvokers.Add(eventInvoker);

    foreach (var listener in rCubeFaceRotationEndListeners) {
      eventInvoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFaceRotationEndInvoker(RCubeFaceRotationEndEventInvoker eventInvoker) {
    rCubeFaceRotationEndInvokers.Remove(eventInvoker);
  }

  public static void AddRCubeFaceRotationEndListener(UnityAction<RCubeFaceRotationEndEventContext> listener) {
    rCubeFaceRotationEndListeners.Add(listener);

    foreach (var invoker in rCubeFaceRotationEndInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #endregion

  #region player turn support

  #region player turn start support

  private static readonly List<PlayerTurnStartEventInvoker> playerTurnStartEventInvokers = new List<PlayerTurnStartEventInvoker>();
  private static readonly List<UnityAction<PlayerPlayData>> playerTurnStartListeners = new List<UnityAction<PlayerPlayData>>();

  public static void AddPlayerTurnStartInvoker(PlayerTurnStartEventInvoker invoker) {
    playerTurnStartEventInvokers.Add(invoker);

    foreach (var listener in playerTurnStartListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemovePlayerTurnStartInvoker(PlayerTurnStartEventInvoker invoker) {
    playerTurnStartEventInvokers.Remove(invoker);
  }

  public static void AddPlayerTurnStartListener(UnityAction<PlayerPlayData> listener) {
    playerTurnStartListeners.Add(listener);

    foreach (var invoker in playerTurnStartEventInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region player turn support

  private static readonly List<PlayerTurnEventInvoker> playerTurnEventInvokers = new List<PlayerTurnEventInvoker>();
  private static readonly List<UnityAction<PlayerPlayData>> playerTurnListeners = new List<UnityAction<PlayerPlayData>>();

  public static void AddPlayerTurnInvoker(PlayerTurnEventInvoker invoker) {
    playerTurnEventInvokers.Add(invoker);

    foreach (var listener in playerTurnListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemovePlayerTurnInvoker(PlayerTurnEventInvoker invoker) {
    playerTurnEventInvokers.Remove(invoker);
  }

  public static void AddPlayerTurnListener(UnityAction<PlayerPlayData> listener) {
    playerTurnListeners.Add(listener);

    foreach (var invoker in playerTurnEventInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion
  
  #endregion

  #region set mark support

  private static readonly List<RCubePieceFaceMarkSetEventInvoker> rCubePieceFaceSignSetEventInvokers = new List<RCubePieceFaceMarkSetEventInvoker>();
  private static readonly List<UnityAction<RCubePieceFaceMarkSetEventContext>> rCubePieceFaceSignSetListeners = new List<UnityAction<RCubePieceFaceMarkSetEventContext>>();

  public static void AddRCubePieceFaceMarkSetInvoker(RCubePieceFaceMarkSetEventInvoker invoker) {
    rCubePieceFaceSignSetEventInvokers.Add(invoker);

    foreach (var listener in rCubePieceFaceSignSetListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubePieceFaceMarkSetInvoker(RCubePieceFaceMarkSetEventInvoker invoker) {
    rCubePieceFaceSignSetEventInvokers.Remove(invoker);
  }

  public static void AddRCubePieceFaceMarkSetListener(UnityAction<RCubePieceFaceMarkSetEventContext> listener) {
    rCubePieceFaceSignSetListeners.Add(listener);

    foreach (var invoker in rCubePieceFaceSignSetEventInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  #region scan for face pieces faces support

  private static readonly List<RCubeFacePiecesFacesRaycastHitEventInvoker> rCubeFacePiecesFacesRaycastHitInvokers = new List<RCubeFacePiecesFacesRaycastHitEventInvoker>();
  private static readonly List<UnityAction<RCubeFacePiecesFacesRaycastHitEventContext>> rCubeFacePiecesFacesRaycastHitListeners = new List<UnityAction<RCubeFacePiecesFacesRaycastHitEventContext>>();

  public static void AddRCubeFacePiecesFacesRaycastHitInvoker(RCubeFacePiecesFacesRaycastHitEventInvoker invoker) {
    rCubeFacePiecesFacesRaycastHitInvokers.Add(invoker);

    foreach (var listener in rCubeFacePiecesFacesRaycastHitListeners) {
      invoker.AddListener(listener);
    }
  }

  public static void RemoveRCubeFacePiecesFacesRaycastHitInvoker(RCubeFacePiecesFacesRaycastHitEventInvoker invoker) {
    rCubeFacePiecesFacesRaycastHitInvokers.Remove(invoker);
  }

  public static void AddRCubeFacePiecesFacesRaycastHitListener(UnityAction<RCubeFacePiecesFacesRaycastHitEventContext> listener) {
    rCubeFacePiecesFacesRaycastHitListeners.Add(listener);

    foreach (var invoker in rCubeFacePiecesFacesRaycastHitInvokers) {
      invoker.AddListener(listener);
    }
  }

  #endregion

  static EventManager() { }
}