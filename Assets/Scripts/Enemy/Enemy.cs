using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    private IAttack _attackLogic;
    private IMove _moveLogic;

    private Transform _target;
    private NavMeshAgent _agent;
    private HealthPoints _hp;
    private MissileData _missileData;
    [SerializeField] bool _alive = true;
    public event Action<Enemy> OnDeath;
    //---------------- Initialize ---------------------
    public void SetStatsFromInfo(EnemyInfo info)
    {
        _attackLogic = info.AttackLogic;
        _moveLogic = info.MoveLogic;

        _moveLogic.Enemy = transform;
        _moveLogic.Speed = info.MoveSpeed;
        _moveLogic.SetTransform(transform);

        _missileData = info.MissileData;
        _attackLogic.SetWeapon(_missileData);

        _hp = new HealthPoints(info.MaxHP);
        _agent = GetComponent<NavMeshAgent>();
    }
    public void SetTarget(Transform target)
    {
        _target = target;
        _moveLogic.SetTarget(target);
        _attackLogic.SetTarget(target);
    }

    internal void SetHealthBar(HealthBarManager manager)
    {
        var healthBar = manager.GetHealthBarForTarget(transform);
        _hp.OnChange += healthBar.ChangeHealth;
        _hp.OnNoHP += () => manager.RemoveHealthBar(transform);
        _hp.OnNoHP += SelfDestroy;
    }
    //---------------- HP ---------------------
    [ContextMenu("Destroy enemy")]
    public void SelfDestroy()
    {
        OnDeath?.Invoke(this);

        Destroy(gameObject);
    }

    public void GetDamage(float value)
    {
        _hp.ChangeHP(-value);
    }

    //---------------- Enemy States ---------------------
    internal void States()
    {
        StartCoroutine(StatesCoroutine());
    }

    private IEnumerator StatesCoroutine()
    {
        float time = 0f;
        while (_alive)
        {
            if (attackPlayer)
            {
                _attackLogic.SetTransform(transform.position);
                _attackLogic.Attack();
                _agent.speed = 0;
                time = _missileData.AttackSpeed;
            }
            else
            {
                _moveLogic.Move();
                _agent.speed = _moveLogic.Speed;
                time = Time.fixedDeltaTime;
            }
            yield return new WaitForSeconds(time);
        }
    }

    private bool canAttack;
    public bool attackPlayer
    {
        get
        {
            RaycastHit info;
            bool hit = Physics.Raycast(transform.position, _target.position - transform.position,out info);
            canAttack = hit && info.transform.CompareTag("Player") && Vector3.Distance(transform.position, _target.position) <= _missileData.AttackRange ? true : false;
            return canAttack;
        }
        private set
        {
            canAttack = value;
        }
    }
}
