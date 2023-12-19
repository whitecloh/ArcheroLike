using UnityEngine;

    [CreateAssetMenu(fileName = "EnemyInfo", menuName = "Archero/EnemyInfo")]
    public class EnemyInfo : ScriptableObject 
    {
        public GameObject Prefab;
        public float MaxHP;  
        public float MoveSpeed;
        public MissileData MissileData;
        [SerializeField] UnityEngine.Object _atackLogics;
        [SerializeField] UnityEngine.Object _moveLogics;
        public IAttack AttackLogic => ScriptableInterface.GetInterface<IAttack>(_atackLogics);
        public IMove MoveLogic => ScriptableInterface.GetInterface<IMove>(_moveLogics);
    }