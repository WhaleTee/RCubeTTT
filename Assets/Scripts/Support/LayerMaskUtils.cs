using UnityEngine;

public static class LayerMaskUtils {
  public static bool EqualsMaskToLayer(int layerMask, int layer) => 1 << layer == layerMask;
  public static LayerMask GetMask(int layer) => 1 << layer;
}