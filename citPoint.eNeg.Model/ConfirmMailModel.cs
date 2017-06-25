
#region → Usings   .
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.Common.Helper;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 04.09.11     yousra reda     •creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPoint.eNeg.Model
{
    #region  Using MEF to export ConfirmMailViewModel
    /// <summary>
    /// Model Layer for making certain operation using user operation table
    /// </summary>
    [Export(typeof(IConfirmMailModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class ConfirmMailModel : IConfirmMailModel
    {

        #region → Fields         .
        private eNegContext mNegContext;
        private Boolean mIsBusy = false;
        #endregion

        #region → Properties     .
        /// <summary>
        /// Protocted property with a getter only to used to update user login and logout data in eNeg Database 
        /// </summary>
        protected eNegContext Context
        {
            get
            {
                if (mNegContext == null)
                {

                    if (mNegContext == null)
                    {
                        mNegContext = new eNegContext();
                    }

                    mNegContext.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_ctx_PropertyChanged);
                }

                return mNegContext;
            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
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

        #region → Event Handlers .

        /// <summary>
        /// Private Event Handler that called after any change in 
        /// HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void _ctx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsLoading":
                    this.IsBusy = mNegContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = mNegContext.IsSubmitting;
                    break;
            }
        }
        #endregion

        #region → Events         .
        /// <summary>
        /// Event Handler For Method  UpdateUserByConfirmMail
        /// Update Locked Flag By false
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> UpdateUserByConfirmMailComplete;

        /// <summary>
        /// Occurs when [get organization by user ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationByUserIDComplete;
        
        /// <summary>
        /// Event Handler For Method PropertyChanged
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

        #endregion Private

        #region → Protected      .

        #region INotifyPropertyChanged Interface implementation
        /// <summary>
        /// Handle changes in any Property
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged Interface implementation

        #endregion Protected

        #region → Public         .

        #region ICofirmationModel Interface implementation

        /// <summary>
        /// public method that call perform query to Open The User To can be login.
        /// Update Locked Flag By false
        /// </summary>
        /// <param name="operationString">Value of OperationString</param>
        /// <param name="operationStringType">Type of the operation string.</param>
        public void UpdateUserByConfirmMail(String operationString, byte operationStringType)
        {
            PerformQuery<User>(Context.UpdateUserByConfirmMailQuery(operationString, operationStringType), UpdateUserByConfirmMailComplete);
        }


        /// <summary>
        /// Gets the organization by its owner ID.
        /// </summary>
        /// <param name="OwnerID">The owner ID.</param>
        public void GetOrganizationByItsOwnerID(Guid OwnerID)
        {
            PerformQuery<Organization>(Context.GetOrganizationByItsOwnerIDQuery(OwnerID), GetOrganizationByUserIDComplete);
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }
        #endregion

        #endregion

        #endregion
    }
}
