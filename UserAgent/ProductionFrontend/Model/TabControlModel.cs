using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Helpers;

namespace Frontend.Model
{
    
   public class TabControlModel
    {
        /// <summary>
        /// Tab Header text
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Tab Content 
        /// </summary>
        public ObservableObject CurrentTabContentViewModel { get; set; }
    }
}
