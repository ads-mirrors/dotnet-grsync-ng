using System;
using System.Collections;
using System.Collections.Generic;
using Grsyncng;
using Gtk;

public partial class MainWindow : Window
{

    private Dictionary<CheckButton, CommandArgument> argumentDict = new Dictionary<CheckButton, CommandArgument>();

    public MainWindow() : base(WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;

    }

    public void UpdateCommand(string command)
    {
        Console.WriteLine(command);
        lbl_cmd.Text = command;
    }

    public void LoadCommandArguments(CommandArgument[] commandArguments)
    {
        int index = 0;
        foreach (CommandArgument commandArgument in commandArguments)
        {
            CheckButton checkButton = new CheckButton();
            checkButton.Label = commandArgument.Description;
            checkButton.ShowAll();

            // add to dict
            argumentDict.Add(checkButton, commandArgument);

            // register state change handler
            if (commandArgument.GetType() == typeof(BoolArgument))
            {
                checkButton.Toggled += BoolArgumentChanged;
            }

            if(index % 2 == 0)
            {
                this.arguments_left.Add(checkButton);

                // Set CheckButton Position
                Fixed.FixedChild w2 = (Fixed.FixedChild)this.arguments_left[checkButton];
                w2.X = 21;
                w2.Y = 40 + (int) Math.Floor((float) (index / 2)) * 20;
            }
            else
            {
                this.arguments_right.Add(checkButton);

                // Set CheckButton Position
                Fixed.FixedChild w1 = (Fixed.FixedChild)this.arguments_right[checkButton];
                w1.X = 21;
                w1.Y = 40 + (int)Math.Floor((float)(index / 2)) * 20;
            }




            index++;
        }
    }

    void BoolArgumentChanged(object sender, EventArgs args) 
    {
        CheckButton checkButton = (CheckButton)sender;
        CommandArgument argument = argumentDict[checkButton];

        argument.SetValue(checkButton.Active);
        MainClass.UpdateCommand();
    }
}
