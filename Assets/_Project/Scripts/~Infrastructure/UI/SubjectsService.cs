using R3;

namespace _Project.Scripts._Infrastructure.UI
{
  public class SubjectsService
  {
    public Subject<Unit> ExitSceneRequest => _exitSceneRequest ??= new Subject<Unit>();
    private Subject<Unit> _exitSceneRequest;
    
    
  }
}