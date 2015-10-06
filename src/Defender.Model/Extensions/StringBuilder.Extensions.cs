using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendSequence<T>(
            this StringBuilder @this,
            IEnumerable<T> seq,
            Func<StringBuilder, T, StringBuilder> fn)
        {
            return seq?.Aggregate(@this, fn);
        }
    }
}
