namespace UnityUtility
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Casts the given object to the given type
        /// </summary>
        /// <remarks>
        /// The return value will be null is the cast failed
        /// </remarks>
        /// <typeparam name="TCast">Type to cast the given object to</typeparam>
        /// <param name="obj">Object to cast</param>
        /// <returns><paramref name="obj"/> casted to type <typeparamref name="TCast"/></returns>
        public static TCast Cast<TCast>(this object obj)
        {
            return (TCast)obj;
        }

        /// <summary>
        /// Tries to cast the given object to the given type
        /// </summary>
        /// <typeparam name="TCast">Type to cast the given object to</typeparam>
        /// <param name="obj">Object to cast</param>
        /// <returns>Whether the cast was successful</returns>
        public static bool TryCast<TCast>(this object obj, out TCast castObject)
        {
            castObject = obj.Cast<TCast>();
            return castObject != null;
        }
    }
}
