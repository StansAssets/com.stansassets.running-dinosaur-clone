using System;
using UnityEngine;

namespace SA.CrossPlatform.App
{
    [Serializable]
    abstract class UM_AbstractContact
    {
        [field: SerializeField]
        public string Name { get; protected set; }
        
        [field: SerializeField]
        public string Phone { get; protected set; }
        
        [field: SerializeField]
        public string Email { get; protected set; }
        
        [field: SerializeField]
        public string OrganizationName { get; protected set; }
        
        [field: SerializeField]
        public string JobTitle { get; protected set; }
        
        [field: SerializeField]
        public string Note { get; protected set; }
    }
}
