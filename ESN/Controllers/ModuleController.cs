using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComLib;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ESN.Controllers
{
    [Route("api")]
    [ApiController]
    public class ModuleController : Controller
    {
        private readonly IAccountBiz _aBiz;
        private readonly IProxyBiz _pBiz;

        public ModuleController(IAccountBiz aBiz, IProxyBiz pBiz)
        {
            _aBiz = aBiz;
            _pBiz = pBiz;
        }

        // GET api/5
        [HttpGet]
        public async Task<ActionResult<string>> Get(string module, string action, string address, string tag, string startblock, string endblock, string sort, string to, string value, string gasPrice, string gas, string apikey)
        {
            ActionResult<string> rtn = Json("");

            if (!apikey.Equals("geminis"))
            {
                return rtn = Json("error");
            }

            switch($"{module}:{action}")
            {
                case "account:txlistinternal":
                    rtn = GetTxListInternal(address, startblock, endblock, sort);
                    break;

                case "account:txlist":
                    rtn = GetTxList(address, startblock, endblock, sort);
                    break;

                case "account:balance":
                    rtn = await _aBiz.GetBalanceAsync(address, "latest");
                    break;

                case "account:balancemulti":
                    rtn = await _aBiz.GetBalanceMultiAsync(address, tag);
                    break;

                case "stats:ethprice":
                    rtn = GetEthPrice();
                    break;

                case "proxy:eth_gasPrice":
                    rtn = await _pBiz.GetGasPriceAsync();
                    break;

                case "proxy:eth_estimateGas":
                    rtn = await _pBiz.GetEstimateGasAsync(to, gas, gasPrice, value);
                    break;

                case "proxy:eth_getTransactionCount":
                    rtn = await _pBiz.GetTransactionCountAsync(address, tag);
                    break;

                default:
                    break;
            }

            return rtn;
        }

        // GET api/5
        [HttpPost]
        public async Task Post([FromBody] string module, string action, string hex, string apikey)
        {
            string rtn;

            if ($"{module}:{action}:{apikey}".Equals("proxy:eth_sendRawTransaction:geminis"))
            {
                rtn = await _pBiz.SendRawTransactionAsync(hex);
            }
        }

        /// <summary>
        /// [BETA] Get a list of 'Internal' Transactions by Address
        /// </summary>
        /// <param name="address"></param>
        /// <param name="startblock"></param>
        /// <param name="endblock"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        private JsonResult GetTxListInternal(string address, string startblock, string endblock, string sort)
        {
            //module=account&action=txlistinternal&address=" + address + "&startblock=0&endblock=99999999&sort=asc&apikey=

            return Json(new
            {
                status = "1",
                message = "OK",
                result = new object[] { new {
                    blockNumber = "54092",
                    timeStamp = "1439048640",
                    hash = "0x9c81f44c29ff0226f835cd0a8a2f2a7eca6db52a711f8211b566fd15d3e0e8d4",
                    from = "0x5abfec25f74cd88437631a7731906932776356f9",
                    to = "",
                    value = "0",
                    contractAddress = "0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae",
                    input = "",
                    type = "create",
                    gas = "1436963",
                    gasUsed = "1436963",
                    traceId = "0",
                    isError = "0",
                    errCode = ""
                },
                new {
                    blockNumber = "54092",
                    timeStamp = "1439048640",
                    hash = "0x9c81f44c29ff0226f835cd0a8a2f2a7eca6db52a711f8211b566fd15d3e0e8d4",
                    from = "0x5abfec25f74cd88437631a7731906932776356f9",
                    to = "0x20d42f2e99a421147acf198d775395cac2e8b03d",
                    value = "11901464239480000000000000",
                    contractAddress = "0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae",
                    input = "",
                    type = "call",
                    gas = "1436963",
                    gasUsed = "1436963",
                    traceId = "0",
                    isError = "0",
                    errCode = ""
                } }
            });
        }

        private JsonResult GetTxList(string address, string startblock, string endblock, string sort)
        {
            //module=account&action=txlist&address=" + address + "&startblock=0&endblock=99999999&sort=asc&apikey=

            return Json(new { status = "1", message = "OK", result = new object[] { new {
                    blockNumber = "54092",
                    timeStamp = "1439048640",
                    hash = "0x9c81f44c29ff0226f835cd0a8a2f2a7eca6db52a711f8211b566fd15d3e0e8d4",
                    nonce = "0",
                    blockHash = "0xd3cabad6adab0b52eb632c386ea194036805713682c62cb589b5abcd76de2159",
                    transactionIndex = "0",
                    from = "0x5abfec25f74cd88437631a7731906932776356f9",
                    to = "",
                    value = "11901464239480000000000000",
                    gas = "2000000",
                    gasPrice = "10000000000000",
                    isError = "0",
                    txreceipt_status = "",
                    input = "0x6060604052600760018181557305096a47749d8bfab0a",
                    contractAddress = "0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae",
                    cumulativeGasUsed = "1436963",
                    gasUsed = "1436963",
                    confirmations = "6359110"
                },
                new {
                    blockNumber = "65204",
                    timeStamp = "1439232889",
                    hash = "0x98beb27135aa0a25650557005ad962919d6a278c4b3dde7f4f6a3a1e65aa746c",
                    nonce = "0",
                    blockHash = "0x373d339e45a701447367d7b9c7cef84aab79c2b2714271b908cda0ab3ad0849b",
                    transactionIndex = "0",
                    from = "0x3fb1cd2cd96c6d5c0b5eb3322d807b34482481d4",
                    to = "0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae",
                    value = "0",
                    gas = "122261",
                    gasPrice = "50000000000",
                    isError = "0",
                    txreceipt_status = "",
                    input = "0xf00d4b5d000000000000000000000000036c8cecce8d8bbf0831d840d7f29c9e3ddefa63000000000000000000000000c5a96db085dda36ffbe390f455315d30d6d3dc52",
                    contractAddress = "",
                    cumulativeGasUsed = "122207",
                    gasUsed = "122207",
                    confirmations = "6347998"
                } } });
        }

        private JsonResult GetEthPrice()
        {
            //module=stats&action=ethprice&apikey=

            return Json(new object[] { new { Id = "111", address = "0x01bfb11299066220e5066f7eb8d42c3f03864d72", amountSatoshi = 100 }
                , new { Id = "222", address = "0x01bfb11299066220e5066f7eb8d42c3f03864d72", amountSatoshi = 112 }
                , new { Id = "333", address = "0x01bfb11299066220e5066f7eb8d42c3f03864d72", amountSatoshi = 123 }});
        }
    }
}
