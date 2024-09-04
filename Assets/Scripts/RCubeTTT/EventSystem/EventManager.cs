using System.Collections.Generic;
using RCubeTTT.EventSystem.EventContext;
using RCubeTTT.EventSystem.EventInvoker;
using RCubeTTT.Model;
using UnityEngine.Events;

namespace RCubeTTT.EventSystem
{
  public static class EventManager {

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

    private static readonly List<UnityAction<RCubePieceFaceMarkSetEventContext>> rCubePieceFaceSignSetListeners =
    new List<UnityAction<RCubePieceFaceMarkSetEventContext>>();

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

    private static readonly List<RCubeFacePiecesFacesRayCastHitEventInvoker> rCubeFacePiecesFacesRaycastHitInvokers =
    new List<RCubeFacePiecesFacesRayCastHitEventInvoker>();

    private static readonly List<UnityAction<RCubeFacePiecesFacesRaycastHitEventContext>> rCubeFacePiecesFacesRaycastHitListeners =
    new List<UnityAction<RCubeFacePiecesFacesRaycastHitEventContext>>();

    public static void AddRCubeFacePiecesFacesRaycastHitInvoker(RCubeFacePiecesFacesRayCastHitEventInvoker invoker) {
      rCubeFacePiecesFacesRaycastHitInvokers.Add(invoker);

      foreach (var listener in rCubeFacePiecesFacesRaycastHitListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void RemoveRCubeFacePiecesFacesRaycastHitInvoker(RCubeFacePiecesFacesRayCastHitEventInvoker invoker) {
      rCubeFacePiecesFacesRaycastHitInvokers.Remove(invoker);
    }

    public static void AddRCubeFacePiecesFacesRaycastHitListener(UnityAction<RCubeFacePiecesFacesRaycastHitEventContext> listener) {
      rCubeFacePiecesFacesRaycastHitListeners.Add(listener);

      foreach (var invoker in rCubeFacePiecesFacesRaycastHitInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion

    #region player win condition support

    private static readonly List<PlayerWinConditionReachedEventInvoker> playerWinConditionReachedEventInvokers =
    new List<PlayerWinConditionReachedEventInvoker>();

    private static readonly List<UnityAction<PlayerWinConditionReachedEventContext>> playerWinConditionReachedListeners =
    new List<UnityAction<PlayerWinConditionReachedEventContext>>();

    public static void AddPlayerWinConditionReachedInvoker(PlayerWinConditionReachedEventInvoker invoker) {
      playerWinConditionReachedEventInvokers.Add(invoker);

      foreach (var listener in playerWinConditionReachedListeners) {
        invoker.AddListener(listener);
      }
    }

    public static void RemovePlayerWinConditionReachedInvoker(PlayerWinConditionReachedEventInvoker invoker) {
      playerWinConditionReachedEventInvokers.Remove(invoker);
    }

    public static void AddPlayerWinConditionReachedListener(UnityAction<PlayerWinConditionReachedEventContext> listener) {
      playerWinConditionReachedListeners.Add(listener);

      foreach (var invoker in playerWinConditionReachedEventInvokers) {
        invoker.AddListener(listener);
      }
    }

    #endregion
  }
}