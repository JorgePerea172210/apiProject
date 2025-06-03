using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace async.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {

            Stopwatch stopwatch = Stopwatch.StartNew();

            Stopwatch.StartNew();

            Thread.Sleep(1000);
            Console.WriteLine("BD conexion.");

            Thread.Sleep(1000);
            Console.WriteLine("Mail enviado");

            Console.WriteLine("Terminado.");

            stopwatch.Stop();

            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Stopwatch.StartNew();

            var task = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("BD conexion.");
                return 16465;
            });

            task.Start();
            Console.WriteLine("Otra cosa");

            var result = await task;

            Console.WriteLine("Terminado");

            stopwatch.Stop();
            return Ok(result + ", Tiempo: "+stopwatch.Elapsed);
        }
    }
}
