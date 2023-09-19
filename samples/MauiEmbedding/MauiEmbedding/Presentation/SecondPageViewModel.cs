using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEmbedding.Presentation
{
	public class SecondPageViewModel : ObservableObject
	{
		private INavigator _navigator;
		public ICommand GoBack { get; }

		public SecondPageViewModel(INavigator navigator)
		{
			_navigator = navigator;
			GoBack = new AsyncRelayCommand(GoBackMainView);
		}

		private async Task GoBackMainView()
		{
			await _navigator.NavigateBackAsync(this);
		}
	}
}
