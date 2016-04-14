using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Xamarin.Forms;
using XamarinChat;
using XamarinChat.iOS;

[assembly: ExportRenderer(typeof(MessageViewCell), typeof(MessageRenderer))]
namespace XamarinChat.iOS
{
	public class MessageRenderer : ViewCellRenderer
	{
		public override UITableViewCell GetCell (Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var textVm = item.BindingContext as ChatMessageViewModel;
			if (textVm != null)
			{
				string text =  (textVm.IsMine ? "Me" : textVm.Name) + ": " + textVm.Message;
				var chatBubble = new ChatBubble(!textVm.IsMine, text);
				return chatBubble.GetCell(tv);
			}
			return base.GetCell(item,reusableCell, tv);
		}
	}
}

