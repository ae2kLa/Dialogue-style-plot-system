public interface ICommand
{
    public void Execute();
    public void OnUpdate();
    public bool IsFinished();
}
