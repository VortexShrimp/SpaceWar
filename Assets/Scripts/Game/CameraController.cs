using UnityEngine;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// This class is used to control the camera in the game.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// The player Transform that the camera will follow.
        /// </summary>
        [SerializeField]
        [Tooltip("The player Transform that the camera will follow.")]
        Transform _player;

        /// <summary>
        /// The camera component attached to this GameObject.
        /// This is used to control the camera's zoom and other things.
        /// </summary>
        Camera _camera;

        const float _MIN_ZOOM = 5f;
        const float _MAX_ZOOM = 15f;

        protected void Awake()
        {
            _camera = GetComponent<Camera>();

            // Set the initial zoom level to the midpoint between MIN_ZOOM and MAX_ZOOM.
            _camera.orthographicSize = (_MIN_ZOOM + _MAX_ZOOM) * 0.5f;
        }

        protected void Update()
        {
            // Lock the camera to the player's position.
            transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);

            // Zoom the camera in and out based on the mouse scroll wheel.
            HandleMouseZoom();
        }

        /// <summary>
        /// Handles zooming the camera in and out using the mouse scroll wheel.
        /// </summary>
        void HandleMouseZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            _camera.orthographicSize -= scroll;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _MIN_ZOOM, _MAX_ZOOM);
        }
    }
}
