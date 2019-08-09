using Rayffer.PersonalPortfolio.Interfaces;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace Rayffer.PersonalPortfolio.Managers
{
    /// <summary>
    /// Base clas for WFC service client implementation
    /// </summary>
    /// <typeparam name="CommunicationClass">This is the class type of the WCF Service this class implement the Service's client for.</typeparam>
    /// <seealso cref="Rayffer.PersonalPortfolio.Interfaces.IClientManager{T}" />
    /// <seealso cref="System.IDisposable" />
    public class ClientManager<CommunicationClass> : IClientManager<CommunicationClass>, IDisposable where CommunicationClass : ICommunicationObject
    {
        #region Properties and fields

        private readonly object lockObject = new object();
        private readonly IClientFactory<CommunicationClass> clientFactory;
        private readonly CommunicationState ReadyToOpen = CommunicationState.Closed | CommunicationState.Closing;
        private readonly CommunicationState ReadyToClose = CommunicationState.Opened | CommunicationState.Opening;

        private CommunicationClass clientInstance = default(CommunicationClass);

        /// <summary>
        /// Gets or sets the services' client instance.
        /// </summary>
        /// <value>
        /// The client instance.
        /// </value>
        protected CommunicationClass ClientInstance
        {
            get
            {
                lock (lockObject)
                {
                    if (clientInstance == null)
                        clientInstance = clientFactory.GetServiceClientInstance();
                }
                return clientInstance;
            }
            set
            {
                lock (lockObject)
                {
                    clientInstance = value;
                }
            }
        }

        #endregion Properties and fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientManager{T}"/> class.
        /// </summary>
        /// <param name="clientFactory">The Service's Client Factory.</param>
        /// <param name="tracingFactory">The Tracing Factory.</param>
        public ClientManager(IClientFactory<CommunicationClass> clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        #endregion Constructors

        #region IClientManager interface Methods

        /// <summary>
        /// Executes a client function.
        /// </summary>
        /// <typeparam name="TResult">Type of resulting value.</typeparam>
        /// <param name="functionToExecute">The function to to executed.</param>
        /// <returns>
        /// The {TResult} type value the function returns.
        /// </returns>
        /// <remarks>
        /// This must be used as foundation for any Client's method call.
        /// It is a caller responsability to open and to close the connection upon this method call.
        /// </remarks>
        public TResult ExecuteClientAction<TResult>(Func<CommunicationClass, TResult> functionToExecute)
        {
            return this.ExecuteClientAction(functionToExecute, false, false);
        }

        public TResult ExecuteClientAction<TResult>(Func<CommunicationClass, TResult> functionToExecute, bool mustOpenConnection = true, bool mustCloseConnection = true)
        {
            TResult result = default(TResult);

            try
            {
                if (mustOpenConnection)
                    OpenConnection();

                result = ExecuteRequest(functionToExecute);
            }
            catch (Exception ex)
            {
                this.WriteLogError(ex, $"Error in {new StackFrame(1).GetMethod().Name} while executing the request");
            }
            finally
            {
                if (mustCloseConnection)
                    CloseConnection();
            }

            return result;
        }

        #endregion IClientManager interface Methods

        #region Private Methods

        /// <summary>
        /// Executes the client Request.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="functionToExecute">The function to be executed.</param>
        /// <param name="serviceClient">The Service's client instance.</param>
        /// <returns>An instance of the type specified by <typeparamref name="TResult"/>.</returns>
        private TResult ExecuteRequest<TResult>(Func<CommunicationClass, TResult> functionToExecute)
        {
            try
            {
                var result = (TResult)functionToExecute(ClientInstance);
                WriteLogInformation($"{functionToExecute?.ToString()} function executed.");
                return result;
            }
            catch (Exception ex)
            {
                this.WriteLogError(ex, $"Error in {new StackFrame(1).GetMethod().Name} while executing the {functionToExecute?.ToString()} function.");
            }

            return default(TResult);
        }

        /// <summary>
        /// Opens the connection after verifying the  correct Service's client state.
        /// </summary>
        private void OpenConnection()
        {
            lock (lockObject)
            {
                if (this.ReadyToOpen.HasFlag(ClientInstance.State))
                {
                    try
                    {
                        ClientInstance.Open();
                        WriteLogInformation("The connection with the Service's client was opened.");
                    }
                    catch (Exception ex)
                    {
                        this.WriteLogError(ex, $"Error in {new StackFrame(1).GetMethod().Name} while opening the connection to the service.");
                    }
                }
            }
        }

        /// <summary>
        /// Closes the connection after verifying the  correct Service's client state.
        /// </summary>
        private void CloseConnection()
        {
            lock (lockObject)
            {
                if (this.ReadyToClose.HasFlag(ClientInstance.State))
                {
                    try
                    {
                        WriteLogInformation("Clossing the Service's client... {0}.", nameof(ClientInstance.State));
                        ClientInstance.Close();
                        WriteLogInformation("Service's client closed.");
                    }
                    catch (Exception ex)
                    {
                        this.WriteLogError(ex, $"Error in {new StackFrame(1).GetMethod().Name} while closing the connection to the service.");
                    }
                }
            }
        }

        #endregion Private Methods

        #region IDisposable interface support

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool isDisposed = false; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                isDisposed = true;

                if (!isDisposing)
                    return;

                /// Dispose managed state (managed objects).
                /// Free unmanaged resources (unmanaged objects) and override a Finalizer below.
                /// Set large fields to null.

                WriteLogInformation("The Client's Manager signaled to be disposed");

                CloseConnection();

                WriteLogInformation("The Client's Manager have been disposed.");
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ClientManager"/> class.
        /// </summary>
        ~ClientManager()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        #endregion IDisposable interface support
    }
}