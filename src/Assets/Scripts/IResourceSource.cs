using System.Collections;

public interface IResourceSource
{
    public string GetId();
    public int GetAvailableOre();
  public  int ExtractOre(int amount); // Извлечение руды
  public  bool IsDepleted(); // Проверка, истощен ли ресурс
  public  string GetResourceName(); // Получение названия ресурса
  public  int OreAvailable { get; } // Количество доступной руды
  public  void FinishRecharge(); // Завершение перезарядки
  public IEnumerator StartRecharge();
  public void StartExtraction();
  public void StopExtraction();
  public event System.Action OnStartRecharge;
  public event System.Action OnFinishRecharge;
}