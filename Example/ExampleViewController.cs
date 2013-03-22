using System;
using System.Drawing;

using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example
{
	public partial class ExampleViewController : UITableViewController
	{
		private List<Post> posts = new List<Post>();

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public ExampleViewController ()
			: base (UserInterfaceIdiomIsPhone ? "ExampleViewController_iPhone" : "ExampleViewController_iPad", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView.Source = new PostTableViewSource(posts);
			
			// Perform any additional setup after loading the view, typically from a nib.
			AppDotNetClient.Instance.GetPath ("stream/0/posts/stream/global", null,
			                                  (request, response) => {
				posts.Clear();
				NSArray postData = (NSArray)((NSDictionary)response)["data"];
				foreach (NSDictionary dict in NSArray.FromArray<NSDictionary>(postData)) {
					posts.Add (new Post(dict));
				}
				TableView.ReloadData();
			}, null);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (UserInterfaceIdiomIsPhone) {
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} else {
				return true;
			}
		}
	}

	class PostTableViewSource : UITableViewSource {
		private List<Post> posts;

		public PostTableViewSource(List<Post> posts) {
			this.posts = posts;
		}

		#region implemented abstract members of UITableViewSource

		public override int RowsInSection (UITableView tableview, int section)
		{
			return posts.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			PostTableViewCell cell = tableView.DequeueReusableCell("CELL") as PostTableViewCell;
			if (cell == null) {
				cell = new PostTableViewCell("CELL");
			}

			cell.Post = posts[indexPath.Row];

			return cell;
		}

		#endregion
	}

	class PostTableViewCell : UITableViewCell {

		public Post Post {
			set {
				DetailTextLabel.Text = value.Text;
			}
		}

		public PostTableViewCell(string id) : base(UITableViewCellStyle.Subtitle, id) { }
	}
}

