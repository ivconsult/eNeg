
#region → Usings   .
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using System.Net.Mail;
using System;
using System.IO;
using System.ComponentModel.DataAnnotations;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using System.ServiceModel;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 02.08.10     M Wahab       Creation
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
    /// Service provide me to send mail 
    /// </summary>
    [EnableClientAccess()]
    public class MailService : DomainService
    {

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
        [Invoke]
        public void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
        {
            try
            {
                if (ServiceAuthentication.IsValid())
                {
                    try
                    {

                        // Instantiate a new instance of MailMessage
                        MailMessage mMailMessage = new MailMessage();

                        // Set the sender address of the mail message
                        mMailMessage.From = new MailAddress(from);

                        // Set the recepient address of the mail message
                        mMailMessage.To.Add(new MailAddress(to));

                        // Check if the bcc value is null or an empty string
                        if ((bcc != null) && (bcc != string.Empty))
                        {
                            // Set the Bcc address of the mail message
                            mMailMessage.Bcc.Add(new MailAddress(bcc));
                        }

                        // Check if the cc value is null or an empty value
                        if ((cc != null) && (cc != string.Empty))
                        {
                            // Set the CC address of the mail message
                            mMailMessage.CC.Add(new MailAddress(cc));
                        }

                        // Set the subject of the mail message
                        mMailMessage.Subject = subject;

                        // Set the body of the mail message
                        mMailMessage.Body = body;

                        // Secify the format of the body as HTML
                        mMailMessage.IsBodyHtml = true;

                        // Set the priority of the mail message to normal
                        mMailMessage.Priority = MailPriority.Normal;

                        // Instantiate a new instance of SmtpClient
                        SmtpClient mSmtpClient = new SmtpClient();

                        //Enable SSl
                        mSmtpClient.EnableSsl = true;

                        mSmtpClient.Timeout = 500000;

                        // Send the mail message

                        mSmtpClient.SendAsync(mMailMessage,null);

                        mSmtpClient.SendCompleted += new SendCompletedEventHandler(mSmtpClient_SendCompleted); 

                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Instance.HandleException(ex, "Policy1");

                    }

                }
                else
                {
                    //// throw fault  
                    throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
                }

            }
            catch (System.Exception ex)
            {
                ExceptionManager.Instance.HandleException(ex, "Policy1");

            }

        }

        /// <summary>
        /// Handles the SendCompleted event of the mSmtpClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.AsyncCompletedEventArgs"/> instance containing the event data.</param>
        void mSmtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error!=null)
            {
                ExceptionManager.Instance.HandleException(e.Error, "Policy1");
            }
        }

        #endregion

        #endregion
    }
}


