
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
#endregion

#region → History  .

/* Date         User            Change
 * 
 *16.08.10     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Data.Web
{
    /// <summary>
    /// NegotiationApplicationStatu class client-side extensions
    /// </summary>
    public partial class NegotiationApplicationStatu
    {
        #region → properties    .

        /// <summary>
        /// Gets the target URL.
        /// </summary>
        /// <value>The target URL.</value>
        public string TargetUrl
        {
            get
            {
                if (!this.Active || this.NegotiationID == Guid.Empty)
                {
                    return null;
                }

                string mTargetUrl = string.Empty;
                mTargetUrl = this.Application.ApplicationBaseAddress +
                           "?ActionType=externalreport&NegID=" +
                       this.NegotiationID.ToString();

                if (this.Negotiation.ActiveConversation != null)
                {
                    mTargetUrl += "&ConvID=" + this.Negotiation.ActiveConversation.ConversationID.ToString();
                }


                return mTargetUrl;
            }
        }



        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Try validate for the NegotiationApplicationStatu class
        /// </summary>
        /// <returns>bool</returns>
        public bool TryValidateObject()
        {


            ValidationContext context = new ValidationContext(this, null, null);
            var validationResults = new Collection<ValidationResult>();

            if (Validator.TryValidateObject(this, context, validationResults, false) == false)
            {
                foreach (ValidationResult error in validationResults)
                {
                    this.ValidationErrors.Add(error);
                }
                return false;
            }


            return true;
        }

        /// <summary>
        /// validates poperty according to criteria
        /// </summary>
        /// <param name="propertyName">propertyName</param>
        /// <returns>bool</returns>
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "NegotiationApplicationStatusID"
             || propertyName == "ApplicationID"
             || propertyName == "NegotiationID"
             || propertyName == "UserID"
             || propertyName == "Active"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "NegotiationApplicationStatusID")
                    return Validator.TryValidateProperty(this.NegotiationApplicationStatusID, context, validationResults);
                if (propertyName == "ApplicationID")
                    return Validator.TryValidateProperty(this.ApplicationID, context, validationResults);
                if (propertyName == "NegotiationID")
                    return Validator.TryValidateProperty(this.NegotiationID, context, validationResults);
                if (propertyName == "UserID")
                    return Validator.TryValidateProperty(this.UserID, context, validationResults);
                if (propertyName == "Active")
                    return Validator.TryValidateProperty(this.Active, context, validationResults);
            }
            return false;
        }

        /// <summary>
        /// Updates the target URL.
        /// </summary>
        public void UpdateTargetUrl()
        {
            this.RaiseDataMemberChanged("TargetUrl");

        }
        #endregion

        #endregion
    }

}
