using UnityEngine;

namespace JuniperSpinToWin
{
    public class Enemy : MonoBehaviour
    {
        public static int Count;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float speed;
        [SerializeField] private Health _health;
        [SerializeField] private float damage = 1;

        private Player _player;

        void Start()
        {
            Count += 1;
        }

        private void FixedUpdate()
        {
            if (!_player) return;
            var newPos = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.fixedDeltaTime);
            _rb.MovePosition(newPos);
        }

        void OnEnable()
        {
            _health.OnDeath += Health_OnDeath;
        }
        void OnDisable()
        {
            _health.OnDeath -= Health_OnDeath;
        }

        void OnDestroy()
        {
            Count -= 1;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        public void Init(Player player)
        {
            _player = player;
        }

        private void Health_OnDeath()
        {
            Destroy(gameObject);
        }
    }
}