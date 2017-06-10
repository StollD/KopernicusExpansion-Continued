using System;
using System.Collections.Generic;
using UnityEngine;

namespace KopernicusExpansion
{
    [Serializable]
    public class SerializedPQSMod : PQSMod
    {
        // static values
        private static Dictionary<string, Dictionary<string, object>> _properties = new Dictionary<string, Dictionary<string, object>>();

        public static string NewSerializationID()
        {
            return Guid.NewGuid().ToString();
        }

        // this is because SerializationID is not public
        [SerializeField]
        protected string SerializationID;

        public void InitializeSerialization()
        {
            SerializationID = NewSerializationID();
        }

        //Properties
        private Dictionary<string, object> Properties
        {
            get
            {
                //return null if our ID is null
                if (string.IsNullOrEmpty(SerializationID))
                {
                    Debug.LogError("SerializationID is null");
                    return null;
                }
                //if there is no entry, add it
                if (!_properties.ContainsKey(this.SerializationID))
                {
                    _properties.Add(SerializationID, new Dictionary<string, object>());
                }
                return _properties[SerializationID];
            }
        }
        public void SetProperty<T>(string name, T value)
        {
            if (!Properties.ContainsKey(name))
                Properties.Add(name, value);
            else
                Properties[name] = value;
        }
        public T GetProperty<T>(string name)
        {
            if (!Properties.ContainsKey(name))
                return default(T);
            else
                return (T)Properties[name];
        }
    }
}
