﻿using System;
namespace Seminar7
{
	public interface IMessageSourceClient<T>
	{
        void Send(ChatMessage message, T toAddr);
        ChatMessage Receive(ref T fromAddr);
        T CreateNewT();
        T GetServer();
    }
}

