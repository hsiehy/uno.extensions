﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
#if WINDOWS_UWP || UNO_UWP_COMPATIBILITY
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
#endif

namespace Uno.Extensions.Navigation.Controls.Managers;

public class TabManager : BaseControlManager<TabView>
{
    public TabManager(INavigationService navigation, RegionControlProvider controlProvider) : base(navigation, controlProvider.RegionControl as TabView)
    {
        Control.SelectionChanged += Tabs_SelectionChanged;
    }

    private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var tvi = e.AddedItems?.FirstOrDefault() as TabViewItem;
        var tabName = tvi.Name;
        Navigation.NavigateByPathAsync(null, tabName);
    }

    private TabViewItem FindByName(string tabName)
    {
        return (from t in Control.TabItems.OfType<TabViewItem>()
                where t.Name == tabName
                select t).FirstOrDefault();
    }

    protected override object InternalShow(string path, Type view, object data, object viewModel)
    {
        var tab = FindByName(path);
        if (tab is not null)
        {
            Control.SelectionChanged -= Tabs_SelectionChanged;
            Control.SelectedItem = tab;
            Control.SelectionChanged += Tabs_SelectionChanged;
        }

        return tab?.Content;
    }
}
