namespace System.Windows.Controls
{
    public static class ButtonExtensions
    {
        public static void ToggleVisibility(this Button @this)
        {
            @this.Visibility = (@this.Visibility == Visibility.Visible)
                               ? Visibility.Collapsed
                               : Visibility.Visible;
        }
    }
}
