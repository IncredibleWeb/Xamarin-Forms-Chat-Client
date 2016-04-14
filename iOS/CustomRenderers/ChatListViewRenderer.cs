using System;
using Xamarin.Forms;
using XamarinChat;
using XamarinChat.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;
using System.Reflection;

[assembly: ExportRenderer (typeof(ChatListView), typeof(ChatListViewRenderer))]
namespace XamarinChat.iOS
{
	public class ChatListViewRenderer : ListViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);
			var table = (UITableView)this.Control;
			table.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			table.Source = new ListViewDataSourceWrapper (this.GetFieldValue<UITableViewSource> (typeof(ListViewRenderer), "dataSource"));
		}
	}

	public class ListViewDataSourceWrapper : UITableViewSource
	{
		private readonly UITableViewSource _underlyingTableSource;

		public ListViewDataSourceWrapper (UITableViewSource underlyingTableSource)
		{
			this._underlyingTableSource = underlyingTableSource;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			return this.GetCellInternal (tableView, indexPath);
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return this._underlyingTableSource.RowsInSection (tableview, section);
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			return this._underlyingTableSource.GetHeightForHeader (tableView, section);
		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			return this._underlyingTableSource.GetViewForHeader (tableView, section);
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return this._underlyingTableSource.NumberOfSections (tableView);
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			this._underlyingTableSource.RowSelected (tableView, indexPath);
		}

		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return this._underlyingTableSource.SectionIndexTitles (tableView);
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return this._underlyingTableSource.TitleForHeader (tableView, section);
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var uiCell = (BubbleCell)GetCellInternal (tableView, indexPath);

			uiCell.SetNeedsLayout ();
			uiCell.LayoutIfNeeded ();

			return uiCell.GetHeight (tableView);
		}

		private UITableViewCell GetCellInternal (UITableView tableView, NSIndexPath indexPath)
		{
			return this._underlyingTableSource.GetCell (tableView, indexPath);
		}
	}

	public static class PrivateExtensions
	{
		public static T GetFieldValue<T> (this object @this, Type type, string name)
		{
			var field = type.GetField (name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);
			return (T)field.GetValue (@this);
		}

		public static T GetPropertyValue<T> (this object @this, Type type, string name)
		{
			var property = type.GetProperty (name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
			return (T)property.GetValue (@this);
		}
	}
}

