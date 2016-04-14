using System;

namespace XamarinChat
{
	public class ChatMessageViewModel:BaseViewModel
	{
		private string _name;

		public string Name {
			get{ return _name; }
			set { 
				_name = value; 
				OnPropertyChanged ("Name");

			}
		}

		private string _message;

		public string Message {
			get{ return _message; }
			set { 
				_message = value; 
				OnPropertyChanged ("Message");
			}
		}

		private string _image;

		public string Image {
			get{ return _image; }
			set { 
				_image = value; 
				OnPropertyChanged ("Image");
			}
		}

		private bool _isMine;

		public bool IsMine {
			get{ return _isMine; }
			set { 
				_isMine = value; 
				OnPropertyChanged ("IsMine");
			}
		}
	}
}

