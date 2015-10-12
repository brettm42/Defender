namespace System.Windows.Controls
{
    public static class xPanelExtensions
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

        public static void Maximise(this StackPanel @this, double maxheight)
        {
            // unhides if hidden
            @this.Visibility = Visibility.Visible;

            // increases height to maximum
            @this.Height = maxheight - (maxheight / 8);
        }
        
        public static void Maximise(this Grid @this, double maxheight)
        {
            // unhides if hidden
            @this.Visibility = Visibility.Visible;

            // increases height to maximum
            @this.Height = maxheight - (maxheight / 8);
        }
    }
}