using System;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace YouVote.Models.Plugin.Ioc
{
	public static class TypeResolver
    {
        private static IWindsorContainer _container;

        private static bool _installedOk;

        public static void Install()
        {
            if (_installedOk)
            {
                return;
            }

            _installedOk = true;
            _container = new WindsorContainer();
            _container.Install(
                FromAssembly.This()
            ); 
            _container.Install(
                 FromAssembly.Named("YouVote.Model")
             );

			// specjalnie dla asp.mvc tu ustawiamy controller factory
			var controllerFactory = new WindsorControllerFactory(_container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
 
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static T Resolve<T>(string key)
        {
            return _container.Resolve<T>(key);
        }

        public static object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public static void Release(object instance)
        {
            _container.Release(instance);
        }
    }
}