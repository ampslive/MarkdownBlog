using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace MarkdownBlog.Functions.Utils
{
    public static class CorsHelper
    {
        public static void AddCorsHeaders(HttpResponseData response)
        {
            response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        }

        public static HttpResponseData CreateCorsPreflightResponse(HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            AddCorsHeaders(response);
            return response;
        }
    }
}
