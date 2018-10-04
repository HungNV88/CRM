using System;
using System.Text;
using System.Xml;
using System.Web.Configuration;
using System.Configuration;
namespace TamoCRM.Core.Commons
{
    public class Config
    {
        public static string GetConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings[0].ConnectionString;
        }
        public static string GetConnectionString(string _key)
        {
            return WebConfigurationManager.ConnectionStrings[_key].ConnectionString;
        }
        public static string GetAppSetting(string _key)
        {
            return WebConfigurationManager.AppSettings[_key];
        }
        public static string Dbo
        {
            get { return GetAppSetting("dbo"); }
        }
        public static string UploadDir
        {
            get { return GetAppSetting("uploadDir"); }
        }
        public static object GetSection(string _name)
        {
            return ConfigurationManager.GetSection(_name);
        }
        public static XmlDocument AddAppSetting(XmlDocument xmlDoc, string _key, string _val)
        {
            XmlElement xmlSetting;
            XmlNode appSettingNode = xmlDoc.SelectSingleNode("//appSettings");
            if (appSettingNode != null)
            {
                xmlSetting = (XmlElement)appSettingNode.SelectSingleNode("//add[@key='" + _key + "']");
                if (xmlSetting != null)
                {
                    xmlSetting.SetAttribute("value", _val);
                }
                else
                {
                    xmlSetting = xmlDoc.CreateElement("add");
                    xmlSetting.SetAttribute("key", _key);
                    xmlSetting.SetAttribute("value", _val);
                    appSettingNode.AppendChild(xmlSetting);
                }
            }
            return xmlDoc;
        }
        public static XmlDocument LoadConfig(string _fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load((_fileName));
            return xmlDoc;
        }
        public static string SaveConfig(string _fileName, XmlDocument xmlDoc)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter((_fileName), Encoding.Unicode);
                writer.Formatting = Formatting.Indented;
                xmlDoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
