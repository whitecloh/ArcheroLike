using System;
using System.Collections;
using UnityEngine;

    public class PlayerManager
    {
        private GameObject _heroGO;
        private MovementInput _input;
        private MissileData _missile;
        private MissileSpawner _missileSpawner = new MissileSpawner();
        private MonoBehaviour _heroController;
        private Transform _attackTarget;
        private Coroutine _attackCoroutine;
        private bool _isAttack;
        private float _hp;

        public GameObject GetHeroGO()
        {
            return _heroGO;
        }

    //------------------ Initialize -------------------------
        public void SetHero(HeroInfo hero , Vector3 position , Joystick joystick)
        {
            if(hero.Prefab != null)
            {
                _heroGO = GameObject.Instantiate(hero.Prefab,position,Quaternion.identity);
                _hp = hero.MaxHP;
                InitController(_heroGO,joystick);
            }
        }
        public void SetHealthBar(HealthBarManager hpBar)
    {
        _heroGO.GetComponent<PlayerController>().BindHealthBar(hpBar);
    }
        public void SetWeapon(MissileData missile)
        {
            _missile = missile;
            _missileSpawner.SetWeapon(_missile);
            _attackCoroutine = _heroController.StartCoroutine(AttackCoroutine());
    }
        public void SetTarget(Transform targetTransform)
    {
        _attackTarget = targetTransform;
    }
        private void InitController(GameObject hero, Joystick joystick)
    {
        var controller = _heroGO.GetComponent<PlayerController>();
        if (_input == null)
        {
            InitInput();
        }
        _input.SetInput(joystick);
        _input.ClearEvents();
        controller.Init(_input, _hp);
        _heroController = controller;
        SubscribeOnInput();
    }
        private void InitInput()
    {
        _input = new MovementInput();
        _input.OnInputBegin += () => { };
        _input.OnInputEnded += () => { };
    }
        public void RemoveHero()
        {
            if(_heroGO != null)
            {
                GameObject.Destroy(_heroGO);
            }
        }
    //------------------ SubscribesToInput -------------------------
        public void SubscribeToInputBegin(Action action)
        {
            _input.OnInputBegin += action;
        }    
        public void SubscribeToInputEnd(Action action)
        {
            _input.OnInputEnded += action;
        }    
        private void SubscribeOnInput()
        {
            _input.OnInputEnded += StartAttack;
            _input.OnInputBegin += EndAttack;
        }
    //------------------ Attack -------------------------
        private void StartAttack()
        {
        _isAttack = true;
        }
        private void EndAttack()
        {
        _isAttack = false;
        }

        private IEnumerator AttackCoroutine()
        {
        var waitTime = new WaitForSeconds(1 / _missileSpawner.CurrentWeapon.AttackSpeed);
        while (_heroController != null)
            {
            if(_attackTarget != null&&_isAttack)
                {
                _heroController.transform.LookAt(_attackTarget);
                Attack();
                yield return waitTime;
                }
            yield return null;
        }
        }

        private void Attack()
    {
        var missile = _missileSpawner.GetMissile();
        missile.transform.position = _heroController.transform.position;
        missile.Init(_missileSpawner.CurrentWeapon, _attackTarget);
    }
}
