using System;
using UnityEngine;

namespace JuniperSpinToWin
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth;
        private float _health;

        public event Action OnDeath;
        public event Action WhenTakingDamage;

        [SerializeField] private float _invencibilityDuration = .25f;
        private float _lastDamageTime;

        private void Start()
        {
            _health = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (!CanTakeDamage()) return;

            float absDamage = Mathf.Abs(damage);
            var newHealth = Mathf.Max(0, _health - damage);

            if (newHealth == _health) return;

            _health = newHealth;

            WhenTakingDamage?.Invoke();

            if (_health <= 0)
                OnDeath?.Invoke();

            _lastDamageTime = Time.time;
        }

        private bool CanTakeDamage()
        {
            var elapsedTime = Time.time - _lastDamageTime;
            if (elapsedTime <= _invencibilityDuration) return false;
            return true;
        }
    }
}
