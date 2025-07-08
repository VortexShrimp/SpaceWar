using UnityEngine;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// This class is used to control the camera in the game.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        /// <summary>
        /// The player Transform that the camera will follow.
        /// </summary>
        Transform _player;

        /// <summary>
        /// The camera component attached to this GameObject.
        /// This is used to control the camera's zoom.
        /// </summary>
        Camera _camera;

        const float MIN_ZOOM = 5f;
        const float MAX_ZOOM = 15f;

        protected void Awake()
        {
            _camera = GetComponent<Camera>();

            // Set the initial zoom level to the midpoint between MIN_ZOOM and MAX_ZOOM.
            _camera.orthographicSize = (MIN_ZOOM + MAX_ZOOM) * 0.5f;
        }

        protected void Update()
        {
            // Lock the camera to the player's position.
            transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);

            // Zoom the camera in and out based on the mouse scroll wheel.
            UpdateZoom();
        }

        void UpdateZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            _camera.orthographicSize -= scroll;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, MIN_ZOOM, MAX_ZOOM);
        }
    }
}
