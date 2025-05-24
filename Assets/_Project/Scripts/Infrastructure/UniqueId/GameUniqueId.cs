namespace _Project.Scripts.Infrastructure.UniqueId
{
  public class GameUniqueId
  {
    private static int _currentID = 0;

    public static int GetNextID() 
    {
      return _currentID++;
    }
    
    public static void Reset() 
    {
      _currentID = 0;
    }

  }
}