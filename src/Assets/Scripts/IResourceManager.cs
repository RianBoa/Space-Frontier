public interface IResourceManager
{
    // ������ ��� ���������� � ����� ��������
  public  void AddResource(ResourceType resourceType, int amount);
  public  bool SpendResource(ResourceType resourceType, int amount);
  public  int GetResourceAmount(ResourceType resourceType);
}
