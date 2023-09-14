public class RCubePieceFaceHitEventContext {
  public string userId { get; private set; }
  
  public RCubePieceFaceHitEventContext(string userId) {
    this.userId = userId;
  }
}