﻿using System;

namespace Alpaca.Markets
{
    internal sealed class AlpacaCryptoStreamingClient :
        DataStreamingClientBase<AlpacaCryptoStreamingClientConfiguration>, 
        IAlpacaCryptoStreamingClient
    {
        public AlpacaCryptoStreamingClient(
            AlpacaCryptoStreamingClientConfiguration configuration)
            : base(configuration.EnsureNotNull(nameof(configuration)))
        {
        }

        public IAlpacaDataSubscription<ITrade> GetTradeSubscription(
            String symbol) => 
            GetSubscription<ITrade, JsonRealTimeTrade>(TradesChannel, symbol);

        public IAlpacaDataSubscription<IQuote> GetQuoteSubscription(
            String symbol) =>
            GetSubscription<IQuote, JsonRealTimeCryptoQuote>(QuotesChannel, symbol);

        public IAlpacaDataSubscription<IBar> GetMinuteBarSubscription() => 
            GetSubscription<IBar, JsonRealTimeBar>(MinuteBarsChannel, WildcardSymbolString);

        public IAlpacaDataSubscription<IBar> GetMinuteBarSubscription(
            String symbol) =>
            GetSubscription<IBar, JsonRealTimeBar>(MinuteBarsChannel, symbol);

        public IAlpacaDataSubscription<IBar> GetDailyBarSubscription(
            String symbol) =>
            GetSubscription<IBar, JsonRealTimeBar>(DailyBarsChannel, symbol);
    }
}
