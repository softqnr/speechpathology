﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpeechPathology.Utils
{
    public class AsyncCommand : Command
    {
        public AsyncCommand(Func<Task> execute) : base(() => execute())
        {
        }
        public AsyncCommand(Func<object, Task> execute) : base(() => execute(null))
        {
        }
    }
}
