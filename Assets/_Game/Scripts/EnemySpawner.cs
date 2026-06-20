using System.Collections.Generic;
using UnityEngine;

namespace JuniperSpinToWin
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private int _maxEnemiesAlive = 10;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private List<Transform> _spawnPoints = new();
        private int _currentIndex = -1;

        [SerializeField] private float _maxSpawnDelay = 1;
        [SerializeField] private float _minSpawnDelay = 0.35f;

        private float _nextSpawnTime;
        private float _lastSpawnTime;

        protected void Update()
        {
            if (!_player) return;

            var elapsedTime = Time.time - _lastSpawnTime;
            if (elapsedTime >= _nextSpawnTime)
            {
                SpawnEnemy();
                _lastSpawnTime = Time.time;
                _nextSpawnTime = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            }
        }

        private void SpawnEnemy()
        {
            if (_maxEnemiesAlive <= Enemy.Count) return;

            _currentIndex = Random.Range(0, _spawnPoints.Count);

            var spawnPoint = _spawnPoints[_currentIndex];
            var enemy = Instantiate(_enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            enemy.Init(_player);
        }
    }
}