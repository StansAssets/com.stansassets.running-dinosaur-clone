using UnityEngine;

namespace SA.CrossPlatform.Samples
{
    public class UM_ObjectRotation : MonoBehaviour
    {
        [SerializeField]
        float m_RotationSpeed = default;

        void Update()
        {
            transform.Rotate(Vector3.back * m_RotationSpeed);
        }
    }
}
