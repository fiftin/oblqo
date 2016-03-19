using System;
using System.Windows.Forms;

namespace Oblqo
{
    static class Program
    {
		static Program()
		{
			var httpasm = typeof(System.Net.Http.HttpClient).Assembly;
			var httpver = new Version(1, 5, 0, 0);

			AppDomain.CurrentDomain.AssemblyResolve += (s, a) => {
				var requestedAssembly = new System.Reflection.AssemblyName(a.Name);
				if (requestedAssembly.Name != "System.Net.Http" || requestedAssembly.Version != httpver)
					return null;

				return httpasm;
			};
		}
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
