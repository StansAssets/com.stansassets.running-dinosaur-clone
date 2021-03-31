using System.Collections;
using System.Collections.Generic;
using StansAssets.ProjectSample.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StansAssets.ProjectSample.Boxes
{
    class PoolableGameObjectsSpawner : MonoBehaviour
    {
        [SerializeField, Range(1, 5)]
        float m_SpawnRange = 1f;

        [SerializeField, Range(0.1f, 5f)]
        float m_SpawnRateMin = 1f;

        [SerializeField, Range(5f, 10f)]
        float m_SpawnRateMax = 1f;

        [SerializeField]
        List<PoolableGameObject> m_ObjetsToSpawn = new List<PoolableGameObject>();

        protected IEnumerator SpawnLoop()
        {
            while (true) {
                yield return new WaitForSeconds(GetNextSpawnDelay ());
                Spawn();
            }
        }

        [ContextMenu("Test Spawn")]
        protected virtual PoolableGameObject Spawn()
        {
            var spawn = App.Services.Get<IPoolingService>()
                .Instantiate<PoolableGameObject>(GetNextObjectToSpawn ().gameObject);

            spawn.transform.position = transform.position;
            return spawn;
        }

        protected PoolableGameObject GetNextObjectToSpawn () {
            return m_ObjetsToSpawn[Random.Range(0, m_ObjetsToSpawn.Count - 1)];
        }

        protected float GetNextSpawnDelay () {
            return GetRandomDelay ();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            var position = transform.position;
            var from = position;
            from.x -= m_SpawnRange;

            var to = position;
            to.x += m_SpawnRange;

            Gizmos.DrawLine(from, to);
        }

        protected void DestroyComponent<T>() where T : Component
        {
            var component = GetComponent<T>();
            if (component != null)
                Destroy(component);
        }

        protected float GetRandomDelay () {
            return Random.Range (m_SpawnRateMin, m_SpawnRateMax);
        }
    }
}
