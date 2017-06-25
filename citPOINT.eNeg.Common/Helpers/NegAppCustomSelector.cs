

#region → Usings   .
using Telerik.Windows.Controls;
using System.Windows;
using citPOINT.eNeg.Data.Web;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 14.09.10     M.Wahab         * creation
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
    /// A Custom Selector that select the proper edit template for Negotiation tree node or Conversation tree node
    /// </summary>
    public class NegAppCustomSelector : DataTemplateSelector
    {
        #region → Fields         .

        private DataTemplate mNegotationTemplate;
        private DataTemplate mApplicationTemplate;

        #endregion Fields

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
        ///  Gets or sets the Application Template
        /// </summary>
        public DataTemplate ApplicationTemplate
        {
            get
            {
                return mApplicationTemplate;
            }
            set
            {
                mApplicationTemplate = value;
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
            else if (item is NegotiationApplicationStatu)
                return this.mApplicationTemplate;
            return null;
        }

        #endregion Public

        #endregion Methods
        
    }
}
