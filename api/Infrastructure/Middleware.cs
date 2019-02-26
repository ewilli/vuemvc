using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace api.Infrastructure
{
    public class HeavyLoadMiddleware
    {
        private readonly RequestDelegate next;

        public HeavyLoadMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            this.BeginInvoke(context);
            await this.next.Invoke(context);
            this.EndInvoke(context);
        }

        private void BeginInvoke(HttpContext context)
        {
            var rand = new System.Random().Next(3000);
            System.Threading.Thread.Sleep(rand);
            // if (rand < 500)
            //     throw new System.Exception("Pech gehapt!");
        }

        private void EndInvoke(HttpContext context)
        {
            // Do custom work after controller execution
        }
    }
}