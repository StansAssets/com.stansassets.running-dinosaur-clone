using UnityEngine;
using System;
using System.Collections;
using SA.iOS.GameKit;

namespace SA.CrossPlatform.GameServices
{
    [Serializable]
    class UM_IOSSavedGameMetadata : UM_iSavedGameMetadata
    {
        ISN_GKSavedGame meta;

        public UM_IOSSavedGameMetadata(ISN_GKSavedGame isn_metadata)
        {
            meta = isn_metadata;
        }

        public string Name => meta.Name;

        public string DeviceName => meta.DeviceName;

        public ISN_GKSavedGame NativeMeta => meta;
    }
}
