public abstract class CreatureState
{
    public readonly static CreatureState Idle = new IdleCreatureState();
    public readonly static CreatureState Move = new MoveCreatureState();
    public readonly static CreatureState Skill = new SkillCreatureState();
    public readonly static CreatureState Dead = new DeadCreatureState();

    public abstract string GetAnimName();
}
