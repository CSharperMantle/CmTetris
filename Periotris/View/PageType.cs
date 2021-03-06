using System;

namespace Periotris.View
{
    internal enum PageType
    {
        StartPage,
        GamePage
    }

    internal static class PageTypeExtenstion
    {
        public static string GetPath(this PageType pageType)
        {
            switch (pageType)
            {
                case PageType.StartPage:
                    return "View/StartPage.xaml";
                case PageType.GamePage:
                    return "View/GamePage.xaml";
                default:
                    throw new ArgumentOutOfRangeException(nameof(pageType), pageType, null);
            }
        }
    }
}
