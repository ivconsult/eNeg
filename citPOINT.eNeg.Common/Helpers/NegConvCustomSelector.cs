#region → Usings   .
using System.Windows;
using citPOINT.eNeg.Data.Web;
using Telerik.Windows.Controls;
#endregion

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// A Custom Selector that select the proper edit template for Negotiation tree node or Conversation tree node
    /// </summary>
    public class NegConvCustomSelector : DataTemplateSelector
    {
        #region → Fields         .

        private DataTemplate mNegotationTemplate;
        private DataTemplate mConversationTemplate;

        #endregion

        #region → Properties     .
       
        /// <summary>
        ///  Gets or sets the NegotationTemplate
        /// </summary>
        public DataTemplate NegotationTemplate
        {
            get
            {
                return mNegotationTemplate;
            }
            set
            {
                mNegotationTemplate = value;
            }
        }

        /// <summary>
        ///  Gets or sets the ConversationTemplate
        /// </summary>
        public DataTemplate ConversationTemplate
        {
            get
            {
                return mConversationTemplate;
            }
            set
            {
                mConversationTemplate = value;
            }
        }

        #endregion Properties

        #region → Methods        .

        #region → Public         .


        /// <summary>
        /// Select the suitable template according to the selected node type
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="container">container</param>
        /// <returns>DataTemplate</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Negotiation)
                return this.mNegotationTemplate;
            else if (item is Conversation)
                return this.mConversationTemplate;
            return null;
        }


        #endregion Public

        #endregion Methods




    }
}
