using System.Threading.Tasks;

namespace UnityUtility
{
    public delegate Task<TReturn> AsyncFunc<TReturn>();
    public delegate Task<TReturn> AsyncFunc<T1, TReturn>(T1 arg);
    public delegate Task<TReturn> AsyncFunc<T1, T2, TReturn>(T1 arg1, T2 arg2);
    public delegate Task<TReturn> AsyncFunc<T1, T2, T3, TReturn>(T1 arg1, T2 arg2, T3 arg3);
    public delegate Task<TReturn> AsyncFunc<T1, T2, T3, T4, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    public delegate Task<TReturn> AsyncFunc<T1, T2, T3, T4, T5, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
}
