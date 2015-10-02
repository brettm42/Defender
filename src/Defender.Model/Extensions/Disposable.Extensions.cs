using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defender.Model.Extensions
{
    public class Disposable
    {
        public static TResult Using<TDisposable, TResult>(
            Func<TDisposable> factory,
            Func<TDisposable, TResult> fn)
            where TDisposable : IDisposable
        {
            using (var disp = factory())
            {
                return fn(disp);
            }
        }
    }
}
