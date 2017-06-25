
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using citPOINT.eNeg.Apps.Common.Interfaces;
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
    /// Conversation class client-side extensions
    /// </summary>
    public partial class Conversation : IConversation, IArchiveItem
    {
        #region → Fields         .

        DateTime? mDateOfLastAction;
        private string mParticipants;

        private bool mIsLoadedOnDemand = true;

        #endregion

        protected override void OnLoaded(bool isInitialLoad)
        {
            base.OnLoaded(isInitialLoad);
            this.IsLoadedOnDemand = true;
        }

        #region → Properties     .

        #region → Implement  IArchiveItem .

        /// <summary>
        /// Gets or sets the type of the archive item.
        /// </summary>
        /// <value>The type of the archive item.</value>
        public ArchiveItemTypes ArchiveItemType
        {
            get
            {
                return ArchiveItemTypes.Conversation;
            }
            set
            {
                //No need to set it;              
            }
        }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public IList<IArchiveItem> Children
        {
            get
            {
                return null;
            }

            set
            {

            }
        }

        /// <summary>
        /// Gets the parent of this entity, if this entity is part of
        /// a composition relationship.
        /// </summary>
        /// <value></value>
        public IArchiveItem Parent
        {
            get
            {
                return this.Negotiation;
            }
            set
            {
                //No need for that
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loaded on demand.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is loaded on demand; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoadedOnDemand
        {
            get
            {
                return mIsLoadedOnDemand;
            }
            set
            {
                if (mIsLoadedOnDemand != value)
                {
                    mIsLoadedOnDemand = value;
                    this.RaisePropertyChanged("IsLoadedOnDemand");
                }

            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get
            {
                return this;
            }
            set
            {
                //No need to set it;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return this.ConversationName;
            }
            set
            {
                this.ConversationName = value;
                this.RaisePropertyChanged("Name");
            }
        }

        #endregion

        #region → Property that contain the list of messages of this conversation except App Messages.
        /// <summary>
        /// Gets the addon messeges.
        /// </summary>
        /// <value>The addon messeges.</value>
        public List<Message> AddonMesseges
        {
            get
            {
                return this.Messages.Where(s => s.IsAppsMessage == false).ToList();
            }
        }
        #endregion

        /// <summary>
        /// Gets the date of last action.
        /// </summary>
        /// <value>The date of last action.</value>
        public DateTime? DateOfLastAction
        {
            get
            {

                return mDateOfLastAction;
            }
            set
            {
                mDateOfLastAction = value;
                this.RaisePropertyChanged("DateOfLastAction");

            }
        }

        /// <summary>
        /// Gets or sets the participants.
        /// </summary>
        /// <value>The participants.</value>
        public string Participants
        {
            get
            {
                return mParticipants;
            }
            set
            {
                mParticipants = value;
                this.RaisePropertyChanged("Participants");
            }
        }

        #endregion

        #region → Methods        .
        #region → Private        .
        /// <summary>
        /// Removes the brackets.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        private string RemoveBrackets(string word)
        {

            //karli <Karli@yahoo.com>
            // <sunder@yahoo.com>

            string TempParticipant = word.Substring(0, word.IndexOf('<'));
            if (string.IsNullOrWhiteSpace(TempParticipant))
            {
                TempParticipant = word.Substring(word.IndexOf('<') + 1, word.IndexOf('>') - word.IndexOf('<') - 1);
            }
            else
            {
                TempParticipant = word;
            }

            return TempParticipant;

        }
        #endregion

        #region → Public         .

        /// <summary>
        /// Refreshes the children.
        /// </summary>
        public void RefreshChildren()
        {
            this.RaisePropertyChanged("Children");
        }

        /// <summary>
        /// Builds the fields.
        /// </summary>
        public void BuildFields()
        {
            #region → Build Date Of Last Action .
            DateTime? tmpDateOfLastAction = null;

            if (this.Messages.Count > 0)
            {
                tmpDateOfLastAction = this.Messages.Max(s => s.MessageDate);
            }
            DateOfLastAction = tmpDateOfLastAction;


            #endregion

            #region → Build Negotiators         .


            List<string> AllNego = new List<string>();

            foreach (var item in this.Messages)
            {

                if (!string.IsNullOrEmpty(item.MessageReceiver))
                {
                    string word = item.MessageReceiver;// RemoveBrackets(item.MessageReceiver);
                    if (!AllNego.Contains(word.TrimStart().TrimEnd()))
                    {
                        AllNego.Add(word);
                    }
                }


                if (!string.IsNullOrEmpty(item.MessageSender))
                {
                    string word = item.MessageSender;// RemoveBrackets(item.MessageSender);
                    if (!AllNego.Contains(word.TrimStart().TrimEnd()))
                    {
                        AllNego.Add(word);
                    }
                }

            }

            AllNego.Sort();

            string tmpParticipant = string.Empty;

            foreach (var item in AllNego)
            {
                if (!string.IsNullOrWhiteSpace(tmpParticipant))
                    tmpParticipant += " - " + item;
                else
                    tmpParticipant = item;
            }

            Participants = tmpParticipant;

            #endregion
        }

        /// <summary>
        /// Try validate for the Conversation class
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
            if (propertyName == "ConversationID"
             || propertyName == "ConversationName"
             || propertyName == "NegotiationID"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "ConversationID")
                    return Validator.TryValidateProperty(this.ConversationID, context, validationResults);
                if (propertyName == "ConversationName")
                    return Validator.TryValidateProperty(this.ConversationName, context, validationResults);
                if (propertyName == "NegotiationID")
                    return Validator.TryValidateProperty(this.NegotiationID, context, validationResults);
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
