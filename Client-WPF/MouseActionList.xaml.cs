using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client_WPF.ServiceDB;

namespace Client_WPF
{
    /// <summary>
    /// Логика взаимодействия для MouseActionList.xaml
    /// </summary>
    public partial class MouseActionList : Window
    {
        public List<CursorPosition> CursorPositions { get; set; }

        public MouseActionList(List<CursorPosition> positions)
        {
            InitializeComponent();
            CursorPositions = positions;
            DataContext = this;
        }
    }
}
