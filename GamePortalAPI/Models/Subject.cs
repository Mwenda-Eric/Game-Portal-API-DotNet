using System;
using System.Text.Json.Serialization;

namespace GamePortalAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Subject
	{
		MATH,
		ENGLISH,
		SCIENCE
	}
}

