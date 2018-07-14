# FormattedTextBox

入力された文字列を、FormattedTextを使って装飾して表示するTextBoxのサンプルです。

![](FormattedTextBox.gif)

## ソースコード

TextBoxのソースコードは次のようになっています。

    public class MyTextBox : TextBox
    {
        /// <summary>
        /// 線の太さ
        /// </summary>
        public int PenSize { get { return penSize; } set { penSize = value; InvalidateVisual(); } }
        /// <summary>
        /// 文字の塗りの色
        /// </summary>
        public Color BrushColor { get { return brushColor; } set { brushColor = value; InvalidateVisual(); } }
        /// <summary>
        /// 文字の線の色
        /// </summary>
        public Color PenColor { get { return penColor; } set { penColor = value; InvalidateVisual(); } }

        private int penSize { get; set; } = 2;
        private Color brushColor { get; set; } = Colors.Green;
        private Color penColor { get; set; } = Colors.Red;

        public MyTextBox()
        {
            Foreground = new SolidColorBrush(Colors.Transparent);
            Background = new SolidColorBrush(Colors.Transparent);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (string.IsNullOrEmpty(Text.Trim()))
                return;

            FormattedText formattedText = new FormattedText(
                Text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize,
                new SolidColorBrush()
            );

            drawingContext.DrawGeometry(
              new SolidColorBrush(BrushColor),
              new Pen(new SolidColorBrush(PenColor), PenSize),
              formattedText.BuildGeometry(new Point(2, 0)));
        }
    }
