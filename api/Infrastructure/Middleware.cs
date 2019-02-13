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
            System.Threading.Thread.Sleep(new System.Random(1).Next(3000));
        }

        private void EndInvoke(HttpContext context)
        {
            // Do custom work after controller execution
        }
    }
}