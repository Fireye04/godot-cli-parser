using Godot;
using System;

public partial class Entrypoint : Node {
    public partial class Cmds : Node {
        public void printThingCommand() { Console.WriteLine("fuck"); }
    }
    public override void _Ready() { GDParser.run(new Cmds()); }
}
