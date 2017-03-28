using System;
using System.Net;
using System.Runtime.Remoting.Messaging;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    var queryParams = req.GetQueryNameValuePairs();

    // parse query parameter
    var a = queryParams
        .FirstOrDefault(q => string.Compare(q.Key, "a", true) == 0)
        .Value;

    var b = queryParams
        .FirstOrDefault(q => string.Compare(q.Key, "b", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set values tp query string or body data
    a = a ?? data?.a;
    b = b ?? data?.b;

    return a == null || b == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass an 'a' and 'b' to add on the query string or in the request body")
        : req.CreateResponse(HttpStatusCode.OK, $"{a} + {b} = {Add(a,b)}");
}

private static double Add(string a, string b)
{
    return Convert.ToDouble(a) + Convert.ToDouble(b); 
}