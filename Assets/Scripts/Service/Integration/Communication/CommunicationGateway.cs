using System.Collections;
using System.Threading.Tasks;
using Core.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Service.Integration
{
    public class CommunicationGateway
    {
        public enum HttpMethod
        {
            Get,
            Post
        }

        private const int TIME_OUT = 30000;

        private static CommunicationGateway instance { get; set; }
        public UnityWebRequest.Result result { get; private set; }

        private string error { get; set; }
        private CommunicationRequest requestData { get; set; }

        public static CommunicationGateway GetInstance()
        {
            if (null == instance) instance = new CommunicationGateway();
            return instance;
        }

        public IEnumerator SyncRequest(CommunicationRequest request)
        {
            var client = CreateWebRequestClient(request);
            if (null == client)
            {
                result = UnityWebRequest.Result.ConnectionError;
                error = "web request client is null";
                yield break;
            }

            yield return client.SendWebRequest();
            result = client.result;
            error = client.error;
            requestData = request;
            if (client.result == UnityWebRequest.Result.Success)
                request?.onSuccess?.Invoke(client);
            else
                request?.onFaild?.Invoke(client);
            yield return null;
        }

        public async Task AsyncRequest(CommunicationRequest request)
        {
            var client = CreateWebRequestClient(request);
            if (null == client)
            {
                result = UnityWebRequest.Result.ConnectionError;
                error = "web request client is null";
                return;
            }

            await client.SendWebRequest();
            result = client.result;
            error = client.error;
            requestData = request;
            if (client.result == UnityWebRequest.Result.Success)
                request?.onSuccess?.Invoke(client);
            else
                request?.onFaild?.Invoke(client);
        }

        public IEnumerator DownloadRequest(CommunicationRequest request)
        {
            if (null == request)
            {
                result = UnityWebRequest.Result.ConnectionError;
                error = "request is null";
                yield break;
            }

            using (var client = UnityWebRequest.Get(request.GetAbsoluteUrl()))
            {
                var asyncOperation = client.SendWebRequest();

                while (true)
                {
                    if (1 <= asyncOperation.progress)
                    {
                        result = client.result;
                        error = client.error;
                        requestData = request;
                        if (client.result == UnityWebRequest.Result.Success)
                        {
                            request?.onSuccess.Invoke(client);
                            break;
                        }

                        request?.onFaild.Invoke(client);
                        break;
                    }

                    if (client.result == UnityWebRequest.Result.ConnectionError || client.result == UnityWebRequest.Result.ProtocolError || client.result == UnityWebRequest.Result.DataProcessingError)
                    {
                        request?.onFaild.Invoke(client);
                        break;
                    }

                    yield return null;
                    var header = client.GetResponseHeader("Content-Length");
                    ulong.TryParse(header, out var size);
                    request?.onDownloadProgress?.Invoke(asyncOperation.progress, size);
                }
            }

            yield return null;
        }

        public void Dump()
        {
            Console.Info(values: $@"Status:{result.ToString()}/Error:{error}");
            requestData?.Dump();
        }

        private UnityWebRequest CreateWebRequestClient(CommunicationRequest request)
        {
            UnityWebRequest client = null;
            switch (request?.method)
            {
                case HttpMethod.Get:
                    client = UnityWebRequest.Get(request.GetAbsoluteUrl());
                    break;
                case HttpMethod.Post:
                    var formData = request?.GetFormData();
                    client = UnityWebRequest.Post(request?.url, formData);
                    break;
            }

            if (null == client) return null;
            client.timeout = TIME_OUT;
            client.SetRequestHeader("Accept", "text/html,application/xhtml+xml,application/xml,application/json;q=0.9,*/*;q=0.8");
            client.SetRequestHeader("Authorization", request?.bearer ?? string.Empty);
            client.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            client.SetRequestHeader("Accept-Language", request?.locale ?? string.Empty);
            return client;
        }
    }
}