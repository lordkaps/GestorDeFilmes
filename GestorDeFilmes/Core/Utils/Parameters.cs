using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeFilmes.Core.Utils
{
    public class Parameter
    {
        private static Parameter instance;
        private readonly Dictionary<string, object> parameters;


        public Parameter()
        {
            parameters = new Dictionary<string, object>();
        }

        public static Parameter Instance
        {
            get
            {
                instance ??= new Parameter();
                return instance;
            }
        }

        public void AddParameter(string key, object value)
        {
            parameters[key] = value;
        }

        public object GetParameter(string key)
        {
            if (parameters.TryGetValue(key, out var value))
            {
                parameters.Remove(key);
                return value;
            }

            return null;
        }
        public void RemoveParameter(string key)
        {
            if (parameters.TryGetValue(key, out var value))
                parameters.Remove(key);
        }

        public bool TryGetParameter(string key, out object value)
        {
            var objeto = GetParameter(key);
            value = null;
            if (objeto != null)
            {
                value = objeto;
                return true;
            }
            return false;
        }

        public TValue GetParameter<TValue>(string key)
        {
            if (parameters.TryGetValue(key, out TValue value))
            {
                parameters.Remove(key);
                return value;
            }
            return default;
        }

        public bool TryGetParameter<TValue>(string key, out TValue value)
        {
            if (parameters.TryGetValue(key, out value))
            {
                parameters.Remove(key);
                return true;
            }
            return false;
        }
    }

    public static class ParameterExtensions
    {
        public static bool TryGetValue<T>(this Dictionary<string, object> parameters, string key, out T value)
        {
            foreach (var parameter in parameters)
            {
                if (string.Compare(parameter.Key, key, StringComparison.Ordinal) == 0)
                {
                    var success = TryGetValueInternal(parameter, typeof(T), out object valueAsObject);
                    value = (T)valueAsObject;
                    return success;
                }
            }

            value = default;
            return false;
        }

        private static bool TryGetValueInternal(KeyValuePair<string, object> parameters, Type type, out object value)
        {
            value = GetDefault(type);
            var success = false;
            if (parameters.Value == null)
            {
                success = true;
            }
            else if (parameters.Value.GetType() == type)
            {
                success = true;
                value = parameters.Value;
            }
            else if (type.IsAssignableFrom(parameters.Value.GetType()))
            {
                success = true;
                value = parameters.Value;
            }
            else if (type.IsEnum)
            {
                var valueAsString = parameters.Value.ToString();
                if (Enum.IsDefined(type, valueAsString))
                {
                    success = true;
                    value = Enum.Parse(type, valueAsString);
                }
                else if (int.TryParse(valueAsString, out var numericValue))
                {
                    success = true;
                    value = Enum.ToObject(type, numericValue);
                }
            }

            if (!success && type.GetInterface("System.IConvertible") != null)
            {
                success = true;
                value = Convert.ChangeType(parameters.Value, type);
            }

            return success;
        }

        private static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }

}
