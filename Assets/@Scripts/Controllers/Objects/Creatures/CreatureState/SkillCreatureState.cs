public class SkillCreatureState : CreatureState
{
    public override string GetAnimName()
    {
        return "attack_a";
    }

    public override float GetUpdateAITick()
    {
        return 0.0f;
    }
}
