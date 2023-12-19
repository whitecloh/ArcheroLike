using UnityEngine;

    public class CameraFollow : MonoBehaviour 
    {
        public static CameraFollow Instance { get; private set; }
        [SerializeField] Vector3 _camOffset = Vector3.zero;
        [SerializeField] float _smoothTime;
        private Transform _target;
        private Vector3 _velocity;


        private void Awake()
        {
            if( Instance == null )
            {
                Instance = this;
            }
        }

        private void Start() 
        {
            if(_target)
            {
                transform.LookAt(_target.position, Vector3.up);
            }
        }

        private void LateUpdate()
        {
            if(_target)
            {
                var newPos = _target.position + _camOffset;
                newPos.z = Mathf.Clamp(newPos.z,-10,10);
                newPos.x = Mathf.Clamp(newPos.x,-5,5);
                transform.position = Vector3.SmoothDamp(transform.position, newPos, ref _velocity, _smoothTime,float.MaxValue,Time.deltaTime);
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }    
