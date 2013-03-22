using System;
using System.Collections.Generic;

using MonoTouch.Foundation;

namespace Example
{
	public class Post
	{
		public string Text { get; set; }
		public User User { get; set; }

		public Post (NSDictionary attributes)
		{
			Text = ((NSString)attributes["text"]).ToString();
			User = new User((NSDictionary)attributes["user"]);
		}
	}
}

