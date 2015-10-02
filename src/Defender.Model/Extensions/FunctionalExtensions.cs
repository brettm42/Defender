using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defender.Model.Extensions
{
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
