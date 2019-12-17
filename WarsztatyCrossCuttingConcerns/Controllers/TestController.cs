using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WarsztatyCrossCuttingConcerns.ActionFilters;
using WarsztatyCrossCuttingConcerns.Repositories;

namespace WarsztatyCrossCuttingConcerns.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> logger;
        private readonly IEasyCachingProviderFactory factory;

        public TestController(
            ILogger<TestController> logger,
            IEasyCachingProviderFactory factory)
        {
            this.logger = logger;
            this.factory = factory;
        }

        [HttpGet("get-user/{id:int}")]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult GetUser(int id)
        {
            logger.LogInformation(" >>> Pobieram usera");

            var repo = new UserRepository();
            var cache = factory.GetCachingProvider("default");

            var user = cache.Get($"user{id}", () => repo.GetUserById(id), TimeSpan.FromSeconds(60));

            logger.LogInformation(" >>> Pobralem usera");
            return Ok(user);
        }








        [HttpGet("ups")]
        public ActionResult Ups()
        {

            throw new Exception("ups");
            
            return Ok();
        }



        [HttpGet("loguj")]
        public ActionResult Loguj()
        {
            logger.LogInformation(" >>> Moja informacja text");

            var info = new { Imie = "Bartek", Kolor = "Zielony", Numer = 123 }; 

            // przykład strukturyzowanych logów -> tekst bedzie 
            // wzbogacony o metadane objektowe
            logger.LogInformation(" >>> Moja informacja struct {@Info}", info);

            string tekst = $"To jest moj tekst {info}";
            string tekst2 = String.Format("To jest moj tekst {0}", info.Imie);

            try
            {
                throw new Exception("oj");
            }
            catch (Exception ex)
            {

                logger.LogError("Exception: ", ex);
            }

            return Ok();
        }
    }
}
