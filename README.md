# Godot CLI Parser
![Enbyware](https://pride-badges.pony.workers.dev/static/v1?label=enbyware&labelColor=%23555&stripeWidth=8&stripeColors=FCF434%2CFFFFFF%2C9C59D1%2C2C2C2C)

## What is it?

Godot CLI parser is an addon for godot that facilitates the creation of an end user command line interface for your godot application. 
Essentially, if you want to quickly and easily define discreet paramaterized commands that can be run directly off your application, Godot CLI Parser can help. 
This is especially helpful for allowing users to run up their own dedicated game servers, but can be broadly applied across any number of other use cases.

## How to use it

Add the [godot-cli-parser folder](https://github.com/Fireye04/godot-cli-parser/tree/main/addons/godot-cli-parser) (~/addons/godot-cli-parser) to your addons/ folder in godot. 
You should next create a new scene called `entrypoint.tscn` and attach a script to the root node.
This script should be similar to [Entrypoint.cs](https://github.com/Fireye04/godot-cli-parser/blob/main/Entrypoint.cs), containing the following boilerplate:
```cs
public partial class Entrypoint : Node {
    public partial class Cmds : Node {
		// Supports parameters!
        public void command_name_here(String item, int val) {
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
```
Next, add any relevant commands under the commands class.

Now, via godot's included [CLI](https://docs.godotengine.org/en/stable/tutorials/editor/command_line_tutorial.html), 
you can go ahead and run `godot entrypoint.tscn --headless -- command_name_here "this will print five" 4`, and the command and arguments will be passed through.

## Contributing! 

Contributions are welcome! Please feel free to open an issue!
