using System.Collections;
using System.Xml;
using TamoCRM.Core.Commons;

namespace TamoCRM.Core.Providers
{
    public class ProviderConfiguration
    {
        private readonly Hashtable _providers = new Hashtable();    
        private string _defaultProvider;
        public Hashtable Providers
        {
            get { return this._providers; }
        }
        public string DefaultProvider
        {
            get { return this._defaultProvider; }
        }
        public static ProviderConfiguration GetProviderConfiguration(string strProvider)
        {
            return (ProviderConfiguration)Config.GetSection("TamoCRM/" + strProvider);
        }
        public void LoadValuesFromConfigurationXml(XmlNode _node)
        {
            XmlAttributeCollection _attributes = _node.Attributes;
            _defaultProvider = _attributes["defaultProvider"].Value;
            foreach(XmlNode _child in _node.ChildNodes)
            {
                if(_child.Name == "providers")
                {
                    GetProviders(_child);
                }
            }
        }
        public void GetProviders(XmlNode _node)
        {
            foreach(XmlNode _providerNode in _node.ChildNodes)
            {
                switch(_providerNode.Name)
                {
                    case "add":
                        Providers.Add(_providerNode.Attributes["name"].Value, new ProviderBase(_providerNode.Attributes));
                        break;
                    case "remove":
                        Providers.Remove(_providerNode.Attributes["name"].Value);
                        break;
                    case "clear":
                        Providers.Clear();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
