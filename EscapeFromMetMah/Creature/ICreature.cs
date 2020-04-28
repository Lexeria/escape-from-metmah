namespace EscapeFromMetMah
{
    public interface ICreature
    {
        Status GetStatus();
        bool IsConflict(ICreature conflictedObject);
        CreatureCommand Act(Level level, int x, int y);
    }
}
