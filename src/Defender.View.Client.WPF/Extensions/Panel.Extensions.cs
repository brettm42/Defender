namespace System.Windows.Controls
{
    public static class StackPanelExtensions
    {
        public static void ToggleVisibility<T>(T[] fields)
        {
            foreach (T field in fields)
            {
                (field as StackPanel).Visibility = ((field as StackPanel).Visibility == Visibility.Visible)
                                                   ? Visibility.Collapsed
                                                   : Visibility.Visible;
            }
        }

        public static void ToggleVisibility(this StackPanel @this)
        {
            @this.Visibility = (@this.Visibility == Visibility.Visible)
                               ? Visibility.Collapsed
                               : Visibility.Visible;
        }

        public static void ToggleVisibility(this Grid @this)
        {
            @this.Visibility = (@this.Visibility == Visibility.Visible)
                               ? Visibility.Collapsed
                               : Visibility.Visible;
        }

        public static void HideFields(object[] fields)
        {
            foreach (object field in fields)
            {
                (field as Control).Visibility = Visibility.Collapsed;
            }
        }

        public static void ShowFields(object[] fields)
        {
            foreach (object field in fields)
            {
                (field as Control).Visibility = Visibility.Visible;
            }
        }

        public static void Maximise(this StackPanel @this)
        {
            // unhides if hidden
            @this.Visibility = Visibility.Visible;

            // increases height to maximum
            // TODO: find a smarter way of constraining the panel expansion/maximising
            //@this.Height = this.ActualHeight - (ElemMinHeight * 2);
            //@this.Height = ElemMaxHeight - ElemMinHeight;
        }

        public static void Maximise(this Grid @this)
        {
            // unhides if hidden
            @this.Visibility = Visibility.Visible;

            // increases height to maximum
            // TODO: find a smarter way of constraining the panel expansion/maximising
            //@this.Height = this.ActualHeight - (ElemMinHeight * 2);
            //@this.Height = ElemMaxHeight - ElemMinHeight;
        }
    }
}
