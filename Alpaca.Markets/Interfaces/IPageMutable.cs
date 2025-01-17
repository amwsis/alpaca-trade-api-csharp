﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Alpaca.Markets
{
    internal interface IPageMutable<TItems> : IPage<TItems>
    {
        public new String? NextPageToken { [UsedImplicitly] get; set; }

        public new IReadOnlyList<TItems> Items { [UsedImplicitly] get; set; }
    }
}
