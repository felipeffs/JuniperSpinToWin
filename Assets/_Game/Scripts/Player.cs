using UnityEngine;

namespace JuniperSpinToWin
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _rotationSpeed = 90;

        [Header("Shoot")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _muzzlePoint;
        [SerializeField] private float _knockbackForce = 2;

        private InputSystem_Actions _inputSystemActions;
        private float _fireRate;
        private float _lastShootTime;

        private void Awake()
        {
            _inputSystemActions = new InputSystem_Actions();
            _inputSystemActions.Player.Enable();
        }

        private void OnDestroy()
        {
            _inputSystemActions.Dispose();
        }

        private void Update()
        {
            var wasShootPressed = _inputSystemActions.Player.Attack.WasPressedThisDynamicUpdate();

            if (wasShootPressed)
            {
                Shoot();
            }
        }

        private void FixedUpdate()
        {
            Spin(Time.deltaTime);
        }

        private void Shoot()
        {
            float elapsedTime = Time.time - _lastShootTime;

            if (elapsedTime >= _fireRate)
            {
                _lastShootTime = Time.time;
                var rotQuat = Quaternion.Euler(0, 0, _rb.rotation);
                Instantiate(_bulletPrefab, _muzzlePoint.position, rotQuat);

                var knockbackForceDir = -transform.up * _knockbackForce;
                _rb.AddForce(knockbackForceDir, ForceMode2D.Impulse);
            }
        }

        private void Spin(float deltaTime)
        {
            var rotation = _rb.rotation - _rotationSpeed * deltaTime;
            _rb.MoveRotation(rotation);
        }
    }
}
