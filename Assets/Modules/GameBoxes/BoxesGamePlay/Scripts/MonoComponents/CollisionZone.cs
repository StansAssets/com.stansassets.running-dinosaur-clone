using System;
using UnityEngine;

namespace StansAssets.ProjectSample.Boxes
{
    [RequireComponent(typeof(BoxCollider2D))]
    abstract class CollisionZone<T> : MonoBehaviour
    {
        public event Action<T> OnCollision;
        protected abstract Color GizmoColor { get; }

        void OnCollisionEnter2D(Collision2D other)
        {
            var component = other.gameObject.GetComponent<T>();
            if (component == null)
            {
                return;
            }

            OnCollision?.Invoke(component);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawWireCube(transform.position,
                transform.GetComponent<Collider2D>().bounds.size);
        }
    }
}
