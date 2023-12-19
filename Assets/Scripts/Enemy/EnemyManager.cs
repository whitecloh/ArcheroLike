using System.Collections.Generic;
using UnityEngine;
using System;

    public class EnemyManager 
    {
        public event Action<Transform> OnEnemySpawned;
        public event Action<Transform> OnEnemyRemoved;
        private Transform _enemiesParent;
        private List<Enemy> _enemies = new List<Enemy>();

        public EnemyManager(Transform enemiesParent)
        {
            _enemiesParent = enemiesParent;
        }

        public void BindHealthBars(HealthBarManager manager)
        {
            for (var i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].SetHealthBar(manager);
            }        
        }

        public void BeginMove()
        {
            for (var i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].States();
            }
        }

        public void SetEnemiesOnPositions(Vector3[] positions, EnemyInfo[] enemies, Transform player)
        {
            if(positions.Length > enemies.Length)
            {
                enemies = GrowEnemiesLength(enemies, positions.Length);
            }
            for (var i = 0; i < positions.Length; i++)
            {
                SpawnEnemy(positions[i], enemies[i]);    
            }
        }

        public void SetRandomEnemiesOnPositions(List<Vector3> positions, EnemyInfo[] enemies, Transform player)
        {
            for (var i = 0; i < positions.Count; i++)
            {
                SpawnEnemy(positions[i], enemies[UnityEngine.Random.Range(0,enemies.Length)]);    
            }
        SetTarget(_enemies, player);
        }

        private void SetTarget(List<Enemy> enemies, Transform target)
        {
            for (var i = 0; i < enemies.Count; i++)
            {
                enemies[i].SetTarget(target);
            }
        }

        private void SpawnEnemy(Vector3 pos, EnemyInfo info)
        {
            var enemy = GameObject.Instantiate(info.Prefab, pos, Quaternion.identity, _enemiesParent).
            GetComponent<Enemy>();
            enemy.SetStatsFromInfo(info);
            enemy.OnDeath += x => OnEnemyRemoved?.Invoke(x.transform);
            _enemies.Add(enemy);
            OnEnemySpawned?.Invoke(enemy.transform);
        }
        
        private EnemyInfo[] GrowEnemiesLength(EnemyInfo[] enemies, int length)
        {
            var prevLength = enemies.Length;
            Array.Resize<EnemyInfo>(ref enemies, length);
            for (var i = prevLength - 1; i < length; i++)
            {
             enemies[i] = enemies[i % prevLength];   
            }
            return enemies;
        }
}
