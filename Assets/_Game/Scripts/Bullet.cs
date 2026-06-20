using UnityEngine;

namespace JuniperSpinToWin
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _damage = 1;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _lifetime = 2f;
        private float _timer;

        public void Start()
        {
            _rb.linearVelocity = _rb.transform.up * _speed;
        }

        void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _lifetime)
                Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) return;

            if (collision.transform.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
            }

            Destroy(gameObject);
        }
    }
}
