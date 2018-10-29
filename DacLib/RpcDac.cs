using System.Threading.Tasks;
using ComLib;
using Microsoft.Extensions.Configuration;

namespace DacLib
{
    public sealed class RpcDac : JsonRpcBase, IRpcDac
    {
        public RpcDac(IConfiguration configuration) : base(configuration) { }

        public async Task<string> GetBlockNumberAsync()
        {
            return await RpcAsync("eth_blockNumber");
        }

        public async Task<string> GetBalanceAsync(string address, string tag)
        {
            return await RpcAsync("eth_getBalance", address, tag);
        }

        public async Task<string> GetBalanceAsync(string address, long blockHeight)
        {
            return await RpcAsync("eth_getBalance", address, blockHeight);
        }

        public async Task<string> GetGasPriceAsync()
        {
            return await RpcAsync("eth_gasPrice");
        }

        public async Task<string> GetTransactionCountAsync(string address, string tag)
        {
            return await RpcAsync("eth_getTransactionCount", address, tag);
        }

        public async Task<string> GetEstimateGasAsync(params object[] ps)
        {
            return await RpcAsync("eth_estimateGas", ps);
        }

        public async Task<string> SendRawTransactionAsync(string hex)
        {
            return await RpcAsync("eth_sendRawTransaction", hex);
        }
    }
}
