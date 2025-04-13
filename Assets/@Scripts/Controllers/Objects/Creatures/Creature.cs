public abstract class Creature : BaseObject
{
    protected override EObjectType ObjectType { get; } = EObjectType.Creture;
    public abstract float Speed { get; protected set; }

    public abstract CreatureState State { get; set; }

    public abstract ECreatureType type { get; }

    protected void UpdateAnimation()
    {
        PlayAnimation(0, State.GetAnimName(), true);
    }
}
