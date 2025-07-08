using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// This class is used to control the asteroid in the game.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class AsteroidController : MonoBehaviour
    {
        [SerializeField]
        float _rotationSpeed;

        void Update()
        {
            // Smoothly rotate the asteroid.
            transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}