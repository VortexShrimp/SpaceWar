using UnityEngine;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// This class is used to control the camera in the game.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        Transform _player;

        void Update()
        {
            transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
        }
    }
}
