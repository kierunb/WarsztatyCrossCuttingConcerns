using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace WarsztatyCrossCuttingConcerns.Interceptors
{
    [Serializable]
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine(">>> Before target call");
            try
            {
                invocation.Proceed();
            }
            catch (Exception)
            {
                Console.WriteLine(">>> Target threw an exception!");
                throw;
            }
            finally
            {
                Console.WriteLine(">>> After target call");
            }
        }
    }
}
