using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace YouVote.Models.Plugin.Ioc
{
	public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly().
                BasedOn<IController>().LifestyleTransient());

            container.Register(AllTypes.FromThisAssembly()
                                .Where(type => type.Name.EndsWith("DataObject"))
                                .WithServiceDefaultInterfaces()
                                .LifestyleTransient());

        }
    }
}