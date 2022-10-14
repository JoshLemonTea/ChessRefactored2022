namespace CommandSystem
{
    public interface ICommand
    {
        bool Commit();

        bool Rollback();
    }
}