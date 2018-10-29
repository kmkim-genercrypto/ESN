using System;
using System.Threading.Tasks;
using ComLib;
using Newtonsoft.Json.Linq;

namespace BizLib
{
    public sealed class ProxyBiz : BizBase, IProxyBiz
    {
        bool _disposed = false;

        private readonly IRpcDac _esnDac;

        public ProxyBiz(IRpcDac esnDac)
        {
            _esnDac = esnDac;
        }

        ~ProxyBiz()
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

            if (disposing)
            {
                _esnDac?.Dispose();
            }

            base.Dispose(disposing);

            _disposed = true;
        }

        public async Task<string> GetGasPriceAsync()
        {
            return await _esnDac.GetGasPriceAsync();
        }

        public async Task<string> GetTransactionCountAsync(string address, string tag)
        {
            return await _esnDac.GetTransactionCountAsync(address, tag);
        }

        /// <summary>
        /// * from : String - (선택 사항) 보내는 계정의 주소입니다.
        /// * to : String - 트랜잭션이 전달되는 주소입니다.
        /// * gas : HexBigInteger - (선택 사항) 트랜잭션 실행을 위해 제공된 가스의 정수값입니다.
        /// * gasPrice : HexBigInteger - (선택 사항) 이 거래의 가스 가격은 기본적으로 평균 네트워크 가스 가격입니다.
        /// * value : HexBigInteger - (선택 사항) 트랜잭션과 함께 전송 된 값입니다.
        /// * data : String - (옵션) 메서드 서명과 인코딩 된 매개 변수의 해시입니다.
        /// * 수량 혹은 문자열 - "latest", "earliest", "pending"
        /// </summary>
        /// <param name="address"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<string> GetEstimateGasAsync(string to, string gas = null, string gasPrice = null, string value = null)
        {
            JObject joe = new JObject();
            joe.Add(new JProperty("to", to));

            if (!(gas is null))
            {
                joe.Add(new JProperty("gas", gas));
            }

            if (!(gasPrice is null))
            {
                joe.Add(new JProperty("gasPrice", gasPrice));
            }

            if (!(value is null))
            {
                joe.Add(new JProperty("value", value));
            }

            return await _esnDac.GetEstimateGasAsync(joe);
        }

        public async Task<string> SendRawTransactionAsync(string hex)
        {
            return await _esnDac.SendRawTransactionAsync(hex);
        }
    }
}
