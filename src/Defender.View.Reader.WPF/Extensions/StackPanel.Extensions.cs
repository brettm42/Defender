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
    }
}
