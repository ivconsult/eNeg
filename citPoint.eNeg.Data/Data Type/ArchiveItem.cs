
#region → Usings   .
using System.Collections.Generic;
using System.ComponentModel;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 3/26/2012 2:22:17 PM      mwahab         • creation
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

namespace citPOINT.eNeg.Data
{
    /// <summary>
    /// Class for Archive Item.
    /// will be used in tree 2012 --> March and so on.
    /// </summary>
    public class ArchiveItem : IArchiveItem
    {
        #region → Fields         .

        private object mValue;
        private string mName;
        private bool mIsLoadedOnDemand = false;

        private ArchiveItemTypes mArchiveItemType;
        private IArchiveItem mParent;
        private IList<IArchiveItem> mChildren;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the type of the archive item.
        /// </summary>
        /// <value>The type of the archive item.</value>
        public ArchiveItemTypes ArchiveItemType
        {
            get
            {
                return mArchiveItemType;
            }
            set
            {
                if (mArchiveItemType != value)
                {
                    mArchiveItemType = value;
                    this.RaisePropertyChanged("ArchiveItemType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get
            {
                return mValue;
            }
            set
            {
                if (mValue != value)
                {
                    mValue = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                if (mName != value)
                {
                    mName = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loaded on demand.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is loaded on demand; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoadedOnDemand
        {
            get
            {
                return mIsLoadedOnDemand;

            }
            set
            {
                mIsLoadedOnDemand = value;
                this.RaisePropertyChanged("IsLoadedOnDemand");
            }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public IArchiveItem Parent
        {
            get
            {
                return mParent;
            }
            set
            {
                if (mParent != value)
                {
                    mParent = value;
                    this.RaisePropertyChanged("Parent");
                }
            }
        }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public IList<IArchiveItem> Children
        {
            get
            {
                return mChildren;
            }
            set
            {
                if (mChildren != value)
                {
                    mChildren = value;
                    this.RaisePropertyChanged("Children");
                }
            }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiveItem"/> class.
        /// </summary>
        public ArchiveItem()
        {
            this.Children = new List<IArchiveItem>();
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Refreshes the children.
        /// </summary>
        public void RefreshChildren()
        {
            this.Children = new List<IArchiveItem>(this.Children);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ArchiveItemType.ToString() + "_" + this.Value.ToString();
        }

        #endregion  Public

        #endregion Methods




    }
}
