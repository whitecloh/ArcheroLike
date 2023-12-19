using System.Collections.Generic;
using UnityEngine;

    public class MissileSpawner 
    {
        private const int POOL_INCREASE_SIZE = 10;
        public MissileData CurrentWeapon => _currentWeapon;
        private MissileData _currentWeapon;
        private List<GameObject> _missilePool;
        private Transform _parent;

        public void SetWeapon(MissileData missile)
        {
         _currentWeapon = missile;
        if (_missilePool == null || _missilePool.Count > 0)
        {
            PopulateMissilePool();
        }
        }

        public Missile GetMissile()
        {
        for(int i=0;i<_missilePool.Count;i++)
        {
            if (!_missilePool[i].activeSelf)
            {
                _missilePool[i].SetActive(true);
                return _missilePool[i].GetComponent<Missile>();
            }
        }
        return null;
        }

        public void HideMissile(GameObject missile)
        {
            missile.SetActive(false);
            _missilePool.Remove(missile);
        }

        private void PopulateMissilePool()
        {
            if (_missilePool == null)
            {
                _missilePool = new List<GameObject>();
            }
            for(var i = 0; i < POOL_INCREASE_SIZE; i++)
            {
                _missilePool.Add(InstantiateNewMissile());
            }
        }

        private GameObject InstantiateNewMissile()
        {
            var result = GameObject.Instantiate(_currentWeapon.MissilePrefab,_parent);
            result.SetActive(false);
            return result;
        }

        private void AddModifier(IModifier modifier)
        {
            
        }
    }