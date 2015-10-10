﻿namespace Defender.Model.Extensions
{
    using System;

    public static class FunctionalExtensions
    {
        public static T Tee<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }

        public static TResult Map<TSource, TResult>(
            this TSource @this,
            Func<TSource, TResult> fn)
        {
            return fn(@this);
        }
    }
}
