using System;

namespace ComLib
{
    public abstract class BizBase : IDisposable
    {
        bool _disposed = false;

        ~BizBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing) { }

            _disposed = true;
        }
    }
}
