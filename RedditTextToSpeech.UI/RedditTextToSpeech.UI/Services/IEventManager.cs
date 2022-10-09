using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.UI.Services
{
    public interface IEventManager
    {
        void Emit<T>(string name, T args) where T : class;
        void Emit(string name);
        void Subscribe<T>(string name, Action<T> action) where T : class;
        void Subscribe(string name, Action action);
    }
}
