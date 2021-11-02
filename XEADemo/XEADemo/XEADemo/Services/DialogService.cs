using System;
using System.Collections.Generic;
using System.Text;

namespace XEADemo.Services
{
    public class DialogService : IDialogService
    {
        public string Message()
        {
            return "Display message from Dependency Injection";
        }
    }
}
