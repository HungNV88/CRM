using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
namespace TamoCRM.Core.Providers
{
    public class ProviderConfigurationHandler : IConfigurationSectionHandler
    {
        public object Create(object _parent, object _context, XmlNode _node)
        {
            ProviderConfiguration _objProviderConfiguration = new ProviderConfiguration();
            _objProviderConfiguration.LoadValuesFromConfigurationXml(_node);
            return _objProviderConfiguration;
        }
    }
}
