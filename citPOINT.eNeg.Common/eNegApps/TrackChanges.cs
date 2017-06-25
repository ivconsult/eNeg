#region → Usings   .
using System.Linq;
using citPOINT.eNeg.Apps.Common.Interfaces;
using System.Collections.Generic;
using System;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 18.03.12     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion

namespace citPOINT.eNeg.Common.eNegApps
{
    /// <summary>
    /// Class for Track Changes.
    /// </summary>
    public class TrackChanges : ITrackChanges
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the observer app list.
        /// </summary>
        /// <value>The observer app list.</value>
        private List<IObserverApp> ObserverAppList { get; set; }

        /// <summary>
        /// Gets or sets the third party app list.
        /// </summary>
        /// <value>The third party app list.</value>
        private List<IeNegApplication> eNegAppsList { get; set; }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackChanges"/> class.
        /// </summary>
        /// <param name="eNegAppsList">The e neg apps list.</param>
        public TrackChanges(List<IeNegApplication> eNegAppsList)
        {
            this.eNegAppsList = eNegAppsList;
            this.ObserverAppList = new List<IObserverApp>();
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Gets the name of the is active for app.
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
        private bool GetIsActiveForAppName(string appName)
        {
            var selectedApp = this.eNegAppsList.Where(s => s.ApplicationTitle.Equals(appName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (selectedApp != null)
            {
                return selectedApp.IsActive;
            }

            return false;
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Adds the observer app.
        /// </summary>
        /// <param name="observerApp">The observer app.</param>
        public void AddObserverApp(IObserverApp observerApp)
        {
            this.ObserverAppList.Add(observerApp);
            
            observerApp.ApplyChanges(GetIsActiveForAppName(observerApp.AppName));
        }

        /// <summary>
        /// Removes the observer app.
        /// </summary>
        /// <param name="observerApp">The observer app.</param>
        public void RemoveObserverApp(IObserverApp observerApp)
        {
            this.ObserverAppList.Remove(observerApp);
        }

        /// <summary>
        /// Notifies all registered Apps.
        /// </summary>
        public void Notify()
        {
            foreach (var AppItem in this.ObserverAppList)
            {
                AppItem.ApplyChanges(GetIsActiveForAppName(AppItem.AppName));
            }
        }
         
        #endregion

        #endregion

    }
}
