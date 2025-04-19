using System.Collections;
using UnityEngine;

public class Monster : Creature
{
    public const string PrefabName = "Monster";
    public override float Speed { get; protected set; } = 5.0f;
    public Vector3 Destination { get; private set; }
    public override ECreatureType Type { get; } = ECreatureType.Monster;


    protected override void Awake()
    {
        base.Awake();
        State = CreatureState.Idle.SetBehavior(new MonsterIdleBehavior());

        StartCoroutine(CoUpdateAI());
    }

    public void SetDestination(Vector3 position)
    {
        Destination = position;
    }

    IEnumerator CoUpdateAI()
    {
        while (true)
        {
            State.Update(this);
            var tick = State.GetUpdateAITick();
            if (tick > 0)
            {
                yield return new WaitForSeconds(tick);
            }
            else yield return null;
        }
    }

    public void SetState(CreatureState newState)
    {
        State = newState;
    }
}
