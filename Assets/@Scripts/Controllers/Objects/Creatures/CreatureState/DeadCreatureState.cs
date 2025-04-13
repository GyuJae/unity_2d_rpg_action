public class DeadCreatureState : CreatureState
{
    public override string GetAnimName()
    {
        return "dead";
    }

    public override float GetUpdateAITick()
    {
        return 1.0f;
    }
}
