using System.Threading.Tasks;

namespace UnityUtility
{
    public delegate Task AsyncAction();
    public delegate Task AsyncAction<T>(T arg);
    public delegate Task AsyncAction<T1, T2>(T1 arg1, T2 arg2);
    public delegate Task AsyncAction<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
    public delegate Task AsyncAction<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    public delegate Task AsyncAction<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
}
