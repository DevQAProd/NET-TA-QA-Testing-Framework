using System.Net;
using ApplicationName.Core.Api.Extensions;
using ApplicationName.ModelsAndFacilities.Facilities.Builders;
using ApplicationName.ModelsAndFacilities.Models.OperationalModels.ApiModels;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Tests.ApplicationName.Core.Api.Extensions
{
    public class TestsHttpResponseMessageExtensions
    {
        [Test]
        public void ShouldReturnHtmlTextHttpResponseModelFromGoogleCom()
        {
            //GIVEN
            var googleComEndpoint = new HttpEndpoint()
            {
                Method = HttpMethod.Get,
                RequestUri = new AppUriBuilder()
                    .WithScheme(Uri.UriSchemeHttps)
                    .WithHost("www.google.com")
            };

            //WHEN
            var textHttpResponseModel = new HttpRequestMessageBuilder(googleComEndpoint)
                .WithHeaders(
                    new Header("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Mobile Safari/537.36"),
                    new Header("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"))
                .Build()
                .Send()
                .EnsureStatusCode(HttpStatusCode.OK)
                .ToTextHttpResponseModel();

            //THEN
            using (new AssertionScope())
            {
                textHttpResponseModel?.ResponseMessage?.Content?.Headers?.ContentType?.MediaType?.Should().BeEquivalentTo("text/html");
                textHttpResponseModel?.ResponseMessage?.Content?.Headers?.ContentType?.CharSet?.Should().BeEquivalentTo("UTF-8");
                textHttpResponseModel?.TextContent?.Should().StartWithEquivalentOf("<!doctype html>");
            }
        }
    }
}
