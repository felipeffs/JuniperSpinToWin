using UnityEngine;

namespace JuniperSpinToWin
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _rotationSpeed = 90;

        private float _rotation;

        void Start()
        {
            _rotation = _rb.rotation;
        }

        void Update()
        {
            Spin(Time.deltaTime);
        }

        private void Spin(float deltaTime)
        {
            _rotation += _rotationSpeed * deltaTime;
            _rb.MoveRotation(_rotation);
        }
    }
}
