
#region → Usings   .
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.Common;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 24.0811     mwahab         • creation
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

namespace citPOINT.eNeg.ViewModel
{

    /// <summary>
    /// Queue Mail Types
    /// </summary>
    public enum QueueMailType
    {
        /// <summary>
        /// Send mail to Organization Owner 
        /// that we need a new member to join the organization
        /// </summary>
        RequestFromOwner,

        /// <summary>
        /// Send mail to alternative owner notifying him.
        /// </summary>
        AlternativeOwner,

        /// <summary>
        /// Ask one to be an owner for organization.
        /// </summary>
        AskToBeOwner,

        /// <summary>
        /// Member removed from organization.
        /// </summary>
        MemeberRemoved,

        /// <summary>
        /// Member activated mean be one of the organization members.
        /// </summary>
        MemeberActivated,

        /// <summary>
        /// in case if one from owner to be member.
        /// </summary>
        NoLongerIsOwner,
    }

    /// <summary>
    /// Class for QueueMail
    /// </summary>
    public class QueueMail
    {
        #region → Fields         .


        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>The organization.</value>
        public Organization Organization { get; set; }

        /// <summary>
        /// Gets or sets the member.
        /// </summary>
        /// <value>The member.</value>
        public User Member { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public User Owner { get; set; }

        /// <summary>
        /// Gets or sets the accept operation string.
        /// </summary>
        /// <value>The accept operation string.</value>
        public string AcceptOperationString { get; set; }

        /// <summary>
        /// Gets or sets the refused operation string.
        /// </summary>
        /// <value>The refused operation string.</value>
        public string RefusedOperationString { get; set; }

        /// <summary>
        /// Gets or sets the mail helper.
        /// </summary>
        /// <value>The mail helper.</value>
        public MailHelper MailHelper { get; set; }

        /// <summary>
        /// Gets or sets the type of the queue mail.
        /// </summary>
        /// <value>The type of the queue mail.</value>
        public QueueMailType QueueMailType { get; set; }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueMail"/> class.
        /// </summary>
        /// <param name="mailHelper">The mail helper.</param>
        /// <param name="queueMailType">Type of the queue mail.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="member">The member.</param>
        /// <param name="acceptOperationString">The accept operation string.</param>
        /// <param name="refusedOperationString">The refused operation string.</param>
        public QueueMail(MailHelper mailHelper, QueueMailType queueMailType, Organization organization, User owner, User member, string acceptOperationString, string refusedOperationString)
        {
            this.MailHelper = mailHelper;
            this.QueueMailType = queueMailType;
            this.Organization = organization;
            this.Owner = owner;
            this.Member = member;
            this.AcceptOperationString = acceptOperationString;
            this.RefusedOperationString = refusedOperationString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueMail"/> class.
        /// </summary>
        /// <param name="mailHelper">The mail helper.</param>
        /// <param name="queueMailType">Type of the queue mail.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="member">The member.</param>
        public QueueMail(MailHelper mailHelper, QueueMailType queueMailType, Organization organization, User owner, User member)
            : this(mailHelper, queueMailType, organization, owner, member, null, null)
        {
        }
        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Sends this instance.
        /// </summary>
        public void Send()
        {
            switch (this.QueueMailType)
            {
                case ViewModel.QueueMailType.RequestFromOwner:
                    this.MailHelper.SendMailForOrganizationOwner(this.Owner, this.Member);
                    break;

                case ViewModel.QueueMailType.AlternativeOwner:
                    this.MailHelper.SendMailForAlternativeOrganizationOwner(this.Owner, this.Organization);
                    break;

                case ViewModel.QueueMailType.AskToBeOwner:
                    this.MailHelper.SendMailAskToBeOwner(this.Owner, this.Organization, this.AcceptOperationString, this.RefusedOperationString);
                    break;

                case ViewModel.QueueMailType.MemeberActivated:
                    this.MailHelper.SendMailMemeberActivated(this.Member, this.Organization);
                    break;

                case ViewModel.QueueMailType.MemeberRemoved:
                    this.MailHelper.SendMailRemoveFromOrganization(this.Member, this.Organization);
                    break;


                case ViewModel.QueueMailType.NoLongerIsOwner:
                    this.MailHelper.SendMailNoLongerIsOwner(this.Member, this.Organization);
                    break;

                default:
                    break;
            }
        }

        #endregion  Public

        #endregion Methods
    }
}
