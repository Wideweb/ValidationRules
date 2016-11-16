using System.Reflection;
using System.Threading.Tasks;

namespace Validation
{
    public static class TypeSystem
    {
        public static Task<T> InvokeMethod<T>(MethodInfo method, object obj, object[] parameters)
        {
            var result = method.Invoke(obj, parameters);

            if (result is Task)
            {
                return (Task<T>)result;
            }

            return Task.FromResult((T)result);
        }
    }
}
