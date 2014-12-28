using System.Web;
using Microsoft.Practices.Unity;
using Unity.WebForms;
using System.Reflection;
using System.Collections.Generic;

[assembly: WebActivator.PostApplicationStartMethod(typeof(IbaMonitoring.App_Start.UnityWebFormsStart), "PostStart")]
namespace IbaMonitoring.App_Start
{
    /// <summary>
    ///		Startup class for the Unity.WebForms NuGet package.
    /// </summary>
    internal static class UnityWebFormsStart
    {
        /// <summary>
        ///     Initializes the unity container when the application starts up.
        /// </summary>
        /// <remarks>
        ///		Do not edit this method. Perform any modifications in the
        ///		<see cref="RegisterDependencies" /> method.
        /// </remarks>
        internal static void PostStart()
        {
            IUnityContainer container = new UnityContainer();
            HttpContext.Current.Application.SetContainer(container);

            RegisterDependencies(container);
        }

        /// <summary>
        ///		Registers dependencies in the supplied container.
        /// </summary>
        /// <param name="container">Instance of the container to populate.</param>
        private static void RegisterDependencies(IUnityContainer container)
        {
            var assemblyList = new List<Assembly>()
            {
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(safnet.iba.IdentityMap))
            };

            container.RegisterTypes(
               AllClasses.FromAssemblies(assemblyList),
               WithMappings.FromMatchingInterface,
               WithName.Default,
               WithLifetime.ContainerControlled
            );
        }
    }
}