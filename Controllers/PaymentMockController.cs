using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MbDotNet;
using MbDotNet.Enums;
using MbDotNet.Exceptions;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using MbDotNet.Models.Responses;
using MockApi.Models;

namespace MockApi.Controllers
{
    [Route("api/PaymentMock")]
    [ApiController]
    public class PaymentMockController : ControllerBase
    {
        MountebankClient _client;
        HttpImposter _imposter;
        public PaymentMockController() : base()
        {
            _client = new MountebankClient();
            _imposter = _client.CreateHttpImposter(8090);
            _client.Submit(_imposter);
        }

        [HttpPost]
        public async Task ReturnOrForward(PaymentDTO pDTO)
        {

            var complexPredicateF = new HttpPredicateFields
            {
                Method = Method.Post,
                Path = "/api/PaymetMock",
                Headers = new Dictionary<string, string>{ { "Accept", "application/json" } }
            };
            var complexPredicate = new EqualsPredicate<HttpPredicateFields>(complexPredicateF);
            var predicateFields = new HttpPredicateFields
            {
                RequestBody = "!test"
            };
            _imposter.AddStub()
                .On(new ContainsPredicate<HttpPredicateFields>(predicateFields))
                .ReturnsStatus(System.Net.HttpStatusCode.OK)
                .On(complexPredicate);

        }
    }
}
