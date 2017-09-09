using EdgeJs;
using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace EdgeTest
{
    [Serializable]
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            if (args.Length > 0 && args[0] == "test")
            {
                p.Run();
            }
            else
            {
                p.RunInAppDomain();
            }
        }

        public void Run()
        {
            Test().Wait();
        }

        public void RunInAppDomain()
        {
            var assemblyFilename = typeof(Program).Assembly.Location;
            RunExeInAppDomain(assemblyFilename);
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

        static void RunExeInAppDomain(string assemblyFilename)
        {
            // Create an Application Domain:
            System.AppDomain newDomain = System.AppDomain.CreateDomain("NewApplicationDomain");

            // Load and execute an assembly:
            newDomain.ExecuteAssembly(assemblyFilename,new[] { "test" });

            // Unload the application domain:
            System.AppDomain.Unload(newDomain);
        }
    }
}
