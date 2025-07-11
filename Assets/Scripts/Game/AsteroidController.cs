using UnityEngine;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// This class is used to control the asteroid in the game.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class AsteroidController : MonoBehaviour
    {
        const float _MIN_ROTATION_SPEED = 25f;
        const float _MAX_ROTATION_SPEED = 75f;
        float _rotationSpeed;

        [SerializeField]
        [Tooltip("Maximum health of the asteroid.")]
        float _maxHealth;
        float _currentHealth;

        [SerializeField]
        Sprite[] _spriteStates;
        
        [SerializeField]
        [Tooltip("Damage dealt to the player on collision.")]
        float _playerCollisionDamage;
        public float PlayerCollisionDamage => _playerCollisionDamage;

        protected void Awake()
        {
            // Get a random rotation speed for the asteroid.
            _rotationSpeed = Random.Range(_MIN_ROTATION_SPEED, _MAX_ROTATION_SPEED);

            // Get a random direction for the rotation.
            if (Random.value < 0.5f)
            {
                _rotationSpeed = -_rotationSpeed;
            }

            // Set the health.
            _currentHealth = _maxHealth;

            // Set the initial sprite state.
            SetSpriteStateActive(0);
        }

        protected void Update()
        {
            // Smoothly rotate the asteroid.
            transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                ProjectileController projectile = collision.gameObject.GetComponent<ProjectileController>();

                _currentHealth -= projectile.Damage;
                Destroy(collision.gameObject);

                // Currently hardcoded, needs to be changed to use a more dynamic approach.
                if (_currentHealth > 66f && _currentHealth <= 99f)
                {
                    SetSpriteStateActive(1);
                }
                else if (_currentHealth > 33 && _currentHealth <= 66f)
                {
                    SetSpriteStateActive(2);
                }
                else if (_currentHealth <= 33)
                {
                    SetSpriteStateActive(3);
                }
                if (_currentHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// Sets the sprite state based on the index provided.
        /// </summary>
        /// <param name="index"></param>
        void SetSpriteStateActive(int index)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _spriteStates[index];
        }
    }
}