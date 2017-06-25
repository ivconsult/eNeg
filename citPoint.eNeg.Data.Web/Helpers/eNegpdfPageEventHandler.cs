#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 02.04.12     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Data.Web
{
    /// <summary>
    /// Custom pdf page event handler that help 
    /// in adding a header image to the pdf document
    /// </summary>
    public class eNegpdfPageEventHandler : PdfPageEventHelper
    {
        #region → Properties     .
        /// <summary>
        /// Gets or sets the image header.
        /// </summary>
        /// <value>The image header.</value>
        public Image ImageHeader { get; set; }
        #endregion

        #region → Methods        .

        /// <summary>
        /// Called when [end page].
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="document">The document.</param>
        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            #region Header   .
            // cell height 
            float cellHeight = document.TopMargin;
            // PDF document size      
            Rectangle page = document.PageSize;

            // create two column table
            PdfPTable head = new PdfPTable(1);
            head.TotalWidth = document.PageSize.Width;// page.Width;

            // add image; PdfPCell() overload sizes image to fit cell
            PdfPCell c = new PdfPCell(ImageHeader, true);
            c.HorizontalAlignment = Element.ALIGN_CENTER;
            c.FixedHeight = cellHeight;
            c.Border = PdfPCell.NO_BORDER;
            head.AddCell(c);

            // since the table header is implemented using a PdfPTable, we call
            // WriteSelectedRows(), which requires absolute positions!
            head.WriteSelectedRows(
              0, -1,  // first/last row; -1 flags all write all rows
              0,      // left offset
                // ** bottom** yPos of the table
              page.Height - cellHeight + head.TotalHeight,
              writer.DirectContent
            );

            #endregion

            #region Footer  .
            PdfPTable footerTbl = new PdfPTable(1);
            footerTbl.TotalWidth = document.PageSize.Width;
            
            //page number
            Chunk myFooter = new Chunk("< " + (document.PageNumber) +" >", FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 8, BaseColor.GRAY));
            PdfPCell footer = new PdfPCell(new Phrase(myFooter));
            footer.Border = Rectangle.NO_BORDER;
            footer.HorizontalAlignment = Element.ALIGN_CENTER;
            footerTbl.AddCell(footer);

            //this is for the position of the footer ... im my case is "+80"
            footerTbl.WriteSelectedRows(0, -1, 0, (document.BottomMargin ), writer.DirectContent);

            #endregion
            //base.OnEndPage(writer, document);
        }
        #endregion

    }
}
