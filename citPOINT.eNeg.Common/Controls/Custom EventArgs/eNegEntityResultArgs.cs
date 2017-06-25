

#region → Usings   .

using System;
using System.ServiceModel.DomainServices.Client;
using System.Collections.Generic;

#endregion

#region → History  .

/* Date         User        Change
 * 
 * 27.07.10     M.wahab     Creation
 * 26.09.10     M.Wahab     Updating XML Comments
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
    /// Return A collection of Entiry
    /// ex. Collection of Employees
    /// </summary>
    /// <typeparam name="T">Entity Type ex.Employee</typeparam>
    public class eNegEntityResultArgs<T> : eNegBaseResultArgs where T : Entity
    {

        #region → Fields         .

        private IEnumerable<T> mResults;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Get a Collection of Entity of Type T
        /// </summary>
        public IEnumerable<T> Results
        {
            get { return mResults; }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="ex">Value of Exception</param>
        public eNegEntityResultArgs(Exception ex)
            : base(ex)
        {

        }
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="results">a collection of Entity of Type T</param>
        public eNegEntityResultArgs(IEnumerable<T> results)
            : base(null)
        {
            mResults = results;

        }

        #endregion Constructors

    }
}
