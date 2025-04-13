using System.Collections;
using UnityEngine;

public class Monster : Creature
{
    public const string PrefabName = "Monster";
    public override float Speed { get; protected set; } = 3.0f;
    public override ECreatureType Type { get; } = ECreatureType.Monster;

    protected override void Awake()
    {
        base.Awake();
        // StartCoroutine(CoUpdateAI());

    }

    IEnumerator CoUpdateAI()
    {
        var tick = State.GetUpdateAITick();
        if (tick > 0) yield return new WaitForSeconds(tick);
        else yield return null;
    }
}
