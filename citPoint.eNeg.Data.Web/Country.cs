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
    public partial class Country
    {
        #region Primitive Properties
    
        public virtual System.Guid CountryID
        {
            get;
            set;
        }
    
        public virtual string CountryName
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<User> User
        {
            get
            {
                if (_user == null)
                {
                    var newCollection = new FixupCollection<User>();
                    newCollection.CollectionChanged += FixupUser;
                    _user = newCollection;
                }
                return _user;
            }
            set
            {
                if (!ReferenceEquals(_user, value))
                {
                    var previousValue = _user as FixupCollection<User>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupUser;
                    }
                    _user = value;
                    var newValue = value as FixupCollection<User>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupUser;
                    }
                }
            }
        }
        private ICollection<User> _user;

        #endregion
        #region Association Fixup
    
        private void FixupUser(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (User item in e.NewItems)
                {
                    item.Country = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (User item in e.OldItems)
                {
                    if (ReferenceEquals(item.Country, this))
                    {
                        item.Country = null;
                    }
                }
            }
        }

        #endregion
    }
}
