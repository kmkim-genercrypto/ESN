using System;
using System.Threading.Tasks;

namespace ComLib
{
    public interface IAccountBiz : IDisposable
    {
        Task<string> GetBalanceAsync(string address, string tag);

        Task<string> GetBalanceMultiAsync(string address, string tag);
    }

    public interface IProxyBiz : IDisposable
    {
        Task<string> GetGasPriceAsync();

        Task<string> GetTransactionCountAsync(string address, string tag);

        Task<string> GetEstimateGasAsync(string to, string gas, string gasPrice, string value);

        Task<string> SendRawTransactionAsync(string hex);
    }

    public interface IStatsBiz : IDisposable
    {
        Task<string> GetBlockHeightAsync();
    }

    public interface IRpcDac : IDisposable
    {
        Task<string> GetBlockNumberAsync();

        Task<string> GetBalanceAsync(string address, long blockHeight);

        Task<string> GetBalanceAsync(string address, string tag);

        Task<string> GetGasPriceAsync();

        Task<string> GetTransactionCountAsync(string address, string tag);

        Task<string> GetEstimateGasAsync(params object[] ps);

        Task<string> SendRawTransactionAsync(string hex);
    }
}
