using UnityEngine;


    internal class GameLogics : MonoBehaviour
    {
        [SerializeField] GameObject[] _rooms;
        [SerializeField] HeroInfo _hero;
        [SerializeField] EnemyInfo[] _enemies;
        [SerializeField] MissileData _weapon;
        [SerializeField] HealthBarManager _healthBarManager;
        [SerializeField] Joystick _joystick;
        [SerializeField] UIManager _uiManager;
        private RoomManager _roomManager;
        private PlayerManager _playerManager;
        private EnemyManager _enemyManager;
        private CameraFollow _playerFolow;
        private TargetManager _targetManager;

        private void Start() 
        {
            _playerFolow = CameraFollow.Instance;
            
            _targetManager = new TargetManager();

            _roomManager = new RoomManager();

            _uiManager.Init();

            _roomManager.OnRoomLoaded += SetPlayer;
            _roomManager.OnRoomLoaded += SetEnemies;

            _roomManager.LoadRoom(_rooms[0]);
        }

        private void SetPlayer(RoomInfo room)
        {
            _playerManager = new PlayerManager();
            _playerManager.SetHero(_hero, room.PlayerSpawnPosition,_joystick);
            _playerManager.SetWeapon(_weapon);
            _healthBarManager.AddHealthBar(_playerManager.GetHeroGO().transform);
            _playerManager.SetHealthBar(_healthBarManager);
            _playerFolow.SetTarget(_playerManager.GetHeroGO().transform);
            _targetManager.SetPlayer(_playerManager.GetHeroGO().transform);
            _playerManager.SubscribeToInputEnd(() => _playerManager.SetTarget(_targetManager.GetClosestEnemy()));
        }
        private void SetEnemies(RoomInfo room)
        {
            _enemyManager = new EnemyManager(new GameObject("Enemies").transform);
            _enemyManager.OnEnemySpawned += _targetManager.AddEnemy;
            _enemyManager.OnEnemySpawned += _healthBarManager.AddHealthBar;
            _enemyManager.OnEnemyRemoved += _targetManager.RemoveEnemy;
            _enemyManager.OnEnemyRemoved += x => _playerManager.SetTarget(_targetManager.GetClosestEnemy());
            _enemyManager.OnEnemyRemoved += x => _uiManager.AddCoins(1);
        var hero = _playerManager.GetHeroGO().transform;
            _enemyManager.SetRandomEnemiesOnPositions(room.EnemySpawnPositions, _enemies, hero);
            _enemyManager.BindHealthBars(_healthBarManager);
            _enemyManager.BeginMove();
        }
    }
