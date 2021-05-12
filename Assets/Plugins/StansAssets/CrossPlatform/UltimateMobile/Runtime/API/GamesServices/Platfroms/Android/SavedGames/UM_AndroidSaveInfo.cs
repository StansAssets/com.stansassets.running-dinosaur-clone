using System;
using SA.Android.GMS.Common.Images;
using SA.Android.GMS.Games;
using UnityEngine;

namespace SA.CrossPlatform.GameServices
{
    class UM_AndroidSaveInfo : UM_SaveInfo
    {
        readonly AN_SnapshotMetadata m_AndroidMeta;

        public UM_AndroidSaveInfo(AN_SnapshotMetadata meta)
        {
            m_AndroidMeta = meta;
            SetProgressValue(meta.ProgressValue);
            SetPlayedTimeMillis(meta.PlayedTime);
            SetDescription(meta.Description);
        }

        public override void LoadCoverImage(Action<Texture2D> callback)
        {
            var manager = new AN_ImageManager();
            manager.LoadImage(m_AndroidMeta.CoverImageUri, (loadResult) =>
            {
                if (loadResult.IsSucceeded)
                    callback.Invoke(loadResult.Image);
                else
                    callback.Invoke(Texture2D.whiteTexture);
            });
        }
    }
}
