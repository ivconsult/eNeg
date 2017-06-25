
#region → Usings   .

using System;
using System.Collections.Generic;
using System.Linq;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 5/3/2011      Team         • creation
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

namespace citPOINT.eNeg.Infrastructure.ExceptionHandling
{
    /// <summary>
    /// Exception Manager
    /// </summary>
    public class ExceptionManager : IExceptionManager
    {

        #region → Fields         .

        private static ExceptionManager mSilverlightExceptionManager = new ExceptionManager();
        private PostHandlingAction mPostHandlingAction;
        private List<IExceptionPolicy> mArrExceptionPolicy;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ExceptionManager Instance
        {
            get
            {
                return mSilverlightExceptionManager;
            }

        }

        /// <summary>
        /// Gets or sets the post handling action.
        /// </summary>
        /// <value>The post handling action.</value>
        public PostHandlingAction postHandlingAction
        {
            set { mPostHandlingAction = value; }
            get { return mPostHandlingAction; }
        }
        
        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionManager"/> class.
        /// </summary>
        public ExceptionManager()
        {
            mPostHandlingAction = PostHandlingAction.None;
            mArrExceptionPolicy = new List<IExceptionPolicy>();
        }

        #endregion

        #region → Methods        .


        #region → Public         .

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        public ExceptionHandlingResult HandleException(Exception e, string policy)
        {
            IExceptionPolicy exPolicy = (from a in mArrExceptionPolicy where a.Name == policy select a).First();
            return exPolicy.Excute(e);
        }

        /// <summary>
        /// Adds the policy.
        /// </summary>
        /// <param name="policyName">Name of the policy.</param>
        /// <returns>True or False</returns>
        public bool AddPolicy(string policyName)
        {
            if (mArrExceptionPolicy.Where(a => a.Name == policyName).Count() == 0) //Not Exist
            {

                try
                {
                    mArrExceptionPolicy.Add(new ExceptionPolicy(policyName));
                    return true;
                }
                catch { }
            }
            return false;
        }

        /// <summary>
        /// Removes the policy.
        /// </summary>
        /// <param name="policyName">Name of the policy.</param>
        /// <returns>True or False</returns>
        public bool RemovePolicy(string policyName)
        {
            IExceptionPolicy p = mArrExceptionPolicy.Where(a => a.Name == policyName).FirstOrDefault();

            if (p != null)
            {
                try
                {
                    mArrExceptionPolicy.Remove(p);
                    return true;
                }
                catch (Exception e)
                {
                    ;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the policy.
        /// </summary>
        /// <param name="policyName">Name of the policy.</param>
        /// <returns>Exception Policy</returns>
        public IExceptionPolicy GetPolicy(string policyName)
        {
            return mArrExceptionPolicy.Where(a => a.Name == policyName).FirstOrDefault();
        }

        #endregion  Public


        #endregion Methods

    }
}
