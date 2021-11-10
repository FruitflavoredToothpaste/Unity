using System;

namespace MyFramework.Tools
{
    public class Singleton<T> where T : class
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (Equals(instance, null))
                {
                    instance = Activator.CreateInstance<T>();
                }

                return instance;
            }
        }
    }
}