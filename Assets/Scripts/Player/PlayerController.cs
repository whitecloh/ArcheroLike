using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    internal class PlayerController : MonoBehaviour ,IDamagable
    {
        [SerializeField] float _moveSpeed = 300;
        private Rigidbody _rigidbody;
        private MovementInput _input;
        private Vector3 _moveDirection;
        private HealthPoints _hp;
    //---------------- Initialize ---------------------
    internal void Init(MovementInput input,float healths)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            _input = input;
            _hp = new HealthPoints(healths);
        }
    //---------------- HP ---------------------
    public void GetDamage(float value)
    {
        _hp.ChangeHP(-value);
    }
    internal void BindHealthBar(HealthBarManager manager)
    {
        var healthBar = manager.GetHealthBarForTarget(transform);
        _hp.OnChange += healthBar.ChangeHealth;
        _hp.OnNoHP += () => manager.RemoveHealthBar(transform);
        _hp.OnNoHP += SelfDestroy;
    }
    private void SelfDestroy()
    {
        Debug.Log("Dead");
    }

    private void Update()
    {
        _moveDirection = _input.GetMovement().normalized * _moveSpeed;
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveDirection * Time.deltaTime;
        if (_moveDirection != Vector3.zero)
        {
            _rigidbody.transform.rotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        }
    }
}
