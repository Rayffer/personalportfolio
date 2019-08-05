using System;
using System.ComponentModel;

namespace Rayffer.PersonalPortfolio.Extensions
{
    public static class ThreadSafeInvokeExtension
    {
        /// <summary>
        /// This methods allows modifying or updating controls created out of the current scope in a safe manner
        /// </summary>
        /// <typeparam name="T">The control type to modify or update</typeparam>
        /// <param name="objectToModify">The control to modify, this type is constrained to a type that implements <seealso cref="ISynchronizeInvoke"/> which tipically is implemented by controls</param>
        /// <param name="actionToPerform">The action to perform on the control</param>
        public static void SafeInvoke<T>(this T objectToModify, Action<T> actionToPerform) where T : ISynchronizeInvoke
        {
            if (objectToModify.InvokeRequired)
                objectToModify.Invoke(actionToPerform, new object[] { objectToModify });
            else
                actionToPerform(objectToModify);
        }
    }
}