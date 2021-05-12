using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA.iOS.XCode;

namespace SA.iOS
{
    class ISN_XcodeRequirements
    {
        readonly List<ISD_Framework> m_frameworks = new List<ISD_Framework>();
        readonly List<ISD_Library> m_libraries = new List<ISD_Library>();
        readonly List<ISD_BuildProperty> m_properties = new List<ISD_BuildProperty>();
        readonly List<ISD_PlistKey> m_plistKeys = new List<ISD_PlistKey>();

        //This is used for drawing UI only
        public List<string> Capabilities = new List<string>();

        public void AddFramework(ISD_Framework framework)
        {
            m_frameworks.Add(framework);
        }

        public void AddInfoPlistKey(ISD_PlistKey variables)
        {
            m_plistKeys.Add(variables);
        }

        public void AddLibrary(ISD_Library library)
        {
            m_libraries.Add(library);
        }

        public void AddBuildProperty(ISD_BuildProperty property)
        {
            m_properties.Add(property);
        }

        public List<ISD_Framework> Frameworks => m_frameworks;

        public List<ISD_Library> Libraries => m_libraries;

        public List<ISD_BuildProperty> Properties => m_properties;

        public List<ISD_PlistKey> PlistKeys => m_plistKeys;
    }
}
