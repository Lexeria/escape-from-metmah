namespace EscapeFromMetMah
{
    public interface ICreature
    {
        Status GetStatus();
        bool IsConflict(ICreature conflictedObject);
        Move Act(Level level, int x, int y);
    }
}
