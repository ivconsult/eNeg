#region → Usings   .
using citPOINT.eNeg.Data.Web;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 28.07.10     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date      set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// this class is used to can export messages or Chart as an PDF Document
    /// </summary>
    public static class PDFGenerator
    {

        #region → Fields         .
        private static Byte[] BytesBuffer;
        #endregion

        #region → Events         .

        /// <summary>
        /// Before Report Export Start.
        /// </summary>
        public static event EventHandler BeforeReportExportStart;

        /// <summary>
        /// After Report Exported.
        /// </summary>
        public static event EventHandler AfterReportExported;

        #endregion

        #region → Methods        .


        #region → Private        .

        /// <summary>
        /// Function Its main purpose is to generate array of bytes from UIElement  
        /// </summary>
        /// <param name="element">Value Of element</param>
        private static void ConvertUIElementToBytes(UIElement element)
        {
            //Create WriteableBitmap object which is what is being exported.
            WriteableBitmap wBitmap = new System.Windows.Media.Imaging.WriteableBitmap(element, null);

            wBitmap.Render(element, new TranslateTransform());

            wBitmap.Invalidate();

            BytesBuffer = GetBuffer(wBitmap);

        }

        #endregion


        #region → Public         .

        /// <summary>
        /// Function accessed directly using class name when you need to export messages in certain conversation.
        /// </summary>
        /// <param name="messages">Value Of message want to exported into PDF Document</param>
        public static bool ExportText(IEnumerable<Message> messages)
        {

            SaveFileDialog d = new SaveFileDialog();
            PdfDocument document = null;

            d.Filter = "PDF file format|*.pdf";
            eNegMessage Message;

            try
            {
                if (d.ShowDialog() == true)
                {

                    document = new PdfDocument();

                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    XFont MainTitleFont = new XFont("Times New Roman", 22, XFontStyle.Bold);
                    XFont SubTitleFont = new XFont("Times New Roman", 18);
                    XFont MessageFont = new XFont("Times New Roman", 12);

                    gfx.DrawString(messages.First().Conversation.ConversationName, MainTitleFont, XBrushes.DarkBlue, new XRect(50, 350, 300, 0));

                    //int PageNo = 0;
                    foreach (Message msg in messages)
                    {


                        PdfPage msgPage = document.AddPage();
                        XGraphics msggfx = XGraphics.FromPdfPage(msgPage);
                        XTextFormatter tf = new XTextFormatter(msggfx);

                        msggfx.DrawString("Subject: ", MainTitleFont, XBrushes.Black, new XRect(100, 50, 0, 0));
                        msggfx.DrawString(msg.MessageSubject, SubTitleFont, XBrushes.Black, new XRect(200, 50, 500, 0));
                        msggfx.DrawString("From: ", MainTitleFont, XBrushes.Black, new XRect(100, 75, 0, 0));
                        msggfx.DrawString(msg.MessageSender, SubTitleFont, XBrushes.Black, new XRect(200, 75, 0, 0));
                        msggfx.DrawString("To: ", MainTitleFont, XBrushes.Black, new XRect(100, 100, 0, 0));
                        msggfx.DrawString(msg.MessageReceiver, SubTitleFont, XBrushes.Black, new XRect(200, 100, 0, 0));
                        msggfx.DrawString("Channel: ", MainTitleFont, XBrushes.Black, new XRect(100, 125, 0, 0));
                        msggfx.DrawString(msg.Channel.ChannelName, SubTitleFont, XBrushes.Black, new XRect(200, 125, 0, 0));
                        msggfx.DrawString("Message: ", MainTitleFont, XBrushes.Black, new XRect(100, 150, 0, 0));
                        msggfx.DrawString("On " + msg.MessageDate.ToString(), SubTitleFont, XBrushes.Black, new XRect(200, 150, 0, 0));


                        int Start = 0;
                        int End = 1800;
                        string RemainingText;

                        // Check wether the size of the message will exceed the size of the pdf page or not
                        if (msg.MessageContent.Length < 1800)
                        {
                            End = msg.MessageContent.Length;

                            string text = msg.MessageContent.Substring(Start, End);
                            RemainingText = msg.MessageContent.Substring(End);

                            tf.DrawString(text, MessageFont, XBrushes.Black, new XRect(150, 170, 400, 900));
                        }
                        else
                        {
                            string text = msg.MessageContent.Substring(Start, End);
                            RemainingText = msg.MessageContent.Substring(End);

                            tf.DrawString(text, MessageFont, XBrushes.Black, new XRect(150, 170, 400, 900));

                            // Still Create new pages amd add them to the pdf document until there is no remaining message
                            while (RemainingText.Length > 0)
                            {
                                Start = 0;
                                End = 1800;

                                if (End > RemainingText.Length)
                                    End = RemainingText.Length;

                                PdfPage TempPage = document.AddPage();
                                XGraphics g = XGraphics.FromPdfPage(TempPage);
                                XTextFormatter f = new XTextFormatter(g);
                                //f.Alignment = XParagraphAlignment.Default;

                                string temptext = RemainingText.Substring(Start, End);

                                f.DrawString(temptext, MessageFont, XBrushes.Black, new XRect(150, 170, 400, 900));

                                RemainingText = RemainingText.Substring(End);

                            }
                        }
                    }



                    document.Save(d.OpenFile());
                    Message = new eNegMessage(Resources.ExportDone);
                    eNegMessanger.SendCustomMessage.Send(Message);
                    return true;
                }
                Message = new eNegMessage(string.Empty);
                eNegMessanger.SendCustomMessage.Send(Message);
                return false;
            }
            catch (Exception ex)
            {
                if (document != null)
                {
                    document.Close();
                }

                Message = new eNegMessage(string.Empty);
                eNegMessanger.SendCustomMessage.Send(Message);

                eNegMessanger.RaiseErrorMessage.Send(ex);

                return false;
            }
        }


        /// <summary>
        /// Function accessed directly using class name when you need to export chart, Diagram, or any other image in PDF format.
        /// </summary>
        /// <param name="element">Value Of element</param>
        /// <param name="Title">The title.</param>
        /// <returns></returns>
        public static bool ExportDiagram(UIElement element, string Title)
        {
            if (BeforeReportExportStart != null)
            {
                BeforeReportExportStart(null, null);
            }

            ConvertUIElementToBytes(element);

            if (AfterReportExported != null)
            {
                AfterReportExported(null, null);
            }

            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "PDF file format|*.pdf";
            PdfDocument document = null;

            try
            {
                if (d.ShowDialog() == true)
                {
                    document = new PdfDocument();
                    PdfPage page = document.AddPage();
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    MemoryStream ms = new MemoryStream(BytesBuffer);
                    XImage ximg = XImage.FromStream(ms);
                    XFont font = new XFont("Times New Roman", 18);

                    gfx.DrawString("Report For Negotiation: " + Title, font, XBrushes.DarkBlue, new XRect(10, 30, 0, 0));

                    gfx.DrawImage(ximg, new Point(10, 70));

                    document.Save(d.OpenFile());
                    document.Close();
                    return true;
                }


            }
            catch (Exception ex)
            {
                if (document != null)
                {
                    document.Close();
                }
                eNegMessanger.RaiseErrorMessage.Send(ex);
            }


            return false;
        }

        /// <summary>
        /// Gets the buffer.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns>
        /// Array of bytes to can be used as a stream
        /// </returns>
        public static byte[] GetBuffer(WriteableBitmap bitmap)
        {
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;

            MemoryStream ms = new MemoryStream();

            #region BMP File Header(14 bytes)

            //the magic number(2 bytes):BM  

            ms.WriteByte(0x42);

            ms.WriteByte(0x4D);

            //the size of the BMP file in bytes(4 bytes)  

            long len = bitmap.Pixels.Length * 4 + 0x36;

            ms.WriteByte((byte)len);

            ms.WriteByte((byte)(len >> 8));

            ms.WriteByte((byte)(len >> 16));

            ms.WriteByte((byte)(len >> 24));

            //reserved(2 bytes)  

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            //reserved(2 bytes)  

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            //the offset(4 bytes)  

            ms.WriteByte(0x36);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            #endregion

            #region Bitmap Information(40 bytes:Windows V3)

            //the size of this header(4 bytes)  

            ms.WriteByte(0x28);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            //the bitmap width in pixels(4 bytes)  

            ms.WriteByte((byte)width);

            ms.WriteByte((byte)(width >> 8));

            ms.WriteByte((byte)(width >> 16));

            ms.WriteByte((byte)(width >> 24));

            //the bitmap height in pixels(4 bytes)  

            ms.WriteByte((byte)height);

            ms.WriteByte((byte)(height >> 8));

            ms.WriteByte((byte)(height >> 16));

            ms.WriteByte((byte)(height >> 24));

            //the number of color planes(2 bytes)  

            ms.WriteByte(0x01);

            ms.WriteByte(0x00);

            //the number of bits per pixel(2 bytes)  

            ms.WriteByte(0x20);

            ms.WriteByte(0x00);

            //the compression method(4 bytes)  

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            //the image size(4 bytes)  

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            //the horizontal resolution of the image(4 bytes)  

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            //the vertical resolution of the image(4 bytes)  

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            //the number of colors in the color palette(4 bytes)  

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            //the number of important colors(4 bytes)  

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            ms.WriteByte(0x00);

            #endregion

            #region Bitmap data

            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    int pixel = bitmap.Pixels[width * y + x];

                    ms.WriteByte((byte)(pixel & 0xff)); //B  

                    ms.WriteByte((byte)((pixel >> 8) & 0xff)); //G  

                    ms.WriteByte((byte)((pixel >> 0x10) & 0xff)); //R  

                    ms.WriteByte(0x00); //reserved  

                }
            }

            #endregion

            return ms.GetBuffer();
        }
        #endregion

        #endregion
    }
}
