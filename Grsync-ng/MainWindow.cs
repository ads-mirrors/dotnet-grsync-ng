using System;
using System.Collections;
using System.Collections.Generic;
using Grsyncng;
using Gtk;

public partial class MainWindow : Window
{

    public delegate void TriggerHandler(object sender);
    public event TriggerHandler OnSimulate;
    public event TriggerHandler OnRun;


    private Dictionary<Argument, CommandArgument> commandArguments;

    public MainWindow() : base(WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;

    }

    public void SetCommand(string command)
    {
        lbl_cmd.Text = command;
    }

    private void LoadSwitchArgument(Argument type, SwitchArgument argument, int index)
    {
        CheckButton checkButton = new CheckButton();
        checkButton.Label = argument.Description;
        checkButton.ShowAll();

        checkButton.Toggled += (object sender, EventArgs e) =>
        {
            argument.Value = checkButton.Active;
        };

        argument.OnValueChanged += (object sender) =>
        {
            checkButton.Active = (bool)((CommandArgument)sender).Value;
        };

        argument.OnInteractableChanged += (object sender) =>
        {
            checkButton.Sensitive = ((CommandArgument)sender).Interactable;
        };

        if (index % 2 == 0)
        {
            this.arguments_left.Add(checkButton);

            // Set CheckButton Position
            Fixed.FixedChild w2 = (Fixed.FixedChild)this.arguments_left[checkButton];
            w2.X = 21;
            w2.Y = 40 + (int)Math.Floor((float)(index / 2)) * 20;
        }
        else
        {
            this.arguments_right.Add(checkButton);

            // Set CheckButton Position
            Fixed.FixedChild w1 = (Fixed.FixedChild)this.arguments_right[checkButton];
            w1.X = 21;
            w1.Y = 40 + (int)Math.Floor((float)(index / 2)) * 20;
        }
    }

    private void LoadStringArgument(Argument type, StringArgument argument)
    {
        if (type == Argument.Destination)
        {
            argument.OnValueChanged += (object sender) =>
            {
                txb_destination.Text = (string)((StringArgument)argument).Value;
            };
            txb_destination.Changed += (object sender, EventArgs e) =>
            {
                argument.Value = ((Entry)sender).Text;
            };
        } else if(type == Argument.Source)
        {
            argument.OnValueChanged += (object sender) =>
            {
                txb_source.Text = (string)((StringArgument)argument).Value;
            };
            txb_source.Changed += (object sender, EventArgs e) =>
            {
                argument.Value = ((Entry)sender).Text;
            };
        }
    }

    public void LoadCommandArguments(Dictionary<Argument,CommandArgument> commandArguments)
    {
        this.commandArguments = commandArguments;

        int index = 0;
        foreach (var kvp in commandArguments)
        {
            CommandArgument commandArgument = kvp.Value;

            // register state change handler
            if (commandArgument is SwitchArgument)
            {
                LoadSwitchArgument(kvp.Key, (SwitchArgument)commandArgument, index);
            } else if(commandArgument is StringArgument)
            {
                LoadStringArgument(kvp.Key, (StringArgument)commandArgument);
            }

            if (kvp.Key == Argument.Destination)
            {
                commandArgument.OnValueChanged += (object sender) =>
                {
                    txb_destination.Text = (string) ((CommandArgument)sender).Value;
                };
            }


            index++;
        }
    }

    protected void OnBtnSourceBrowseClicked(object sender, EventArgs e)
    {
        if (SelectFolder("Select Source", out string filename) == ResponseType.Accept)
        {
            commandArguments[Argument.Source].Value = filename;
        }
    }

    protected void OnBtnDestBrowseClicked(object sender, EventArgs e)
    {
        if (SelectFolder("Select Destination", out string filename) == ResponseType.Accept)
        {
            commandArguments[Argument.Destination].Value = filename;
        }
    }

    protected ResponseType SelectFolder(string title, out string filename)
    {
        var dialog = new FileChooserDialog(title, null, FileChooserAction.SelectFolder,
            "Cancel", ResponseType.Cancel,
            "Select", ResponseType.Accept);
        dialog.ShowAll();

        ResponseType responseType = (ResponseType)dialog.Run();
        filename = dialog.Filename;
        dialog.Hide();

        return responseType;
    }

    protected void OnBtnRunClicked(object sender, EventArgs e)
    {
        OnRun?.Invoke(this);
    }

    protected void OnBtnSimulateClicked(object sender, EventArgs e)
    {
        OnSimulate?.Invoke(this);
    }
}
