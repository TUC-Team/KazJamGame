using System;
using UnityEngine;

namespace Assets.FPS.Scripts
{
    public class Pushable : MonoBehaviour
    {
        [Tooltip("Multiplier to apply to the received pushing force")]
        public float forceMultiplier = 1f;

        private Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        internal void Push(Vector3 forced)
        {
            var resultForce = forced * forceMultiplier;
            if (_animator != null)
            {
                _animator.SetTrigger("pushed");
            }

            transform.position += resultForce;
        }
    }
}
