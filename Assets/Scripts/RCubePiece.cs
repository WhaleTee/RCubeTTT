using System;
using UnityEngine;

public class RCubePiece : PrimitiveCube {
  public RCubePieceSide Up { get; private set; }
  public RCubePieceSide Down { get; private set; }
  public RCubePieceSide Left { get; private set; }
  public RCubePieceSide Right { get; private set; }
  public RCubePieceSide Front { get; private set; }
  public RCubePieceSide Back { get; private set; }

  public RCubePiece(GameObject parent, Vector3 localPosition, Vector3 localScale, Quaternion rotation) :
  base(parent, localPosition, localScale, rotation) { }

  public RCubePieceSide EnableSide(Side side) {
    return side switch {
             Side.Up =>
             Up ??= new RCubePieceSide(
               Side.Up,
               GameObject,
               new Vector3(0, .5f, 0),
               new Vector3(.9f, .1f, .9f),
               Quaternion.identity
             ),

             Side.Down =>
             Down ??= new RCubePieceSide(
               Side.Down,
               GameObject,
               new Vector3(0, -.5f, 0),
               new Vector3(.9f, .1f, .9f),
               Quaternion.identity
             ),

             Side.Left =>
             Left ??= new RCubePieceSide(
               Side.Left,
               GameObject,
               new Vector3(-.5f, 0, 0),
               new Vector3(.1f, .9f, .9f),
               Quaternion.identity
             ),

             Side.Right =>
             Right ??= new RCubePieceSide(
               Side.Right,
               GameObject,
               new Vector3(.5f, 0, 0),
               new Vector3(.1f, .9f, .9f),
               Quaternion.identity
             ),

             Side.Front =>
             Front ??= new RCubePieceSide(
               Side.Front,
               GameObject,
               new Vector3(0, 0, .5f),
               new Vector3(.9f, .9f, .1f),
               Quaternion.identity
             ),

             Side.Back =>
             Back ??= new RCubePieceSide(
               Side.Back,
               GameObject,
               new Vector3(0, 0, -.5f),
               new Vector3(.9f, .9f, .1f),
               Quaternion.identity
             ),
             var _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
           };
  }

  public void DisableSide(Side side) {
    switch (side) {
      case Side.Up:
        Up = null;
        break;
      case Side.Down:
        Down = null;
        break;
      case Side.Left:
        Left = null;
        break;
      case Side.Right:
        Right = null;
        break;
      case Side.Front:
        Front = null;
        break;
      case Side.Back:
        Back = null;
        break;
      default: throw new ArgumentOutOfRangeException(nameof(side), side, null);
    }
  }

  public void SwapSide(Side from, Side to) {
    var side = from switch {
                 Side.Up => Up,
                 Side.Down => Down,
                 Side.Left => Left,
                 Side.Right => Right,
                 Side.Front => Front,
                 Side.Back => Back,
                 _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
               };

    if (side != null) {
      var toSide = EnableSide(to);
      toSide.SetSign(side.GetSign());
      DisableSide(from);
    }
  }

  public override string ToString() {
    return GameObject.name;
  }
}