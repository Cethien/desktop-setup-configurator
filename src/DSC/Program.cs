// using System.Text.Json;
// using System.Text.Json.Serialization;
// using DSC.Domain;


void Solve(string cmd, params string[] args)
{
    switch (cmd)
    {
        case "help":
        default:
            DisplayHelp();
            break;
    }
}

void DisplayHelp()
{
    var msg = "Help";
    System.Console.WriteLine(msg);
}

//run program
var subCommand = args[0];
var arguments = args.Skip(1).ToArray();
Solve(subCommand, arguments);