public interface IResourceManager
{
    // Методы для добавления и траты ресурсов
  public  void AddResource(ResourceType resourceType, int amount);
  public  bool SpendResource(ResourceType resourceType, int amount);
  public  int GetResourceAmount(ResourceType resourceType);
}
