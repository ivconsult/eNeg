
#region → Usings   .
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.eNeg.Common.eNegApps;
using citPOINT.eNeg.ViewModel;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using citPOINT.eNeg.Common;
using System.Windows.Controls;
#endregion

#region → History  .
/* Date         User        Change
 * 
 * 20.03.12    m.Wahab     • creation
 * 
 */
#endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

#endregion

namespace citPOINT.eNeg.Client.Helper
{
    /// <summary>
    /// Initializes Prism to start this quickstart Prism application 
    /// to use Managed Extensibility Framework (MEF).
    /// </summary>
    public class IntializationAppIntegration : MefBootstrapper
    {
        /// <summary>
        /// Gets the shell user interface
        /// </summary>
        /// <value>The shell user interface.</value>
        public ApplicationItegrationShell ShellView { get; set; }

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="DependencyObject"/>, the
        /// <see cref="IntializationAppIntegration"/> will attach the default <seealso cref="Microsoft.Practices.Composite.Regions.IRegionManager"/> of
        /// the application in its <see cref="Microsoft.Practices.Composite.Presentation.Regions.RegionManager.RegionManagerProperty"/> attached property
        /// in order to be able to add regions by using the <seealso cref="Microsoft.Practices.Composite.Presentation.Regions.RegionManager.RegionNameProperty"/>
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<ApplicationItegrationShell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// The base implemention ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            this.ShellView = (ApplicationItegrationShell)this.Shell;

        }

        /// <summary>
        /// Configures the <see cref="AggregateCatalog"/> used by MEF.
        /// </summary>
        /// <remarks>
        /// The base implementation does nothing.
        /// </remarks>
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            // Add this assembly to export ModuleTracker
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IntializationAppIntegration).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MainPlatformInfo).Assembly));
        }

        /// <summary>
        /// Configures the <see cref="CompositionContainer"/>.
        /// May be overwritten in a derived class to add specific type mappings required by the application.
        /// </summary>
        /// <remarks>
        /// The base implementation registers all the types direct instantiated by the bootstrapper with the container.
        /// The base implementation also sets the ServiceLocator provider singleton.
        /// </remarks>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.ComposeExportedValue<IMainPlatformInfo>(MainPlatformInfo.Instance);
        }

        /// <summary>
        /// Creates the <see cref="IModuleCatalog"/> used by Prism.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The base implementation returns a new ModuleCatalog.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog ModuleCatalog = new ModuleCatalog();

            ApplicationViewModel vm = ApplicationViewModel.Instance;

            if (vm != null)
            {
                foreach (var app in vm.eNegApplicationSource)
                {
                    if (!string.IsNullOrEmpty(app.ModuleName) &&
                        !string.IsNullOrEmpty(app.AssemblyName) &&
                        !string.IsNullOrEmpty(app.XapFilePath))
                    {
                        ModuleCatalog.AddModule(new ModuleInfo()
                        {
                            ModuleName = app.ModuleName,
                            InitializationMode = InitializationMode.WhenAvailable,
                            ModuleType = app.AssemblyName, //"HelloWorldModule.HelloWorldModule, HelloWorldModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            Ref = app.XapFilePath,
                            State = ModuleState.NotStarted
                        });
                    }
                }
            }

            return ModuleCatalog;
        }
    }
}
