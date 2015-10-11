namespace System
{
    public static class Disposable
    {
        public static TResult Using<TDisposable, TResult>(
            Func<TDisposable> factory,
            Func<TDisposable, TResult> fn)
            where TDisposable : IDisposable
        {
            using (var disp = factory()) return fn(disp);
        }
    }
}
