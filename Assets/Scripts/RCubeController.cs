using UnityEngine;

public class RCubeController : MonoBehaviour {
  [SerializeField]
  private GameObject cubePiecePrefab;
  [SerializeField]
  private float cubePieceSize;

  [SerializeField]
  private Color[] colors;

  private RCube3X3 rCube3X3;

  private void Start() {
    rCube3X3 = new RCube3X3(
      gameObject,
      cubePiecePrefab,
      cubePieceSize,
      colors[0],
      colors[1],
      colors[2],
      colors[3],
      colors[4],
      colors[5]
    );
  }

  private void CreateCube() { }
}