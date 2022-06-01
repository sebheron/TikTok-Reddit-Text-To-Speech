using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerArgs;
using RedditTextToSpeech.Core;

namespace RedditTextToSpeech.Presentation
{
    public class Arguments
    {
        [ArgRequired(PromptIfMissing = true)]
        public string Url { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        public string Background { get; set; }

        public string? Output { get; set; }

        public Gender? Gender { get; set; }

        public TimeSpan? Start { get; set; }

        public int? Comments { get; set; }

        public string? Server { get; set; }

        public string? Key { get; set; }

        public bool? Alternate { get; set; }
    }
}
