using UnityEngine;

public interface IAttack 
    {
        void Attack();
    void SetWeapon(MissileData data);
    void SetTarget(Transform target);
    void SetTransform(Vector3 transform);

    }