using System;
using Godot;

public partial class Entrypoint : Node {
    public partial class Cmds : Node {
        public void defaultCmd() { GD.Print("test"); }

        public void test(String item, int val) {
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
