public abstract class CreatureState
{
    public readonly static CreatureState Idle = new IdleCreatureState();
    public readonly static CreatureState Move = new MoveCreatureState();
    public readonly static CreatureState Skill = new SkillCreatureState();
    public readonly static CreatureState Dead = new DeadCreatureState();

    ICreatureBehavior behavior = new NullCreatureBehavior();

    public CreatureState SetBehavior(ICreatureBehavior newBehavior)
    {
        behavior = newBehavior ?? new NullCreatureBehavior();
        return this;
    }

    public void Update(Creature creature)
    {
        behavior.Update(creature);
    }

    public abstract string GetAnimName();
    public abstract float GetUpdateAITick();
}
