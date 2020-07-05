using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CinemaNick
{
    class CollectionViewCustom: CollectionView
    {
        public CollectionViewCustom()
        {
            SelectionMode = SelectionMode.Single;
            VerticalOptions = LayoutOptions.StartAndExpand;
            ItemsLayout = new GridItemsLayout(Device.RuntimePlatform == Device.Android ? 1 : 2, ItemsLayoutOrientation.Vertical);
        }
    }
}
