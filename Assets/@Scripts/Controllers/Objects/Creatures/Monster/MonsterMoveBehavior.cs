using UnityEngine;

public class MonsterMoveBehavior : ICreatureBehavior
{
    public void Update(Creature creature)
    {
        if (creature is not Monster monster)
            return;


        var direction = monster.Destination - monster.transform.position;
        var moveDist = Mathf.Min(direction.magnitude, Time.deltaTime * monster.Speed);
        monster.TranslateEx(direction.normalized * moveDist);
        if (direction.sqrMagnitude <= 0.01f)
        {
            monster.SetState(CreatureState.Idle.SetBehavior(new MonsterIdleBehavior()));
        }
    }
}
