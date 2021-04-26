using UnityEngine;
using System;
using System.Collections;
using SA.Android.GMS.Games;

namespace SA.CrossPlatform.GameServices
{
    [Serializable]
    class UM_AndroidSavedGameMetadata : UM_iSavedGameMetadata
    {
        AN_SnapshotMetadata meta;

        public UM_AndroidSavedGameMetadata(AN_SnapshotMetadata an_metadata)
        {
            meta = an_metadata;
        }

        public string Name => meta.Title;

        public string DeviceName => meta.DeviceName;

        public AN_SnapshotMetadata NativeMeta => meta;
    }
}
