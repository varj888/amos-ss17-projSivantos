using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestmachineFrontend1;
using TestMachineFrontend1.Helpers;
using TestMachineFrontend1.Model;

namespace TestMachineFrontend1.ViewModel
{
    public class DebugViewModel : ObservableObject
    {
        private ObservableCollection<DebugModel> debugList = new ObservableCollection<DebugModel>();

        public class DebugModel
        {
            public string Origin { get; set; }
            public string Text { get; set; }
        }

        public DebugViewModel()
        {
        }

        public ObservableCollection<DebugModel> GetDebugInfo()
        {
            return debugList;
        }

        public void AddDebugInfo(string origin, string msg)
        {
            debugList.Add(new DebugModel { Origin = origin, Text = msg });
        }

        //now AddDebugInfo
        //public void addMessage(string origin, string msg)
        //{
        //    this.debug.Items.Insert(0, new DebugModel { Origin = origin, Text = msg });
        //}
    }
}
