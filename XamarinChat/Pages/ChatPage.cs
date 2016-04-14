using System;
using Xamarin.Forms;

namespace XamarinChat
{
	public class ChatPage : BasePage<ChatViewModel>
	{
		private Entry nameEntry;
		private Entry messageEntry;
		private Button sendMessageButton;
		private ListView messagesListView;
		private Button joinButton;

		public ChatPage ()
		{
			//Join Room Button
			joinButton = new Button {
				Text="Join Group"
			};
			joinButton.SetBinding (Button.CommandProperty, "JoinRoomCommand");

			//Init Name Entry
			nameEntry = new Entry {			
				TextColor = Color.Black
			};
			nameEntry.SetBinding (Entry.TextProperty, "ChatMessage.Name", BindingMode.TwoWay);

			//Init Message Entry
			messageEntry = new Entry {	
				TextColor = Color.Black
			};
			messageEntry.SetBinding (Entry.TextProperty, "ChatMessage.Message");

			sendMessageButton = new Button {
				Text = "Send Message"
			};
			sendMessageButton.SetBinding (Button.CommandProperty, "SendMessageCommand");

			//Init List
//			messagesListView = new ListView ();
//			messagesListView.SetBinding (ListView.ItemsSourceProperty, "Messages");

			var messageList = new ChatListView();
			messageList.VerticalOptions = LayoutOptions.FillAndExpand;
			messageList.SetBinding(ChatListView.ItemsSourceProperty, new Binding("Messages"));
			messageList.ItemTemplate = new DataTemplate(CreateMessageCell);

			var stackLayout = new StackLayout () {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (5, 20, 5, 10),
				Children = {
					joinButton,
					nameEntry,
					messageEntry,
					sendMessageButton
				}
			};

			Content = new StackLayout
			{
				Children=
				{
					stackLayout,
					messageList
				}
			};
		}

		private Cell CreateMessageCell()
		{
			var timestampLabel = new Label();
			timestampLabel.SetBinding(Label.TextProperty, new Binding("Timestamp", stringFormat: "[{0:HH:mm}]"));
			timestampLabel.TextColor = Color.Silver;
			timestampLabel.Font = Font.SystemFontOfSize(14);

			var authorLabel = new Label();
			authorLabel.SetBinding(Label.TextProperty, new Binding("ChatMessage.Name", stringFormat: "{0}: "));
			authorLabel.TextColor = Device.OnPlatform(Color.Blue, Color.Yellow, Color.Yellow);
			authorLabel.Font = Font.SystemFontOfSize(14);

			var messageLabel = new Label();
			messageLabel.SetBinding(Label.TextProperty, new Binding("ChatMessage.Message"));
			messageLabel.Font = Font.SystemFontOfSize(14);

			var stack = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children = {authorLabel, messageLabel}
			};

			if (Device.Idiom == TargetIdiom.Tablet)
			{
				stack.Children.Insert(0, timestampLabel);
			}

			var view = new MessageViewCell
			{
				View = stack
			};
			return view;
		}
	}
}

