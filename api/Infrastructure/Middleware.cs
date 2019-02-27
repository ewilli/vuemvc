using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace api.Infrastructure
{
    public class HeavyLoadMiddleware
    {
        private readonly RequestDelegate next;
        private readonly int maxLoadMillisec;
        private bool genRandomError;

        public HeavyLoadMiddleware(RequestDelegate next, int maxLoadMillisec = 3000, bool genRandomError = false)
        {
            Debug.Print("Start emulating heavy load...");
            this.next = next;
            this.maxLoadMillisec = maxLoadMillisec;
            this.genRandomError = genRandomError;
        }

        public async Task Invoke(HttpContext context)
        {
            this.BeginInvoke(context);
            await this.next.Invoke(context);
            this.EndInvoke(context);
        }

        private void BeginInvoke(HttpContext context)
        {
            var rand = new System.Random().Next(maxLoadMillisec);
            System.Threading.Thread.Sleep(rand);
            if (genRandomError && rand < (maxLoadMillisec / 8))
                throw new System.Exception("bad luck!");
        }

        private void EndInvoke(HttpContext context)
        {
            // Do custom work after controller execution
        }
    }
}