//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace citPOINT.eNeg.Data.Web
{
    public partial class Category
    {
        #region Primitive Properties
    
        public virtual int CategoryID
        {
            get;
            set;
        }
    
        public virtual string CategoryName
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<CategoryLog> CategoryLogs
        {
            get
            {
                if (_categoryLogs == null)
                {
                    var newCollection = new FixupCollection<CategoryLog>();
                    newCollection.CollectionChanged += FixupCategoryLogs;
                    _categoryLogs = newCollection;
                }
                return _categoryLogs;
            }
            set
            {
                if (!ReferenceEquals(_categoryLogs, value))
                {
                    var previousValue = _categoryLogs as FixupCollection<CategoryLog>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupCategoryLogs;
                    }
                    _categoryLogs = value;
                    var newValue = value as FixupCollection<CategoryLog>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupCategoryLogs;
                    }
                }
            }
        }
        private ICollection<CategoryLog> _categoryLogs;

        #endregion
        #region Association Fixup
    
        private void FixupCategoryLogs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (CategoryLog item in e.NewItems)
                {
                    item.Category = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (CategoryLog item in e.OldItems)
                {
                    if (ReferenceEquals(item.Category, this))
                    {
                        item.Category = null;
                    }
                }
            }
        }

        #endregion
    }
}