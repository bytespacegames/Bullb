using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bullb
{
    public partial class Bullb : Form
    {
        public bool doesChangeTitle = false;
        public bool allowsViewingContext;
        public String fileLoc;
        public Bullb()
        {
            InitializeComponent();
        }

        public static ChromiumWebBrowser chromiumWebBrowser1;
        private void Form1_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();

            //TextWriter tw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Resources\Settings.bulbsettings");
            TextReader tr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\Resources\Settings.bulbsettings");
            fileLoc = AppDomain.CurrentDomain.BaseDirectory + @"\Resources\" + tr.ReadLine();
            this.Text = tr.ReadLine();
            this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.BaseDirectory + @"\Resources\" + tr.ReadLine());
            if (tr.ReadLine().Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + tr.ReadLine();
            } else
            {
                tr.ReadLine();
            }
            String tempVal = "false";
            tempVal = tr.ReadLine();
            if (tempVal.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                doesChangeTitle = true;
            } else
            {
                doesChangeTitle = false;
            }

            int x = int.Parse(tr.ReadLine());
            int y = int.Parse(tr.ReadLine());
            this.SetBounds(this.Bounds.X, this.Bounds.Y, x, y);
            if (tr.ReadLine().Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                allowsViewingContext = true;
            }
            else
            {
                allowsViewingContext = false;
            }
            tr.Close();
            Cef.Initialize(settings);

            chromiumWebBrowser1 = new ChromiumWebBrowser(fileLoc);
            this.panel1.Controls.Add(chromiumWebBrowser1);
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.TitleChanged += chromiumWebBrowser_TitleChanged;
            chromiumWebBrowser1.LoadError += chromiumWebBrowser1_404Error;
            chromiumWebBrowser1.ConsoleMessage += OnConsoleMessage;
            chromiumWebBrowser1.Load(fileLoc);
            if (!(allowsViewingContext)) { chromiumWebBrowser1.MenuHandler = new EmptyContextHandler(); }
            //chromiumWebBrowser1.ForeColor = Color.DarkGray;
        }
        private void chromiumWebBrowser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (doesChangeTitle) { this.Text = e.Title; }
            }));
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void chromiumWebBrowser1_404Error(object sender, LoadErrorEventArgs e)
        {
            if (e.ErrorText.Equals("ERR_NAME_NOT_RESOLVED"))
            {
                loadError(chromiumWebBrowser1);
            }
        }
        public void loadError(ChromiumWebBrowser c)
        {
            c.LoadHtml("<html> <head> <title>Load Error: Page not found</title> <style> body{ background-color: 383838; } h1 { color: white; font-family: Tahoma; } h3 { color: white; font-family: Tahoma; } .center { margin: auto; position: absolute; border: 2px solid white; padding: 10px; left: 50%; top: 50%; -webkit-transform: translate(-50%, -50%); transform: translate(-50%, -50%); text-align:center; } </style> </head> <body> <div class=\"center\"> <h1>Error: this page couldn't be found.</h1> <h3>This could be because: <br><br>You cannot connect to the page<br><br> The page is blocked by your administrators or firewall <br><br>The page does not exist or you have the url wrong.</h3> </div> </body></html>", "http://ramhtml");
        }
        public void OnConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            if (e.Message.Contains("OpenFile,"))
            {
                Process process = new Process();
                process.StartInfo.FileName = e.Message.Split(',').Last();
                process.StartInfo.Arguments = "-n";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();
            }
            if (e.Message.Contains("BulbRefresh;"))
            {
                chromiumWebBrowser1.Refresh();
            }
            if (e.Message.Contains("BulbBack;"))
            {
                if (chromiumWebBrowser1.CanGoBack)
                {
                    chromiumWebBrowser1.Back();
                }
            }
            if (e.Message.Contains("BulbForward;"))
            {
                if (chromiumWebBrowser1.CanGoBack)
                {
                    chromiumWebBrowser1.Forward();
                }
            }
            if (e.Message.Contains("CloseBulbApp;"))
            {
                Application.Exit();
            }
            if (e.Message.Contains("CloseProcessByName,"))
            {
                foreach (var process in Process.GetProcessesByName(e.Message.Split(',').Last()))
                {
                    process.Kill();
                }
            }
            if (e.Message.Contains("ChangeBulbFormTitle,")) {
                Invoke(new Action(() =>
                {
                    this.Text = e.Message.Split(',').Last();
                }));
            }
            if (e.Message.Contains("ChangeBulbFormIconFromResources,"))
            {
                Invoke(new Action(() =>
                {
                    this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.BaseDirectory + @"\Resources\" + e.Message.Split(',').Last());
                }));
            }
            if (e.Message.Contains("ChangeBulbFormIconByLoc,"))
            {
                Invoke(new Action(() =>
                {
                    this.Icon = Icon.ExtractAssociatedIcon(e.Message.Split(',').Last());
                }));
            }
            if (e.Message.Contains("SetResizable,"))
            {
                Invoke(new Action(() =>
                {
                    if (e.Message.Split(',').Last() == "false")
                    {
                        this.FormBorderStyle = FormBorderStyle.FixedSingle;
                        this.MaximizeBox = false;
                    } else
                    {
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        this.MaximizeBox = true;
                    }
                }));
            }
            if (e.Message.Contains("ShowMinimizeBox,"))
            {
                Invoke(new Action(() =>
                {
                    if (e.Message.Split(',').Last() == "false")
                    {
                        this.MinimizeBox = false;
                    }
                    else
                    {
                        this.MinimizeBox = true;
                    }
                }));
            }
            if (e.Message.Contains("ShowMaximizeBox,"))
            {
                Invoke(new Action(() =>
                {
                    if (e.Message.Split(',').Last() == "false")
                    {
                        this.MaximizeBox = false;
                    }
                    else
                    {
                        this.MaximizeBox = true;
                    }
                }));
            }
            if (e.Message.Contains("RunBatchCmdAsAdmin,"))
            {
                ProcessStartInfo processInfo;

                processInfo = new ProcessStartInfo("cmd.exe", "/c " + e.Message.Split(',').Last());
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.Verb = "runas";
                Process.Start(processInfo);

            }
            if (e.Message.Contains("RunBullbScript,"))
            {
                String loc = @e.Message.Split(',').Last();
                runBullbScript(loc);

            }
        }
        public void runBullbScript(string loc)
        {
            var linearray = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @loc).ReadToEnd().Split(new char[] { '\n' });
            int lines = linearray.Count() - 1;
            if (loc.Contains(".bullbform"))
            {
                String formType = "MessageBox";
                int ParamCount = 1;
                String param1 = "Undefined";
                String param2 = "Undefined";
                for (int i = 0; i <= lines; i++)
                {
                    String line = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + @loc).Skip(i).Take(1).FirstOrDefault();
                    if (line.Contains("FormType,"))
                    {
                        formType = line.Split(',').Last();
                    }
                }
                for (int i = 0; i <= lines; i++)
                {
                    String line = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + @loc).Skip(i).Take(1).FirstOrDefault();
                    if (formType == "MessageBox")
                    {
                        if (line.Contains("ParameterCount,"))
                        {
                            ParamCount = int.Parse(line.Split(',').Last());
                        }
                    }
                    if (line.Contains("Param1,"))
                    {
                        param1 = line.Split(',').Last();
                    }
                    if (line.Contains("Param2,"))
                    {
                        param2 = line.Split(',').Last();
                    }
                }
                if (ParamCount >= 2)
                {
                    MessageBox.Show(param1, param2);
                } else if (ParamCount == 1)
                {
                    MessageBox.Show(param1);
                }
            }
        }
    }
}