using System.Collections.Generic;
using StansAssets.Foundation.Patterns;
using UnityEngine;

namespace StansAssets.ProjectSample.Core
{
    public class GameObjectsPool : IPoolingService
    {
        readonly GameObject m_Root;
        readonly Dictionary<GameObject, ObjectPool<GameObject>> m_ActivePool = new Dictionary<GameObject, ObjectPool<GameObject>>();

        public GameObjectsPool(string name)
        {
            m_Root = new GameObject(name);
            Object.DontDestroyOnLoad(m_Root);
        }

        public GameObject Instantiate(GameObject origin)
        {
            return Instantiate(origin, Vector3.one, Quaternion.identity);
        }

        public GameObject Instantiate(GameObject origin, Vector3 position, Quaternion rotation)
        {
            var poolable = Instantiate<PoolableGameObject>(origin);
            var transform = poolable.transform;
            transform.position = position;
            transform.rotation = rotation;
            return poolable.gameObject;
        }

        public T Instantiate<T>(GameObject origin) where T : PoolableGameObject
        {
            var pool = GetPool(origin);
            var gameObject = pool.Get();
            var poolableObject = gameObject.GetComponent<PoolableGameObject>();
            gameObject.SetActive(true);
            poolableObject.Init();
            poolableObject.SetOnReleaseCallback(() =>
            {
                Release(origin, gameObject);
            });
            return (T)poolableObject;
        }

        void Release(GameObject origin, GameObject instance)
        {
            var pool = GetPool(origin);
            instance.SetActive(false);
            pool.Release(instance);
        }

        ObjectPool<GameObject> GetPool(GameObject origin)
        {
            if (m_ActivePool.ContainsKey(origin))
            {
                return m_ActivePool[origin];
            }

            var pool = MakePool(origin);
            m_ActivePool.Add(origin, pool);
            return pool;
        }

        ObjectPool<GameObject> MakePool(GameObject origin)
        {
            var pool = new ObjectPool<GameObject>(() => Object.Instantiate(origin),
                gameObject =>
                {
                    gameObject.SetActive(true);
                    gameObject.transform.SetParent(m_Root.transform);
                }, gameObject =>
                {
                    gameObject.SetActive(false);
                    gameObject.transform.SetParent(m_Root.transform);
                });

            return pool;
        }
    }
}
