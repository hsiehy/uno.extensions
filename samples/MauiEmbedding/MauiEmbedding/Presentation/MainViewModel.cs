using MauiEmbedding.Loader;
using TreehouseInterface;

namespace MauiEmbedding.Presentation;

public partial class MainViewModel : ObservableObject
{
	private INavigator _navigator;

	[ObservableProperty]
	private string? name;
	public ICommand GoToSecond { get; }
	public ICommand GoToImport { get; }

	public MainViewModel(
		IStringLocalizer localizer,
		IOptions<AppConfig> appInfo,
		INavigator navigator)
	{
		_navigator = navigator;
		Title = "Main";
		Title += $" - {localizer["ApplicationName"]}";
		Title += $" - {appInfo?.Value?.Environment}";
		GoToSecond = new AsyncRelayCommand(GoToSecondView);
		GoToImport = new AsyncRelayCommand(GoToImportView);
	}

	private async Task GoToImportView()
	{
		try
		{
			StorageFile pickedFile = await StorageFile.GetFileFromPathAsync(@"C:\Users\Yuan\Desktop\Work\TestProject\UnoTestModule\UnoTestModule\bin\Debug\net7.0\UnoTestModule.dll");
			var loadContext = new ModuleLoadContext(pickedFile.Path);
			var assembly = loadContext.LoadFromAssemblyPath(pickedFile.Path);
			var moduleType = assembly.DefinedTypes.First(i => i.FullName.Contains("UnoTestModule.UnoTestModule"));
			var instance = assembly.CreateInstance(moduleType.FullName) as ITreehouseModule;
			var app = Application.Current as App;
			var viewRegistryService = app.Host.Services.GetRequiredService<IViewRegistry>();
			var routeRegistryService = app.Host.Services.GetRequiredService<IRouteRegistry>();
			instance.OnInitialized(viewRegistryService, routeRegistryService);
			//viewRegistryService.Register(new ViewMap<UnoTestPage, UnoTestViewModel>());
			//var viewMap = viewRegistryService.FindByViewModel<UnoTestViewModel>();
			//routeRegistryService.Register(new RouteMap("UnoTest", viewMap));
			var result = await _navigator.NavigateRouteAsync(this, "UnoTest");
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}

	}
	private async Task GoToSecondView()
	{
		var app = Application.Current as App;
		var viewRegistryService = app.Host.Services.GetRequiredService<IViewRegistry>();
		viewRegistryService.Register(new ViewMap<SecondPage, SecondPageViewModel>());
		var routeRegistryService = app.Host.Services.GetRequiredService<IRouteRegistry>();
		var viewMap = viewRegistryService.FindByViewModel<SecondPageViewModel>();
		routeRegistryService.Register(new RouteMap("Second", viewMap));
		await _navigator.NavigateRouteAsync(this, "Second");
	}

	public string? Title { get; }

	
}
