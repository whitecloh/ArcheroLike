using System.Collections.Generic;
using UnityEngine;

    public class HealthBarManager : MonoBehaviour
    {
        [SerializeField] GameObject _healthBarPrefab;
        [SerializeField] Vector3 _deafaultOffset;
        private Dictionary<Transform,HealthBar> _healthBars = new Dictionary<Transform, HealthBar>();

        public void AddHealthBar(Transform target)
        {
            if(!_healthBars.ContainsKey(target))
            {
                var newHealthBar = Instantiate(_healthBarPrefab, transform).GetComponent<HealthBar>();
                _healthBars.Add(target, newHealthBar);
            }
        }
        public HealthBar GetHealthBarForTarget(Transform target)
        {
            HealthBar result = null;

            if(_healthBars.ContainsKey(target))
            {
                result = _healthBars[target];
            }
            return result;
        }
        public void RemoveHealthBar(Transform target)
        {
            if(_healthBars.ContainsKey(target))
            {
                var healthbar = _healthBars[target].gameObject;
                _healthBars.Remove(target);
                Destroy(healthbar);
            }
            else
            {
                Debug.LogError($"Key {target} not found!");
            }
        }
        
        private void LateUpdate() 
        {
            foreach (var item in _healthBars)
            {
                item.Value.transform.position =  item.Key.position + _deafaultOffset;
            }
        }
    }
