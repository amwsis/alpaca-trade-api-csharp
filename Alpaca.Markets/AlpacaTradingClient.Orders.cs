﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alpaca.Markets
{
    internal sealed partial class AlpacaTradingClient
    {
        public async Task<IReadOnlyList<IOrder>> ListOrdersAsync(
            ListOrdersRequest request,
            CancellationToken cancellationToken = default) =>
            await _httpClient.GetAsync<IReadOnlyList<IOrder>, List<JsonOrder>>(
                await request.EnsureNotNull(nameof(request))
                    .GetUriBuilderAsync(_httpClient).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

        public Task<IOrder> PostOrderAsync(
            NewOrderRequest request,
            CancellationToken cancellationToken = default) =>
            postOrderAsync(request.EnsureNotNull(nameof(request)).Validate().GetJsonRequest(), cancellationToken);

        public Task<IOrder> PostOrderAsync(
            OrderBase orderBase,
            CancellationToken cancellationToken = default) =>
            postOrderAsync(orderBase.EnsureNotNull(nameof(orderBase)).Validate().GetJsonRequest(), cancellationToken);

        private Task<IOrder> postOrderAsync(
            JsonNewOrder jsonNewOrder,
            CancellationToken cancellationToken = default) =>
            _httpClient.PostAsync<IOrder, JsonOrder, JsonNewOrder>(
                "v2/orders", jsonNewOrder, cancellationToken);

        public Task<IOrder> PatchOrderAsync(
            ChangeOrderRequest request,
            CancellationToken cancellationToken = default) =>
            _httpClient.PatchAsync<IOrder, JsonOrder, ChangeOrderRequest>(
                request.EnsureNotNull(nameof(request)).Validate().GetEndpointUri(),
                request, cancellationToken);

        public async Task<IOrder> GetOrderAsync(
            String clientOrderId,
            CancellationToken cancellationToken = default) =>
            await _httpClient.GetAsync<IOrder, JsonOrder>(
                new UriBuilder(_httpClient.BaseAddress!)
                {
                    Path = "v2/orders:by_client_order_id",
                    Query = await new QueryBuilder()
                        .AddParameter("client_order_id", clientOrderId)
                        .AsStringAsync().ConfigureAwait(false)
                },
                cancellationToken).ConfigureAwait(false);

        public Task<IOrder> GetOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default) =>
            _httpClient.GetAsync<IOrder, JsonOrder>(
                $"v2/orders/{orderId:D}", cancellationToken);

        public Task<Boolean> DeleteOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default) =>
            _httpClient.TryDeleteAsync(
                $"v2/orders/{orderId:D}", cancellationToken);

        public Task<IReadOnlyList<IOrderActionStatus>> DeleteAllOrdersAsync(
            CancellationToken cancellationToken = default) =>
            _httpClient.DeleteAsync<IReadOnlyList<IOrderActionStatus>, List<JsonOrderActionStatus>>(
                    "v2/orders", cancellationToken);
    }
}
