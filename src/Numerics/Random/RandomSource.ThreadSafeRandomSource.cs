// <copyright file="RandomSource.ThreadSafeRandomSource.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
//
// Copyright (c) 2009-2016 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

using System;
using System.Threading;

namespace MathNet.Numerics.Random
{
    public abstract partial class RandomSource
    {
        protected sealed class ThreadSafeRandomSource<T> : RandomSource
            where T : RandomSource
        {
            private readonly ThreadLocal<T> Local;

            public ThreadSafeRandomSource(ThreadLocal<T> local) : base(false)
            {
                Local = local;
            }

            public sealed override double NextDouble()
                => Local.Value.NextDouble();

            protected sealed override double DoSample()
                => Local.Value.DoSample();

            protected sealed override void DoSampleBytes(byte[] buffer)
                => Local.Value.DoSampleBytes(buffer);

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            protected sealed override void DoSampleBytes(Span<byte> buffer)
                => Local.Value.DoSampleBytes(buffer);
#endif

            protected sealed override int DoSampleInt32WithNBits(int bitCount)
                => Local.Value.DoSampleInt32WithNBits(bitCount);

            protected sealed override long DoSampleInt64WithNBits(int bitCount)
                => Local.Value.DoSampleInt64WithNBits(bitCount);

            protected sealed override int DoSampleInteger()
                => Local.Value.DoSampleInteger();

            protected sealed override int DoSampleInteger(int maxExclusive)
                => Local.Value.DoSampleInteger(maxExclusive);

            protected sealed override int DoSampleInteger(int minInclusive, int maxExclusive)
                => Local.Value.DoSampleInteger(minInclusive, maxExclusive);
        }
    }
}
