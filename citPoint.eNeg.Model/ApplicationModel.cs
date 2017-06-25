#region → Usings   .
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 18.03.12     mwahab         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Model
{
    #region " Using MEF to export Application Model  "
    /// <summary>
    /// Class for ApplicationModel 
    /// </summary>
    #endregion
    [Export(typeof(IApplicationModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ApplicationModel : IApplicationModel
    {
        #region → Fields         .

        private eNegContext mNegContext;
        private bool mIsBusy;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        private eNegContext Context
        {
            get
            {
                if (mNegContext == null)
                {
                    mNegContext = new eNegContext();

                    mNegContext.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ctx_PropertyChanged);
                }

                return mNegContext;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                if (this.mIsBusy != value)
                {
                    this.mIsBusy = value;
                    this.OnPropertyChanged("IsBusy");
                }
            }
        }

        #endregion

        #region → Event Handler  .

        /// <summary>
        /// Private Event Handler that called after any change in 
        /// HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void ctx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsLoading":
                    this.IsBusy = mNegContext.IsLoading;
                    break;
            }
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [get application complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Application>> GetApplicationComplete;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of eNeg Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {
            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        #endregion
  
        #region → Protected      .

        #region "INotifyPropertyChanged Interface implementation"


        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion "INotifyPropertyChanged Interface implementation"

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets application asynchronously
        /// </summary>
        public void GetApplicationAsync()
        {
            PerformQuery<Application>(this.Context.GetApplicationsQuery(), this.GetApplicationComplete);
        }

        #endregion
        
        #endregion

    }
}
