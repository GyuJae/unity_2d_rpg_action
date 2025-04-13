public abstract class Creature : BaseObject
{
    CreatureState state;
    public override EObjectType ObjectType { get; } = EObjectType.Creature;
    public abstract float Speed { get; protected set; }
    protected CreatureState State
    {
        get { return state; }
        set
        {
            if (value == state) return;
            state = value;
            UpdateAnimation();
        }
    }

    public abstract ECreatureType Type { get; }

    void UpdateAnimation()
    {
        PlayAnimation(0, State.GetAnimName(), true);
    }
}
