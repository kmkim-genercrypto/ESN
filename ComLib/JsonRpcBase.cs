using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nethereum.JsonRpc.Client;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace ComLib
{
    /// <summary>
    /// rpc 추상 클래스
    /// </summary>
    public abstract class JsonRpcBase : IDisposable
    {
        #region config
        static string _rpcHost = "127.0.0.1";
        static string _rpcPort = "13090";
        #endregion

        bool _disposed = false;

        public TimeSpan ConnectionTimeout { get; set; } = TimeSpan.FromSeconds(20.0);

        private HttpClient _httpClient;

        public JsonRpcBase(IConfiguration configuration)
        {
            _rpcHost = configuration.GetSection("rpc:host").Value;
            _rpcPort = configuration.GetSection("rpc:port").Value;

            _httpClient = new HttpClient();
        }

        ~JsonRpcBase()
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
                _httpClient.Dispose();
            }

            _disposed = true;
        }

        protected async Task<string> RpcAsync(string method, params object[] ps)
        {
            JObject joe = new JObject();

            try
            {
                joe.Add(new JProperty("jsonrpc", "2.0"));
                joe.Add(new JProperty("id", 1));
                joe.Add(new JProperty("method", method));

                if (ps.Length > 0)
                {
                    JArray props = new JArray();

                    for (int i = 0; i < ps.Length; i++)
                        props.Add(ps[i]);

                    joe.Add(new JProperty("params", props));
                }

                string s = JsonConvert.SerializeObject(joe);

                var httpContent = new StringContent(s, Encoding.UTF8, "application/json");

                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(ConnectionTimeout);

                var httpResponseMessage = await _httpClient.PostAsync($"http://{_rpcHost}:{_rpcPort}", httpContent, cancellationTokenSource.Token)
                    .ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();

                var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                using (var streamReader = new StreamReader(stream))
                using (var reader = new JsonTextReader(streamReader))
                {
                    var serializer = JsonSerializer.Create();
                    var message = serializer.Deserialize<JObject>(reader);                    

                    return message.ToString();
                }
            }
            catch (TaskCanceledException ex)
            {
                throw new RpcClientTimeoutException($"Rpc timeout after {ConnectionTimeout.TotalMilliseconds} milliseconds", ex);
            }
            catch (Exception ex)
            {
                throw new RpcClientUnknownException("Error occurred when trying to send rpc requests(s)", ex);
            }
        }
    }
}
