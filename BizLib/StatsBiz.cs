using System;
using ComLib;

namespace BizLib
{
    public sealed class StatsBiz : BizBase
    {
        bool _disposed = false;

        private readonly IRpcDac _esnDac;

        public StatsBiz(IRpcDac esnDac)
        {
            _esnDac = esnDac;
        }

        ~StatsBiz()
        {
            Dispose(false);
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing) { }

            base.Dispose(disposing);

            _disposed = true;
        }
    }
}
