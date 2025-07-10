using UnityEngine;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// This class is used to control the player in the game.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Movement speed of the player.")]
        float _accelaration;

        [SerializeField]
        [Tooltip("Maximum speed of the player.")]
        [Range(0f, 100f)]
        float _maxSpeed;

        [SerializeField]
        [Tooltip("Maximum health of the player.")]
        float _maxHealth;
        float _currentHealth;

        [SerializeField]
        [Tooltip("Projectile prefab to be instantiated.")]
        GameObject _projectilePrefab;

        /// <summary>
        /// Flag to determine if the player can take collision damage.
        /// This is used to prevent multiple collisions from taking damage in a short time.
        /// </summary>
        bool _canTakeCollisionDamage;

        /// <summary>
        /// Cooldown time for taking collision damage.
        /// Time inbetween collisions where the player cannot take damage.
        /// </summary>
        const float _COLLISION_DAMAGE_COOLDOWN = 1f;

        Rigidbody2D _rigidbody2D;
        Vector2 _movement;

        protected void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            // Initialize health.
            _currentHealth = _maxHealth;
            _canTakeCollisionDamage = true;
        }

        protected void Update()
        {
            // Get input in update to ensure smooth movement.
            _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // Rotate the player to face the mouse cursor.
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
            transform.up = direction;

            if (Input.GetKeyDown(KeyCode.Mouse0) == true)
            {
                var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
                Destroy(projectile, 10f); // Destroy the projectile after 10 seconds to prevent clutter.
            }
        }

        protected void FixedUpdate()
        {
            // Move the player in FixedUpdate for physics consistency.
            _rigidbody2D.AddForce(_movement * _accelaration * Time.fixedDeltaTime, ForceMode2D.Force);

            // Clamp the player's velocity to prevent excessive speed.
            if (_rigidbody2D.linearVelocity.magnitude > _maxSpeed)
            {
                _rigidbody2D.linearVelocity = _rigidbody2D.linearVelocity.normalized * _maxSpeed;
            }
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Asteroid"))
            {
                if (_canTakeCollisionDamage == false)
                {
                    return;
                }

                AsteroidController asteroid = collision.gameObject.GetComponent<AsteroidController>();
                _currentHealth -= asteroid.PlayerCollisionDamage;

                // Start cooldown for collision damage.
                _canTakeCollisionDamage = false;
                Invoke(nameof(ResetCollisionDamageCooldown), _COLLISION_DAMAGE_COOLDOWN);

                if (_currentHealth <= 0f)
                {
                    // HACK: Need to implement a game over state or respawn logic.
                    Destroy(gameObject);
                }
            }
        }

        void ResetCollisionDamageCooldown()
        {
            _canTakeCollisionDamage = true;
        }
    }
}
