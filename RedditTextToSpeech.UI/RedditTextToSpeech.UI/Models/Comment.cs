using RedditTextToSpeech.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.UI.Models
{
    internal class Comment : IComment
    {
        public Comment(string username, string first)
        {
            this.Username = username;
            this.First = first; 
        }

        public string Username { get; }

        public string First { get; }
    }
}
