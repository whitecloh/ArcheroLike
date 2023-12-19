using UnityEngine;

    public interface IMove 
    {
        float Speed { get; set; }
        Transform Enemy { set; }
        void Move();    
        void SetTarget(Transform target);
        void SetTransform(Transform transform);
    }