﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FederationGatewayApi.Contracts;
using FederationGatewayApi.Services;
using Moq;
using Moq.Protected;
using NUnit.Framework;


namespace DIGNDB.App.SmitteStop.Testing.ServiceTest.Gateway
{
    public class WebContextReaderTests
    {

        public IGatewayWebContextReader _webContextReader { get; set; }
       
        public WebContextMock _webContextMock { get; set; }

        [SetUp]
        public void Init()
        {
            _webContextReader = new GatewayWebContextReader();
            _webContextMock = new WebContextMock();
        }

        [Test]
        public void ReaderReadsObjectsBasedOnJSON()
        {

            var mockResponseBody = _webContextMock.MockValidBodyJSON();
            var keys = _webContextReader.GetItemsFromRequest(mockResponseBody);
            Assert.That(keys.Count > 0);

        }

        [Test]
        public void ReaderReadsTheJSONContentFromHTTPRequest()
        {
            var mockResponse = _webContextMock.MockHttpResponse();
            var response = _webContextReader.ReadHttpContextStream(mockResponse);
            Assert.AreEqual(response, _webContextMock.MockValidBodyJSON());
        }
    
    }
}
