using System;
using Microsoft.Extensions.Configuration;
using Nethereum.Web3;

namespace ComLib
{
    // Nuget. 필요
    // Nethereum.Web3
    public abstract class Web3Base : IDisposable
    {
        #region config
        string _rpcHost = "127.0.0.1";
        string _rpcPort = "13090";
        #endregion

        bool _disposed = false;

        protected Web3 _web3 = null;
        string _rpcVersion = string.Empty;

        public Web3Base(IConfiguration configuration)
        {
            _rpcHost = configuration.GetSection("rpc:host").Value;
            _rpcPort = configuration.GetSection("rpc:port").Value;

            _web3 = new Web3($"http://{_rpcHost}:{_rpcPort}");
        }

        ~Web3Base()
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

            if (disposing)
            {
                _web3 = null;
            }

            _disposed = true;
        }
    }
}

