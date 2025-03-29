using System;
using System.Collections.Generic;
using System.Text;
using Core.Extensions;
using UnityEngine.Networking;
using Console = Core.IO.Console;

namespace Service.Integration
{
    public partial class CommunicationRequest
    {
        public CommunicationRequest()
        {
            method = CommunicationGateway.HttpMethod.Get;
            locale = string.Empty;
            paramter = new Dictionary<string, object>();
            binaryFileList = new List<CommunicationBinaryFileRequest>();
        }

        public Dictionary<string, object> paramter { get; set; }

        public string locale { get; set; }

        public string bearer { get; set; }
        public Action<UnityWebRequest> onSuccess { get; set; }
        public Action<UnityWebRequest> onFaild { get; set; }
        public Action<float, ulong> onDownloadProgress { get; set; }
        public CommunicationGateway.HttpMethod method { get; set; }

        public Uri url { get; set; }

        public List<CommunicationBinaryFileRequest> binaryFileList { get; set; }

        public int dataLength => Encoding.UTF8.GetBytes(queryString).Length;

        public string queryString => CreateQueryData();

        public string queryRawData => CreateQueryData(",");

        public List<IMultipartFormSection> GetFormData()
        {
            var formData = new List<IMultipartFormSection>();
            paramter.ForEach(pair => { formData.Add(new MultipartFormDataSection(pair.Key, pair.Value.ToString())); });
            binaryFileList.ForEach(binaryFile =>
            {
                if (0 < binaryFile?.data?.Length)
                    formData.Add(new MultipartFormFileSection(binaryFile?.fieldName, binaryFile?.data, binaryFile?.fileName, binaryFile?.mimeType));
            });
            return formData;
        }

        public string CreateQueryData(string delimiter = "&")
        {
            var result = new StringBuilder();
            paramter.For((i, pair) =>
            {
                var condition = $"{pair.Key}={pair.Value}";
                if (0 < i)
                    condition = $"{delimiter}{condition}";
                result.Append(condition);
            });
            return result.ToString();
        }

        public string GetAbsoluteUrl()
        {
            var query = queryString ?? string.Empty;
            return query?.IsNullOrEmpty() ?? false ? url?.ToString() : string.Join("?", url?.ToString(), query);
        }

        public void Dump()
        {
            var query = queryRawData;
            if (0 < query.Length)
            {
                if (method == CommunicationGateway.HttpMethod.Get)
                    Console.Info(values: string.Join("?", url, queryString));
                else if (method == CommunicationGateway.HttpMethod.Post)
                    Console.Info(values: string.Join(":", url, GetFormData().ToString()));
            }
            else
            {
                Console.Info(values: url);
            }
        }
    }
}