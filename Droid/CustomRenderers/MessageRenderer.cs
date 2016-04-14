
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamarinChat;
using XamarinChat.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using System.Net;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(MessageViewCell), typeof(MessageRenderer))]
namespace XamarinChat.Droid
{
	public class MessageRenderer : ViewCellRenderer
	{
		protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
		{
			var inflatorservice = (LayoutInflater)Forms.Context.GetSystemService(Android.Content.Context.LayoutInflaterService);
			var dataContext = item.BindingContext as ChatMessageViewModel;

			var textMsgVm = dataContext as ChatMessageViewModel;
			if (textMsgVm != null)
			{
				if (!string.IsNullOrEmpty(textMsgVm.Image))
				{
					var template = (LinearLayout)inflatorservice.Inflate(textMsgVm.IsMine ? Resource.Layout.image_item_owner : Resource.Layout.image_item_opponent, null, false);
					//template.FindViewById<TextView>(Resource.Id.timestamp).Text = textMsgVm.Timestamp.ToString("HH:mm");
					template.FindViewById<TextView>(Resource.Id.nick).Text = textMsgVm.IsMine ? "Me:" : textMsgVm.Name + ":";
					template.FindViewById<ImageView>(Resource.Id.image).SetImageBitmap(GetImageBitmapFromUrl(textMsgVm.Image));
					return template;
				}
				else
				{
					var template = (LinearLayout)inflatorservice.Inflate(textMsgVm.IsMine ? Resource.Layout.message_item_owner : Resource.Layout.message_item_opponent, null, false);
					//template.FindViewById<TextView>(Resource.Id.timestamp).Text = textMsgVm.Timestamp.ToString("HH:mm");
					template.FindViewById<TextView>(Resource.Id.nick).Text = textMsgVm.IsMine ? "Me:" : textMsgVm.Name + ":";
					template.FindViewById<TextView>(Resource.Id.message).Text = textMsgVm.Message;
					return template;
				}
			}

			return base.GetCellCore(item, convertView, parent, context);
		}

		private Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;
			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}
			return imageBitmap;
		}


		protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnCellPropertyChanged(sender, e);
		}
	}
}

