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

        [SerializeField]
        float _damage;

        public float Damage
        {
            get { return _damage; }
        }

        protected void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            // Send the projectile in the direction the player is facing.
            // This assumes the projectile is instantiated at the player's position and rotation.
            _rigidbody2D.linearVelocity = transform.up * _speed;
        }
    }
}
