using System;
using System.Threading.Tasks;

namespace XamarinChat
{
	public interface IChatServices
	{
		Task Connect();
		Task Send(ChatMessage message, string roomName);
		Task JoinRoom(string roomName);
		event EventHandler<ChatMessage> OnMessageReceived;
	}
}

