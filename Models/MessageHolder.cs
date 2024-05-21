using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Models
{
    public class MessageHolder : INotifyPropertyChanged
    { 
        private string _text;
        public string Text { get; set; }

        private static MessageHolder _instance;
       

        public event PropertyChangedEventHandler? PropertyChanged;

        public static MessageHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MessageHolder();
                }
                return _instance;
            }
        }

    }
}
