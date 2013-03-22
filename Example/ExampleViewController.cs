using System;
using System.Drawing;

using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using AFNetworking;

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
					if (dict["deleted"] != null) {
						continue;
					}
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

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var post = posts[indexPath.Row];
			var size = tableView.StringSize(post.Text, UIFont.SystemFontOfSize(12), new SizeF(220, 9999999999), UILineBreakMode.WordWrap);
			return Math.Max (70, size.Height + 45);
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
		private Post _post;

		public Post Post {
			set {
				_post = value;
				TextLabel.Text = value.User.Username;
				DetailTextLabel.Text = value.Text;
				ImageView.SetImageUrl(new NSUrl(value.User.AvatarImageUrl), new UIImage("profile-image-placeholder.png"));
				SetNeedsLayout();
			}
		}

		public PostTableViewCell(string id) : base(UITableViewCellStyle.Subtitle, id) {
			TextLabel.AdjustsFontSizeToFitWidth = true;
			TextLabel.TextColor = UIColor.DarkGray;
			DetailTextLabel.Font = UIFont.SystemFontOfSize(12);
			DetailTextLabel.Lines = 0;
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
		}

		private float CalculatePostHeight() {
			var size = StringSize(_post.Text, UIFont.SystemFontOfSize(12), new SizeF(220, 9999999999), UILineBreakMode.WordWrap);
			return Math.Max (70, size.Height + 45);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageView.Frame = new RectangleF(10, 10, 50, 50);
			TextLabel.Frame = new RectangleF(70, 10, 240, 20);

			var detailsFrame = new RectangleF(TextLabel.Frame.Location, TextLabel.Frame.Size);
			detailsFrame.Offset(0, 25);
			detailsFrame.Height = CalculatePostHeight() - 45;
			DetailTextLabel.Frame = detailsFrame;
		}
	}
}

