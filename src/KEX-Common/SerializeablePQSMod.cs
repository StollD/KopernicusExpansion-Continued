using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace KopernicusExpansion
{
    [Serializable]
    public class SerializedPQSMod : PQSMod
    {
        // static values
        private static Dictionary<String, Dictionary<String, Object>> _properties = new Dictionary<String, Dictionary<String, Object>>();

        public static String NewSerializationID()
        {
            return Guid.NewGuid().ToString();
        }

        // this is because SerializationID is not public
        [SerializeField]
        protected String SerializationID;

        public void InitializeSerialization()
        {
            SerializationID = NewSerializationID();
        }

        //Properties
        private Dictionary<String, Object> Properties
        {
            get
            {
                //return null if our ID is null
                if (String.IsNullOrEmpty(SerializationID))
                {
                    Debug.LogError("SerializationID is null");
                    return null;
                }
                //if there is no entry, add it
                if (!_properties.ContainsKey(this.SerializationID))
                {
                    _properties.Add(SerializationID, new Dictionary<String, Object>());
                }
                return _properties[SerializationID];
            }
        }
        public void SetProperty<T>(String name, T value)
        {
            if (!Properties.ContainsKey(name))
                Properties.Add(name, value);
            else
                Properties[name] = value;
        }
        public T GetProperty<T>(String name)
        {
            if (!Properties.ContainsKey(name))
                return default(T);
            else
                return (T)Properties[name];
        }
    }
}
