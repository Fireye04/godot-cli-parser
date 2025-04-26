using Godot;
using System;

public partial class Entrypoint : Node {
    public partial class Cmds : Node {
        public void defaultCommand() { GD.Print("test"); }

        public void testCommand(String item, int val) {
            GD.Print(item);
            GD.Print(val + 1);
        }
    }
    public override void _Ready() {
        Error result = GDParser.run(new Cmds());
        if (result != Error.Ok) {
            GD.Print(result);
        }
    }
}
