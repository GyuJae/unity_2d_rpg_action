public class IdleCreatureState : CreatureState
{
    public override string GetAnimName()
    {
        return "idle";
    }

    public override float GetUpdateAITick()
    {
        return 0.5f;
    }
}
