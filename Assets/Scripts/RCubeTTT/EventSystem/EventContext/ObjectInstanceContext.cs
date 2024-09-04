namespace RCubeTTT.EventSystem.EventContext
{
  public class ObjectInstanceContext {
    public int instanceId { get; private set; }
    
    public ObjectInstanceContext(int instanceId) {
      this.instanceId = instanceId;
    }
  }
}