using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClauseParser.Models.Symbol;
using Newtonsoft.Json;

namespace ClauseParser.Code.Services
{
    public class SymbolsJsonConverter : JsonConverter<Symbol>
    {
        public override void WriteJson(JsonWriter writer, Symbol value, JsonSerializer serializer)
        {
            var val = value.Serialize();

            foreach (var keyValuePair in Consts.CodesDictionary)
            {
                var singleChar = new string((char)keyValuePair.Value, 1);
                val = val.Replace(keyValuePair.Key, singleChar);
            }

            writer.WriteValue(val);
        }

        public override Symbol ReadJson(JsonReader reader, Type objectType, Symbol existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override bool CanRead => false;
    }
}
