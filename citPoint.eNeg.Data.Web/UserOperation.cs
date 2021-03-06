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
    public partial class UserOperation
    {
        #region Primitive Properties
    
        public virtual System.Guid OperationID
        {
            get;
            set;
        }
    
        public virtual Nullable<System.Guid> UserID
        {
            get { return _userID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_userID != value)
                    {
                        if (User != null && User.UserID != value)
                        {
                            User = null;
                        }
                        _userID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<System.Guid> _userID;
    
        public virtual string OperationString
        {
            get;
            set;
        }
    
        public virtual Nullable<byte> Type
        {
            get;
            set;
        }
    
        public virtual string NewEmailAddress
        {
            get;
            set;
        }
    
        public virtual Nullable<System.Guid> RequestUserID
        {
            get;
            set;
        }
    
        public virtual Nullable<System.Guid> OrganizationID
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual User User
        {
            get { return _user; }
            set
            {
                if (!ReferenceEquals(_user, value))
                {
                    var previousValue = _user;
                    _user = value;
                    FixupUser(previousValue);
                }
            }
        }
        private User _user;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.UserOperations.Contains(this))
            {
                previousValue.UserOperations.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.UserOperations.Contains(this))
                {
                    User.UserOperations.Add(this);
                }
                if (UserID != User.UserID)
                {
                    UserID = User.UserID;
                }
            }
            else if (!_settingFK)
            {
                UserID = null;
            }
        }

        #endregion
    }
}
