# ListVewCrashTest
Demo app for testing a crash in iOS and Android using Xamarin Forms and Dynamic-Data.

UPDATE: Fixed - I was using Scheduler.Default that apparantly doesn't guarantee dispatching to the UI thread, so thanks
        to Erlend Angelson on the DynamicData Slack channel I used https://github.com/PureWeen/XamarinDispatchScheduler
	    instead, and the problem goes away.

		So, in conclusion, I wasn't updating the list on the UI thread.


The crash currently occurs when you quickly enter something in the search editor. Enter some numbers quickly a few times and the following crash will occur.

iOS StackTrace:

at Xamarin.Forms.ListProxy.get_Item (System.Int32 index) [0x0000b] in D:\agent_work\1\s\Xamarin.Forms.Core\ListProxy.cs:127 
  at Xamarin.Forms.ListProxy.System.Collections.IList.get_Item (System.Int32 index) [0x00000] in D:\agent_work\1\s\Xamarin.Forms.Core\ListProxy.cs:444 
  at Xamarin.Forms.Internals.TemplatedItemsList`2[TView,TItem].get_Item (System.Int32 index) [0x00008] in <3754ae802c714b298131b9c985b370ad>:0 
  at Xamarin.Forms.Platform.iOS.ListViewRenderer+ListViewDataSource.GetCellForPath (Foundation.NSIndexPath indexPath) [0x00007] in D:\agent_work\1\s\Xamarin.Forms.Platform.iOS\Renderers\ListViewRenderer.cs:1099 
  at Xamarin.Forms.Platform.iOS.ListViewRenderer+ListViewDataSource.GetCell (UIKit.UITableView tableView, Foundation.NSIndexPath indexPath) [0x00056] in D:\agent_work\1\s\Xamarin.Forms.Platform.iOS\Renderers\ListViewRenderer.cs:883 
  at (wrapper managed-to-native) ObjCRuntime.Messaging.void_objc_msgSend(intptr,intptr)
  at UIKit.UITableView.EndUpdates () [0x00008] in /Library/Frameworks/Xamarin.iOS.framework/Versions/11.8.0.20/src/Xamarin.iOS/UIKit/UITableView.g.cs:294 
  at Xamarin.Forms.Platform.iOS.ListViewRenderer.UpdateItems (System.Collections.Specialized.NotifyCollectionChangedEventArgs e, System.Int32 section, System.Boolean resetWhenGrouped) [0x00156] in D:\agent_work\1\s\Xamarin.Forms.Platform.iOS\Renderers\ListViewRenderer.cs:552 
  at Xamarin.Forms.Platform.iOS.ListViewRenderer.OnCollectionChanged (System.Object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) [0x00000] in D:\agent_work\1\s\Xamarin.Forms.Platform.iOS\Renderers\ListViewRenderer.cs:307 
  at Xamarin.Forms.Internals.TemplatedItemsList`2[TView,TItem].OnCollectionChanged (System.Collections.Specialized.NotifyCollectionChangedEventArgs e) [0x0000a] in <3754ae802c714b298131b9c985b370ad>:0 
  at Xamarin.Forms.Internals.TemplatedItemsList`2[TView,TItem].OnProxyCollectionChanged (System.Object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e, System.Boolean fixWindows) [0x0001a] in <3754ae802c714b298131b9c985b370ad>:0 
  at Xamarin.Forms.Internals.TemplatedItemsList`2[TView,TItem].OnProxyCollectionChanged (System.Object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) [0x00000] in <3754ae802c714b298131b9c985b370ad>:0 
  at Xamarin.Forms.ListProxy.OnCollectionChanged (System.Collections.Specialized.NotifyCollectionChangedEventArgs e) [0x0000a] in D:\agent_work\1\s\Xamarin.Forms.Core\ListProxy.cs:233 
  at Xamarin.Forms.ListProxy+<>c__DisplayClass33_0.<OnCollectionChanged>b__0 () [0x00018] in D:\agent_work\1\s\Xamarin.Forms.Core\ListProxy.cs:206 
  at Foundation.NSAsyncActionDispatcher.Apply () [0x00000] in /Library/Frameworks/Xamarin.iOS.framework/Versions/11.8.0.20/src/Xamarin.iOS/Foundation/NSAction.cs:163 
  at (wrapper managed-to-native) UIKit.UIApplication.UIApplicationMain(int,string[],intptr,intptr)
  at UIKit.UIApplication.Main (System.String[] args, System.IntPtr principal, System.IntPtr delegate) [0x00005] in /Library/Frameworks/Xamarin.iOS.framework/Versions/11.8.0.20/src/Xamarin.iOS/UIKit/UIApplication.cs:79 
  at UIKit.UIApplication.Main (System.String[] args, System.String principalClassName, System.String delegateClassName) [0x00038] in /Library/Frameworks/Xamarin.iOS.framework/Versions/11.8.0.20/src/Xamarin.iOS/UIKit/UIApplication.cs:63 
  at ListViewCrashTest.iOS.Application.Main (System.String[] args) [0x00002] in C:\Users\johnbell\source\ListVewCrashTest\ListViewCrashTest\ListViewCrashTest.iOS\Main.cs:19 
