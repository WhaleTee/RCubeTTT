using System;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public sealed class RangeVectorAttribute : PropertyAttribute {
  #region fields

  public readonly Vector3 min = Vector3.zero;
  public readonly Vector3 max = Vector3.zero;

  #endregion

  #region constructors

  /// <summary>
  ///   <para>Attribute used to make a Vector variable in a script be restricted to a specific range.</para>
  /// </summary>
  /// <param name="min">The array with the minimum allowed values.</param>
  /// <param name="max">The array with the maximum allowed values.</param>
  public RangeVectorAttribute(float[] min, float[] max) {
    min ??= new float[] { };
    max ??= new float[] { };

    if (min.Length > 3 || max.Length > 3) {
      throw new ArgumentException("min and max must be of length 3 or less");
    }

    switch (min.Length) {
      case 3:
        this.min.x = min[0];
        this.min.y = min[1];
        this.min.z = min[2];
        break;
      case 2:
        this.min.x = min[0];
        this.min.y = min[1];
        break;
      case 1:
        this.min.x = min[0];
        break;
    }

    switch (max.Length) {
      case 3:
        this.max.x = max[0];
        this.max.y = max[1];
        this.max.z = max[2];
        break;
      case 2:
        this.max.x = max[0];
        this.max.y = max[1];
        break;
      case 1:
        this.max.x = max[0];
        break;
    }
  }

  public RangeVectorAttribute(float[] max) : this(new float[] { }, max ?? new float[] { }) { }

  #endregion
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RangeVectorAttribute))]
public class RangeVectorDrawer : PropertyDrawer {
  #region fields

  private const int HELP_HEIGHT = 24; // pixels

  #endregion

  #region properties

  private RangeVectorAttribute rangeVectorAttribute => attribute as RangeVectorAttribute;

  #endregion

  #region methods

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    HandleChanges(position, property, label);
  }

  private void HandleChanges(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUI.BeginChangeCheck();

    switch (property.propertyType) {
      case SerializedPropertyType.Vector2: {
        var val = EditorGUI.Vector2Field(position, label, property.vector2Value);

        if (EditorGUI.EndChangeCheck()) {
          val.x = Mathf.Clamp(val.x, rangeVectorAttribute.min.x, rangeVectorAttribute.max.x);
          val.y = Mathf.Clamp(val.y, rangeVectorAttribute.min.y, rangeVectorAttribute.max.y);
          property.vector2Value = val;
        }

        break;
      }
      case SerializedPropertyType.Vector2Int: {
        var val = EditorGUI.Vector2IntField(position, label, property.vector2IntValue);

        if (EditorGUI.EndChangeCheck()) {
          val.x = Mathf.RoundToInt(Mathf.Clamp(val.x, rangeVectorAttribute.min.x, rangeVectorAttribute.max.x));
          val.y = Mathf.RoundToInt(Mathf.Clamp(val.y, rangeVectorAttribute.min.y, rangeVectorAttribute.max.y));
          property.vector2IntValue = val;
        }

        break;
      }
      case SerializedPropertyType.Vector3: {
        var val = EditorGUI.Vector3Field(position, label, property.vector3Value);

        if (EditorGUI.EndChangeCheck()) {
          val.x = Mathf.Clamp(val.x, rangeVectorAttribute.min.x, rangeVectorAttribute.max.x);
          val.y = Mathf.Clamp(val.y, rangeVectorAttribute.min.y, rangeVectorAttribute.max.y);
          val.z = Mathf.Clamp(val.z, rangeVectorAttribute.min.z, rangeVectorAttribute.max.z);
          property.vector3Value = val;
        }

        break;
      }
      case SerializedPropertyType.Vector3Int: {
        var val = EditorGUI.Vector3IntField(position, label, property.vector3IntValue);

        if (EditorGUI.EndChangeCheck()) {
          val.x = Mathf.RoundToInt(Mathf.Clamp(val.x, rangeVectorAttribute.min.x, rangeVectorAttribute.max.x));
          val.y = Mathf.RoundToInt(Mathf.Clamp(val.y, rangeVectorAttribute.min.y, rangeVectorAttribute.max.y));
          val.z = Mathf.RoundToInt(Mathf.Clamp(val.z, rangeVectorAttribute.min.z, rangeVectorAttribute.max.z));
          property.vector3IntValue = val;
        }

        break;
      }
      default:
        var helpPosition = position;
        helpPosition.height = HELP_HEIGHT;
        EditorGUI.HelpBox(helpPosition, "Use RangeVector with Vector2, Vector2Int, Vector3 or Vector3Int.", MessageType.Error);
        break;
    }
  }

  #endregion
}

#endif