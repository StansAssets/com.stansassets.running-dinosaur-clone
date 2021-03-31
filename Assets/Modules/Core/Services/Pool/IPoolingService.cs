using UnityEngine;

namespace StansAssets.ProjectSample.Core
{
    public interface IPoolingService
    {
        GameObject Instantiate(GameObject origin);
        GameObject Instantiate(GameObject origin, Vector3 position, Quaternion rotation);
        T Instantiate<T>(GameObject origin) where T : PoolableGameObject;
    }
}
