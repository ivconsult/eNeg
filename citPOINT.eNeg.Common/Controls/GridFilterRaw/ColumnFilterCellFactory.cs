#region → Usings   .
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 24.03.12   Yousra Reda         • creation
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
namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Static class acts as a factory that is responsible for 
    /// generating the appropriate filter cell according to the column type
    /// </summary>
    public static class ColumnFilterCellFactory
    {
        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Creates the column header.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public static ColumnFilterCell CreateColumnHeader(GridViewDataColumn column)
        {
            ColumnFilterCell columnHeader = CreateColumnHeaderFromType(Nullable.GetUnderlyingType(column.DataType));

            columnHeader.FilterDescriptor = new Telerik.Windows.Data.FilterDescriptor();
            columnHeader.FilterDescriptor.Operator = FilterOperator.Contains;
            columnHeader.FilterDescriptor.Member = column.DataMemberBinding.Path.Path;
            columnHeader.FilterDescriptor.MemberType = column.DataType;
            

            if (columnHeader is DateTimeColumnHeader)
            {
                columnHeader.FilterDescriptor.Operator = FilterOperator.IsNotEqualTo;
            }
            else if (columnHeader is TextColumnHeader)
            {
                columnHeader.FilterDescriptor.Operator = FilterOperator.Contains;
                columnHeader.FilterDescriptor.Value = null;
                switch (columnHeader.FilterDescriptor.Member)
                {
                    case "MessageSender":
                        (columnHeader as TextColumnHeader).uxTextFilter.EmptyContent = "Enter Sender";
                        break;

                    case "MessageReceiver":
                        (columnHeader as TextColumnHeader).uxTextFilter.EmptyContent = "Enter Receiver";
                        break;

                    case "MessageChannel":
                        (columnHeader as TextColumnHeader).uxTextFilter.EmptyContent = "Enter Channel";
                        break;

                    case "MessageSubject":
                        (columnHeader as TextColumnHeader).uxTextFilter.EmptyContent = "Enter Subject";
                        break;
                }
            }

            return columnHeader;
        }
        #endregion

        #region → Private        .

        /// <summary>
        /// Creates the type of the column header from.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static ColumnFilterCell CreateColumnHeaderFromType(Type type)
        {
            if ((type == typeof(DateTime?)) || (type == typeof(DateTime)))
            {
                return new DateTimeColumnHeader();
            }


            return new TextColumnHeader();
        }

        #endregion

        #endregion
    }
}
