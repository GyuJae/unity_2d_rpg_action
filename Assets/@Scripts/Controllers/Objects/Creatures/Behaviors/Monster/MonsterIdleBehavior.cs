using UnityEngine;

public class MonsterIdleBehavior : ICreatureBehavior
{
    const float SearchDistance = 8.0f;

    public void Update(Creature creature)
    {
        if (creature is not Monster monster)
            return;

        TryStartPatrol(monster);
        FindNearestHeroInRange(monster);
    }

    static void FindNearestHeroInRange(Monster monster)
    {

        Hero target = null;
        var bestDistanceSqr = float.MaxValue;
        var searchDistanceSqr = SearchDistance * SearchDistance;

        foreach (var hero in Managers.Object.Heroes)
        {
            var dir = hero.transform.position - monster.transform.position;
            var distToTargetSqr = dir.sqrMagnitude;

            if (distToTargetSqr > searchDistanceSqr)
                continue;

            if (distToTargetSqr > bestDistanceSqr)
                continue;

            target = hero;
            bestDistanceSqr = distToTargetSqr;
        }

        monster.SetTarget(target);
        if (target is not null)
        {
            monster.SetState(CreatureState.Move.SetBehavior(new MonsterMoveBehavior()));
        }
    }

    static void TryStartPatrol(Monster monster)
    {

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
