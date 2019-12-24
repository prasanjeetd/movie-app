using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class FlattenNestedJSONConverter<T> : JsonConverter where T : new()
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var daat = JObject.Load(reader);
            var result = new T();

            foreach (var prop in result.GetType().GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance))
            {
                string propName = string.Empty;
                //filter out non-Json attributes
                var attr = prop.GetCustomAttributes(false).Where(a => a.GetType() == typeof(JsonPropertyAttribute)).FirstOrDefault();
                if (attr != null)
                {
                    propName = ((JsonPropertyAttribute)attr).PropertyName;
                }
                //If no JsonPropertyAttribute existed, or no PropertyName was set,
                //still attempt to deserialize the class member
                if (string.IsNullOrWhiteSpace(propName))
                {
                    propName = prop.Name.ToLower();
                    //propName = textInfo.ToTitleCase(propName.ToLower()).Replace("_","");
                }
                //split by the delimiter, and traverse recursevly according to the path
                var nests = propName.Split('/');
                object propValue = null;
                JToken token = null;
                for (var i = 0; i < nests.Length; i++)
                {
                    if (token == null)
                    {
                        token = daat[nests[i]];
                    }
                    else
                    {
                        token = token[nests[i]];
                    }
                    if (token == null)
                    {
                        //silent fail: exit the loop if the specified path was not found
                        break;
                    }
                    else
                    {
                        //store the current value
                        if (token is JValue)
                        {
                            propValue = ((JValue)token).Value;
                        }
                        else if (token is JArray)
                        {
                            
                            propValue = ((JArray)token).ToObject<string[]>();

                            //prop = token.
                        }
                    }
                }

                if (propValue != null)
                {
                    //workaround for numeric values being automatically created as Int64 (long) objects.
                    if (propValue is long && prop.PropertyType == typeof(Int32))
                    {
                        prop.SetValue(result, Convert.ToInt32(propValue));
                    }
                    else
                    {
                        prop.SetValue(result, propValue);
                    }
                }
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //JToken t = JToken.FromObject(value);
            //t.WriteTo(writer);

            //serializer.Converters.Remove(this);
            //JToken jToken = JToken.FromObject(value, serializer);
            //serializer.Converters.Add(this);

            JObject jo = new JObject();
            Type type = value.GetType();
            //jo.Add("type", type.Name);

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.CanRead)
                {
                    object propVal = prop.GetValue(value, null);
                    if (propVal != null)
                    {
                        string pn = char.ToLower(prop.Name[0]) + prop.Name.Substring(1);
                        jo.Add(pn, JToken.FromObject(propVal, serializer));
                    }
                }
            }
            jo.WriteTo(writer);

        }
    }
}
