using System;

using MonoTouch.Foundation;

namespace Example
{
	public class User
	{
		public string Username { get; set; }
		public string AvatarImageUrl { get; set; }

		public User (NSDictionary attributes)
		{
			Username = ((NSString)attributes["username"]).ToString();
			AvatarImageUrl = ((NSString)attributes.ValueForKeyPath(new NSString("avatar_image.url"))).ToString();
		}
	}
}

