using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;

namespace ListViewCrashTest
{
	public class MainViewModel : INotifyPropertyChanged
	{
		
		private readonly Subject<string> _searchSubject = new Subject<string>();
		private DataSource _dataSource = new DataSource();
		private bool _isWorking = false;
		private string _searchText;
		private ICommand _loadCommand;

		public event PropertyChangedEventHandler PropertyChanged;

		public MainViewModel()
		{
			var searchPredicate = _searchSubject
									.Throttle(TimeSpan.FromMilliseconds(100)) 
									.StartWith(string.Empty)
									.Select(GetSearchFunction);

			var filteredList = _dataSource.ToObservable()
				.Filter(searchPredicate)
				.ObserveOn(Scheduler.Default)
				.Bind(out _list)
				.Subscribe();
		}

		public ICommand LoadCommand => _loadCommand ?? (_loadCommand = new Xamarin.Forms.Command(async () =>
		{
			IsWorking = true;
			_dataSource.ToObservable().Subscribe(s => IsWorking = false);
			await _dataSource.Initialise();
		}));

		private ReadOnlyObservableCollection<Foo> _list;
		public ReadOnlyObservableCollection<Foo> Items => _list;


		public bool IsWorking
		{
			get => _isWorking;
			private set => Set(ref _isWorking, value);
		}

		public string SearchText
		{
			get => _searchText;
			set
			{
				if (Set(ref _searchText, value))
				{
					_searchSubject.OnNext(value);
				}
			}
		}

		private Func<Foo, bool> GetSearchFunction(string searchText)
		{
			return s => string.IsNullOrEmpty(s.Title) ||
						s.Title.ToLowerInvariant().Contains(searchText.ToLowerInvariant());
		}

		private bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
		{
			return Set(propertyName, ref field, newValue);
		}

		private bool Set<T>(string propertyName, ref T field, T newValue)
		{
			if (EqualityComparer<T>.Default.Equals(field, newValue))
			{
				return false;
			}
			field = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}


		private void OnPropertyChanged([CallerMemberName]string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
