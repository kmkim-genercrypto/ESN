using System.Threading.Tasks;
using ComLib;
using Microsoft.Extensions.Configuration;

namespace DacLib
{
    //사용하지 않음
    public sealed class Web3Dac : Web3Base
    {
        public Web3Dac(IConfiguration configuration) : base(configuration) { }

        public async Task<string> GetEthBlockNumberAsync()
        {
            var hexBigInteger = await _web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();

            return hexBigInteger.Value.ToString();
        }

        public async Task<string> GetBalanceAsync(string address)
        {
            return await GetBalanceAsync(address, 0);
        }

        public async Task<string> GetBalanceAsync(string address, long blockHeight)
        {
            await Task.Delay(100);

            //return Convert.ToDecimal((await _web3.Eth.GetBalance.SendRequestAsync(address, "latest")).Value.ToString());

            System.Numerics.BigInteger rtn;

            if (blockHeight <= 0)
            {
                rtn = (await _web3.Eth.GetBalance.SendRequestAsync(address)).Value;
            }
            else
            {
                rtn = (await _web3.Eth.GetBalance.SendRequestAsync(address, blockHeight)).Value;
            }

            return rtn.ToString();
        }
    }
}
