using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace TamoCRM.Core.Commons.Utilities
{
    public class MiscUtility
    {

        public static string UpdateQueryStringItem(HttpRequest httpRequest, string queryStringKey, string newQueryStringValue)
        {
            StringBuilder NewURL = new StringBuilder();

            NewURL.Append(httpRequest.RawUrl);

            if (httpRequest.QueryString[queryStringKey] != null)
            {
                string OrignalSet = String.Format("{0}={1}", queryStringKey, httpRequest.QueryString[queryStringKey]);
                string NewSet = String.Format("{0}={1}", queryStringKey, newQueryStringValue);
                NewURL.Replace(OrignalSet, NewSet);
            }
            else if (httpRequest.QueryString.Count == 0)
            {
                NewURL.AppendFormat("?{0}={1}", queryStringKey, newQueryStringValue);
            }
            else
            {
                NewURL.AppendFormat("&{0}={1}", queryStringKey, newQueryStringValue);
            }

            return NewURL.ToString();
        }

        public static string UpdateQueryStringItem(HttpRequest httpRequest, string[] queryStringKeys, string[] newQueryStringValues)
        {
            StringBuilder NewURL = new StringBuilder();

            NewURL.Append(httpRequest.RawUrl.Replace("%20", " "));
            bool check = true;
            for (int i = 0; i < queryStringKeys.GetLength(0); i++)
            {
                string queryStringKey = queryStringKeys[i];
                string newQueryStringValue = newQueryStringValues[i];
                if (httpRequest.QueryString[queryStringKey] != null)
                {
                    string OrignalSet = String.Format("{0}={1}", queryStringKey, httpRequest.QueryString[queryStringKey]);
                    string NewSet = String.Format("{0}={1}", queryStringKey, newQueryStringValue);
                    NewURL.Replace(OrignalSet, NewSet);
                }
                else if (httpRequest.QueryString.Count == 0)
                {
                    if (newQueryStringValue != "" && newQueryStringValue != null)
                    {
                        if (check)
                        {
                            NewURL.AppendFormat("?{0}={1}", queryStringKey, newQueryStringValue);
                            check = false;
                        }
                        else NewURL.AppendFormat("&{0}={1}", queryStringKey, newQueryStringValue);
                    }
                }
                else if (newQueryStringValue != "" && newQueryStringValue != null) NewURL.AppendFormat("&{0}={1}", queryStringKey, newQueryStringValue);
            }

            return NewURL.ToString();
        }
        public static string UpdateQueryString(HttpRequest httpRequest, string queryStringKey, string newQueryStringValue)
        {
            StringBuilder NewURL = new StringBuilder();
            NewURL.Append(httpRequest.RawUrl);

            if (httpRequest.QueryString[queryStringKey] != null)
            {
                string OrignalSet = String.Format("{0}/{1}", queryStringKey, httpRequest.QueryString[queryStringKey]);
                string NewSet = String.Format("{0}/{1}", queryStringKey, newQueryStringValue);
                NewURL.Replace(OrignalSet, NewSet);
            }
            else if (httpRequest.QueryString.Count == 0)
            {
                NewURL.AppendFormat("?{0}/{1}", queryStringKey, newQueryStringValue);
            }
            else
            {
                NewURL = NewURL.Insert(httpRequest.RawUrl.LastIndexOf("/"),
                                       string.Format("/{0}/{1}", queryStringKey, newQueryStringValue));
                //NewURL.AppendFormat("&{0}/{1}", queryStringKey, newQueryStringValue);
            }

            return NewURL.ToString();
        }
        public static void FillTreeData(ListItemCollection lst, DataTable dtCommands, string fieldKey, string fieldName, string fieldParentID, string sortBy)
        {
            lst.Clear();
            DataRow[] drRoots = dtCommands.Select(fieldParentID + "  = " + 0, sortBy);
            foreach (DataRow row in drRoots)
            {
                ListItem item = new ListItem();
                item.Value = row[fieldKey].ToString();
                item.Text = row[fieldName].ToString();
                item.Attributes.Add("Level", "0");
                lst.Add(item);
                LoadCmdItem(lst, item, dtCommands, fieldKey, fieldName, fieldParentID, sortBy);
            }
        }

        private static void LoadCmdItem(ListItemCollection lst, ListItem curItem, DataTable dtCommands, string fieldKey, string fieldName, string fieldParentID, string sortBy)
        {
            int level = Convert.ToInt32(curItem.Attributes["Level"]);
            level += 1;
            int curID = Convert.ToInt32(curItem.Value);
            DataRow[] drChilds = dtCommands.Select(fieldParentID + " = " + curID);
            foreach (DataRow row in drChilds)
            {
                ListItem childItem = new ListItem();
                childItem.Text = MiscUtility.StringIndent(level) + row[fieldName].ToString();
                childItem.Value = row[fieldKey].ToString();
                childItem.Attributes.Add("Level", level.ToString());
                lst.Add(childItem);
                LoadCmdItem(lst, childItem, dtCommands, fieldKey, fieldName, fieldParentID, sortBy);
            }
        }

        public static void FillIndex(DropDownList dropIndex, int min, int max, int selected)
        {
            dropIndex.Items.Clear();
            for (int i = min; i <= max; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                if (i == selected) item.Selected = true;
                else item.Selected = false;
                dropIndex.Items.Add(item);
            }
        }

        public static void FillIndex(DropDownList dropIndex, int max, int selected)
        {
            dropIndex.Items.Clear();
            for (int i = 0; i <= max; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                if (i == selected) item.Selected = true;
                else item.Selected = false;
                dropIndex.Items.Add(item);
            }
        }

        public static void SelectItemFromList(ListControl list, string selectedValue)
        {
            list.SelectedIndex = -1;
            ListItem item = list.Items.FindByValue(selectedValue);
            if (item != null)
                item.Selected = true;
        }

        public static void SelectItemFromList(ListControl list, string[] selectedValues)
        {
            list.SelectedIndex = -1;
            for (int i = 0; i < selectedValues.Length; i++)
            {
                string selectedValue = selectedValues[i];
                ListItem item = list.Items.FindByValue(selectedValue);
                if (item != null)
                    item.Selected = true;
            }
        }

        public static string StringIndent(int level)
        {
            string retVal = string.Empty;
            for (int i = 0; i < level; i++)
                retVal += ".....";
            return retVal;
        }

        public static string GetSubstring(string Content, int leng)
        {
            if (Content.Length > leng)
            {
                string temp = Content.Substring(0, leng);
                temp = temp.Replace("\r", "").Replace("\n", "").Replace("\t", "");
                Content = temp.Substring(0, temp.LastIndexOf(" ")) + "...";
            }
            else
            {
                Content = Content.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            }
            return Content;
        }

        public static byte[] BinarySerialize(object objToSerialize)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, objToSerialize);
            return stream.GetBuffer();
        }

        public static object BinaryDeserialize(byte[] data)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(data);
            formatter.Deserialize(stream);
            return stream.GetBuffer();
        }
        public static string RemoveHtmlTags(string input)
        {
            string retVal = string.Empty;
            Regex r = new Regex("<[^>]*>");
            retVal = r.Replace(input, string.Empty);
            return retVal;
        }
    }
}