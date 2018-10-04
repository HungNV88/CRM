using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace TamoCRM.Core.Commons.Utilities
{
    public class XmlHelper
    {
        
        public static void AppendElement(ref XmlDocument objDoc, XmlNode objNode, string attName, string attValue, bool includeIfEmpty) 
        { 
            AppendElement(ref objDoc, objNode, attName, attValue, includeIfEmpty, false); 
        } 
        
        public static void AppendElement(ref XmlDocument objDoc, XmlNode objNode, string attName, string attValue, bool includeIfEmpty, bool CDATA) 
        { 
            if (attValue == "" & !includeIfEmpty) { 
                return; // TODO: might not be correct. Was : Exit Sub 
            } 
            if (CDATA) { 
                objNode.AppendChild(CreateCDataElement(objDoc, attName, attValue)); 
            } 
            else { 
                objNode.AppendChild(CreateElement(objDoc, attName, attValue)); 
            } 
        } 
        
        public static XmlAttribute CreateAttribute(XmlDocument objDoc, string attName, string attValue) 
        { 
            XmlAttribute attribute = objDoc.CreateAttribute(attName); 
            attribute.Value = attValue; 
            return attribute; 
        } 
        
        public static void CreateAttribute(XmlDocument objDoc, XmlNode objNode, string attName, string attValue) 
        { 
            XmlAttribute attribute = objDoc.CreateAttribute(attName); 
            attribute.Value = attValue; 
            objNode.Attributes.Append(attribute); 
        } 
        
        public static XmlElement CreateElement(XmlDocument objDoc, string NodeName) 
        { 
            return objDoc.CreateElement(NodeName); 
        } 
        
        public static XmlElement CreateElement(XmlDocument objDoc, string NodeName, string NodeValue) 
        { 
            XmlElement element = objDoc.CreateElement(NodeName); 
            element.InnerText = NodeValue; 
            return element; 
        } 
        
        public static XmlElement CreateCDataElement(XmlDocument objDoc, string NodeName, string NodeValue) 
        { 
            XmlElement element = objDoc.CreateElement(NodeName); 
            element.AppendChild(objDoc.CreateCDataSection(NodeValue)); 
            return element; 
        } 
        
        public static object Deserialize(string xmlObject, System.Type type) 
        { 
            XmlSerializer ser = new XmlSerializer(type); 
            StringReader sr = new StringReader(xmlObject); 
            object obj = ser.Deserialize(sr); 
            sr.Close(); 
            return obj; 
        } 
        
        public static Dictionary<int, TValue> DeSerializeDictionary<TValue>(Stream objStream, string rootname) 
        { 
            Dictionary<int, TValue> objDictionary = null; 
            
            XmlDocument xmlDoc = new XmlDocument(); 
            xmlDoc.Load(objStream); 
            
            objDictionary = new Dictionary<int, TValue>(); 
            
            foreach (XmlElement xmlItem in xmlDoc.SelectNodes(rootname + "/item")) { 
                int key = Convert.ToInt32(xmlItem.GetAttribute("key")); 
                string typeName = xmlItem.GetAttribute("type"); 
                
                TValue objValue = Activator.CreateInstance<TValue>(); 
                
                //Create the XmlSerializer 
                XmlSerializer xser = new XmlSerializer(objValue.GetType()); 
                
                //A reader is needed to read the XML document. 
                XmlTextReader reader = new XmlTextReader(new StringReader(xmlItem.InnerXml)); 
                
                // Use the Deserialize method to restore the object's state, and store it 
                // in the Hashtable 
                objDictionary.Add(key, (TValue)xser.Deserialize(reader)); 
            } 
            
            return objDictionary; 
        } 
        
        public static Hashtable DeSerializeHashtable(string xmlSource, string rootname) 
        { 
            Hashtable objHashTable; 
            if (xmlSource != "") { 
                objHashTable = new Hashtable(); 
                
                XmlDocument xmlDoc = new XmlDocument(); 
                xmlDoc.LoadXml(xmlSource); 
                
                foreach (XmlElement xmlItem in xmlDoc.SelectNodes(rootname + "/item")) { 
                    string key = xmlItem.GetAttribute("key"); 
                    string typeName = xmlItem.GetAttribute("type"); 
                    
                    //Create the XmlSerializer 
                    XmlSerializer xser = new XmlSerializer(Type.GetType(typeName)); 
                    
                    //A reader is needed to read the XML document. 
                    XmlTextReader reader = new XmlTextReader(new StringReader(xmlItem.InnerXml)); 
                    
                    // Use the Deserialize method to restore the object's state, and store it 
                    // in the Hashtable 
                    objHashTable.Add(key, xser.Deserialize(reader)); 
                } 
            } 
            else { 
                objHashTable = new Hashtable(); 
            } 
            return objHashTable; 
        } 
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Gets the value of a node 
        /// </summary> 
        /// <param name="nav">Parent XPathNavigator</param> 
        /// <param name="path">The Xpath expression to the value</param> 
        /// <returns></returns> 
        /// <history> 
        /// [cnurse] 11/08/2004 moved from PortalController and made Public Shared 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public static string GetNodeValue(XPathNavigator nav, string path) 
        { 
            string strValue = null; 
            
            XPathNavigator elementNav = nav.SelectSingleNode(path); 
            if (elementNav != null) { 
                strValue = elementNav.Value; 
            } 
            
            return strValue; 
            
        } 
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Gets the value of node 
        /// </summary> 
        /// <param name="objNode">Parent node</param> 
        /// <param name="NodeName">Child node to look for</param> 
        /// <param name="DefaultValue">Default value to return</param> 
        /// <returns></returns> 
        /// <remarks> 
        /// If the node does not exist or it causes any error the default value will be returned. 
        /// </remarks> 
        /// <history> 
        /// [VMasanas] 09/09/2004 Created 
        /// [cnurse] 11/08/2004 moved from PortalController and made Public Shared 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public static string GetNodeValue(XmlNode objNode, string NodeName, string DefaultValue)
        {
            string strValue = string.Empty;
            if(DefaultValue != null)
                strValue = DefaultValue; 
            
            try { 
                strValue = objNode[NodeName].InnerText; 
                
                if (strValue == "" & DefaultValue != "") { 
                    strValue = DefaultValue; 
                } 
            } 
            catch { 
                // node does not exist - legacy issue 
            } 
            
            return strValue; 
            
        } 
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Gets the value of node 
        /// </summary> 
        /// <param name="objNode">Parent node</param> 
        /// <param name="NodeName">Child node to look for</param> 
        /// <param name="DefaultValue">Default value to return</param> 
        /// <returns></returns> 
        /// <remarks> 
        /// If the node does not exist or it causes any error the default value will be returned. 
        /// </remarks> 
        /// <history> 
        /// [VMasanas] 09/09/2004 Added new method to return converted values 
        /// [cnurse] 11/08/2004 moved from PortalController and made Public Shared 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public static bool GetNodeValueBoolean(XmlNode objNode, string NodeName, bool DefaultValue)// ERROR: Unsupported modifier : In, Optional bool DefaultValue) 
        { 
            bool bValue = DefaultValue; 
            
            try { 
                bValue = Convert.ToBoolean(objNode[NodeName].InnerText); 
            } 
            catch { 
                // node does not exist / data conversion error - legacy issue: use default value 
            } 
            
            return bValue; 
            
        } 
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Gets the value of node 
        /// </summary> 
        /// <param name="objNode">Parent node</param> 
        /// <param name="NodeName">Child node to look for</param> 
        /// <param name="DefaultValue">Default value to return</param> 
        /// <returns></returns> 
        /// <remarks> 
        /// If the node does not exist or it causes any error the default value will be returned. 
        /// </remarks> 
        /// <history> 
        /// [VMasanas] 09/09/2004 Added new method to return converted values 
        /// [cnurse] 11/08/2004 moved from PortalController and made Public Shared 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public static DateTime GetNodeValueDate(XmlNode objNode, string NodeName, DateTime DefaultValue) 
        { 
            DateTime dateValue = DefaultValue; 
            
            try { 
                if (objNode[NodeName].InnerText != "") { 
                    
                    dateValue = Convert.ToDateTime(objNode[NodeName].InnerText); 
                } 
            } 
            catch { 
                // node does not exist / data conversion error - legacy issue: use default value 
            } 
            
            return dateValue; 
            
        } 
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Gets the value of node 
        /// </summary> 
        /// <param name="objNode">Parent node</param> 
        /// <param name="NodeName">Child node to look for</param> 
        /// <param name="defaultValue">Default value to return</param> 
        /// <returns></returns> 
        /// <remarks> 
        /// If the node does not exist or it causes any error the default value will be returned. 
        /// </remarks> 
        /// <history> 
        /// [VMasanas] 09/09/2004 Added new method to return converted values 
        /// [cnurse] 11/08/2004 moved from PortalController and made Public Shared 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public static int GetNodeValueInt(XmlNode objNode, string NodeName, int defaultValue)// ERROR: Unsupported modifier : In, Optional int DefaultValue) 
        { 
            int intValue = defaultValue; 
            
            try { 
                intValue = Convert.ToInt32(objNode[NodeName].InnerText); 
            } 
            catch { 
                // node does not exist / data conversion error - legacy issue: use default value 
            } 
            
            return intValue; 
            
        } 
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Gets the value of node 
        /// </summary> 
        /// <param name="objNode">Parent node</param> 
        /// <param name="NodeName">Child node to look for</param> 
        /// <param name="defaultValue">Default value to return</param> 
        /// <returns></returns> 
        /// <remarks> 
        /// If the node does not exist or it causes any error the default value will be returned. 
        /// </remarks> 
        /// <history> 
        /// [VMasanas] 09/09/2004 Added new method to return converted values 
        /// [cnurse] 11/08/2004 moved from PortalController and made Public Shared 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public static float GetNodeValueSingle(XmlNode objNode, string NodeName, float defaultValue)// ERROR: Unsupported modifier : In, Optional float DefaultValue) 
        {
            float sValue = defaultValue; 
            
            try { 
                sValue = Convert.ToSingle(objNode[NodeName].InnerText); 
            } 
            catch { 
                // node does not exist / data conversion error - legacy issue: use default value 
            } 
            
            return sValue; 
            
        } 
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// GetXMLContent loads the xml content into an Xml Document 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="ContentUrl">The url to the xml text</param> 
        /// <returns>An XmlDocument</returns> 
        /// <history> 
        /// [cnurse] 9/23/2004 Moved XML to a separate Project 
        /// [cnurse] 1/21/2005 Moved to XmlUtils class 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [Obsolete("This method is obsolete.")] 
        public static XmlDocument GetXMLContent(string ContentUrl) 
        { 
            XmlDocument functionReturnValue = null; 
            //This function reads an Xml document via a Url and returns it as an XmlDocument object 
            
            functionReturnValue = new XmlDocument(); 
            WebRequest req = WebRequest.Create(ContentUrl); 
            WebResponse result = req.GetResponse(); 
            Stream ReceiveStream = result.GetResponseStream(); 
            XmlReader objXmlReader = new XmlTextReader(result.GetResponseStream()); 
            functionReturnValue.Load(objXmlReader); 
            return functionReturnValue; 
            
        } 
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// GetXSLContent loads the xsl content into an Xsl Transform 
        /// </summary> 
        /// <remarks> 
        /// Even though this method uses and returns the deprecated class XslTransform, 
        /// it has been retained for backwards compatability. 
        /// </remarks> 
        /// <param name="ContentURL">The url to the xsl text</param> 
        /// <returns>An XslTransform</returns> 
        /// <history> 
        /// [cnurse] 9/23/2004 Moved XML to a separate Project 
        /// [cnurse] 1/21/2005 Moved to XmlUtils class 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        [Obsolete("This method is obsolete.")]
        public static System.Xml.Xsl.XslCompiledTransform GetXSLContent(string ContentURL) 
        {
            System.Xml.Xsl.XslCompiledTransform functionReturnValue = null;

            functionReturnValue = new System.Xml.Xsl.XslCompiledTransform(); 
            WebRequest req = WebRequest.Create(ContentURL); 
            WebResponse result = req.GetResponse(); 
            Stream ReceiveStream = result.GetResponseStream(); 
            XmlReader objXSLTransform = new XmlTextReader(result.GetResponseStream()); 
            functionReturnValue.Load(objXSLTransform, null, null); 
            return functionReturnValue; 
            
        } 
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Serializes an object to Xml using the XmlAttributes 
        /// </summary> 
        /// <param name="obj">The object to Serialize</param> 
        /// <returns></returns> 
        /// <remarks> 
        /// An Xml representation of the object. 
        /// </remarks> 
        /// <history> 
        /// [cnurse] 11/25/2004 created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public static string Serialize(object obj) 
        { 
            
            string xmlObject; 
            IDictionary dic = obj as IDictionary; 
            if ((dic != null)) { 
                xmlObject = SerializeDictionary(dic, "dictionary"); 
            } 
            else { 
                XmlDocument xmlDoc = new XmlDocument(); 
                XmlSerializer xser = new XmlSerializer(obj.GetType()); 
                StringWriter sw = new StringWriter(); 
                
                xser.Serialize(sw, obj); 
                
                xmlDoc.LoadXml(sw.GetStringBuilder().ToString()); 
                XmlNode xmlDocEl = xmlDoc.DocumentElement; 
                xmlDocEl.Attributes.Remove(xmlDocEl.Attributes["xmlns:xsd"]); 
                xmlDocEl.Attributes.Remove(xmlDocEl.Attributes["xmlns:xsi"]); 
                
                xmlObject = xmlDocEl.OuterXml; 
            } 
            return xmlObject; 
        } 
        
        public static string SerializeDictionary(IDictionary Source, string rootName) 
        { 
            string strString; 
            if (Source.Count != 0) { 
                XmlSerializer xser; 
                StringWriter sw; 
                
                XmlDocument xmlDoc = new XmlDocument(); 
                XmlElement xmlRoot = xmlDoc.CreateElement(rootName); 
                xmlDoc.AppendChild(xmlRoot); 
                
                foreach (object key in Source.Keys) { 
                    //Create the item Node 
                    XmlElement xmlItem = xmlDoc.CreateElement("item"); 
                    
                    //Save the key name and the object type 
                    xmlItem.SetAttribute("key", Convert.ToString(key)); 
                    xmlItem.SetAttribute("type", Source[key].GetType().AssemblyQualifiedName.ToString()); 
                    
                    //Serialize the object 
                    XmlDocument xmlObject = new XmlDocument(); 
                    xser = new XmlSerializer(Source[key].GetType()); 
                    sw = new StringWriter(); 
                    xser.Serialize(sw, Source[key]); 
                    xmlObject.LoadXml(sw.ToString());
                    
                    //import and append the node to the root 
                    xmlItem.AppendChild(xmlDoc.ImportNode(xmlObject.DocumentElement, true)); 
                    xmlRoot.AppendChild(xmlItem); 
                } 
                
                //Return the OuterXml of the profile 
                strString = xmlDoc.OuterXml; 
            } 
            else { 
                strString = ""; 
            } 
            return strString; 
        } 
        
        public static void UpdateAttribute(XmlNode node, string attName, string attValue) 
        { 
            if ((node != null)) { 
                XmlAttribute attrib = node.Attributes[attName]; 
                attrib.InnerText = attValue; 
            } 
        } 
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Xml Encodes HTML 
        /// </summary> 
        /// <param name="HTML">The HTML to encode</param> 
        /// <returns></returns> 
        /// <history> 
        /// [cnurse] 09/29/2005 moved from Globals 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public static string XMLEncode(string HTML) 
        { 
            return "<![CDATA[" + HTML + "]]>"; 
        } 
        
        public static void XSLTransform(XmlDocument doc, ref StreamWriter writer, string xsltUrl) 
        {

            System.Xml.Xsl.XslCompiledTransform xslt = new System.Xml.Xsl.XslCompiledTransform(); 
            xslt.Load(xsltUrl); 
            //Transform the file. 
            xslt.Transform(doc, null, writer); 
        } 

    }
    public class RssItem
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public FeedType FeedType { get; set; }

        public RssItem()
        {
            Link = "";
            Title = "";
            Content = "";
            PublishDate = DateTime.Today;
            FeedType = FeedType.RSS;
        }
    }
    public class FeedParser
    {
        /// <summary>
        /// Parses the given <see cref="FeedType"/> and returns a <see cref="IList&lt;Item&gt;"/>.
        /// </summary>
        /// <returns></returns>
        public IList<RssItem> Parse(string url, FeedType feedType)
        {
            switch (feedType)
            {
                case FeedType.RSS:
                    return ParseRss(url);
                case FeedType.RDF:
                    return ParseRdf(url);
                case FeedType.Atom:
                    return ParseAtom(url);
                default:
                    throw new NotSupportedException(string.Format("{0} is not supported", feedType.ToString()));
            }
        }

        /// <summary>
        /// Parses an Atom feed and returns a <see cref="IList&lt;Item&gt;"/>.
        /// </summary>
        public virtual IList<RssItem> ParseAtom(string url)
        {
            try
            {
                XDocument doc = XDocument.Load(url);

                // Feed/Entry
                var entries = from item in doc.Root.Elements().Where(i => i.Name.LocalName == "entry")
                              select new RssItem
                              {
                                  FeedType = FeedType.Atom,
                                  Content = item.Elements().First(i => i.Name.LocalName == "content").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value,
                                  PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "published").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                              };

                return entries.ToList();
            }
            catch
            {
                return new List<RssItem>();
            }
        }

        /// <summary>
        /// Parses an RSS feed and returns a <see cref="IList&lt;Item&gt;"/>.
        /// </summary>
        public virtual IList<RssItem> ParseRss(string url)
        {
            try
            {
                XDocument doc = XDocument.Load(url);

                // RSS/Channel/item
                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                              select new RssItem
                              {
                                  FeedType = FeedType.RSS,
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                              };

                return entries.ToList();
            }
            catch
            {
                return new List<RssItem>();
            }
        }

        /// <summary>
        /// Parses an RDF feed and returns a <see cref="IList&lt;Item&gt;"/>.
        /// </summary>
        public virtual IList<RssItem> ParseRdf(string url)
        {
            try
            {
                XDocument doc = XDocument.Load(url);

                // <item> is under the root
                var entries = from item in doc.Root.Descendants().Where(i => i.Name.LocalName == "item")
                              select new RssItem
                              {
                                  FeedType = FeedType.RDF,
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "date").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                              };

                return entries.ToList();
            }
            catch
            {
                return new List<RssItem>();
            }
        }

        private DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.MinValue;
        }
    }

    /// <summary>
    /// Represents the XML format of a feed.
    /// </summary>
    public enum FeedType
    {
        /// <summary>
        /// Really Simple Syndication format.
        /// </summary>
        RSS,
        /// <summary>
        /// RDF site summary format.
        /// </summary>
        RDF,
        /// <summary>
        /// Atom Syndication format.
        /// </summary>
        Atom
    }

}
