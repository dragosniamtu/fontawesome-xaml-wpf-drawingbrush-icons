# fontawesome-xaml-wpf-drawingbrush-icons

WPF `ResourceDictionary` containing 786 Font Awesome 4.7.0 icons exposed as `DrawingBrush` resources.

## What is in this repo

- `XamlTemplatesEngine.Icons/Icons.xaml` contains the generated `DrawingBrush` resources.
- `XamlTemplatesEngine.Icons/XamlTemplatesEngine.Icons.csproj` builds the WPF library that embeds the icon dictionary.
- `THIRD-PARTY-NOTICES.md` contains the Font Awesome attribution and licensing details for the source glyphs used to generate the brushes.

## Usage

Merge the resource dictionary into your WPF application:

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/XamlTemplatesEngine.Icons;component/Icons.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

Then use any icon brush by key:

```xml
<Rectangle Width="16"
           Height="16"
           Fill="{DynamicResource Icon-Github}" />
```

## Build

```powershell
dotnet build ".\\01 Font Awesome in XAML.sln"
```

## Licensing

The repository code and project files are licensed under the MIT License. The generated icon geometry in `XamlTemplatesEngine.Icons/Icons.xaml` is derived from Font Awesome 4.7.0 glyph data and is subject to the applicable third-party notices and license terms described in `THIRD-PARTY-NOTICES.md`.

This repository is not affiliated with or endorsed by Font Awesome.
