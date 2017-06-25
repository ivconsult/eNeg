#region → Usings   .
using System;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 29.11.10    Y.Mohamed     creation
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
    /// eNeg Message Custom Message Type.
    /// </summary>
    public class eNegMessage
    {
        #region → Fields         .
        private string mMessage = "";
        private ImageType mMessageType = ImageType.Success;
        private string mViewName;
        private Guid mReceiverApplicationID;
                
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return mMessage; }
            set { mMessage = value; }
        }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public ImageType MessageType
        {
            get { return mMessageType; }
            set { mMessageType = value; }
        }
         
        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        /// <value>The name of the view.</value>
        public string ViewName
        {
            get { return mViewName; }
            set { mViewName = value; }
        }

        /// <summary>
        /// Gets or sets the receiver application ID.
        /// </summary>
        /// <value>The receiver application ID.</value>
        public Guid ReceiverApplicationID
        {
            get { return mReceiverApplicationID; }
            set { mReceiverApplicationID = value; }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegMessage" /> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        public eNegMessage(string Message)
        {
            this.Message = Message;
        } 
        
        /// <summary>
        /// Initializes a new instance of the <see cref="eNegMessage"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="MessageType">Type of the message.</param>
        public eNegMessage(string Message, ImageType MessageType)
        {
            this.Message = Message;
            this.MessageType = MessageType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegMessage"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="MessageType">Type of the message.</param>
        /// <param name="receiverApplicationID">The receiver application ID.</param>
        public eNegMessage(string Message, ImageType MessageType,Guid receiverApplicationID)
        {
            this.Message = Message;
            this.MessageType = MessageType;
            this.ReceiverApplicationID = receiverApplicationID;
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [show message completed].
        /// </summary>
        public EventHandler ShowMessageCompleted;
        
        #endregion
    }
}
