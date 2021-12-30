using System;
using BitstampApp.Websocket.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BitstampApp.Websocket.Json
{
    internal class BitstampStringEnumConverter : StringEnumConverter
    {
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                var val = reader.Value;

                if (val is string valS && string.IsNullOrWhiteSpace(valS))
                {
                    return existingValue;
                }


                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                Log.Warn(
                    $"Can't parse enum, value: {reader.Value}, target type: {objectType}, using default '{existingValue}'");
                return existingValue;
            }
        }
    }
}