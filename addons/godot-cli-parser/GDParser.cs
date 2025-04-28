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
                if (name == "default") {
                    continue;
                }
                root.AddCommand(new Command(name));
            }
        }
        ParseResult pr = getArgs(root);
        Godot.Collections.Array arr = new Godot.Collections.Array();
        if (pr.Tokens.Count == 1) {
            target.Callv("defaultCommand", []);
            return Error.Ok;
        }
        int i = 0;
        foreach (Token item in pr.Tokens) {
            if (i <= 1) {
                i++;
                continue;
            }
            arr.Add(item.Value);
            i++;
        }

        Godot.Collections.Array methods = new Godot.Collections.Array();
        foreach (Godot.Collections.Dictionary item in target.GetMethodList()) {
            methods.Add(item["name"]);
            if ((String)item["name"] ==
                pr.CommandResult.Command.Name + "Command") {
                if (((Godot.Collections.Array)item["args"]).Count !=
                    arr.Count) {
                    return Error.InvalidParameter;
                }
            }
        }
        if (!methods.Contains(pr.CommandResult.Command.Name + "Command")) {
            return Error.DoesNotExist;
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
