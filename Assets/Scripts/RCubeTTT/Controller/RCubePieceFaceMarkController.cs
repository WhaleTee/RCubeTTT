using RCubeTTT.Model;
using UnityEngine;

namespace RCubeTTT.Controller
{
  public class RCubePieceFaceMarkController : MonoBehaviour {
    [field: SerializeField] 
    public MarkType markType { get; private set; }
  }
}