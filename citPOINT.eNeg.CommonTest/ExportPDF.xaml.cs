

#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 28.02.11     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.CommonTest
{
    /// <summary>
    /// Class used to test Export PDF process for Text and for User Control
    /// </summary>
    public partial class ExportPDF : UserControl
    {

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportPDF" /> class.
        /// </summary>
        public ExportPDF()
        {
            InitializeComponent();
        }
        #endregion


        #region → Event Handlers .

        /// <summary>
        /// Handles the Click event of the uxcmdGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void uxcmdGenerateText_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Message> messages;

            string text = "New Message have been sent, New Message have been sent, New Message have been sent\n" +
                "New Message have been sent, New Message have been sent, New Message have been sent\n" +
                "New Message have been sent, New Message have been sent, New Message have been sent\n" +
                "New Message have been sent, New Message have been sent, New Message have been sent\n" +
                "New Message have been sent, New Message have been sent, New Message have been sent\n" +
                "New Message have been sent, New Message have been sent, New Message have been sent\n" +
                "New Message have been sent, New Message have been sent, New Message have been sent\n" +
                "New Message have been sent, New Message have been sent, New Message have been sent\n" +
                "New Message have been sent, New Message have been sent, New Message have been sent\n";

            List<Message> MessageList = new List<Message>()
            {
                new Message(){MessageContent=text, MessageSubject = "Message Subject 1",MessageSender = "Sender 1",MessageReceiver = "Receiver 1",MessageDate = DateTime.Now,Channel = new Channel(){ChannelID = Guid.NewGuid(),ChannelName="Outlook"},Conversation= new Conversation(){ConversationID =Guid.NewGuid(),ConversationName="Conversation 1"}},
                new Message(){MessageContent=text, MessageSubject = "Message Subject 2",MessageSender = "Sender 2",MessageReceiver = "Receiver 2",MessageDate = DateTime.Now,Channel = new Channel(){ChannelID = Guid.NewGuid(),ChannelName="Outlook"},Conversation= new Conversation(){ConversationID =Guid.NewGuid(),ConversationName="Conversation 2"}},
                new Message(){MessageContent=text, MessageSubject = "Message Subject 3",MessageSender = "Sender 3",MessageReceiver = "Receiver 3",MessageDate = DateTime.Now,Channel = new Channel(){ChannelID = Guid.NewGuid(),ChannelName="Outlook"},Conversation= new Conversation(){ConversationID =Guid.NewGuid(),ConversationName="Conversation 3"}}
            };

            messages = MessageList;
            if (PDFGenerator.ExportText(messages))
                uxTxtControl.Text = "PDF Generated Sucessfully";
            else
                uxTxtControl.Text = "An error has been occured";
        }

        /// <summary>
        /// Handles the Click event of the uxcmdGenerateControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void uxcmdGenerateControl_Click(object sender, RoutedEventArgs e)
        {
            if (PDFGenerator.ExportDiagram(uxTxtControl,"New Title"))
                uxTxtControl.Text = "PDF Generated Sucessfully";
            else
                uxTxtControl.Text = "An error has been occured";
        }

        #endregion
    }
}
