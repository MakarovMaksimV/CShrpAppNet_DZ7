using System;
using System.Text.Json;

namespace Seminar7
{
    public class ChatMessage
    {
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string Text { get; set; }
        public int? Id { get; set; }
        public Command Command { get; set; }

        public string Serialize()
        {

            return $"{FromName}|{ToName}|{Text}|{Id}|{(int)Command}";
        }

        public static ChatMessage Deserialize(string data)
        {
            var parts = data.Split('|');
            return new ChatMessage
            {
                FromName = parts[0],
                ToName = parts[1],
                Text = parts[2],
                Id = string.IsNullOrEmpty(parts[3]) ? null : (int?)int.Parse(parts[3]),
                Command = (Command)int.Parse(parts[4])
            };
        }
    }
}

