using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace VehicleTracker.IntegrationTests
{
    public abstract class IntegrationTestBase: IClassFixture<BaseTest>
    {
        public readonly BaseTest _factory;
        public readonly HttpClient _client;
        public IntegrationTestBase(BaseTest fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }
    }
}