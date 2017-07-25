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
    /// <summary>
    /// ViewModel which main puropose is to display
    /// debug information in GUI which corresponds
    /// to executeed events or errors
    /// </summary>
    public class DebugViewModel : ObservableObject
    {
        private DebugModel debugModel;
        private ObservableCollection<DebugModel> debugList;

        public DebugViewModel()
        {
            debugList = new ObservableCollection<DebugModel>();
        }

        /// <summary>
        /// List in GUI which contains debug messages
        /// </summary>
        public ObservableCollection<DebugModel> DebugList
        {
            get { return debugList; }
            set
            {
                debugList = value;
                OnPropertyChanged("DebugList");
            }
        }

        public ObservableCollection<DebugModel> GetDebugInfo()
        {
            return debugList;
        }

        /// <summary>
        /// Add new message to debug list
        /// </summary>
        /// <param name="origin">source of the message</param>
        /// <param name="msg">message itself</param>
        public void AddDebugInfo(string origin, string msg)
        {
            if (ReferenceEquals(debugModel, null))
            {
                debugModel = new DebugModel();
            }
            debugModel.Origin = origin;
            debugModel.Text = msg;
            debugList.Add(debugModel);
        }
    }
}
