using System;

using AFNetworking;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace Example {
	public class AppDotNetClient : AFHTTPClient {
		private static AppDotNetClient _client;
		
		public static AppDotNetClient Instance {
			get {
				if (_client == null) {
					_client = new AppDotNetClient();
				}
				return _client;
			}
		}

		public AppDotNetClient () : base (new NSUrl ("https://alpha-api.app.net")) {
			this.RegisterHTTPOperationClass (new Class (typeof(AFJSONRequestOperation)));
			this.SetDefaultHeader ("Accept", "application/json");
		}
	}
}
