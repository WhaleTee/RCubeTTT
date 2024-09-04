namespace RCubeTTT.Model
{
  /// <summary>
  /// Defines the rotation context.
  /// </summary>
  public enum RotationType {
    /// <summary>
    /// The context relative to the parent transform.
    /// </summary>
    Local,

    /// <summary>
    /// The context relative to the world.
    /// </summary>
    Global
  }
}