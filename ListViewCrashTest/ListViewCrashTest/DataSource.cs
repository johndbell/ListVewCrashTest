using System;
using System.Threading.Tasks;
using DynamicData;

namespace ListViewCrashTest
{
	public class DataSource
	{
		SourceCache<Foo, int> _data = new SourceCache<Foo, int>(t=>t.Id);

		public async Task Initialise(int count = 5000)
		{
			await Task.Run(() =>
			{
				_data.Edit(innerCache =>
				{
					innerCache.Clear();
					for (int i = 0; i < count; i++)
					{
						innerCache.AddOrUpdate(new Foo { Id = i, Title = $"Foo Number {i:00000}" });
					}
				});
			});
		}

		public IObservable<IChangeSet<Foo, int>> ToObservable()
		{
			return _data.Connect();
		}
	}
}
