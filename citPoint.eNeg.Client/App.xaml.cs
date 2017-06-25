#region → Usings   .
using citPOINT.eNeg.Common;
using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Browser;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using citPOINT.eNeg.Infrastructure.Logging;
using System.IO;
using citPOINT.eNeg.ViewModel;
using citPOINT.eNeg.Common.eNegApps;
using citPOINT.eNeg.Apps.Common.Enums;
using Telerik.Windows.Controls;
using citPOINT.eNeg.Themes;
#endregion Usings

namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// App
    /// </summary>
    public partial class App : System.Windows.Application
    {

        #region → Fields         .

        private string PageName = "";
        private string OperationString = "";
        private IsolatedStorageFileStream fs;
        private StreamReader reader;
        private StreamWriter writer;

        /// <summary>
        /// Cast Type.
        /// </summary>
        private enum CastType
        {
            /// <summary>
            /// Guid Type
            /// </summary>
            Guid,

            /// <summary>
            /// String Type
            /// </summary>
            String
        }

        #endregion Fields

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");


            ClientExceptionHandlerProvider.RepaireExceptionPolicies();

            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
            try
            {

                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n--------------\r\n" + (ex.InnerException != null ? ex.InnerException.Message : ""));
            }
        }

        #endregion Constructors

        #region → Event Handlers .

        /// <summary>
        /// Handles the Startup event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                StyleManager.ApplicationTheme = new eNegTheme();
                StyleManager.ApplicationTheme.IsApplicationTheme = true;

                MainPlatformInfo.Instance.CurrentPlatform = Apps.Common.Enums.PlatformTypes.MainPlatform;

                #region "Loading The base Address"

                try
                {
                    int index = System.Windows.Application.Current.Host.Source.AbsoluteUri.IndexOf("ClientBin", StringComparison.CurrentCultureIgnoreCase);
                    AppConfigurations.HostBaseAddress = System.Windows.Application.Current.Host.Source.AbsoluteUri.Substring(0, index);
                }
                catch { }

                #endregion "Loding The base Address"

                AppConfigurations.IsRunningOutOfBrowser = App.Current.IsRunningOutOfBrowser;
                MainPlatformInfo.Instance.IsRunningOutOfBrowser = App.Current.IsRunningOutOfBrowser;

                #region → Reading From Query String .

                if (!App.Current.IsRunningOutOfBrowser)
                {
                    //In case you navigate from web platform To adding New Negotiation or in case of addon to web plat form 
                    AppConfigurations.NegotiationIDParameter = (Guid?)GetFromQueryString("NegotiationID", CastType.Guid);

                    // In case you navigate from webplatForm To addon to Edit Negotiation or Display One                    
                    AppConfigurations.ActionTypeParameter = (string)GetFromQueryString("ActionType", CastType.String) ?? string.Empty;

                    // In case you navigate from Addon To Webplat form for Conversation Details                             
                    AppConfigurations.ConversationIDParameter = (Guid?)GetFromQueryString("ConversationID", CastType.Guid);

                    // In case you navigate from Addon To Webplat form for expanding certain message in Conversation Details
                    AppConfigurations.MessageIDParameter = (Guid?)GetFromQueryString("MessageID", CastType.Guid);

                    //Application ID for opening active app. incase if prefapp settings button or see details.
                    AppConfigurations.ApplicationIDParameter = (Guid?)GetFromQueryString("ApplicationID", CastType.Guid);

                    // In case you navigate from Addon To Webplat form for Conversation Details
                    //the negotiation or Conversation status parameter Open Or Closed.
                    AppConfigurations.StatusParameter = (string)GetFromQueryString("Status", CastType.String) ?? string.Empty;

                    // In case you Owner want to see the  member profile
                    AppConfigurations.ProfileUserID = (Guid)(GetFromQueryString("MemberID", CastType.Guid) ?? Guid.Empty);
                }

                #endregion


                if ((e.InitParams.Count > 0 && e.InitParams.ContainsKey("IsAddon") && e.InitParams["IsAddon"] == "Yes") ||
                    App.Current.IsRunningOutOfBrowser)
                {
                    MainPlatformInfo.Instance.CurrentPlatform = PlatformTypes.AddOn;
                    AppConfigurations.IsAddon = true;

                    #region Case adding new negotiation from web plateform
                    IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
                    if (appSettings.Contains("AddNegotiation") &&
                             bool.Parse(appSettings["AddNegotiation"].ToString()))
                    {
                        AppConfigurations.IsStartNewNegotiation = true;
                    }
                    #endregion

                    this.RootVisual = new MainPageAddon();
                }
                else
                {
                    MainPlatformInfo.Instance.CurrentPlatform = PlatformTypes.MainPlatform;
                    AppConfigurations.IsAddon = false;

                    IntialQueryString();

                    #region → Special For Automation Test  .

                    if (PageName == "ClearCash")
                    {
                        LogOutAutomationTest x = new LogOutAutomationTest();
                        x.ClearCash();
                        PageName = "";
                    }
                    else if (PageName == ViewTypes.ProfileDetails && AppConfigurations.ProfileUserID == Guid.Empty)
                    {
                        PageName = string.Empty;
                    }



                    #endregion

                    this.RootVisual = new MainWebPlatform(PageName, OperationString);//new MainPagePlatform(PageName, OperationString); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OH Begin +" + ex.Message);
            }
        }

        /// <summary>
        /// Handles the Exit event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Application_Exit(object sender, EventArgs e)
        {
            IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
            if (AppConfigurations.IsStartNewNegotiation)
            {
                if (appSettings.Contains("TempUserName"))
                {
                    appSettings.Remove("TempUserName");
                }
                if (appSettings.Contains("AddNegotiation"))
                {
                    appSettings.Remove("AddNegotiation");
                }
                appSettings.Save();
                AppConfigurations.IsStartNewNegotiation = false;
            }
        }

        /// <summary>
        /// Handles the UnhandledException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.ApplicationUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached || true)
            {
                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;

                //if (e.ExceptionObject != null)
                //{
                //    if (e.ExceptionObject.InnerException != null)
                //    {
                //        MessageBox.Show(e.ExceptionObject.Message + "\r\n" + e.ExceptionObject.InnerException.Message, "Error", MessageBoxButton.OK);
                //    }
                //    else
                //        MessageBox.Show(e.ExceptionObject.Message, "Error", MessageBoxButton.OK);
                //}

                //Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        #endregion Event Handlers

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Intials the query string.
        /// </summary>
        private void IntialQueryString()
        {
            if (HtmlPage.Document.QueryString.ContainsKey("PageName"))
            {
                PageName = HtmlPage.Document.QueryString["PageName"].ToString();
            }
            else
            {
                IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
                if (appSettings.Contains("UserName"))
                {
                    PageName = "LoadUser";
                }
            }

            if (HtmlPage.Document.QueryString.ContainsKey("OpStr"))
            {
                OperationString = HtmlPage.Document.QueryString["OpStr"].ToString();
            }
        }

        /// <summary>
        /// Reports the error to DOM.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.ApplicationUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                // System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Gets from query string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="castType">Type of the cast.</param>
        /// <returns></returns>
        private Object GetFromQueryString(string key, CastType castType)
        {

            if (HtmlPage.Document.QueryString.ContainsKey(key) && !string.IsNullOrEmpty(HtmlPage.Document.QueryString[key]))
            {
                string value = HtmlPage.Document.QueryString[key].ToString();

                switch (castType)
                {
                    case CastType.Guid:
                        Guid tmpvalue;
                        if (Guid.TryParse(value, out tmpvalue))
                        {
                            return tmpvalue;
                        }
                        break;
                    case CastType.String:

                        break;
                    default:
                        break;
                }

                return HtmlPage.Document.QueryString[key].ToString();
            }
            else
            {
                return null;
            }
        }

        #endregion Private

        #region → Public         .

        #endregion Public

        #endregion Methods

    }
}
