using System;
using System.ServiceModel;

namespace Rayffer.PersonalPortfolio.Interfaces
{
    public interface IClientManager<CommunicationClass> where CommunicationClass : ICommunicationObject
    {
        TResult ExecuteClientAction<TResult>(Func<CommunicationClass, TResult> functionToExecute);

        TResult ExecuteClientAction<TResult>(Func<CommunicationClass, TResult> functionToExecute, bool mustOpenConnection = true, bool mustCloseConnection = true);
    }
}