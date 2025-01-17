﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alpaca.Markets
{
    internal sealed class JsonSubscriptionUpdate
    {
        [JsonProperty(PropertyName = "action", Required = Required.Default)]
        public JsonAction Action { get; set; }

        [JsonProperty(PropertyName = "trades", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<String>? Trades { get; set; }

        [JsonProperty(PropertyName = "quotes", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<String>? Quotes { get; set; }

        [JsonProperty(PropertyName = "bars", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<String>? MinuteBars { get; set; }

        [JsonProperty(PropertyName = "dailyBars", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<String>? DailyBars { get; set; }

        [JsonProperty(PropertyName = "statuses", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<String>? Statuses { get; set; }

        [JsonProperty(PropertyName = "lulds", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<String>? Lulds { get; set; }
    }
}
