#region → Usings   .
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
    /// Negotiation class client-side extensions
    /// </summary>
    public partial class Negotiation : INegotiation, IArchiveItem
    {

        #region → Fields         .

        private IEnumerable<string> mNegotiators;
        private DateTime? mDateOfLastAction;
        private Conversation mActiveConversation;

        private IArchiveItem mParent;
        private bool mIsLoadedOnDemand;

        #endregion

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
                return ArchiveItemTypes.Negotiation;
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
                var archiveList = new List<IArchiveItem>();

                foreach (var convItem in this.OrderedConversations)
                {
                    archiveList.Add(new ArchiveItem()
                        {
                            ArchiveItemType = ArchiveItemTypes.Conversation,
                            Name = convItem.Name,
                            Value = convItem,
                            Parent = this,
                            IsLoadedOnDemand = true
                        });
                }

                return archiveList;
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
                return mParent;
            }
            set
            {
                if (mParent != value)
                {
                    mParent = value;
                    this.RaisePropertyChanged("Parent");
                }

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
                return this.NegotiationName;
            }
            set
            {
                this.NegotiationName = value;
                this.RaisePropertyChanged("Name");
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the negotiators.
        /// </summary>
        /// <value>The negotiators.</value>
        public IEnumerable<string> Negotiators
        {
            get
            {
                return mNegotiators;
            }
            set
            {
                mNegotiators = value;
                this.RaisePropertyChanged("Negotiators");
            }
        }

        /// <summary>
        /// Gets or sets the active conversation.
        /// </summary>
        /// <value>The active conversation.</value>
        public Conversation ActiveConversation
        {
            get
            {
                return mActiveConversation;
            }

            set
            {
                if (mActiveConversation != value)
                {

                    mActiveConversation = value;

                    this.RaiseDataMemberChanged("ActiveConversation");

                    foreach (var negotiationApplicationStatus in this.NegotiationApplicationStatus)
                    {
                        negotiationApplicationStatus.UpdateTargetUrl();
                    }
                }
            }
        }

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
        /// Gets or sets a value indicating whether this instance is closed.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        public bool IsClosed
        {
            get
            {
                return this.StatusID.HasValue && this.StatusID.Value == Guid.Parse("dfcea50d-18a2-4e41-9be8-1673e88101c4");
            }
        }

        /// <summary>
        /// Gets the ordered conversations.
        /// </summary>
        /// <value>The ordered conversations.</value>
        public List<Conversation> OrderedConversations
        {
            get
            {
                return this.Conversations.OrderBy(s => s.ConversationName).ToList();
            }
        }
        #endregion

        #region → Methods        .

        #region → Public         .

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

        /// <summary>
        /// Refreshes the conversations.
        /// </summary>
        public void RefreshConversations()
        {
            this.RaisePropertyChanged("OrderedConversations");
        }

        /// <summary>
        /// Refreshes the children.
        /// </summary>
        public void RefreshChildren()
        {
            this.RaisePropertyChanged("Children");
        }

        /// <summary>
        /// Builds the negotiators for current negotiation.
        /// </summary>
        public void BuildNegotiators()
        {
            #region → Build Negotiators         .

            List<string> AllNego = new List<string>();

            foreach (var conv in Conversations)
            {
                foreach (var item in conv.Messages)
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
            }

            AllNego.Sort();
            
            Negotiators = AllNego;

            #endregion
        }
         
        /// <summary>
        /// Sets the date of last action.
        /// </summary>
        public void SetDateOfLastAction()
        {
            //Select Last Date of Action from its Conversations
            var expectConversation = this._conversations.OrderByDescending(d => d.DateOfLastAction).FirstOrDefault();

            if (expectConversation != null)
            {
                this.DateOfLastAction = expectConversation.DateOfLastAction;
            }
            else
            {
                this.DateOfLastAction = null;
            }

        }

        /// <summary>
        /// Try validate for the Negotiation class
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
        /// Validates a property accoding to validation criteria
        /// </summary>
        /// <param name="propertyName">propertyName</param>
        /// <returns>bool</returns>
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "NegotiationID"
             || propertyName == "NegotiationName"
             || propertyName == "CreatedDate"
             || propertyName == "StatusID"
             || propertyName == "Deleted"
             || propertyName == "DeletedDate"
             || propertyName == "UserID"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "NegotiationID")
                    return Validator.TryValidateProperty(this.NegotiationID, context, validationResults);
                if (propertyName == "NegotiationName")
                    return Validator.TryValidateProperty(this.NegotiationName, context, validationResults);
                if (propertyName == "CreatedDate")
                    return Validator.TryValidateProperty(this.CreatedDate, context, validationResults);
                if (propertyName == "StatusID")
                    return Validator.TryValidateProperty(this.StatusID, context, validationResults);
                if (propertyName == "Deleted")
                    return Validator.TryValidateProperty(this.Deleted, context, validationResults);
                if (propertyName == "DeletedDate")
                    return Validator.TryValidateProperty(this.DeletedDate, context, validationResults);
                if (propertyName == "UserID")
                    return Validator.TryValidateProperty(this.UserID, context, validationResults);
            }
            return false;
        }


        #endregion
        #endregion

    }
}
