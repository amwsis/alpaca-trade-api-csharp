﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Alpaca.Markets
{
    [SuppressMessage(
        "Microsoft.Performance", "CA1812:Avoid uninstantiated internal classes",
        Justification = "Object instances of this class will be created by Newtonsoft.JSON library.")]
    internal sealed class JsonLatestBestBidOffer : IQuote
    {
        [JsonProperty(PropertyName = "xbbo", Required = Required.Always)]
        public JsonHistoricalQuote Nested { get; set; } = new ();

        [JsonProperty(PropertyName = "symbol", Required = Required.Always)]
        public String Symbol { get; set; } = String.Empty;

        [JsonIgnore]
        public DateTime TimestampUtc => Nested.TimestampUtc;

        [JsonIgnore]
        public String AskExchange => Nested.AskExchange;

        [JsonIgnore]
        public String BidExchange => Nested.BidExchange;

        [JsonIgnore]
        public Decimal BidPrice => Nested.BidPrice;

        [JsonIgnore]
        public Decimal AskPrice => Nested.AskPrice;

        [JsonIgnore]
        public Decimal BidSize => Nested.BidSize;

        [JsonIgnore]
        public Decimal AskSize => Nested.AskSize;

        [JsonIgnore]
        public String Tape => Nested.Tape;
    
        [JsonIgnore]
        public IReadOnlyList<String> Conditions => Nested.Conditions;
    }
}
