﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShaderTools.Utilities.Threading
{
    internal static class SemaphoreSlimExtensions
    {
        public static SemaphoreDisposer DisposableWait(this SemaphoreSlim semaphore, CancellationToken cancellationToken = default(CancellationToken))
        {
            semaphore.Wait(cancellationToken);
            return new SemaphoreDisposer(semaphore);
        }

        public static async Task<SemaphoreDisposer> DisposableWaitAsync(this SemaphoreSlim semaphore, CancellationToken cancellationToken = default(CancellationToken))
        {
            await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            return new SemaphoreDisposer(semaphore);
        }

        internal struct SemaphoreDisposer : IDisposable
        {
            private readonly SemaphoreSlim _semaphore;

            public SemaphoreDisposer(SemaphoreSlim semaphore)
            {
                _semaphore = semaphore;
            }

            public void Dispose()
            {
                _semaphore.Release();
            }
        }
    }
}
