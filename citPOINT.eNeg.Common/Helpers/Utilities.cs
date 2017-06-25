


#region → Usings   .
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System;
using citPOINT.eNeg.Data.Web;
using System.Collections.Generic;
using System.Windows.Controls;
using citPOINT.eNeg.Apps.Common.Interfaces;

#endregion

#region → History  .
/* Date         User        Change
 * 
 * 22.07.10    m.Wahab     • creation
 * 16.08.10    Yousra      • Save Current UserID 
 * 17.08.10    Yousra      • Indicate whether we work on addon or on web Platform
 * 
 */
#endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

#endregion

namespace citPOINT.eNeg.Common
{

    /// <summary>
    /// Used To Apply or getting Any global configurations
    /// </summary>
    public class AppConfigurations
    {

        #region → Properties     .

        #region Static

        /// <summary>
        /// Detect if the client runngin in the Offline Mode Or Online Mode
        /// </summary>
        public static bool IsOffLineMode
        {
            get
            {
                return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            }
        }

        /// <summary>
        /// Gets or sets the current login user.
        /// </summary>
        /// <value>The current login user.</value>
        public static LoginUser CurrentLoginUser { get; set; }

        /// <summary>
        /// Gets or sets the is start new negotiation.
        /// </summary>
        /// <value>The is start new negotiation.</value>
        public static bool IsStartNewNegotiation { get; set; }

        /// <summary>
        /// Gets or sets the profile user ID.
        /// </summary>
        /// <value>The profile user ID.</value>
        public static Guid ProfileUserID { get; set; }

        /// <summary>
        /// Determin If We Are working in Addon
        /// </summary>
        public static bool IsAddon { get; set; }

        /// <summary>
        /// Carry the Base Address
        /// e.g http://Localhost:9000/
        /// </summary>
        public static string HostBaseAddress { get; set; }

        /// <summary>
        /// Indicates whether Application running out of browser or not  
        /// </summary>
        public static bool IsRunningOutOfBrowser { get; set; }

        /// <summary>
        /// Indicates whether In Addon is second time to reload page which contain   
        /// </summary>
        public static bool AddonIsPostBack { get; set; }

        /// <summary>
        /// Gets or sets the negotiatio ID parameter.
        /// </summary>
        /// <value>The negotiatio ID parameter.</value>
        public static Guid? NegotiationIDParameter { get; set; }

        /// <summary>
        /// Gets or sets the conversation ID parameter.
        /// </summary>
        /// <value>The conversation ID parameter.</value>
        public static Guid? ConversationIDParameter { get; set; }

        /// <summary>
        /// Gets or sets the message ID parameter.
        /// </summary>
        /// <value>The message ID parameter.</value>
        public static Guid? MessageIDParameter { get; set; }

        /// <summary>
        /// Gets or sets the action type parameter.
        /// </summary>
        /// <value>The action type parameter.</value>
        public static string ActionTypeParameter { get; set; }

        /// <summary>
        /// Gets or sets the application ID parameter.
        /// </summary>
        /// <value>The application ID parameter.</value>
        public static Guid? ApplicationIDParameter { get; set; }


        /// <summary>
        /// Gets or sets the negotiation or Conversation status parameter.
        /// </summary>
        /// <value>The negotiation or Conversation status parameter.</value>
        public static string StatusParameter { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is status paramter is open.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is status paramter is open; otherwise, <c>false</c>.
        /// </value>
        public static bool IsStatusParamterIsOpen
        {
            get
            {
                if (string.IsNullOrEmpty(StatusParameter) || string.IsNullOrWhiteSpace(StatusParameter))
                    return true;

                if (StatusParameter.ToLower() == "Closed".ToLower())
                    return false;

                return true;
            }

        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove original owner].
        /// </summary>
        /// <value><c>true</c> if [remove original owner]; otherwise, <c>false</c>.</value>
        public static bool RemoveOriginalOwner { get; set; }

        /// <summary>
        /// Gets or sets the application region.
        /// </summary>
        /// <value>The application region.</value>
        public static ContentControl ApplicationRegion{ get; set; }

        #endregion
        
        #endregion
    }

    #region Serialization class

    /// <summary>
    /// Used To Serialize Deserialize Any Objects
    /// </summary>
    public class Serialization
    {

        #region → Methods        .

        #region → Public         . Static
        /// <summary>
        /// Converting a strem of string data to the current logItem 
        /// </summary>
        /// <param name="objectToSerialize">Value Of objectToSerialize</param>
        /// <returns>serialization result</returns>
        public static string SerializeToJsonString(object objectToSerialize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                        new DataContractJsonSerializer(objectToSerialize.GetType());

                serializer.WriteObject(ms, objectToSerialize);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }


        /// <summary>
        /// Mean return the actual Object From a string
        /// </summary>
        /// <param name="jsonString">Logging stream</param>
        /// <returns>the actual object of type log item</returns>
        public static T DeserializeToPerson<T>(string jsonString)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer =
                        new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }

        #endregion

        #endregion
    }

    #endregion
}
