using System.Security.Principal;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace YouVote.Models.Plugin.Ioc
{
	public class SessionStateInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IIdentity>()
			  .LifeStyle.Transient
			  .UsingFactoryMethod(() => HttpContext.Current.User.Identity));

			container.Register(Component.For<IPrincipal>()
			  .LifeStyle.Transient
			  .UsingFactoryMethod(() => HttpContext.Current.User));

			container.Register(Component.For<HttpSessionStateBase>()
				.LifeStyle.Transient
				.UsingFactoryMethod(() => new HttpSessionStateWrapper(HttpContext.Current.Session)));

			container.Register(Component.For<ISessionState>().ImplementedBy<NewSessionState>()
				.LifeStyle.Transient);
		}

	}
}