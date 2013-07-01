using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PorkSpleenClient
{
    public partial class LoadContentControl : UserControl
    {
        Thread loading;

        public Thread Loading
        {
            get { return loading; }
            set { loading = value; }
        }
        public LoadContentControl()
        {
            InitializeComponent();
        }
    }
}
