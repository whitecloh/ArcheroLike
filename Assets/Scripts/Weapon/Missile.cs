using UnityEngine;

    public class Missile : MonoBehaviour 
    {
        private Rigidbody _rigidbody;
        private MissileData _data;
        
        public void Init(MissileData data, Transform target)
        {
            _rigidbody = GetComponent<Rigidbody>();
            transform.LookAt(target);
            var direction = target.position - transform.position;
        _data = data;
        _rigidbody.velocity = direction.normalized * _data.FlightSpeed;
        }

        private void OnCollisionEnter(Collision other) 
        {
            var damagableObject = other.gameObject.GetComponent<IDamagable>(); 
            if(damagableObject != null)
            {
                damagableObject.GetDamage(_data.Damage);
            }
            gameObject.SetActive(false);
        }
    }
