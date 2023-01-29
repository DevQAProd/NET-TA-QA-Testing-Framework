﻿using System.Net;
using ApplicationName.ModelsAndFacilities.Facilities.Interfaces;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.AuthModels;
using Microsoft.Extensions.Primitives;

namespace ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels
{
    public class HttpAuthParameters : AuthParameters, IHttpAuthParameters
    {
        public List<Header> Headers { get; set; }
        public Cookies Cookies { get; set; }
        public Dictionary<string, StringValues> QueryStringKeysValues { get; set; }

        public HttpAuthParameters(NetworkCredential credentials = null, Dictionary<string, object> data = null, List<Header> requestHeaders = null, Cookies cookies = null, Dictionary<string, StringValues> queryStringKeysValues = null)
            : base(credentials, data)
        {
            Headers = requestHeaders;
            Cookies = cookies;
            QueryStringKeysValues = queryStringKeysValues;
        }

        public void ClearHttpAuthParameters()
        {
            Headers?.Clear();
            Cookies?.Clear();
            QueryStringKeysValues?.Clear();
        }
    }
}
