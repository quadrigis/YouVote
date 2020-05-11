using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NHibernateOrg = NHibernate;
using NHibernate.Cfg;
using YouVote.Common.DesignByContract;

namespace YouVote.Common.NHibernate
{
    public static class NHibernateSession
    {
        /// <summary>
        ///     The default factory key used if only one database is being communicated with.
        /// </summary>
        public static readonly string DefaultFactoryKey = "nhibernate.current_session";

        /// <summary>
        ///     Maintains a dictionary of NHibernate session factories, one per database.  The key is 
        ///     the "factory key" used to look up the associated database, and used to decorate respective
        ///     repositories.  If only one database is being used, this dictionary contains a single
        ///     factory with a key of <see cref = "DefaultFactoryKey" />.
        /// </summary>
        private static readonly Dictionary<string, NHibernateOrg.ISessionFactory> SessionFactories =
            new Dictionary<string, NHibernateOrg.ISessionFactory>();

        private static NHibernateOrg.IInterceptor _registeredInterceptor;

        /// <summary>
        ///     Used to get the current NHibernate session if you're communicating with a single database.
        ///     When communicating with multiple databases, invoke <see cref = "CurrentFor" /> instead.
        /// </summary>
        public static NHibernateOrg.ISession Current
        {
            get
            {
                return CurrentFor(DefaultFactoryKey);
            }
        }

        /// <summary>
        ///     An application-specific implementation of ISessionStorage must be setup either thru
        ///     <see cref = "InitStorage" /> or one of the <see cref = "Init" /> overloads.
        /// </summary>
        public static ISessionStorage Storage { get; set; }


        [CLSCompliant(false)]
        public static Configuration AddConfiguration(
            string factoryKey,
            string[] mappingAssemblies,
            AutoPersistenceModel autoPersistenceModel,
            string cfgFile,
            IDictionary<string, string> cfgProperties,
            string validatorCfgFile,
            IPersistenceConfigurer persistenceConfigurer)
        {
            var config = AddConfiguration(
                factoryKey,
                mappingAssemblies,
                autoPersistenceModel,
                ConfigureNHibernate(cfgFile, cfgProperties),
                validatorCfgFile,
                persistenceConfigurer);

            return config;
        }

        [CLSCompliant(false)]
        public static Configuration AddConfiguration(
            string factoryKey,
            string[] mappingAssemblies,
            AutoPersistenceModel autoPersistenceModel,
            Configuration cfg,
            string validatorCfgFile,
            IPersistenceConfigurer persistenceConfigurer)
        {
            var sessionFactory = CreateSessionFactoryFor(
                mappingAssemblies, autoPersistenceModel, cfg, persistenceConfigurer);

            return AddConfiguration(factoryKey, sessionFactory, cfg, validatorCfgFile);
        }

        [CLSCompliant(false)]
        public static Configuration AddConfiguration(
            string factoryKey, NHibernateOrg.ISessionFactory sessionFactory, Configuration cfg, string validatorCfgFile)
        {
            Check.Require(
                !SessionFactories.ContainsKey(factoryKey),
                "A session factory has already been configured with the key of " + factoryKey);

            SessionFactories.Add(factoryKey, sessionFactory);

            return cfg;
        }

        /// <summary>
        ///     This method is used by application-specific session storage implementations
        ///     and unit tests. Its job is to walk thru existing cached sessions and Close() each one.
        /// </summary>
        public static void CloseAllSessions()
        {
            if (Storage != null)
            {
                foreach (var session in Storage.GetAllSessions())
                {
                    if (session.IsOpen)
                    {
                        session.Close();
                    }
                }
            }
        }

        /// <summary>
        ///     Used to get the current NHibernate session associated with a factory key; i.e., the key 
        ///     associated with an NHibernate session factory for a specific database.
        /// 
        ///     If you're only communicating with one database, you should call <see cref = "Current" /> instead,
        ///     although you're certainly welcome to call this if you have the factory key available.
        /// </summary>
        public static NHibernateOrg.ISession CurrentFor(string factoryKey)
        {
            Check.Require(!string.IsNullOrEmpty(factoryKey), "factoryKey may not be null or empty");
            Check.Require(Storage != null, "An ISessionStorage has not been configured");
            Check.Require(
                SessionFactories.ContainsKey(factoryKey),
                "An ISessionFactory does not exist with a factory key of " + factoryKey);

            var session = Storage.GetSessionForKey(factoryKey);

            if (session == null)
            {
                if (_registeredInterceptor != null)
                {
                    session = SessionFactories[factoryKey].OpenSession(_registeredInterceptor);
                }
                else
                {
                    session = SessionFactories[factoryKey].OpenSession();
                }

                Storage.SetSessionForKey(factoryKey, session);
            }

            return session;
        }

        /// <summary>
        ///     Returns the default ISessionFactory using the DefaultFactoryKey.
        /// </summary>
        public static NHibernateOrg.ISessionFactory GetDefaultSessionFactory()
        {
            return GetSessionFactoryFor(DefaultFactoryKey);
        }

        /// <summary>
        ///     Return an ISessionFactory based on the specified factoryKey.
        /// </summary>
        public static NHibernateOrg.ISessionFactory GetSessionFactoryFor(string factoryKey)
        {
            if (!SessionFactories.ContainsKey(factoryKey))
            {
                return null;
            }

            return SessionFactories[factoryKey];
        }

        public static Configuration Init(ISessionStorage storage, string[] mappingAssemblies)
        {
            return Init(storage, mappingAssemblies, null, null, null, null, null);
        }

        public static Configuration Init(ISessionStorage storage, string[] mappingAssemblies, string cfgFile)
        {
            return Init(storage, mappingAssemblies, null, cfgFile, null, null, null);
        }

        public static Configuration Init(
            ISessionStorage storage, string[] mappingAssemblies, IDictionary<string, string> cfgProperties)
        {
            return Init(storage, mappingAssemblies, null, null, cfgProperties, null, null);
        }

        public static Configuration Init(
            ISessionStorage storage, string[] mappingAssemblies, string cfgFile, string validatorCfgFile)
        {
            return Init(storage, mappingAssemblies, null, cfgFile, null, validatorCfgFile, null);
        }

        [CLSCompliant(false)]
        public static Configuration Init(
            ISessionStorage storage, string[] mappingAssemblies, AutoPersistenceModel autoPersistenceModel)
        {
            return Init(storage, mappingAssemblies, autoPersistenceModel, null, null, null, null);
        }

        [CLSCompliant(false)]
        public static Configuration Init(
            ISessionStorage storage,
            string[] mappingAssemblies,
            AutoPersistenceModel autoPersistenceModel,
            string cfgFile)
        {
            return Init(storage, mappingAssemblies, autoPersistenceModel, cfgFile, null, null, null);
        }

        [CLSCompliant(false)]
        public static Configuration Init(
            ISessionStorage storage,
            string[] mappingAssemblies,
            AutoPersistenceModel autoPersistenceModel,
            IDictionary<string, string> cfgProperties)
        {
            return Init(storage, mappingAssemblies, autoPersistenceModel, null, cfgProperties, null, null);
        }

        [CLSCompliant(false)]
        public static Configuration Init(
            ISessionStorage storage,
            string[] mappingAssemblies,
            AutoPersistenceModel autoPersistenceModel,
            string cfgFile,
            string validatorCfgFile)
        {
            return Init(storage, mappingAssemblies, autoPersistenceModel, cfgFile, null, validatorCfgFile, null);
        }

        [CLSCompliant(false)]
        public static Configuration Init(
            ISessionStorage storage,
            string[] mappingAssemblies,
            AutoPersistenceModel autoPersistenceModel,
            string cfgFile,
            IDictionary<string, string> cfgProperties,
            string validatorCfgFile)
        {
            return Init(
                storage, mappingAssemblies, autoPersistenceModel, cfgFile, cfgProperties, validatorCfgFile, null);
        }

        [CLSCompliant(false)]
        public static Configuration Init(
            ISessionStorage storage,
            string[] mappingAssemblies,
            AutoPersistenceModel autoPersistenceModel,
            string cfgFile,
            IDictionary<string, string> cfgProperties,
            string validatorCfgFile,
            IPersistenceConfigurer persistenceConfigurer)
        {
            InitStorage(storage);
            try
            {
                return AddConfiguration(
                    DefaultFactoryKey,
                    mappingAssemblies,
                    autoPersistenceModel,
                    cfgFile,
                    cfgProperties,
                    validatorCfgFile,
                    persistenceConfigurer);
            }
            catch
            {
                // If this NHibernate config throws an exception, null the Storage reference so 
                // the config can be corrected without having to restart the web application.
                Storage = null;
                throw;
            }
        }

        public static void InitStorage(ISessionStorage storage)
        {
            Check.Require(storage != null, "storage mechanism was null but must be provided");
            Check.Require(Storage == null, "A storage mechanism has already been configured for this application");
            Storage = storage;
        }

        public static bool IsConfiguredForMultipleDatabases()
        {
            return SessionFactories.Count > 1;
        }

        public static void RegisterInterceptor(NHibernateOrg.IInterceptor interceptor)
        {
            Check.Require(interceptor != null, "interceptor may not be null");

            _registeredInterceptor = interceptor;
        }

        public static void RemoveSessionFactoryFor(string factoryKey)
        {
            if (GetSessionFactoryFor(factoryKey) != null)
            {
                SessionFactories.Remove(factoryKey);
            }
        }

        /// <summary>
        ///     To facilitate unit testing, this method will reset this object back to its
        ///     original state before it was configured.
        /// </summary>
        public static void Reset()
        {
            if (Storage != null)
            {
                foreach (var session in Storage.GetAllSessions())
                {
                    session.Dispose();
                }
            }

            SessionFactories.Clear();

            Storage = null;
            _registeredInterceptor = null;
        }

        private static Configuration ConfigureNHibernate(string cfgFile, IDictionary<string, string> cfgProperties)
        {
            var cfg = new Configuration();

            if (cfgProperties != null)
            {
                cfg.AddProperties(cfgProperties);
            }

            if (string.IsNullOrEmpty(cfgFile) == false)
            {
                return cfg.Configure(cfgFile);
            }

            if (File.Exists("Hibernate.cfg.xml"))
            {
                return cfg.Configure();
            }

            return cfg;
        }

        private static NHibernateOrg.ISessionFactory CreateSessionFactoryFor(
            IEnumerable<string> mappingAssemblies,
            AutoPersistenceModel autoPersistenceModel,
            Configuration cfg,
            IPersistenceConfigurer persistenceConfigurer)
        {
            var fluentConfiguration = Fluently.Configure(cfg);

            if (persistenceConfigurer != null)
            {
                fluentConfiguration.Database(persistenceConfigurer);
            }

            fluentConfiguration.Mappings(
                m =>
                    {
                        foreach (var mappingAssembly in mappingAssemblies)
                        {
                            var assembly = Assembly.LoadFrom(MakeLoadReadyAssemblyName(mappingAssembly));

                            m.HbmMappings.AddFromAssembly(assembly);
                            m.FluentMappings.AddFromAssembly(assembly).Conventions.AddAssembly(assembly);
                        }

                        if (autoPersistenceModel != null)
                        {
                            m.AutoMappings.Add(autoPersistenceModel);
                        }
                    });

            fluentConfiguration.ExposeConfiguration(c => ConfigureValidator(c));

            return fluentConfiguration.BuildSessionFactory();
        }

        /// <summary>
        /// http://brendanjerwin.com/development/dotnet/2009/03/11/using-nhibernate-validator-with-fluent-nhibernate.html
        /// </summary>
        /// <param name="nhibernateConfiguration"></param>
        /// <returns></returns>
        private static ValidatorEngine ConfigureValidator(Configuration nhibernateConfiguration)
        {
            NHibernateOrg.Validator.Cfg.Environment.SharedEngineProvider = new NHibernateOrg.Validator.Event.NHibernateSharedEngineProvider();

            var nhvc = new XmlConfiguration();
            nhvc.Properties[NHibernateOrg.Validator.Cfg.Environment.ApplyToDDL] = "true";
            nhvc.Properties[NHibernateOrg.Validator.Cfg.Environment.AutoregisterListeners] = "true";
            nhvc.Properties[NHibernateOrg.Validator.Cfg.Environment.ValidatorMode] = "UseAttribute";

            var ve = new ValidatorEngine();
            ve.Configure(nhvc);
            nhibernateConfiguration.Initialize(ve);

            return ve;
        }

        private static string MakeLoadReadyAssemblyName(string assemblyName)
        {
            return (assemblyName.IndexOf(".dll") == -1) ? assemblyName.Trim() + ".dll" : assemblyName.Trim();
        }
    }
}