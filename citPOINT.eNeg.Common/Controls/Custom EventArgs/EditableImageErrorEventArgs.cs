
#region → Usings   .
using System;

#endregion

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Class that inherit the EventArgs Class
    /// </summary>
    public class EditableImageErrorEventArgs : EventArgs
    {
        #region → Fields         .
        private string _errorMessage = string.Empty;
        #endregion

        #region → Constructors   .
        /// <summary>
        /// Propert that return the error Message if found
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        #endregion
    }


}