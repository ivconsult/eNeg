
#region → Usings   .
using System;
using System.Windows.Media;
using System.IO;
#endregion

namespace citPOINT.eNeg.Common
{ 
 
        /// <summary>
    /// Helper class used to convert a control to Editable Image to can convert to can draw at as an Image in exported PDFDocument
    /// </summary>
    public class EditableImage
    {
        #region → Fields         .

        private int _width = 0;
        private int _height = 0;
        private bool _init = false;
        private byte[] _buffer;
        private int _rowLength;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Represent the Width of the IMage
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (_init)
                {
                    OnImageError("Error: Cannot change Width after the EditableImage has been initialized");
                }
                else if ((value <= 0) || (value > 2047))
                {
                    OnImageError("Error: Width must be between 0 and 2047");
                }
                else
                {
                    _width = value;
                }
            }
        }

        /// <summary>
        /// Represent the height of the image
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (_init)
                {
                    OnImageError("Error: Cannot change Height after the EditableImage has been initialized");
                }
                else if ((value <= 0) || (value > 2047))
                {
                    OnImageError("Error: Height must be between 0 and 2047");
                }
                else
                {
                    _height = value;
                }
            }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableImage"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public EditableImage(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        #endregion Constructor

        #region → Events         .

        /// <summary>
        /// Event that raised when there is an error in the process of converstion
        /// </summary>
        public event EventHandler<EditableImageErrorEventArgs> ImageError;

        #endregion Events
         
        #region → Methods        .
                
        #region → Private        .

        /// <summary>
        /// Called when [image error].
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private void OnImageError(string msg)
        {
            if (null != ImageError)
            {
                EditableImageErrorEventArgs args = new EditableImageErrorEventArgs();
                args.ErrorMessage = msg;
                ImageError(this, args);
            }
        }
         
        #endregion Private
        
        #region → Public         .

        /// <summary>
        /// Call another method which get the RGB color component separatly 
        /// </summary>
        /// / <param name="col">Column number</param>
        /// <param name="row">Row number</param>
        /// <param name="color">RGB Color Value</param>
        public void SetPixel(int col, int row, Color color)
        {
            SetPixel(col, row, color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Draw the new image (step by atep)
        /// </summary>
        /// <param name="col">Column number</param>
        /// <param name="row">Row number</param>
        /// <param name="red">RGB Colors Red Value</param>
        /// <param name="green">RGB Colors Green Value</param>
        /// <param name="blue">RGB Colors Blue Value</param>
        /// <param name="alpha">RGB Colors Alpha Value</param>
        public void SetPixel(int col, int row, byte red, byte green, byte blue, byte alpha)
        {
            if (!_init)
            {
                _rowLength = _width * 4 + 1;
                _buffer = new byte[_rowLength * _height];

                // Initialize
                for (int idx = 0; idx < _height; idx++)
                {
                    _buffer[idx * _rowLength] = 0;      // Filter bit
                }

                _init = true;
            }

            if ((col > _width) || (col < 0))
            {
                OnImageError("Error: Column must be greater than 0 and less than the Width");
            }
            else if ((row > _height) || (row < 0))
            {
                OnImageError("Error: Row must be greater than 0 and less than the Height");
            }

            // Set the pixel
            int start = _rowLength * row + col * 4 + 1;
            _buffer[start] = red;
            _buffer[start + 1] = green;
            _buffer[start + 2] = blue;
            _buffer[start + 3] = alpha;
        }

        /// <summary>
        /// Method that return the color of certain region of pixels
        /// </summary>
        /// <param name="col">Column Number</param>
        /// <param name="row">Row Number</param>
        /// <returns>Color of that surrounded region</returns>
        public Color GetPixel(int col, int row)
        {
            if ((col > _width) || (col < 0))
            {
                OnImageError("Error: Column must be greater than 0 and less than the Width");
            }
            else if ((row > _height) || (row < 0))
            {
                OnImageError("Error: Row must be greater than 0 and less than the Height");
            }

            Color color = new Color();
            int _base = _rowLength * row + col + 1;

            color.R = _buffer[_base];
            color.G = _buffer[_base + 1];
            color.B = _buffer[_base + 2];
            color.A = _buffer[_base + 3];

            return color;
        }

        /// <summary>
        /// Convert the image into stream of bytes
        /// </summary>
        /// <returns>stream of bytes</returns>
        public Stream GetStream()
        {
            Stream stream;

            if (!_init)
            {
                OnImageError("Error: Image has not been initialized");
                stream = null;
            }
            else
            {
                stream = PngEncoder.Encode(_buffer, _width, _height);
            }

            return stream;
        }

        #endregion Public

        #endregion Methods
    }
}
