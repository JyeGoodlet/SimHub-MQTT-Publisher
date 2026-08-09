using GameReaderCommon;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimHub.MQTTPublisher.Payload
{
    public static class PayloadBuilder
    {
        private static readonly Dictionary<string, PropertyInfo> _propertyCache = new Dictionary<string, PropertyInfo>();

        /// <summary>
        /// Builds a flat payload dictionary from the configured field list. Each field name is
        /// resolved against SimHub's StatusDataBase at runtime via reflection, so any current or
        /// future SimHub property can be included without code changes.
        /// Built-in fields: "time" (Unix ms timestamp), "userId".
        /// </summary>
        public static Dictionary<string, object> Build(
            GameData data,
            SimHubMQTTPublisherPluginUserSettings userSettings,
            PayloadConfig config)
        {
            var payload = new Dictionary<string, object>(config.Fields.Count);

            foreach (var field in config.Fields)
            {
                if (field == "time")
                {
                    payload["time"] = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    continue;
                }

                if (field == "userId")
                {
                    payload["userId"] = userSettings.UserId.ToString();
                    continue;
                }

                var prop = GetProperty(data.NewData, field);
                if (prop != null)
                {
                    payload[field] = prop.GetValue(data.NewData);
                }
            }

            return payload;
        }

        private static PropertyInfo GetProperty(object target, string name)
        {
            if (!_propertyCache.TryGetValue(name, out var prop))
            {
                prop = target.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
                _propertyCache[name] = prop;
            }
            return prop;
        }
    }
}
