using Godot;
using System;

public partial class Entrypoint : Node {
    public partial class Cmds : Node {
        public void printThingCommand(String item, int val) {
            GD.Print(item);
            GD.Print(val + 1);
        }
    }
    public override void _Ready() { GDParser.run(new Cmds()); }
}
