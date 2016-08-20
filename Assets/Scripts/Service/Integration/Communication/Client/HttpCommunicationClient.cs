//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Service.Integration.Communication;
using Service.Integration.Communication.Entity;
namespace Service.Integration.Communication.Client {
public sealed class HttpCommunicationClient : BaseCommunicationClient {
    private const int TIME_OUT = 10000;
    public override CommunicationResponse Request(CommunicationRequest parameter) {
        CommunicationResponse response = new CommunicationResponse();
        try {
            string requestUrl = parameter.GetURL();
            HttpWebRequest request = HttpWebRequest.Create(requestUrl) as HttpWebRequest;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.ContentType = "application/x-AsyncWebClient";
            request.Method = parameter.methodName;
            request.KeepAlive = false;
            request.ContentLength = parameter.dataLength;
            request.UserAgent = this.userAgent.Create();
            request.Timeout = HttpCommunicationClient.TIME_OUT;
            if (parameter.methodName.Equals("POST")) {
                Stream streamRequest = request.GetRequestStream();
                using(BinaryWriter byteWriter = new BinaryWriter(streamRequest)) {
                    byteWriter.Write(parameter.GetQueryString());
                    byteWriter.Flush();
                    byteWriter.Close();
                }
            }
            using(System.Net.HttpWebResponse webresponse = (HttpWebResponse)request.GetResponse()) {
                using(System.IO.Stream stream = webresponse.GetResponseStream()) {
                    if (Regex.IsMatch(requestUrl, @"(\.html$|\.js$|\.xml|\.css|\.txt$)")) {
                        using(System.IO.StreamReader reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8)) {
                            string text = reader.ReadToEnd();
                            response.data = text;
                            reader.Close();
                        }
                    } else {
                        using(System.IO.BinaryReader reader = new System.IO.BinaryReader(stream)) {
                            response.data = reader.ReadBytes(1024 * 1000 * 10);
                            reader.Close();
                        }
                    }
                    stream.Close();
                }
                webresponse.Close();
            }
            response.status = CommunicationResponse.ResponseStatus.SUCCESS;
            response.request = parameter;
            return response;
        } catch (WebException we) {
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