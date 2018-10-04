using System;
using System.Collections;
using System.Text;
using System.Xml;
namespace TamoCRM.Core.Providers
{
    public class ProviderBase
    {
        private string _providerName;
        private string _providerType;
        private Hashtable _providerAttributes = new Hashtable();

        public ProviderBase(string _name, string _type)
        {
            this._providerName = _name;
            this._providerType = _type;
        }
        public ProviderBase(XmlAttributeCollection _attributes)
        {
            _providerName = _attributes["name"].Value;
            _providerType = _attributes["type"].Value;
            foreach (XmlAttribute attribute in _attributes)
            {
                if (attribute.Name != "name" && attribute.Name != "type")
                {
                    _providerAttributes[attribute.Name] = attribute.Value;
                }
            }
        }

        public string ProviderName
        {
            get { return this._providerName; }
        }
        public string ProviderType
        {
            get { return this._providerType; }
        }
        public Hashtable ProviderAttributes
        {
            get { return this._providerAttributes; }
        }
    }
}
