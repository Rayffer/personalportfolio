using Rayffer.PersonalPortfolio.Interfaces;
using System;
using System.ServiceModel;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Rayffer.PersonalPortfolio.ClientFactories
{
    /// <summary>
    /// Base class for all Service's client factories. This class must be used whenever a connection to
    /// another WFC Service is required to perform requests.
    /// </summary>
    /// <typeparam name="ServiceClientInterface">The type of the Service's client interface.</typeparam>
    /// <typeparam name="ServiceClientClass">The class type which implemets the <typeparamref name="ServiceClientInterface"/> interface.</typeparam>
    /// <seealso cref="Parkare.Lince.Servers.Common.ServiceClient.Interfaces.IClientFactory{ServiceClientClass}" />
    /// <example>
    /// TODO: Example
    /// </example>
    public class ServiceClientFactory<ServiceClientInterface, ServiceClientClass> : IClientFactory<ServiceClientClass>, IDisposable where ServiceClientClass : ICommunicationObject, ServiceClientInterface
                                                                                                                                    where ServiceClientInterface : ICommunicationObject
    {
        #region Fields

        public readonly string RegistrationNameForConstructorWithEndpoint = "EndPointResolution";
        public readonly string RegistrationNameForConstructorWithEndpointAndRemoteAddress = "EndPointRemoteAddressResolution";
        private readonly object lockObject = new object();
        private ServiceClientInterface clientInstance;
        private readonly IUnityContainer container;
        private readonly ITypeLifetimeManager typeLifetimeManager;

        #endregion Fields

        #region Properties

        public string EndpointName { get; }

        public string RemoteAddress { get; }

        #endregion Properties

        #region Constructors

        public ServiceClientFactory(
            string endpointName,
            string remoteAddress,
            IUnityContainer container,
            ITypeLifetimeManager typeLifetimeManager)
        {
            
            EndpointName = endpointName;
            RemoteAddress = remoteAddress;
            this.typeLifetimeManager = typeLifetimeManager;
            this.container = container.CreateChildContainer();

            RegisterServiceClient();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Gets the Service's  client current instance.
        /// </summary>
        /// <returns>The <typeparamref name="ServiceClientClass"/> instance.</returns>
        /// <remarks>
        /// Implementation of this method is imposed by the interface <see cref="IClientFactory{ServiceClientClass}" />.
        /// </remarks>
        public virtual ServiceClientClass GetServiceClientInstance()
        {
            return (ServiceClientClass)InstanceCreation();
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        /// Determines whether the Service's client instance is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the Service's client instance is valid; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// An instance is (not) valid if it has not been created yet, if its communication state is different from
        /// <see cref="System.ServiceModel.CommunicationState.Opened"/> or if it has been disposed.
        /// </remarks>
        protected virtual bool IsInstanceValid()
        {
            if (clientInstance == null)
                return false;

            try
            {
                if (clientInstance.State != CommunicationState.Opened)
                    return false;
            }
            catch (System.ObjectDisposedException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Registers the instantiation for all the Service's client constructor.
        /// </summary>
        /// <remarks>
        /// The ServiceClientInterface is the Interface Type of the Service's client.</typeparam>
        /// The ServiceClientClass is the Class Type which implements the ServiceClientInterface.</typeparam>
        /// 3 Constructor are registered:
        ///  * Without paramenters.
        ///  * With one parameter: Endpoint name.
        ///  * With two parameters: Endpoint name and Remote address.
        /// Uses the <see cref="TypeLifetimeManagerModes"> passed through this class' construtor
        /// to specify the Lifetime scope (Singleton, Normal, etc.) of the future Service's Clients.</param>
        /// </remarks>
        protected virtual void RegisterServiceClient()
        {
            container.RegisterType<
                ServiceClientInterface,
                ServiceClientClass>
                (
                    typeLifetimeManager,
                    new InjectionMember[] { new InjectionConstructor() }

                );
            container.RegisterType<
                ServiceClientInterface,
                ServiceClientClass>
                (
                    RegistrationNameForConstructorWithEndpoint,
                    typeLifetimeManager,
                    new InjectionMember[]
                    {
                        new InjectionConstructor(EndpointName)
                    }
                );

            container.RegisterType<
                ServiceClientInterface,
                ServiceClientClass>
                (
                    RegistrationNameForConstructorWithEndpointAndRemoteAddress,
                    typeLifetimeManager,
                    new InjectionMember[]
                    {
                        new InjectionConstructor(EndpointName, RemoteAddress)
                    }
                );
        }

        /// <summary>
        /// Resolves the Service's client instance from the Injection container.
        /// </summary>
        /// <param name="registrationName">Name of the registration.</param>
        /// <returns>An instance of type <typeparamref name="ServiceClientInterface"/></returns>
        protected virtual ServiceClientInterface ResolveServiceClientInstance(string registrationName = "")
        {
            ServiceClientInterface instance;
            lock (lockObject)
            {
                instance = container.Resolve<ServiceClientInterface>(registrationName);
            }

            return instance;
        }


        #endregion Protected methods

        #region Private Methods

        /// <summary>
        /// This method carries out the Service's client instantiation, controling whether the current
        /// living instance is valid or not. If it is not valid it will resolve a new instance.
        /// </summary>
        /// <returns>The Service's client instance ready to use.</returns>
        private ServiceClientInterface InstanceCreation()
        {
            if (IsInstanceValid())
                return clientInstance;

            if (string.IsNullOrEmpty(EndpointName))
            {
                clientInstance = ResolveServiceClientInstance(string.Empty);
                return clientInstance;
            }
            if (string.IsNullOrEmpty(RemoteAddress))
            {
                clientInstance = ResolveServiceClientInstance(RegistrationNameForConstructorWithEndpoint);
                return clientInstance;
            }

            clientInstance = ResolveServiceClientInstance(RegistrationNameForConstructorWithEndpointAndRemoteAddress);

            return clientInstance;
        }

        #endregion Private Methods

        #region IDisposable Interface Support

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
        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                isDisposed = true;

                if (!isDisposing)
                    return;

                if (clientInstance != null)
                    clientInstance.Close();
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ServiceClientFactory{ServiceClientInterface, ServiceClientClass}"/> class.
        /// </summary>
        ~ServiceClientFactory()
        {
            Dispose(false);
        }

        #endregion IDisposable Interface Support
    }
}