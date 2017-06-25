



#region → Usings   .
using System;
using citPOINT.eNeg.Data.Web;
using System.ServiceModel.DomainServices.Client;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 05.08.10     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion


namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Used to Send a Mail.
    /// </summary>
    public class MailHelper
    {

        #region → Fields         .

        private MailContext mMailContext;
        #endregion

        #region → Constructors   .
        /// <summary>
        /// Class Constructor
        /// </summary>
        public MailHelper()
        {
            mMailContext = new MailContext();
        }
        #endregion

        #region → Events         .
        /// <summary>
        /// Call Back Event After Sending Mail
        /// </summary>
        public event Action<InvokeOperation> MailSendComplete;
        #endregion

        #region → Methods        .

        #region → Public         .
        /// <summary>
        /// Sends an mail message
        /// </summary>
        /// <param name="from">Sender address</param>
        /// <param name="to">Recepient address</param>
        /// <param name="bcc">Bcc recepient</param>
        /// <param name="cc">Cc recepient</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>
        public void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
        {
            mMailContext.SendMailMessage(from, to, bcc, cc, subject, body, MailSendComplete, null);
        }
        
        /// <summary>
        /// Sends an mail message
        /// </summary>
        /// <param name="to">Recepient address</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>
        public void SendMailMessage(string to, string subject, string body)
        {
            SendMailMessage("XXx@XXx.com", to, null, null, subject, body);
        }

        /// <summary>
        /// For Sending Reset Request Mail
        /// </summary>
        /// <param name="EmailAddress">Recepient address</param>
        /// <param name="FirstName">Value of First Name</param>
        /// <param name="LastName">Value of Last Name</param>
        /// <param name="OperationString">Some Charaters</param>
        public void SendResetMail(string EmailAddress, string FirstName, string LastName, string OperationString)
        {
            OperationString = OperationString.Replace("+", "%2B");

            string Address = AppConfigurations.HostBaseAddress + "Default.aspx?PageName=Reset&OpStr=" + OperationString;

            string Link = "<a href=\"" + Address + "\">reset username and/or password</a>";

            string MessageContent = "Dear " + (string.IsNullOrEmpty(FirstName) ? EmailAddress : (FirstName + " " + LastName)) + ",<br/><br/>";

            MessageContent += "Please click on link " + Link + " or insert the web address " + Address + " directly in your web browser in order to change your username and/or password.";

            SendMailMessage(EmailAddress, "[eNeg] Reset Login", MessageContent);
        }

        /// <summary>
        /// For Sending Reset Request Mail
        /// </summary>
        /// <param name="EmailAddress">Recepient address</param>
        /// <param name="FirstName">Value of First Name</param>
        /// <param name="LastName">Value of Last Name</param>
        /// <param name="OperationString">The operation string.</param>
        public void SendConfirmationMail(string EmailAddress, string FirstName, string LastName, string OperationString)
        {
            OperationString = OperationString.Replace("+", "%2B");

            string Address = AppConfigurations.HostBaseAddress + "Default.aspx?PageName=ConfirmMail&OpStr=" + OperationString;

            string Link = "<a href=\"" + Address + "\">Activate your Account</a>";
            
            string MessageContent = "Dear " + (string.IsNullOrEmpty(FirstName) ? EmailAddress : (FirstName + " " + LastName)) + ",<br/><br/>";

            MessageContent += "Please click on link " + Link + " or insert the web address " + Address + " directly in your web browser in order to Activate your Account.";

            SendMailMessage(EmailAddress, "[eNeg] Confirmation Login", MessageContent);
        }

        /// <summary>
        /// Sends the mail for organization owner.
        /// </summary>
        /// <param name="`anizationOwner">The organization owner.</param>
        /// <param name="NewMember">The new member.</param>
        public void SendMailForOrganizationOwner(User OrganizationOwner, User NewMember)
        {
            string profileLink = AppConfigurations.HostBaseAddress + "Default.aspx?PageName=ProfileDetails&MemberID=" + NewMember.UserID.ToString();
            string orgLink = AppConfigurations.HostBaseAddress + "Default.aspx?PageName=OrganizationManagement&MemberID=" + NewMember.UserID.ToString();
            
            profileLink = "<a href=\"" + profileLink + "\">profile</a>";
            orgLink = "<a href=\"" + orgLink + "\">here</a>";

            string ownerName = (string.IsNullOrEmpty(OrganizationOwner.FirstName) ? OrganizationOwner.EmailAddress : (OrganizationOwner.FirstName + " " + OrganizationOwner.LastName));

            string memberName = (string.IsNullOrEmpty(NewMember.FirstName) ? NewMember.EmailAddress : (NewMember.FirstName + " " + NewMember.LastName));

            string MessageContent = @"
                    Dear @ownerName ,<br/><br/>
                    The user @newMember wants to enter your organization. Here is the link to his @profileLink <br/><br/>

                    Please reply to his request @orgLink. <br/><br/>

                    Thanks and best regards<br/>
                    eNeg System<br/><br/>
                    ";

            MessageContent = MessageContent.Replace("@ownerName", ownerName)
                                           .Replace("@newMember", memberName)
                                           .Replace("@profileLink", profileLink)
                                           .Replace("@orgLink", orgLink);

            SendMailMessage(OrganizationOwner.EmailAddress, "[eNeg] Organization join Request", MessageContent);
        }

        /// <summary>
        /// Sends the mail for alternative organization owner.
        /// </summary>
        /// <param name="AlternativeOwner">The alternative owner.</param>
        /// <param name="organization">The organization.</param>
        public void SendMailForAlternativeOrganizationOwner(User AlternativeOwner, Organization organization)
        {
            string ownerName = (string.IsNullOrEmpty(AlternativeOwner.FirstName) ? AlternativeOwner.EmailAddress : (AlternativeOwner.FirstName + " " + AlternativeOwner.LastName));
            string orgLink = AppConfigurations.HostBaseAddress + "Default.aspx?PageName=OrganizationManagement";

            orgLink = "<a href=\"" + orgLink + "\">here</a>";

            string MessageContent = @"
                    Dear @ownerName ,<br/><br/>
                    
                    You are choosen to be owner in the organization @orgName . <br/><br/>
                    
                    You can see details about it @orgLink. <br/><br/>

                    Thanks and best regards<br/>
                    eNeg System<br/><br/>
                    ";

            MessageContent = MessageContent.Replace("@ownerName", ownerName)
                                           .Replace("@orgName", organization.OrganizationName)
                                           .Replace("@orgLink", orgLink);
            
            SendMailMessage(AlternativeOwner.EmailAddress, "[eNeg] Candidate to be Organization Owner", MessageContent);
        }

        /// <summary>
        /// Sends the mail ask to be owner.
        /// </summary>
        /// <param name="NewOwner">The new owner.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="acceptOperationString">The accept org link.</param>
        /// <param name="refuseOperationString">The refuse org link.</param>
        public void SendMailAskToBeOwner(User NewOwner, Organization organization, string acceptOperationString, string refuseOperationString)
        {
            string ownerName = (string.IsNullOrEmpty(NewOwner.FirstName) ? NewOwner.EmailAddress : (NewOwner.FirstName + " " + NewOwner.LastName));

            string acceptOrgLink;
            if (AppConfigurations.RemoveOriginalOwner)
            {
                acceptOrgLink = AppConfigurations.HostBaseAddress + "Default.aspx?PageName=AcceptToBeOwner_RemoveOriginal&OpStr=" + acceptOperationString;
                //AppConfigurations.RemoveOriginalOwner = false;
            }
            else
            {
                acceptOrgLink = AppConfigurations.HostBaseAddress + "Default.aspx?PageName=AcceptToBeOwner&OpStr=" + acceptOperationString;
            }
            string refuseOrgLink = AppConfigurations.HostBaseAddress + "Default.aspx?PageName=RefuseToBeOwner&OpStr=" + refuseOperationString;

            acceptOrgLink = "<a href=\"" + acceptOrgLink + "\">here</a>";
            refuseOrgLink = "<a href=\"" + refuseOrgLink + "\">here</a>";

            string MessageContent = @"
                    Dear @ownerName ,<br/><br/>
                    
                    You have been chosen to be the owner of organization @orgName . <br/><br/>
                    
                    To confirm this new role click @acceptOrgLink. <br/><br/>
                    
                    and to refuse it click @refuseOrgLink. <br/><br/>

                    Thanks and best regards<br/>
                    eNeg System<br/><br/>
                    ";

            MessageContent = MessageContent.Replace("@ownerName", ownerName)
                                           .Replace("@orgName", organization.OrganizationName)
                                           .Replace("@acceptOrgLink", acceptOrgLink)
                                           .Replace("@refuseOrgLink", refuseOrgLink);
            
            SendMailMessage(NewOwner.EmailAddress, "[eNeg] Want to be Organization Owner?", MessageContent);
        }

        /// <summary>
        /// Sends the mail remove from organization.
        /// </summary>
        /// <param name="organizationMember">The organization member.</param>
        /// <param name="organization">The organization.</param>
        public void SendMailRemoveFromOrganization(User organizationMember, Organization organization)
        {
            string ownerName = (string.IsNullOrEmpty(organizationMember.FirstName) ? organizationMember.EmailAddress : (organizationMember.FirstName + " " + organizationMember.LastName));

            string MessageContent = @"
                    Dear @ownerName ,<br/><br/>
                    
                    Sorry, you are not a memeber in organization @orgName any more. <br/><br/>
                    
                    As you have been deleted from managment side, contact us for more information. <br/><br/>

                    Thanks and best regards<br/>
                    eNeg System<br/><br/>
                    ";

            MessageContent = MessageContent.Replace("@ownerName", ownerName)
                                           .Replace("@orgName", organization.OrganizationName);


            SendMailMessage(organizationMember.EmailAddress, "[eNeg] Sorry, Removed from Organization", MessageContent);
        }

        /// <summary>
        /// Sends the mail memeber activated.
        /// </summary>
        /// <param name="organizationMember">The organization member.</param>
        /// <param name="organization">The organization.</param>
        public void SendMailMemeberActivated(User organizationMember, Organization organization)
        {
            string ownerName = (string.IsNullOrEmpty(organizationMember.FirstName) ? organizationMember.EmailAddress : (organizationMember.FirstName + " " + organizationMember.LastName));

            string MessageContent = @"
                    Dear @ownerName ,<br/><br/>
                    
                    Congratulations, your request to join organization @orgName has been accepted. <br/><br/>
                    
                    Now you can publish your negotiations, your preference sets, and have a look on the currently published ones. <br/><br/>

                    Thanks and best regards<br/>
                    eNeg System<br/><br/>
                    ";

            MessageContent = MessageContent.Replace("@ownerName", ownerName)
                                           .Replace("@orgName", organization.OrganizationName);


            SendMailMessage(organizationMember.EmailAddress, "[eNeg] Accepted in Organization", MessageContent);
        }

        /// <summary>
        /// Sends the mail no longer is owner.
        /// </summary>
        /// <param name="organizationMember">The organization member.</param>
        /// <param name="organization">The organization.</param>
        public void SendMailNoLongerIsOwner(User organizationMember, Organization organization)
        {
            string memberName = (string.IsNullOrEmpty(organizationMember.FirstName) ? organizationMember.EmailAddress : (organizationMember.FirstName + " " + organizationMember.LastName));
            string ownerName = (string.IsNullOrEmpty(AppConfigurations.CurrentLoginUser.FirstName) ? AppConfigurations.CurrentLoginUser.EmailAddress : (AppConfigurations.CurrentLoginUser.FirstName + " " + AppConfigurations.CurrentLoginUser.LastName));

            string MessageContent = @"
                    Dear @MemberName ,<br/><br/>
                    
                    The owner @ownerName in the organization <br/><br/>
                    
                    has changed your role to be member instead of owner. <br/><br/>

                    Thanks and best regards<br/>
                    eNeg System<br/><br/>
                    ";

            MessageContent = MessageContent.Replace("@MemberName", memberName)
                                           .Replace("@ownerName", ownerName)
                                           .Replace("@orgName", organization.OrganizationName);

            SendMailMessage(organizationMember.EmailAddress, "[eNeg] Your Role in Organization changed", MessageContent);
        }

        /// <summary>
        /// Sends the mail member accepted to be owner.
        /// </summary>
        /// <param name="organizationOwner">The organization owner.</param>
        /// <param name="newOrganizationOwner">The new organization owner.</param>
        /// <param name="organization">The organization.</param>
        public void SendMailMemberAcceptedToBeOwner(User organizationOwner,User newOrganizationOwner, Organization organization)
        {
            string OwnerName = (string.IsNullOrEmpty(organizationOwner.FirstName) ? organizationOwner.EmailAddress : (organizationOwner.FirstName + " " + organizationOwner.LastName));
            string NewOwnerName = (string.IsNullOrEmpty(newOrganizationOwner.FirstName) ? newOrganizationOwner.EmailAddress : (newOrganizationOwner.FirstName + " " + newOrganizationOwner.LastName));

            string MessageContent = @"
                    Dear @OwnerName ,<br/><br/>
                    
                    The member @NewOwnerName in organization @orgName. <br/><br/>
                    
                    accepted the role owner that has been offered from your side. <br/><br/>

                    Thanks and best regards<br/>
                    eNeg System<br/><br/>
                    ";

            MessageContent = MessageContent.Replace("@OwnerName", OwnerName)
                                           .Replace("@NewOwnerName", NewOwnerName)
                                           .Replace("@orgName", organization.OrganizationName);

            SendMailMessage(organizationOwner.EmailAddress, "[eNeg] New owner in the organization", MessageContent);
        }

        /// <summary>
        /// Sends the mail member refused to be owner.
        /// </summary>
        /// <param name="organizationOwner">The organization owner.</param>
        /// <param name="newOrganizationOwner">The new organization owner.</param>
        /// <param name="organization">The organization.</param>
        public void SendMailMemberRefusedToBeOwner(User organizationOwner,User newOrganizationOwner, Organization organization)
        {
            string OwnerName = (string.IsNullOrEmpty(organizationOwner.FirstName) ? organizationOwner.EmailAddress : (organizationOwner.FirstName + " " + organizationOwner.LastName));
            string NewOwnerName = (string.IsNullOrEmpty(newOrganizationOwner.FirstName) ? newOrganizationOwner.EmailAddress : (newOrganizationOwner.FirstName + " " + newOrganizationOwner.LastName));

            string MessageContent = @"
                    Dear @OwnerName ,<br/><br/>
                    
                    The member @NewOwnerName in organization @orgName. <br/><br/>
                    
                    refused the role owner that has been offered from your side. <br/><br/>

                    Thanks and best regards<br/>
                    eNeg System<br/><br/>
                    ";

            MessageContent = MessageContent.Replace("@OwnerName", OwnerName)
                                           .Replace("@NewOwnerName", NewOwnerName)
                                           .Replace("@orgName", organization.OrganizationName);

            SendMailMessage(organizationOwner.EmailAddress, "[eNeg] Member Refused to be Owner", MessageContent);
        }
        
        #endregion

        #endregion
    }
}
