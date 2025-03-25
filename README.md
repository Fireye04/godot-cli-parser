# Godot CLI Parser

Godot CLI parser is an addon for godot that facilitates the creation of an end user command line interface for your godot application. 
Essentially, if you want to quickly and easily define discreet paramaterized commands that can be run directly off your application, Godot CLI Parser can help. 
This is especially helpful for allowing users to run up their own dedicated game servers, but can be broadly applied across any number of other use cases.

## How to use it

Add the [godot-cli-parser folder](https://github.com/Fireye04/godot-cli-parser/tree/67cc7c308dcb155ef5ce0df0aa69b3093e6efc30/addons) to your addons/ folder in godot. 
You should next create a new scene called `entrypoint.tscn` and attach a script to the root node.
This script should be similar to [entrypoint.gd](https://github.com/Fireye04/godot-cli-parser/blob/main/entrypoint.gd), containing the following boilerplate:
``` gdscript
extends Node

class commands:
	extends Node
	# Add commands here!

func _ready() -> void:
	var c = commands.new()
	var result: Dictionary = parser.run(c)
	c.queue_free()
	if result.error:
		print(error_string(result.error) + ": " + result.message)
		get_tree().quit()
```
Next, add any relevant commands under the commands class.

Now, via godot's included [CLI](https://docs.godotengine.org/en/stable/tutorials/editor/command_line_tutorial.html), 
you can go ahead and run `godot entrypoint.tscn --headless -- command arg1 arg2`, and the command and arguments will be passed through.

## Contributing! 

Contributions are welcome! Please feel free to open an issue!
