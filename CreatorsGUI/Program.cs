using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorsGUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CreatorsGUI creatorsGUIForm = new CreatorsGUI();
            Finals finalsForm = new Finals(creatorsGUIForm);

            Application.Run(creatorsGUIForm); // Näytä ensin CreatorsGUI-form
            finalsForm.Show();

        }
    }
}
