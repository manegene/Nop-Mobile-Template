using System;
using System.Threading.Tasks;

namespace habahabamall.Helpers
{

    public static class RetryHelper
    {
        public static void Retry(int times, TimeSpan delay, Action operation)
        {
            int attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    System.Console.WriteLine(attempts);
                    operation();
                    break;
                }
                catch (Exception)
                {
                    if (attempts == times)
                    {
                        throw;
                    }

                    Task.Delay(delay).Wait();
                }
            } while (true);
        }
    }
}
