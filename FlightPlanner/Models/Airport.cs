﻿using System.Text.Json.Serialization;

namespace Flight_plannerAPI.Models
{
    public class Airport
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }
    }
}