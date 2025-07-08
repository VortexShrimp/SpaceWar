using UnityEngine;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// This class is used to control the player in the game.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        Rigidbody2D _rigidbody2D;
        Vector2 _movement;

        [SerializeField]
        float _moveSpeed;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // Get input in update to ensure smooth movement.
            _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // Rotate the player to face the mouse cursor.
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
            transform.up = direction;
        }

        void FixedUpdate()
        {
            // Move the player in FixedUpdate for physics consistency.
            _rigidbody2D.AddForce(_movement * _moveSpeed * Time.fixedDeltaTime);
        }
    }
}
