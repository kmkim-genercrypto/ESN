using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ComLib;

namespace BizLib
{
    public sealed class AccountBiz : BizBase, IAccountBiz
    {
        bool _disposed = false;

        private readonly IRpcDac _esnDac;

        public AccountBiz(IRpcDac esnDac)
        {
            _esnDac = esnDac;
        }

        ~AccountBiz()
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

        /// <summary>
        /// [BETA] Get a list of 'Internal' Transactions by Address
        /// </summary>
        /// <param name="address"></param>
        /// <param name="startblock"></param>
        /// <param name="endblock"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<string> GetTxListInternal(string address, string startblock, string endblock, string sort)
        {
            //module=account&action=txlistinternal&address=" + address + "&startblock=0&endblock=99999999&sort=asc&apikey=

            return "";
        }

        public async Task<string> GetTxList(string address, string startblock, string endblock, string sort)
        {
            //module=account&action=txlist&address=" + address + "&startblock=0&endblock=99999999&sort=asc&apikey=

            return "";
        }

        public async Task<string> GetBalanceAsync(string address, string tag)
        {
            //module=account&action=balance&address=" + address + "&apikey=

            return await _esnDac.GetBalanceAsync(address, tag);
        }

        public async Task<string> GetBalanceMultiAsync(string address, string tag)
        {
            //module=account&action=balancemulti&address=" + address + "&tag=latest&apikey=

            string[] arr = address.Split(",");

            JObject joe = new JObject();
            joe.Add(new JProperty("status", "1"));
            joe.Add(new JProperty("message", "OK"));

            JArray aRtn = new JArray();
            foreach(string add in arr)
            {
                aRtn.Add(new JObject(new JProperty("account", add), new JProperty("balance", JObject.Parse(await _esnDac.GetBalanceAsync(add, tag))["result"].ToString())));
            }

            joe.Add(new JProperty("result", aRtn));

            return joe.ToString();
        }
    }
}
