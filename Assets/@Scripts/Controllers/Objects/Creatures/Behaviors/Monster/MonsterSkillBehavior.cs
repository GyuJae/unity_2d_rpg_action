public class MonsterSkillBehavior : ICreatureBehavior
{
    public void Update(Creature creature)
    {
        if (creature is not Monster monster)
            return;

        if (monster.ExistCoWait()) return;
        monster.SetState(CreatureState.Idle.SetBehavior(new MonsterIdleBehavior()));

    }
}
