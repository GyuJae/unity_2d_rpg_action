using UnityEngine;

public class MonsterIdleBehavior : ICreatureBehavior
{
    public void Update(Creature creature)
    {
        if (creature is not Monster monster)
            return;

        // === Patrol ===
        var patrolPercent = 10;
        var rand = Random.Range(0, 100);
        if (rand <= patrolPercent)
        {
            var randomOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f));

            monster.SetDestination(monster.transform.position + randomOffset);
            monster.SetState(CreatureState.Move.SetBehavior(new MonsterMoveBehavior()));
        }
    }
}
