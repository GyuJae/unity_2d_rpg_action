using System.Collections;
using UnityEngine;

public abstract class Creature : BaseObject
{
    protected Coroutine coWait;
    CreatureState state;
    public override EObjectType ObjectType { get; } = EObjectType.Creature;
    public abstract float Speed { get; protected set; }
    protected CreatureState State
    {
        get { return state; }
        set
        {
            if (value == state) return;
            state = value;
            UpdateAnimation();
        }
    }

    public abstract ECreatureType Type { get; }

    void UpdateAnimation()
    {
        PlayAnimation(0, State.GetAnimName(), true);
    }

    public void StartWait(float seconds)
    {
        CancelWait();
        coWait = StartCoroutine(CoWait(seconds));
    }

    IEnumerator CoWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        coWait = null;
    }

    public void CancelWait()
    {
        if (coWait != null)
            StopCoroutine(coWait);
        coWait = null;
    }

    public bool ExistCoWait()
    {
        return coWait is not null;
    }
}
