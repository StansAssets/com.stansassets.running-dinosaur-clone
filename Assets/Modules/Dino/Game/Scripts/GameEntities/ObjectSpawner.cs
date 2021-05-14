using System.Collections.Generic;
using System.Linq;
using StansAssets.Foundation.Patterns;
using UnityEngine;

namespace StansAssets.Dino.Game
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject m_ExampleObject;
        [SerializeField] float m_RequiredSpeed;
        [SerializeField] float m_MinFramesGap = 50;
        [SerializeField] RectTransform m_backToPoolPosition;

        int ReleasePositionX => (int)m_backToPoolPosition.anchoredPosition.x;

        PrefabPool m_Pool;
        readonly List<GameObject> m_ActiveObjects = new List<GameObject>();

        Vector3 GetSpawnPosition() => transform.position;
        // min speed required to spawn this type of obstacles
        internal float RequiredSpeed => m_RequiredSpeed;
        // min space after the obstacle (in score points)
        internal float RequiredSpace => m_MinFramesGap;

        void Start()
        {
            m_ExampleObject.SetActive(false);
            m_Pool = new PrefabPool(m_ExampleObject, 5);
        }

        void FixedUpdate()
        {
            var toRemove = m_ActiveObjects.Where(ShouldReleaseObject).ToList();
            foreach (var obj in toRemove) {
                m_Pool.Release(obj);
                m_ActiveObjects.Remove(obj);
                // Move object back under Spawner
                obj.transform.SetParent(transform);
            }
        }

        bool ShouldReleaseObject(GameObject obj)
        {
            float positionX = obj.transform.position.x;
            return positionX < ReleasePositionX;
        }

        public void Reset()
        {
            foreach (var obj in m_ActiveObjects)
            {
                m_Pool.Release(obj);
            }

            m_ActiveObjects.Clear();
            m_Pool?.Clear();
        }

        public GameObject GetObject()
        {
            var result = m_Pool.Get();
            m_ActiveObjects.Add(result);
            result.SetActive(true);
            result.transform.SetPositionAndRotation(GetSpawnPosition(), Quaternion.identity);
            return result;
        }
    }
}
