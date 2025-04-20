using UnityEngine;

public class MonsterMoveBehavior : ICreatureBehavior
{
    const float AttackDistance = 1.0f;

    public void Update(Creature creature)
    {
        if (creature is not Monster monster)
            return;


        if (monster.ExistTarget())
        {
            var dir = monster.Target.transform.position - monster.transform.position;
            var distToTargetSqr = dir.sqrMagnitude;
            var attackDistanceSqr = AttackDistance * AttackDistance;

            if (distToTargetSqr < attackDistanceSqr)
            {
                // 공격 범위 이내로 들어왔으면 공격.
                monster.SetState(CreatureState.Move.SetBehavior(new MonsterSkillBehavior()));
                monster.StartWait(2.0f);
            }
            else
            {
                // 공격 범위 밖이라면 추적.
                var moveDist = Mathf.Min(dir.magnitude, Time.deltaTime * monster.CreatureData.MoveSpeed);
                monster.TranslateEx(dir.normalized * moveDist);

                // 너무 멀어지면 포기.
                if (distToTargetSqr < 8.0f * 8.0f)
                    return;

                // TODO 원래 처음 위치로 이동되게 수정
                monster.SetTarget(null);
                monster.SetState(CreatureState.Idle.SetBehavior(new MonsterIdleBehavior()));
            }
        }
        else
        {
            var direction = monster.Destination - monster.transform.position;
            var moveDist = Mathf.Min(direction.magnitude, Time.deltaTime * monster.CreatureData.MoveSpeed);
            monster.TranslateEx(direction.normalized * moveDist);
            if (direction.sqrMagnitude <= 0.01f)
            {
                monster.SetState(CreatureState.Idle.SetBehavior(new MonsterIdleBehavior()));
            }
        }
    }
}
