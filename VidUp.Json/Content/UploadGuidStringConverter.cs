﻿using System;
using Drexel.VidUp.Business;
using Newtonsoft.Json;

namespace Drexel.VidUp.Json.Content
{
    public class UploadGuidStringConverter : JsonConverter<Upload>
    {

        public override Upload ReadJson(JsonReader reader, Type objectType, Upload existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string guidString = (string)reader.Value;
            if (string.IsNullOrWhiteSpace(guidString))
            {
                return null;
            }

            Upload upload = JsonDeserializationContent.AllUploads.Find(upload2 => upload2.Guid == Guid.Parse(guidString));
            if (upload == null)
            {
                throw new InvalidOperationException("Upload not found in all uploads.");
            }

            return upload;
        }

        public override void WriteJson(JsonWriter writer, Upload value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Guid);
        }
    }
}
