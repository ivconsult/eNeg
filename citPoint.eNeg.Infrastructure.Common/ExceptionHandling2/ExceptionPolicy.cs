
#region → Usings   .
using System;
using System.Collections.Generic;
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
    /// Exception Policy
    /// </summary>
    public class ExceptionPolicy : IExceptionPolicy
    {
        #region → Fields         .

        private List<IHandler> mArrHandler = new List<IHandler>();
        private string mPolicy;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return mPolicy;
            }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionPolicy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ExceptionPolicy(string name)
        {
            mPolicy = name;
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        public bool AddHandler(IHandler handler)
        {
            mArrHandler.Add(handler);
            return true;
        }

        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        public bool RemoveHandler(IHandler handler)
        {
            mArrHandler.Remove(handler);
            return true;
        }

        /// <summary>
        /// Excutes the specified Exception.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns>Exception Handling Result</returns>
        public ExceptionHandlingResult Excute(Exception e)
        {
            ExceptionHandlingResult result = null;

            switch (ExceptionManager.Instance.postHandlingAction)
            {
                case PostHandlingAction.None:
                    {
                        Exception newException = null;
                        foreach (IHandler handler in mArrHandler)
                        {
                            if (newException != null)
                                newException = handler.Excute(newException);
                            else
                                newException = handler.Excute(e);
                            result = new ExceptionHandlingResult(newException, false, newException.Message);
                        }
                    }
                    break;
                case PostHandlingAction.NotifyRethrow:
                    {
                        Exception newException = null;
                        foreach (IHandler handler in mArrHandler)
                        {
                            if (newException != null)
                                newException = handler.Excute(newException);
                            else
                                newException = handler.Excute(e);
                        }
                        throw (e);
                    }
                    break;
                case PostHandlingAction.ThrowNewException:
                    {
                        Exception newException = null;
                        foreach (IHandler handler in mArrHandler)
                        {
                            if (newException != null)
                                newException = handler.Excute(newException);
                            else
                                newException = handler.Excute(e);
                        }
                        throw (newException);
                    }
                    break;
                default:
                    break;
            }

            return result;
        }
        #endregion  Public

        #endregion Methods

    }
}
