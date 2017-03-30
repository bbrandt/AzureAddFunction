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

    var aVal = Convert.ToDouble(a);
    var bVal = Convert.ToDouble(b);

    return a == null || b == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass an 'a' and 'b' to add on the query string or in the request body")
        : req.CreateResponse(HttpStatusCode.OK, new {operation = "Add", a = aVal, b = bVal, result = Add(aVal, bVal) });
}


private static double Add(double a, double b)
{
    return a + b;
}

