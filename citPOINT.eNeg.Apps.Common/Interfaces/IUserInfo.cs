#region → Usings   .
using System;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 18.03.12    mwahab         • creation
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

namespace citPOINT.eNeg.Apps.Common.Interfaces
{
    /// <summary>
    /// Interface for Users information .
    /// </summary>
    public interface IUserInfo
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>The user ID.</value>
        Guid UserID { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        string EmailAddress { get; set; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        string DisplayName { get; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        string FirstName { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        string FullName { get; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        bool? Gender { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>The mobile.</value>
        string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        string Phone { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        string City { get; set; }

        /// <summary>
        /// Gets or sets the culture ID.
        /// </summary>
        /// <value>The culture ID.</value>
        int? CultureID { get; set; }

        /// <summary>
        /// Gets or sets the country ID.
        /// </summary>
        /// <value>The country ID.</value>
        Guid? CountryID { get; set; }

        /// <summary>
        /// Gets or sets the LCID.
        /// </summary>
        /// <value>The LCID.</value>
        int? LCID { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the has public profile.
        /// </summary>
        /// <value>The has public profile.</value>
        bool? HasPublicProfile { get; set; }

        /// <summary>
        /// Gets or sets the profile description.
        /// </summary>
        /// <value>The profile description.</value>
        string ProfileDescription { get; set; }
        
        #endregion
    }
}
