using UnityEngine;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// This class is used to control the projectile in the game.
    /// </summary>

    public class ProjectileController : MonoBehaviour
    {
        Rigidbody2D _rigidbody2D;

        [SerializeField]
        float _speed;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.linearVelocity = transform.up * _speed;
        }
    }
}
