using EdgeJs;
using System;
using System.Threading.Tasks;

namespace EdgeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {
            Test().Wait();
        }

        public static async Task Test()
        {
            var onMessage = (Func<object, Task<object>>)( (message) =>
            {
                Console.WriteLine($"Got this message: {message}");
                return Task.FromResult((object)"ok");
            });

            var func = Edge.Func(@"
            return function (params, callback) {

                params.onMessage('!!_!! This is cool', function(error, result) {});
                callback(null, 'Node.js welcomes ' + params.message);
            }
        ");
            
            Console.WriteLine(await func(new { message = "YES", onMessage = onMessage }));
        }
    }
}
