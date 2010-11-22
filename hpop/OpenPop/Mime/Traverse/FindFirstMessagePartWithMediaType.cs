﻿namespace OpenPop.Mime.Traverse
{
	///<summary>
	/// Finds the first <see cref="MessagePart"/> which have a given MediaType in a
	/// depth first traversal.
	///</summary>
	internal class FindFirstMessagePartWithMediaType : IQuestionAnswerMessageTraverser<string, MessagePart>
	{
		/// <summary>
		/// Finds the first <see cref="MessagePart"/> with the given MediaType
		/// </summary>
		/// <param name="message">The <see cref="Message"/> to start looking in</param>
		/// <param name="question">The MediaType to look for. Has to be in lowercase.</param>
		/// <returns>A <see cref="MessagePart"/> with the given MediaType or <see langword="null"/> if no such <see cref="MessagePart"/> was found</returns>
		public MessagePart VisitMessage(Message message, string question)
		{
			return VisitMessagePart(message.MessagePart, question);
		}

		/// <summary>
		/// Finds the first <see cref="MessagePart"/> with the given MediaType
		/// </summary>
		/// <param name="messagePart">The <see cref="MessagePart"/> to start looking in</param>
		/// <param name="question">The MediaType to look for. Has to be in lowercase.</param>
		/// <returns>A <see cref="MessagePart"/> with the given MediaType or <see langword="null"/> if no such <see cref="MessagePart"/> was found</returns>
		public MessagePart VisitMessagePart(MessagePart messagePart, string question)
		{
			if (messagePart.ContentType.MediaType.Equals(question))
				return messagePart;

			if(messagePart.IsMultiPart)
			{
				foreach (MessagePart part in messagePart.MessageParts)
				{
					MessagePart result = VisitMessagePart(part, question);
					if (result != null)
						return result;
				}
			}

			return null;
		}
	}
}