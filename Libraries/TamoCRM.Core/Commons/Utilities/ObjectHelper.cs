using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace TamoCRM.Core.Commons.Utilities
{
    public static class ObjectHelper
    {
        //Support local cached properties of type to improved performance

        private static readonly Hashtable cachedProperties = Hashtable.Synchronized(new Hashtable());

        private static PropertyInfo[] GetCachedProperties(Type curType)
        {
            var retVal = (PropertyInfo[])cachedProperties[curType.ToString()];
            if (retVal == null)
            {
                retVal = curType.GetProperties();
                cachedProperties[curType.ToString()] = retVal;
            }
            return retVal;
        }

        //End cached properties
        private static bool IsColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }

        public static object CreateObject(Type type, IDataReader dr)
        {
            object objTarget = null;
            try
            {
                if (dr.Read())
                {
                    objTarget = Activator.CreateInstance(type);
                    var objProperties = GetCachedProperties(type);
                    foreach (var property in objProperties
                        .Where(property => property.CanWrite)
                        .Where(property => !Convert.IsDBNull(dr[property.Name])))
                    {
                        property.SetValue(objTarget, dr[property.Name], null);
                    }

                }
            }
            finally
            {
                dr.Close();
            }
            return objTarget;
        }

        public static T FillObject<T>(IDataReader _dr)
        {

            var objTarget = default(T);
            if (_dr == null) return objTarget;
            try
            {
                if (_dr.Read())
                {
                    objTarget = Activator.CreateInstance<T>();
                    if (objTarget.GetType() == typeof(INotifyPropertyChange))
                    {
                        ((INotifyPropertyChange)objTarget).SetNotifyPropertyChange(false);
                    }
                    var objProperties = GetCachedProperties(typeof(T));
                    foreach (var property in objProperties
                        .Where(property => IsColumnExists(_dr, property.Name))
                        .Where(property => property.CanWrite && !Convert.IsDBNull(_dr[property.Name])))
                    {
                        property.SetValue(objTarget, _dr[property.Name], null);
                    }
                    if (objTarget.GetType() == typeof(INotifyPropertyChange))
                    {
                        ((INotifyPropertyChange)objTarget).SetNotifyPropertyChange(true);
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return objTarget;
        }


        public static List<T> FillCollection<T>(IDataReader _dr)
        {
            var _list = new List<T>();
            if (_dr == null) return _list;
            try
            {
                while (_dr.Read())
                {
                    var objTarget = CreateObject<T>();
                    if (objTarget.GetType() == typeof(INotifyPropertyChange))
                    {
                        ((INotifyPropertyChange)objTarget).SetNotifyPropertyChange(false);
                    }
                    var objProperties = GetCachedProperties(typeof(T));
                    foreach (var property in objProperties
                        .Where(c => IsColumnExists(_dr, c.Name))
                        .Where(c => c.CanWrite && !Convert.IsDBNull(_dr[c.Name])))
                    {
                        property.SetValue(objTarget, _dr[property.Name], null);
                    }
                    if (objTarget.GetType() == typeof(INotifyPropertyChange))
                    {
                        ((INotifyPropertyChange)objTarget).SetNotifyPropertyChange(true);
                    }
                    _list.Add(objTarget);
                }
            }
            finally
            {
                _dr.Close();
            }
            return _list;
        }

        public static List<T> FillCollection<T>(IDataReader _dr, bool closedatareader)
        {
            var _list = new List<T>();
            if (_dr == null) return _list;
            try
            {
                while (_dr.Read())
                {
                    T objTarget = CreateObject<T>();
                    if (objTarget.GetType() == typeof(INotifyPropertyChange))
                    {
                        ((INotifyPropertyChange)objTarget).SetNotifyPropertyChange(false);
                    }
                    var objProperties = GetCachedProperties(typeof(T));
                    foreach (var property in objProperties
                        .Where(property => IsColumnExists(_dr, property.Name))
                        .Where(property => property.CanWrite && !Convert.IsDBNull(_dr[property.Name])))
                    {
                        property.SetValue(objTarget, _dr[property.Name], null);
                    }
                    if (objTarget.GetType() == typeof(INotifyPropertyChange))
                    {
                        ((INotifyPropertyChange)objTarget).SetNotifyPropertyChange(true);
                    }
                    _list.Add(objTarget);
                }
            }
            finally
            {
                if (closedatareader)
                    _dr.Close();
            }
            return _list;
        }

        public static void CloseDataReader(IDataReader dr, bool closeReader)
        {
            //close datareader
            if (dr != null && closeReader)
            {
                dr.Close();
            }
        }

        public static void Transform<TTarget, TFrom>(TFrom from, ref TTarget objTarget)
        {

            var objTargetProperties = GetCachedProperties(typeof(TTarget));
            var objFromProperties = GetCachedProperties(typeof(TFrom));
            foreach (var fromProperty in objFromProperties)
            {
                foreach (var targetProperty in objTargetProperties
                    .Where(targetProperty => targetProperty.CanWrite && fromProperty.Name == targetProperty.Name))
                {
                    targetProperty.SetValue(objTarget, fromProperty.GetValue(@from, null), null);
                }
            }
        }

        public static TTarget Transform<TTarget, TFrom>(TFrom from)
        {

            var objTarget = Activator.CreateInstance<TTarget>();
            var objTargetProperties = GetCachedProperties(typeof(TTarget));
            var objFromProperties = GetCachedProperties(typeof(TFrom));
            foreach (var fromProperty in objFromProperties)
            {
                foreach (var targetProperty in objTargetProperties
                    .Where(targetProperty => targetProperty.CanWrite && fromProperty.Name == targetProperty.Name))
                {
                    targetProperty.SetValue(objTarget, fromProperty.GetValue(@from, null), null);
                }
            }
            return objTarget;
        }

        private static T CreateObject<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static T CreateObject<T>(string XMLString)
        {
            try
            {
                var oXmlSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute(typeof(T).Name));
                var obj = oXmlSerializer.Deserialize(new StringReader(XMLString));
                return obj == null ? default(T) : (T)obj;
            }
            catch
            {
                return default(T);
            }
        }

        public static string StandardDatetimeToString(DateTime? dateTime, bool navigate = false)
        {
            var dateTimeNow = DateTime.Now;
            if (dateTime == null) return string.Empty;

            // DateTime
            string time;
            var value = navigate
                            ? (dateTime.Value - dateTimeNow).TotalMilliseconds
                            : (dateTimeNow - dateTime.Value).TotalMilliseconds;
            if (value < 0) return string.Empty;

            value = Math.Round(value / 1000);
            if (value < 60)
                time = "<b>" + value + "</b>" + " giây";
            else
            {
                value = Math.Round(value / 60);
                if (value < 60)
                    time = "<b>" + value + "</b>" + " phút";
                else
                {
                    value = Math.Round(value / 60);
                    if (value < 24)
                        time = "<b>" + value + "</b>" + " giờ";
                    else
                    {
                        value = Math.Round(value / 24);
                        time = "<b>" + value + "</b>" + " ngày";
                    }
                }
            }
            return time + (navigate ? " nữa" : " trước");
        }
    }
}
