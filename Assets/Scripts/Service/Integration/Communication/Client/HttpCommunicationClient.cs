//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Service.Integration.Communication.Entity;
using UnityEngine;

namespace Service.Integration.Communication.Client
{
    public sealed class HttpCommunicationClient : BaseCommunicationClient
    {
        private const int TIME_OUT = 10000;

        public override CommunicationResponse Request(CommunicationRequest parameter)
        {
            var response = new CommunicationResponse();
            try
            {
                var requestUrl = parameter.GetURL();
                var request = HttpWebRequest.Create(requestUrl) as HttpWebRequest;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.ContentType = "application/x-AsyncWebClient";
                request.Method = parameter.methodName;
                request.KeepAlive = false;
                request.ContentLength = parameter.dataLength;
                request.UserAgent = userAgent.Create();
                request.Timeout = TIME_OUT;
                if (parameter.methodName.Equals("POST"))
                {
                    var streamRequest = request.GetRequestStream();
                    using (var byteWriter = new BinaryWriter(streamRequest))
                    {
                        byteWriter.Write(parameter.GetQueryString());
                        byteWriter.Flush();
                        byteWriter.Close();
                    }
                }

                using (var webresponse = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = webresponse.GetResponseStream())
                    {
                        if (Regex.IsMatch(requestUrl, @"(\.html$|\.js$|\.xml|\.css|\.txt$)"))
                            using (var reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                var text = reader.ReadToEnd();
                                response.data = text;
                                reader.Close();
                            }
                        else
                            using (var reader = new BinaryReader(stream))
                            {
                                response.data = reader.ReadBytes(1024 * 1000 * 10);
                                reader.Close();
                            }

                        stream.Close();
                    }

                    webresponse.Close();
                }

                response.status = CommunicationResponse.ResponseStatus.SUCCESS;
                response.request = parameter;
                return response;
            }
            catch (WebException we)
            {
                response.data = null;
                response.status = CommunicationResponse.ResponseStatus.FAILD;
                response.request = parameter;
                response.message = we.Message;
                Debug.LogError(we.Message);
            }

            return response;
        }
    }
}