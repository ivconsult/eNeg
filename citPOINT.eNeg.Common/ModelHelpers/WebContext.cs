#region → Usings   .
using System.Security.Principal;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
 
#endregion

#region → History  .
/*
        User            change              Date
 *      Dergham         Update strcuture    11.08.10
 
 */

#endregion

#region → ToDos    .
#endregion

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Context for the RIA application.
    /// </summary>
    /// <remarks>
    /// This context extends the base to make application services and types available
    /// for consumption from code and xaml.
    /// </remarks>
    public sealed partial class WebContext : WebContextBase
    {

        #region → Properties     .
        /// <summary>
        /// Gets the context that is registered as a lifetime object with the current application.
        /// </summary>
        /// <exception cref="InvalidOperationException"> is thrown if there is no current application,
        /// no contexts have been added, or more than one context has been added.
        /// </exception>
        /// <seealso cref="System.Windows.Application.ApplicationLifetimeObjects"/>
        public new static WebContext Current
        {
            get
            {
                return ((WebContext)(WebContextBase.Current));
            }
        }

        /// <summary>
        /// Gets a user representing the authenticated identity.
        /// </summary>
        public new IPrincipal User
        {
            get
            {
                return (base.User);
            }
        }
        #endregion
        
        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the WebContext class.
        /// </summary>
        public WebContext()
        {
            this.OnCreated();
        }
        #endregion
        
        #region → Methods        .

        /// <summary>
        /// This method is invoked from the constructor once initialization is complete and
        /// can be used for further object setup.
        /// </summary>
        partial void OnCreated();

        #endregion

    }
}
