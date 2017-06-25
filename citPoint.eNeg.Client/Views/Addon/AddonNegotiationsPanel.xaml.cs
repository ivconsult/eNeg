
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.ServiceModel.DomainServices.Client;
using System.Collections.Generic;
using System.Windows.Controls;
using System.IO;
using GalaSoft.MvvmLight;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 21.03.12   Yousra Reda         • creation
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
namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// Negotiation Panel for all negotiaions types
    /// </summary>
    public partial class AddonNegotiationsPanel : UserControl
    {
        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="NegotiationsPanel"/> class.
        /// </summary>
        public AddonNegotiationsPanel()
        {
            InitializeComponent();
        }
        #endregion
    }
}
