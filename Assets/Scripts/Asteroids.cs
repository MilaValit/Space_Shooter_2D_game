using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Asteroids : Destructible
    {
        public enum Size
        {
            Small,
            Normal,
            Big
        }

        [SerializeField] private Size size;
        [SerializeField] private float spawnUpForce;
        [SerializeField] private float verticalSpeed;
        [SerializeField] private float horizontalSpeed;
        [SerializeField] private float limit;

        private Vector3 velocity;

        private void Awake()
        {
            EventOnDeath.AddListener(OnAsteroidDestroyed);

            SetSize(size);
        }

        /*private void OnDestroy()
        {
            EventOnDeath.RemoveListener(OnAsteroidDestroyed);
        }*/
        private void Update()
        {
            Move();
        }

        public void SpawmAsteroids()
        {
            for (int i = 0; i < 3; i++)
            {
                Asteroids asteroid = Instantiate(this, transform.position, Quaternion.identity);
                asteroid.SetSize(size - 1);
                //asteroid.AddVerticalVelocity(spawnUpForce);
                asteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                asteroid.SetHorizontalDirection((i % 2 * 2) - 1);
                asteroid.SetVerticalDirection((i % 2 * 2) - 1);
            }
        }

        private void Move()
        {
            if (velocity.y >= limit || velocity.y <= -limit)
            {
                verticalSpeed *= -1;
            }

            if (velocity.x >= limit || velocity.x <= -limit)
            {
                horizontalSpeed *= -1;
            }

            velocity.y -= verticalSpeed * Time.deltaTime;
            velocity.x = horizontalSpeed * Time.deltaTime;

            transform.position += velocity * Time.deltaTime;
        }

        public void SetSize(Size size)
        {
            if (size < 0) return;

            transform.localScale = GetVectorFromSize(size);
            this.size = size;
        }
        private Vector3 GetVectorFromSize(Size size)
        {
            if (size == Size.Big) return new Vector3(1, 1, 1);
            if (size == Size.Normal) return new Vector3(0.6f, 0.6f, 0.6f);
            if (size == Size.Small) return new Vector3(0.4f, 0.4f, 0.4f);           

            return Vector3.one;
        }

        public void AddVerticalVelocity(float velocity)
        {
            this.velocity.y += velocity;
        }

        public void SetHorizontalDirection(float direction)
        {
            velocity.x = Mathf.Sign(direction) * horizontalSpeed;
        }

        public void SetVerticalDirection(float direction)
        {
            velocity.y = Mathf.Sign(direction) * verticalSpeed;
        }

        private void OnAsteroidDestroyed()
        {
            if (size != Size.Small)
            {
                SpawmAsteroids();                
            }
            Destroy(gameObject);
        }
    }
}