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
                GD.Print();
                name = name.Substring(0, name.LastIndexOf("Command"));
                root.AddCommand(new Command(name));
            }
        }
        ParseResult pr = getArgs(root);
        Godot.Collections.Array arr = new Godot.Collections.Array();
        int i = 0;
        foreach (Token item in pr.Tokens) {
            if (i == 0) {
                i++;
                continue;
            }
            arr.Add((Godot.Variant)item.Value);
            i++;
        }
        target.Callv(pr.CommandResult.Command.Name + "Command", arr);
        return Error.Ok;
    }

    public static ParseResult getArgs(Command root) {
        string[] args = OS.GetCmdlineUserArgs();
        if (args.Length == 0) {
            return null;
        }
        Parser p = new Parser(root);
        return p.Parse(args);
    }
}
