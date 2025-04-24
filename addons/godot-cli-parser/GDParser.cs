using Godot;
using System;
using System.CommandLine;
using System.CommandLine.Parsing;

public partial class GDParser : SceneTree {
    public static Error run(Node target) {
        Command root = new Command(nameof(target));
        foreach (Godot.Collections.Dictionary command in target
                     .GetMethodList()) {
            string name = (string)command["name"];
            if (name.EndsWith("Command")) {
                name = name.Substring(0, name.LastIndexOf("Command"));
                root.AddCommand(new Command(name));
            }
        }
        getArgs(root);
        return Error.Ok;
    }

    public static void getArgs(Command root) {
        string[] args = OS.GetCmdlineUserArgs();
        if (args.Length == 0) {
            return;
        }
        Parser p = new Parser(root);
        GD.Print(p.Parse(args).ToString());
    }
}
