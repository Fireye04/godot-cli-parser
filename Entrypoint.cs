using Godot;
using System;

public partial class Entrypoint : Node {
    public partial class Cmds : Node {
        public void printThingCommand(String item) { GD.Print(item); }
    }
    public override void _Ready() { GDParser.run(new Cmds()); }
}
