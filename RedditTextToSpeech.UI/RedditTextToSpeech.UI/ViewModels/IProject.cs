using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.UI.ViewModels
{
    public interface IProject
    {
        public IList<IEntry> Entries { get; }
    }
}
