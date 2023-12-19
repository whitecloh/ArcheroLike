using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "MissileData", menuName = "Archero/MissileData")]
    public class MissileData : ScriptableObject
    {
    public GameObject MissilePrefab => _missilePrefab;
        public float FlightSpeed => _flightSpeed;
        public float AttackSpeed => _attackSpeed;
        public float Damage => _damage;
        public float AttackRange => _attackRange;
        public List<IModifier> Modifiers = new List<IModifier>();
        
        [SerializeField] GameObject _missilePrefab;
        [SerializeField] float _flightSpeed;
        [SerializeField] float _attackSpeed;
        [SerializeField] float _damage;
        [SerializeField] float _attackRange;
}