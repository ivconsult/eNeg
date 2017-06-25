
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
    /// Message class client-side extensions
    /// </summary>
    public partial class Message
    {
        #region → Properties     .

        /// <summary>
        /// Gets the message sender without brackets.
        /// </summary>
        /// <value>The message sender without brackets.</value>
        public string MessageSenderWithoutBrackets
        {
            get
            {
                if (!string.IsNullOrEmpty(MessageSender))
                {
                    return MessageSender.Replace("<", "").Replace(">", "");
                }
                return MessageSender;
            }
        }

        /// <summary>
        /// Gets the message receiver without brackets.
        /// </summary>
        /// <value>The message receiver without brackets.</value>
        public string MessageReceiverWithoutBrackets
        {
            get
            {
                if (!string.IsNullOrEmpty(MessageReceiver))
                {
                    return MessageReceiver.Replace("<", "").Replace(">", "");
                }
                return MessageReceiver;
            }
        }

        /// <summary>
        /// Gets the message date without time.
        /// </summary>
        /// <value>The message date without time.</value>
        public DateTime? MessageDateWithoutTime
        {
            get
            {
                if (MessageDate != null)
                {
                    return MessageDate.Value.Date;
                }
                return MessageDate;
            }
        }
        #endregion

        #region → Event handlers .

        /// <summary>
        /// Called when an <see cref="T:System.ServiceModel.DomainServices.Client.Entity"/> property has changed.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == "MessageSender")
            {
                this.RaisePropertyChanged("MessageSenderWithoutBrackets");
            }
            else if (e.PropertyName == "MessageReceiver")
            {
                this.RaisePropertyChanged("MessageReceiverWithoutBrackets");
            }
            else if (e.PropertyName == "MessageDate")
            {
                this.RaisePropertyChanged("MessageDateWithoutTime");
            }
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Try validate for the Message class
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
            if (propertyName == "MessageID"
             || propertyName == "ConversationID"
             || propertyName == "MessageContent"
             || propertyName == "MessageSubject"
             || propertyName == "MessageSender"
             || propertyName == "MessageReceiver"
             || propertyName == "MessageDate"
             || propertyName == "ChannelID"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "MessageID")
                    return Validator.TryValidateProperty(this.MessageID, context, validationResults);
                if (propertyName == "ConversationID")
                    return Validator.TryValidateProperty(this.ConversationID, context, validationResults);
                if (propertyName == "MessageContent")
                    return Validator.TryValidateProperty(this.MessageContent, context, validationResults);
                if (propertyName == "MessageSubject")
                    return Validator.TryValidateProperty(this.MessageSubject, context, validationResults);
                if (propertyName == "MessageSender")
                    return Validator.TryValidateProperty(this.MessageSender, context, validationResults);
                if (propertyName == "MessageReceiver")
                    return Validator.TryValidateProperty(this.MessageReceiver, context, validationResults);
                if (propertyName == "MessageDate")
                    return Validator.TryValidateProperty(this.MessageDate, context, validationResults);
                if (propertyName == "ChannelID")
                    return Validator.TryValidateProperty(this.ChannelID, context, validationResults);
                if (propertyName == "Deleted")
                    return Validator.TryValidateProperty(this.Deleted, context, validationResults);
                if (propertyName == "DeletedBy")
                    return Validator.TryValidateProperty(this.DeletedBy, context, validationResults);
                if (propertyName == "DeletedOn")
                    return Validator.TryValidateProperty(this.DeletedOn, context, validationResults);
            }
            return false;
        }

        #endregion

        #endregion
    }
}
