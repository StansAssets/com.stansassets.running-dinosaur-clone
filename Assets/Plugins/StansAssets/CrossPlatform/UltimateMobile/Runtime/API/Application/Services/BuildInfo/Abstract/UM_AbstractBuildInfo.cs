using UnityEngine;

namespace SA.CrossPlatform.App
{
    abstract class UM_AbstractBuildInfo
    {
        public virtual string Identifier => Application.identifier;
        public virtual string Version => Application.version;
    }
}
