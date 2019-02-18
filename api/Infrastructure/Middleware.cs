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
            // Do custom work before controller execution
            var rand = new System.Random(1).Next(3000);
            System.Threading.Thread.Sleep(rand);
        }

        private void EndInvoke(HttpContext context)
        {
            // Do custom work after controller execution
        }
    }
}