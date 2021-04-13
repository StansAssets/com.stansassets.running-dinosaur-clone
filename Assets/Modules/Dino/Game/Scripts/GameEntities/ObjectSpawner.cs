using System.Collections.Generic;
using System.Linq;
using StansAssets.Foundation.Patterns;
using UnityEngine;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class ObjectSpawner : ScreenSizeDependent
    {
        [SerializeField] GameObject m_ExampleObject;
        [SerializeField] int m_MinPositionX = -1000;
        [SerializeField] float m_RequiredSpeed;
        [SerializeField] float m_MinFramesGap = 50;

        PrefabPool m_Pool;
        readonly List<GameObject> m_ActiveObjects = new List<GameObject>();

        protected Vector3 GetSpawnPosition() => transform.position;
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
            var toRemove = m_ActiveObjects.Where(OutOfValidRange).ToList();
            foreach (var obj in toRemove) {
                m_Pool.Release(obj);
                m_ActiveObjects.Remove(obj);
            }
        }

        bool OutOfValidRange(GameObject obj)
        {
            float positionX = obj.transform.position.x;
            return positionX < m_MinPositionX || positionX > 2500;
        }

        public void Reset()
        {
            foreach (var obj in m_ActiveObjects)
                m_Pool.Release(obj);

            m_ActiveObjects.Clear();
            m_Pool.Clear();
        }

        public GameObject GetObject()
        {
            var result = m_Pool.Get();
            m_ActiveObjects.Add(result);
            result.SetActive(true);
            result.transform.SetPositionAndRotation(GetSpawnPosition(), Quaternion.identity);
            return result;
        }

        public override void UpdateScreenWidth(int screenWidthDelta)
        {
            // current implementation requires no resizing
        }
    }
}
