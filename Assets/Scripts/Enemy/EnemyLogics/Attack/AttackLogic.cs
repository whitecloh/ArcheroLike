using UnityEngine;

    public class AttackLogic : ScriptableObject,IAttack  
    {
    private Vector3 _spawnPosition;
    private Transform _target;
    private MissileSpawner _missileSpawner = new MissileSpawner();

    public void SetWeapon(MissileData data)
    {
        _missileSpawner.SetWeapon(data);
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    public void SetTransform(Vector3 position)
    {
        _spawnPosition = position;
    }
        public void Attack()
        {
        var missile = _missileSpawner.GetMissile();
        missile.transform.position = _spawnPosition;
        missile.Init(_missileSpawner.CurrentWeapon, _target);
    }
    }