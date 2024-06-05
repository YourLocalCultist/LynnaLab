using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using LynnaLib;

namespace LynnaLab
{
    /// Dialog window for building oracles-disasm and then running the result
    public class BuildDialog : Gtk.Dialog
    {
        MainWindow mainWindow;
        ProcessOutputView processView;

        Process makeProcess;
        Process emulatorProcess;

        Gtk.CheckButton closeCheckBox;

        public Project Project
        {
            get
            {
                return mainWindow.Project;
            }
        }

        public BuildDialog(MainWindow parent)
            : base("Building project", parent.Window, Gtk.DialogFlags.DestroyWithParent, Gtk.Stock.Close, Gtk.ResponseType.Close)
        {
            this.mainWindow = parent;

            var startInfo = new ProcessStartInfo
            {
                FileName = "/usr/bin/make",
                Arguments = Project.GameString,
                WorkingDirectory = Project.BaseDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            makeProcess = new Process { StartInfo = startInfo };
            makeProcess.EnableRaisingEvents = true;
            makeProcess.Exited += (e, a) => Gtk.Application.Invoke((e2, a2) => OnMakeExited());

            processView = new ProcessOutputView();
            processView.MarginBottom = 6;
            processView.AttachAndStartProcess(makeProcess);

            var topLabel = new Gtk.Label("Building oracles-disasm. The first time will take several minutes.");
            topLabel.Halign = Gtk.Align.Start;
            topLabel.MarginBottom = 6;

            var scrolledWindow = new Gtk.ScrolledWindow();
            scrolledWindow.Add(processView);
            scrolledWindow.ShadowType = Gtk.ShadowType.EtchedIn;
            scrolledWindow.SetPolicy(Gtk.PolicyType.Automatic, Gtk.PolicyType.Automatic);
            scrolledWindow.SetSizeRequest(600, 300);
            scrolledWindow.Hexpand = true;
            scrolledWindow.Vexpand = true;

            // Scroll down when text is added to processView
            processView.SizeAllocated += (e, a) =>
            {
                var adjustment = scrolledWindow.Vadjustment;
                adjustment.Value = adjustment.Upper - adjustment.PageSize;
            };

            // Tags for text coloring
            var tagTable = processView.Buffer.TagTable;
            var redTag = new Gtk.TextTag("red");
            var greenTag = new Gtk.TextTag("green");
            redTag.Foreground = "red";
            greenTag.Foreground = "green";
            tagTable.Add(redTag);
            tagTable.Add(greenTag);

            closeCheckBox = new Gtk.CheckButton("Close dialog with emulator");
            closeCheckBox.Active = mainWindow.GlobalConfig.CloseRunDialogWithEmulator;

            // Assemble the dialog
            this.ContentArea.Add(topLabel);
            this.ContentArea.Add(scrolledWindow);
            this.ContentArea.Add(closeCheckBox);

            this.Response += (o, a) =>
            {
                makeProcess.Close();
                this.Destroy();
            };

            this.ShowAll();
        }

        /// Append text to processView text buffer
        void AppendText(string text, string tag = null)
        {
            text += '\n';
            var buffer = processView.Buffer;
            processView.Buffer.Text += text;

            if (tag != null)
            {
                var startIter = buffer.EndIter;
                var endIter = buffer.EndIter;
                endIter.BackwardChars(text.Length);
                buffer.ApplyTag(tag, startIter, endIter);
            }
        }

        void OnMakeExited()
        {
            if (makeProcess.ExitCode != 0)
            {
                AppendText($"\nError: make exited with code {makeProcess.ExitCode}", "red");
                return;
            }

            AppendText("\nBuild completed successfuly!\n", "green");

            string runCommand = mainWindow.GlobalConfig.EmulatorCommand;

            if (runCommand == null)
            {
                if ((runCommand = mainWindow.PromptForEmulator(true)) == null)
                {
                    AppendText($"Emulator not configured, couldn't run {Project.GameString}.gbc.", "red");
                    return;
                }
            }

            string fullCommand = runCommand + $" {Project.GameString}.gbc";

            AppendText("Attempting to run with the following command (reconfigure with File -> Select Emulator)...");
            AppendText(fullCommand + '\n');

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fullCommand.Split()[0],
                Arguments = string.Join(" ", fullCommand.Split().Skip(1)),
                WorkingDirectory = Project.BaseDirectory,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            emulatorProcess = new Process { StartInfo = startInfo };
            emulatorProcess.EnableRaisingEvents = true;
            emulatorProcess.Exited += (e, a) => Gtk.Application.Invoke((e2, a2) => OnEmulatorExited());

            try
            {
                if (!processView.AttachAndStartProcess(emulatorProcess))
                {
                    AppendText("Error: Emulator process could not be started.", "red");
                    return;
                }
            }
            catch (Win32Exception)
            {
                AppendText("Error: Emulator process could not be started.", "red");
                return;
            }

            // No apparent error, update global config with the run command
            if (mainWindow.GlobalConfig.EmulatorCommand != runCommand)
            {
                mainWindow.GlobalConfig.EmulatorCommand = runCommand;
                mainWindow.GlobalConfig.Save();
            }

            // Kill existing emulator process if it exists
            mainWindow.RegisterEmulatorProcess(emulatorProcess);
        }

        void OnEmulatorExited()
        {
            mainWindow.GlobalConfig.CloseRunDialogWithEmulator = closeCheckBox.Active;
            mainWindow.GlobalConfig.Save();

            if (closeCheckBox.Active)
            {
                this.Destroy();
            }
        }
    }
}
