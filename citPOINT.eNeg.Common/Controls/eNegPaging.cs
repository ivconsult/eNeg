#region → Usings   .
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Controls;
using System.Windows.Media;

#endregion

#region → History  .

/* Date         User
 * 
 * 17.08.10     Mohamed Abdulwahab
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
    /// Used As Connected Mode Pagger
    /// </summary>
    public class eNegPaging
    {

        #region → Fields         .

        /// <summary>
        /// The Current Page Number
        /// </summary>
        public int CurrentPageNumber = 1;


        /// <summary>
        /// Number Of Items Per Page
        /// </summary>
        public  int ITEMSPAGESIZE = 5;

        /// <summary>
        /// Number of Pages displayed In the Panel Control
        /// eg.  1 2 3 
        /// </summary>
        public readonly int ITEMSDISPLAYPAGECOUNT = 3;

        /// <summary>
        /// Total Number Of Pages ex. 10 Pages
        /// </summary>
        public int ItemsPagesCount = 0;

        /// <summary>
        /// Total Number of Items  Items.count
        /// </summary>
        public int ItemsCount = 0;

        /// <summary>
        /// StackPanel Pager Control
        /// </summary>
        public StackPanel uxPagePanel { get; set; }

        #endregion Fields


        #region → Methods        .
        #region → Private        .


        /// <summary>
        /// Ceilling For Integer
        /// </summary>
        /// <param name="x">first Number</param>
        /// <param name="y">Second Number</param>
        /// <returns>Ceilling Value</returns>
        private int Ceiling(int x, int y)
        {
            return int.Parse(Math.Ceiling(
                double.Parse(x.ToString()) /
                double.Parse(y.ToString())).ToString());
        }

        /// <summary>
        /// Generate New HyperlinkButton and Return It
        /// </summary>
        /// <param name="text">The Content of The Button ex."1"</param>
        /// <param name="Command">the Command That Will Be Carry Out</param>
        /// <param name="CurrentPage">the Current Page Number</param>
        /// <returns>Return HyperlinkButton</returns>
        private HyperlinkButton GetHyperPageLink(string text, RelayCommand<string> Command, int CurrentPage)
        {
            HyperlinkButton uxlnk = new HyperlinkButton();
            uxlnk.Command = Command;
            uxlnk.CommandParameter = text;
            uxlnk.Content = text;
            if (int.Parse(text) == CurrentPage)
            {
                //uxlnk.IsEnabled = false;
                uxlnk.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                //uxlnk.FontSize = uxlnk.FontSize * 1.5;

            }

            uxlnk.Margin = new System.Windows.Thickness(text.Length > 3 ? -1 : 2);
            return uxlnk;
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Relay command of Navigate To Page
        /// </summary>
        public RelayCommand<string> GetTableByPageNumber;

        /// <summary>
        /// Building Paging System e.g 1,2,3
        /// </summary>
        public void BuildPaging()
        {
            if (ItemsCount > 0)
            {
                if (CurrentPageNumber <= 0)
                    CurrentPageNumber = 1;
            }

            if (CurrentPageNumber > 0)
            {
                //Calculating The Count of pages ex.10 Pages
                ItemsPagesCount = Ceiling(ItemsCount, ITEMSPAGESIZE);

                //Start of Paging Group ex 1 or 4,7
                int GroupCountStart = Ceiling(CurrentPageNumber, ITEMSDISPLAYPAGECOUNT);

                #region "Group Start"

                /*
                 * incase if we are in group 2 e.g then you need get the start
                 * page number of the second group
                 * =((2-1)*3) +1   = 
                 * where 2 is groupcount
                 * 3 is number of pages displayed at once 1,2,3 ---- or 4,5,6
                 *
                 */

                if (GroupCountStart > 1)
                {
                    GroupCountStart = ((GroupCountStart - 1) * ITEMSDISPLAYPAGECOUNT) + 1;
                }

                #endregion "Group Start"

                #region "GroupCountEnd"

                int GroupCountEnd = GroupCountStart + ITEMSDISPLAYPAGECOUNT - 1;
                GroupCountEnd = GroupCountEnd > ItemsPagesCount ? ItemsPagesCount : GroupCountEnd;

                #endregion "GroupCountEnd"

                #region "Page panel"
                if (uxPagePanel != null)
                {
                    uxPagePanel.Children.Clear();
                    for (int i = GroupCountStart; i <= GroupCountEnd; i++)
                        uxPagePanel.Children.Add(GetHyperPageLink((i).ToString(), GetTableByPageNumber, CurrentPageNumber));
                }
                #endregion

            }
            else
            {
                if (uxPagePanel != null)
                {
                    uxPagePanel.Children.Clear();
                }
            }
        }

        #endregion
        #endregion Methods
    }
 
}
