namespace EscapeFromMetMah
{
    class Terrain : ICreature
    {
        public Status Status { get; private set; }

        public CreatureCommand Act(Level level, int x, int y) => new CreatureCommand();

        public Status GetStatus() => Status;

        public bool IsConflict(ICreature conflictedObject) => false;
    }
}
