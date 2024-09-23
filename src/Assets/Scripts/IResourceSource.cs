using System.Collections;

public interface IResourceSource
{
    public string GetId();
    public int GetAvailableOre();
  public  int ExtractOre(int amount); // ���������� ����
  public  bool IsDepleted(); // ��������, ������� �� ������
  public  string GetResourceName(); // ��������� �������� �������
  public  int OreAvailable { get; } // ���������� ��������� ����
  public  void FinishRecharge(); // ���������� �����������
  public IEnumerator StartRecharge();
  public void StartExtraction();
  public void StopExtraction();
  public event System.Action OnStartRecharge;
  public event System.Action OnFinishRecharge;
}