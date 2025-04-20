using System.Collections;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Creature : BaseObject
{
    protected Coroutine coWait;
    CreatureState state;

    public override EObjectType ObjectType { get; } = EObjectType.Creature;
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

    public CreatureData CreatureData { get; private set; }
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

    public virtual void SetInfo(int templateID)
    {
        DataTemplateID = templateID;
        CreatureData = Managers.Data.CreatureDict[templateID];
        gameObject.name = $"{CreatureData.DataId}_{CreatureData.DescriptionTextID}";

        // Collider
        Collider.offset = new Vector2(CreatureData.ColliderOffsetX, CreatureData.ColliderOffstY);
        Collider.radius = CreatureData.ColliderRadius;

        // RigidBody
        RigidBody.mass = CreatureData.Mass;

        // Spine
        SkeletonAnim.skeletonDataAsset = Managers.Resource.Load<SkeletonDataAsset>(CreatureData.SkeletonDataID);
        SkeletonAnim.Initialize(true);

        // Register AnimEvent
        if (SkeletonAnim.AnimationState != null)
        {
            // SkeletonAnim.AnimationState.Event -= OnAnimEventHandler;
            // SkeletonAnim.AnimationState.Event += OnAnimEventHandler;
        }

        // Spine SkeletonAnimation은 SpriteRenderer 를 사용하지 않고 MeshRenderer을 사용함.
        // 그렇기떄문에 2D Sort Axis가 안먹히게 되는데 SortingGroup을 SpriteRenderer, MeshRenderer을같이 계산함.
        var sg = Utils.GetOrAddComponent<SortingGroup>(gameObject);
        sg.sortingOrder = SortingLayers.CREATURE;

        // Stat
        MaxHp = CreatureData.MaxHp;
        Hp = CreatureData.MaxHp;
        Atk = CreatureData.MaxHp;
        MaxHp = CreatureData.MaxHp;
        MoveSpeed = CreatureData.MoveSpeed;

        // State
        State = CreatureState.Idle.SetBehavior(new MonsterIdleBehavior());
    }

    #region Stats

    public float Hp { get; set; }
    public float MaxHp { get; set; }
    public float MaxHpBonusRate { get; set; }
    public float HealBonusRate { get; set; }
    public float HpRegen { get; set; }
    public float Atk { get; set; }
    public float AttackRate { get; set; }
    public float Def { get; set; }
    public float DefRate { get; set; }
    public float CriRate { get; set; }
    public float CriDamage { get; set; }
    public float DamageReduction { get; set; }
    public float MoveSpeedRate { get; set; }
    public float MoveSpeed { get; set; }

    #endregion
}
