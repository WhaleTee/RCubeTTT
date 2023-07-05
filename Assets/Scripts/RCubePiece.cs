using System;
using UnityEngine;

public class RCubePiece : PrimitiveCube {
  public RCubePieceSide top { get; private set; }
  public RCubePieceSide down { get; private set; }
  public RCubePieceSide left { get; private set; }
  public RCubePieceSide right { get; private set; }
  public RCubePieceSide front { get; private set; }
  public RCubePieceSide back { get; private set; }

  public RCubePiece(GameObject parent, Vector3 localPosition, Vector3 localScale, Quaternion rotation) :
  base(parent, localPosition, localScale, rotation) { }

  public RCubePieceSide InitSide(Side side) {
    return side switch {
             Side.Top =>
             top ??= new RCubePieceSide(
               gameObject,
               new Vector3(0, .5f, 0),
               new Vector3(.9f, .1f, .9f),
               Quaternion.identity
             ),

             Side.Down =>
             down ??= new RCubePieceSide(
               gameObject,
               new Vector3(0, -.5f, 0),
               new Vector3(.9f, .1f, .9f),
               Quaternion.identity
             ),

             Side.Left =>
             left ??= new RCubePieceSide(
               gameObject,
               new Vector3(-.5f, 0, 0),
               new Vector3(.1f, .9f, .9f),
               Quaternion.identity
             ),

             Side.Right =>
             right ??= new RCubePieceSide(
               gameObject,
               new Vector3(.5f, 0, 0),
               new Vector3(.1f, .9f, .9f),
               Quaternion.identity
             ),

             Side.Front =>
             front ??= new RCubePieceSide(
               gameObject,
               new Vector3(0, 0, .5f),
               new Vector3(.9f, .9f, .1f),
               Quaternion.identity
             ),

             Side.Back =>
             back ??= new RCubePieceSide(
               gameObject,
               new Vector3(0, 0, -.5f),
               new Vector3(.9f, .9f, .1f),
               Quaternion.identity
             ),
             var _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
           };
  }
}