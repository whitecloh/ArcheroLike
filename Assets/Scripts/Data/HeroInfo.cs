using UnityEngine;

    [CreateAssetMenu(fileName = "HeroInfo", menuName = "Archero/HeroInfo")]
    public class HeroInfo : ScriptableObject 
    {
        public GameObject Prefab;
        public float MaxHP;
    }