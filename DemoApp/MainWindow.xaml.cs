using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DemoApp;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    private readonly ObservableCollection<IconItem> _icons;
    private readonly ICollectionView _filteredIcons;
    private string _searchText = string.Empty;

    public MainWindow()
    {
        InitializeComponent();

        _icons = new ObservableCollection<IconItem>(LoadIcons());
        _filteredIcons = CollectionViewSource.GetDefaultView(_icons);
        _filteredIcons.Filter = FilterIcon;

        DataContext = this;
        RefreshFilter();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public ICollectionView FilteredIcons => _filteredIcons;

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value)
            {
                return;
            }

            _searchText = value;
            OnPropertyChanged();
            RefreshFilter();
        }
    }

    public bool HasResults => _filteredIcons.Cast<object>().Any();

    public string ResultSummary => $"{_filteredIcons.Cast<object>().Count()} of {_icons.Count} icons";

    private bool FilterIcon(object item)
    {
        if (item is not IconItem icon)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            return true;
        }

        return icon.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
               icon.ResourceKey.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
    }

    private void RefreshFilter()
    {
        _filteredIcons.Refresh();
        OnPropertyChanged(nameof(HasResults));
        OnPropertyChanged(nameof(ResultSummary));
    }

    private static IEnumerable<IconItem> LoadIcons()
    {
        var dictionary = Application.Current.Resources.MergedDictionaries
            .FirstOrDefault(resourceDictionary => resourceDictionary.Source?.OriginalString.Contains("Icons.xaml", StringComparison.OrdinalIgnoreCase) == true)
            ?? new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/XamlTemplatesEngine.Icons;component/Icons.xaml", UriKind.Absolute)
            };

        return dictionary.Keys
            .OfType<string>()
            .Where(resourceKey => resourceKey.StartsWith("Icon-", StringComparison.Ordinal))
            .OrderBy(resourceKey => resourceKey, StringComparer.OrdinalIgnoreCase)
            .Select(resourceKey => new IconItem(resourceKey, (Brush)dictionary[resourceKey]!));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public sealed record IconItem(string ResourceKey, Brush Brush)
    {
        public string Name => ResourceKey["Icon-".Length..];
    }
}
