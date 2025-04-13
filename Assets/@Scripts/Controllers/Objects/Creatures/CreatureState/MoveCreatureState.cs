public class MoveCreatureState : CreatureState
{
    public override string GetAnimName()
    {
        return "move";
    }

    public override float GetUpdateAITick()
    {
        return 1.0f;
    }
}
