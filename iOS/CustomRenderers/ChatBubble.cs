using System;
using Xamarin.Forms;
using UIKit;
using Foundation;
using MonoTouch.Dialog;

namespace XamarinChat.iOS
{
	public class ChatBubble : MonoTouch.Dialog.Element, IElementSizing
	{
		bool isLeft;

		public ChatBubble(bool isLeft, string text)
			: base(text)
		{
			this.isLeft = isLeft;
		}


		public override UITableViewCell GetCell(UITableView tv)
		{
			var cell = tv.DequeueReusableCell(isLeft ? BubbleCell.KeyLeft : BubbleCell.KeyRight) as BubbleCell;
			if (cell == null)
				cell = new BubbleCell(isLeft);
			cell.Update(Caption);
			return cell;
		}

		public nfloat GetHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return BubbleCell.GetSizeForText(tableView, Caption).Height + BubbleCell.BubblePadding.Height;
		}
	}
}

