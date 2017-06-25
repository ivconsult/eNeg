
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using System.Linq;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 06.09.11     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.MVVM.UnitTest
{
    /// <summary>
    /// Mock of Register Model
    /// </summary>
    public class MockConfirmModel : IConfirmMailModel
    {
        #region → Fields         .
        private List<User> _users;
        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Any Data in current Context has changed
        /// e.g Added,Modified,or Deleted.
        /// </summary>
        /// <value></value>
        public bool HasChanges
        {
            get { return false; }
        }

        /// <summary>
        /// is Current Context is Busy in Loading Data or Submitting.
        /// </summary>
        /// <value></value>
        public bool IsBusy
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the user organization.
        /// </summary>
        /// <value>The user organization.</value>
        public Organization UserOrganization
        {
            get
            {
                return new Organization()
                {
                    OrganizationID = Guid.NewGuid(),
                    OrganizationName = "citPoint",
                    OrganizationTypeID = 1
                };
            }
        }


        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MockConfirmModel"/> class.
        /// </summary>
        public MockConfirmModel()
        {
            _users = new List<User>()
                            {
                               new User()
                                {
                                    UserID = new Guid("6E60D43F-5C70-48D9-8511-2564F58E483E"),
                                    EmailAddress = "Yousra.Reda@gmail.com",
                                    Password = "123456A",
                                    NewPassword = string.Empty,
                                    PasswordConfirmation = "123456A",
                                    Locked = false,
                                    IPAddress = "10.0.02.2",
                                    LastLoginDate = DateTime.Now,
                                    CreateDate = DateTime.Now,
                                    AnswerHash = string.Empty,
                                    AnswerSalt = string.Empty,
                                    Online = false,
                                    Disabled = false,
                                    FirstName = string.Empty,
                                    LastName = string.Empty,
                                    CompanyName = string.Empty,
                                    Address = string.Empty,
                                    City = string.Empty,
                                    Phone = string.Empty,
                                    Mobile = string.Empty,
                                    Gender = false,
                                    Reset = false
                                },
                                 new User()
                                {
                                    UserID = Guid.NewGuid(),
                                    EmailAddress = "TestUser@gmail.com",
                                    Password = "123456A",
                                    NewPassword = string.Empty,
                                    PasswordConfirmation = "123456A",
                                    Locked = false,
                                    IPAddress = "10.0.02.2",
                                    LastLoginDate = DateTime.Now,
                                    CreateDate = DateTime.Now,
                                    AnswerHash = string.Empty,
                                    AnswerSalt = string.Empty,
                                    Online = false,
                                    Disabled = false,
                                    FirstName = string.Empty,
                                    LastName = string.Empty,
                                    CompanyName = string.Empty,
                                    Address = string.Empty,
                                    City = string.Empty,
                                    Phone = string.Empty,
                                    Mobile = string.Empty,
                                    Gender = false,
                                    Reset = false
                                }
                            };
        }

        #endregion Constructor

        #region → Events         .

        /// <summary>
        /// Occurs when [get organization by user ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationByUserIDComplete;

        /// <summary>
        /// Event Handler For Method  UpdateUserByConfirmMail
        /// Update Locked Flag By false
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> UpdateUserByConfirmMailComplete;

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region → Methods        .

        /// <summary>
        /// Updates the user by confirm mail.
        /// </summary>
        /// <param name="OperationString">The operation string.</param>
        /// <param name="OperationStringType">Type of the operation string.</param>
        public void UpdateUserByConfirmMail(string OperationString, byte OperationStringType)
        {
            if (UpdateUserByConfirmMailComplete != null)
            {
                if (OperationString == "1pM86JgdOLUAXihLaEaqRWazGmFOJ6Tum2JGhvTaPFIa/PvgrH/pPtukGaUZUNOvnZBLhJfR2sGNdQGnPR0u8s")
                {
                    UpdateUserByConfirmMailComplete(this, new eNegEntityResultArgs<User>(_users));
                }
                else
                {
                    UpdateUserByConfirmMailComplete(this, new eNegEntityResultArgs<User>(new List<User>()));
                }
            }
        }


        /// <summary>
        /// Gets the organization by its owner ID.
        /// </summary>
        /// <param name="OwnerID">The owner ID.</param>
        public void GetOrganizationByItsOwnerID(Guid OwnerID)
        {
            if (GetOrganizationByUserIDComplete != null)
            {
                GetOrganizationByUserIDComplete(this, new eNegEntityResultArgs<Organization>(new List<Organization>() { UserOrganization }));
            }
        }

        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {

        }

        #endregion Methods



    }
}
